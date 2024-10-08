﻿using Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorchSharp;
using TorchSharp.Modules;
using static AI.MonteCarloSearch;
using static TorchSharp.torch;

namespace AI
{
    public class DoubleTuple
    {
        public float[] probabilities;
        public float v;

        public DoubleTuple(float[] x, float v)
        {
            this.probabilities = x;
            this.v = v;
        }
    }
    public class TorchNetwork : nn.Module, Model
    {
        public List<Layer> layers = new();


        public Linear actionHead;
        private Linear valueHead;

        public Device device;
        public string nameID;

        public string filepath { get; set; }
        public bool autosave { get; set; }
        public double learnrate { get; set; }
        public int depth { get; set; }
        public Func<string, bool> logger { get; set; }

        public TorchNetwork(string nameID, List<Layer> nn_layers, string filepath, bool autosave, double learningrate, Func<string,bool> logger, int depth) : base(nameID)
        {
            this.layers = nn_layers;


            this.actionHead = nn_layers.Last().GetLinear();
            this.valueHead = nn.Linear(nn_layers[nn_layers.Count-2].outputsize, 1);
            nn_layers.Remove(nn_layers.Last()); //Remove the last item from the list, we need to deal with this manually

            this.to(torch.device("cpu"));
            this.device = torch.device("cpu");

            this.name = nameID;
            this.nameID = nameID;

            RegisterComponents();

            this.filepath = filepath;
            this.autosave = autosave;
            this.learnrate = learningrate;
            this.logger = logger;
            this.depth = depth;
        }

        public struct TensorTuple
        {
            public Tensor tensor1;
            public Tensor tensor2;

            public TensorTuple(Tensor tensor1, Tensor tensor2)
            {
                this.tensor1 = tensor1;
                this.tensor2 = tensor2;
            }
        }
        public TensorTuple Foward(Tensor x)
        {
            foreach (var layer in layers)
            {
                if (layer.type == Layer.LayerType.Linear)
                {
                    x = nn.functional.relu(layer.GetLinear().cpu().forward(x)); //Operate on the layers
                }
                else
                {
                    x = layer.GetConvolutional().cpu().forward(x); //Operate on the layers
                }
            }

            var action_logits = actionHead.forward(x);
            var value_logit = valueHead.forward(x);

            Tensor tanh_v = tanh(value_logit);
            
            Softmax softmax = nn.Softmax(x.Dimensions-1);

            Tensor tensor_v = softmax.forward(action_logits);
            softmax.Dispose();
            return new TensorTuple(tensor_v, tanh_v);
        }
        public DoubleTuple Predict(Array data)
        {
            Tensor board_tensor = from_array(data).to(device); //Could not find 'FloatTensor' after extensive research, discovered it was obselete
            no_grad(); //Disable gradients
            var tuple = Foward(board_tensor);
            enable_grad(); //Re-enable gradients
            
            var x = tuple.tensor1.cpu().data<float>().ToArray(); //Probability representation in board int[] format
            var y = tuple.tensor2.cpu().data <float>()[0]; //Array only ever has one item
            return new DoubleTuple(x, y);
        }

        public int BestMove(BackendBoard backendBoard, int s)
        {
            var game = backendBoard.Flipp(s); //Keep same if 1, flip it is -1
            if (s == -1)
            {
                s = 1; //Reset s to 1
            }
            var args = new Dictionary<string, int>() { { "num_simulations", depth } };

            var mcts = new MCTS(args);
            var root = mcts.Run(this, game, s);
            return root.SelectChild().Key;
        }
        public int EstMove(BackendBoard backendBoard, int s)
        {
            var game = backendBoard.Flipp(s); //Keep same if 1, flip it is -1
            if (s == -1)
            {
                s = 1; //Reset s to 1
            }
            var probabilities = Predict(game.Get2d()).probabilities.Multiply(game.ValidMoveMask());
            double max = double.MinValue;
            int bestmove = 0;
            for (int i = 0; i < probabilities.Length; ++i)
            {
                if (probabilities[i] > max)
                {
                    max = probabilities[i];
                    bestmove = i;
                }
            }
            return bestmove;
        }

        public void Train(int trainingdepth)
        {
            Dictionary<string, int> arguments = new Dictionary<string, int>()
            {
                {"batch_size",64},
                {"numIters",trainingdepth},
                {"num_simulations",100},
                {"numEps",100},
                {"numItersForTrainExamplesHistory",20},
                {"epochs",2},
            };
            var game = new BackendBoard(6, 7, 4);

            var trainer = new Trainer(game, this, arguments, logger);
            trainer.Learn();
        }

        public void SaveModel()
        {
            save(filepath);
        }
        public void ExportModel(string exportpath)
        {
            save(exportpath);
        }

    }
    public interface Model : ComputerPlayer
    {
        public DoubleTuple Predict(Array data);
        public int depth { get; set; }
    }
    public class MCTS_AI : Model
    {
        public MCTS_AI(int depth)
        {
            this.depth = depth;
        }

        public int depth { get; set; }
        public int BestMove(BackendBoard backendBoard, int s)
        {
            var game = backendBoard.Flipp(s); //Keep same if 1, flip it is -1
            if (s == -1)
            {
                s = 1; //Reset s to 1
            }
            var args = new Dictionary<string, int>() { { "num_simulations", depth } };

            var mcts = new MCTS(args);
            var root = mcts.Run(this, game, s);
            return root.SelectChild().Key;
        }

        public DoubleTuple Predict(Array data)
        {
            return new DoubleTuple(new float[7] {0.142f, 0.142f, 0.142f, 0.142f, 0.142f, 0.142f, 0.142f},0);
        }
    }
    public interface ComputerPlayer
    {
        public int BestMove(BackendBoard backendBoard, int s);
    }

    public class Layer
    {
        public enum LayerType
        {
            Linear,
            Convolutional,
        }
        public LayerType type;
        public int inputsize;
        public int outputsize;

        private int STRIDESIZE = 1;
        public int stridesize
        {
            get { return STRIDESIZE; }
            set { STRIDESIZE = value; }
        }

        public static Layer BeginConv   = new Layer(LayerType.Convolutional, 42, 4, 1);
        public static Layer BeginLinear = new Layer(LayerType.Linear       , 42, 60);
        public static Layer EndLinear   = new Layer(LayerType.Linear       , 60, 7);
        public Layer(LayerType type, int inputsize, int outputsize, int stridesize)
        {
            this.type = type;
            this.inputsize = inputsize;
            this.outputsize = outputsize;
            this.stridesize = stridesize;
        }
        public Layer(LayerType type, int inputsize, int outputsize)
        {
            this.type = type;
            this.inputsize = inputsize;
            this.outputsize = outputsize;
        }

        public Linear GetLinear()
        {
            if (type != LayerType.Linear)
            {
                throw new Exception("Cannot call \"GetLinear()\" on a Convolutional layer");
            }
            return linear;
        }
        public Conv1d GetConvolutional()
        {
            if (type != LayerType.Convolutional)
            {
                throw new Exception("Cannot call \"GetConvolutional()\" on a Linear layer");
            }
            return conv1D;
        }
        private Linear linear;
        private Conv1d conv1D;

        public void Setup()
        {

            if (type == LayerType.Linear)
            {
                linear = nn.Linear(inputsize, outputsize);
            }
            else
            {
                conv1D = nn.Conv1d(42, 60, 7, stridesize);
            }
        }
    }
}