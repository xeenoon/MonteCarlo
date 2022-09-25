using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorchSharp;
using TorchSharp.Modules;
using static TorchSharp.torch;

namespace MonteCarlo
{
    public class DoubleTuple
    {
        public double[] probabilities;
        public double v;

        public DoubleTuple(double[] x, double v)
        {
            this.probabilities = x;
            this.v = v;
        }
    }
    public class TorchNetwork : Model
    {
        private int boardsize;
        private int actionsize;
        private nn.Module network;
        private Linear fc1;
        private Linear fc2;

        public Linear actionHead;
        private Linear valueHead;

        private Device device;

        public TorchNetwork(int boardsize, int actionsize, nn.Module network, Device device)
        {
            //super(Connect2Model, self).__init__()
            this.boardsize = boardsize;
            this.actionsize = actionsize;
            this.network = network;
            this.fc1 = nn.Linear(boardsize, 16);
            this.fc2 = nn.Linear(16, 16);
            this.device = device;
        }
        public struct TensorTuple //tfw
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
            x = nn.functional.relu(fc1.forward(x));
            x = nn.functional.relu(fc2.forward(x)); //.foward????? only method that works

            var action_logits = actionHead.forward(x);
            var value_logit = valueHead.forward(x);

            return new TensorTuple(nn.functional.softmax(action_logits, 1), tanh(value_logit));
        }
        public DoubleTuple Predict(int[] board) //From my knowledge of neural networks, this SHOULD return a float?
                                               //His code returns a tuple of something, probably two floats?
        {
            Tensor board_tensor = tensor(board).to(device); //Could not find 'FloatTensor' after extensive research, discovered it was obselete
            no_grad(); //Disable gradients
            var tuple = Foward(board_tensor);
            enable_grad(); //Re-enable gradients
            
            var x = tuple.tensor1.cpu().data<double>().ToArray(); //Probability representation in board int[] format
            var y = tuple.tensor2.cpu().data <double>()[0]; //Array only ever has one item

            return new DoubleTuple(x,y); //I honestly have no idea wtf is going on here
        }
    }

    public interface Model
    {
        public abstract DoubleTuple Predict(int[] board);
    }
}
