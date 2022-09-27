using Numpy;
using Numpy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorchSharp;
using static AI.MonteCarloSearch;
using static TorchSharp.torch;
using Game;

namespace AI
{
    public class Trainer
    {
        public TorchNetwork model;
        public BackendBoard game;
        public Dictionary<string, int> args;
        public MCTS mcts;
        public Func<string, bool> Log;

        public Trainer(BackendBoard game, TorchNetwork model, Dictionary<string, int> args, Func<string, bool> log)
        {
            this.model = model;
            this.game = game;
            this.args = args;
            mcts = new MCTS(args);
            Log = log;
        }
        public class ProbabilityDistribution
        {
            public BackendBoard boardstate;
            public int currplayer;
            public float[] probabilities;

            public ProbabilityDistribution(BackendBoard boardstate, int currplayer, float[] action_probs)
            {
                this.boardstate = boardstate;
                this.currplayer = currplayer;
                this.probabilities = action_probs;
            }
        }
        public List<ProbabilityDistribution> ExcecuteEpisode()
        {
            List<ProbabilityDistribution> train_examples = new List<ProbabilityDistribution>();
            int current_player = 1;
            var state = new BackendBoard(6, 7, 4); //Create an empty board
            var reverse = game;
            while (true)
            {
                reverse = state.Flipp(current_player);  //Flip the board to the opponents perspective
                mcts = new MCTS(args);
                var root = mcts.Run(model, reverse, 1);
                float[] action_probs = Enumerable.Repeat((float)0, reverse.width).ToArray();
                foreach (var child in root.children)
                {
                    action_probs[child.Key] = child.Value.visitCount;
                    //Manufacture the probabilities to mimic the montecarlo search
                }
                action_probs = action_probs.Divide(action_probs.Sum());
                train_examples.Add(new ProbabilityDistribution(reverse, current_player, action_probs));

                var action = root.SelectAction(0);
                state = state.NextState(current_player, action); //Simulate the move
                current_player *= -1;
                var reward = state.GetReward(current_player);
                if (state.IsFinished()) //Game over
                {
                    List<ProbabilityDistribution> toreturn = new List<ProbabilityDistribution>();
                    foreach (var example in train_examples)
                    {
                        int v = (current_player != example.currplayer ? -1 : 1);
                        toreturn.Add(new ProbabilityDistribution(example.boardstate, reward * v, example.probabilities));
                        //Flip the reward if we didn't win to be negative
                    }
                    return toreturn;
                }
            }
        }
        public void Learn()
        {
            for (int i = 0; i < args["numIters"]; ++i) //Iterate through the iterations requested
            {
                var trainexamples = new List<ProbabilityDistribution>();
                for (int e = 0; e < args["numEps"]; ++e) //Iterate through number of episodes
                {
                    var examples = ExcecuteEpisode(); //Simulate a game
                    trainexamples.AddRange(examples); //Add the game positions
                }

                trainexamples.Shuffle();
                Log("");
                Log(string.Format("{0} out of {1} iterations train data: ", i+1, args["numIters"]));
                Train(trainexamples);
                Log("------------------------------------------");
          //      var filename = "latest.pth";
          //      Save(".", filename);
            }
            mcts = null;
        }

        private void Train(List<ProbabilityDistribution> examples)
        {
            var optimizer = optim.Adam(model.parameters(), 0.0005);
            List<float> pi_losses = new List<float>();
            List<float> v_losses = new List<float>();

            for (int epoch = 0; epoch < args["epochs"]; ++epoch)
            {
                model.train();
                int batchidx = 0;

                while (batchidx < (int)(examples.Count / args["batch_size"]))
                {
                    NDarray<int> sample_ids = np.random.randint(examples.Count,size: new int[1] { args["batch_size"] });

                    List<ProbabilityDistribution> list = (from i in sample_ids.ToList() select examples[i]).ToList().ToList();

                    List<float> allboards = new List<float>();
                    for (int i = 0; i < list.Count; i++)
                    {
                        allboards.AddRange(list[i].boardstate.board);
                    }
                    var boards = from_array(list.Select(i => i.boardstate.board).ToArray().TwoDimensional());
                    var probabilities = from_array(list.Select(i => i.probabilities).ToArray().TwoDimensional());
                    var values = from_array(list.Select(i => i.currplayer).ToArray());
                    //Create tensors for all the NN inputs

                    //Predict
                    var b_cuda = boards.contiguous().to(model.device);
                    var p_cuda = probabilities.contiguous().to(model.device);
                    var v_cuda = values.contiguous().to(model.device);

                    //Compute output
                    var data = model.Foward(b_cuda);
                    var l_pi = Loss_pi(p_cuda, ref data.tensor1);
                    var l_v = Loss_v(v_cuda, ref data.tensor2);
                    var totalloss = l_pi + l_v;

                    pi_losses.Add(l_pi.item<float>());
                    v_losses.Add(l_v.item<float>());

                    optimizer.zero_grad();
                    totalloss.backward();
                    optimizer.step();


                    batchidx++;

                  //  LastPiExamples = data.tensor1.detach();
                  //  LastProbExamples = probabilities[0];
                }
            }

            var pl = np.mean(np.array(pi_losses.ToArray()));
            var vl = np.mean(np.array(v_losses.ToArray()));
            Log(String.Format("Policy Loss: {0}", pl));
            Log(String.Format("Value Loss: {0}", vl));
            Log("Start pos result:");
            //Console.WriteLine(LastPiExamples[0].ToList().ToArray().Write());
            //Console.WriteLine(LastProbExamples.ToList().ToArray().Write());
            //Log(model.Predict(new BackendBoard(6, 7, 4).board).probabilities.Write());
        }

        private void Save(string folder, string filename)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            var filepath = folder + "\\" + filename;
            model.Save(filepath);
        }
        private Tensor Loss_pi(Tensor targets, ref Tensor outputs)
        {
            var loss = -(targets * torch.log(outputs)).sum(1);
            return loss.mean();
        }

        private Tensor Loss_v(Tensor targets, ref Tensor outputs) {
            Tensor t = targets - outputs.view(-1);
            var loss = torch.sum(t*t) / targets.size()[0];
            return loss;
        }
    }
}
