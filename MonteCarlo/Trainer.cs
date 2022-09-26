using Numpy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorchSharp;
using static MonteCarlo.MonteCarloSearch;
using static TorchSharp.torch;

namespace MonteCarlo
{
    internal class Trainer
    {
        public TorchNetwork model;
        public BackendBoard game;
        public Dictionary<string, int> args;
        public MCTS mcts;

        public Trainer(BackendBoard game, TorchNetwork model, Dictionary<string, int> args)
        {
            this.model = model;
            this.game = game;
            this.args = args;
            mcts = new MCTS(game, model, args);
        }
        public class ProbabilityDistribution
        {
            public BackendBoard boardstate;
            public int currplayer;
            public double[] probabilities;

            public ProbabilityDistribution(BackendBoard boardstate, int currplayer, double[] action_probs)
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
            var state = new BackendBoard(1, 4, 2); //Create an empty board
            while (true)
            {
                var reverse = game.Flipped(); //Flip the board to the opponents perspective
                mcts = new MCTS(game, model, args);
                var root = mcts.Run(model, reverse, 1);
                double[] action_probs = Enumerable.Repeat((double)0, reverse.width).ToArray();
                foreach (var child in root.children)
                {
                    action_probs[child.Key] = child.Value.visitCount;
                    //Manufacture the probabilities to mimic the montecarlo search
                }
                action_probs = action_probs.Divide(action_probs.Sum());
                train_examples.Add(new ProbabilityDistribution(reverse, current_player, action_probs));

                var action = root.SelectAction(0);
                reverse = reverse.NextState(action, current_player); //Simulate the move
                current_player *= -1;
                var reward = reverse.GetReward(current_player);
                if (reward != 0) //Game over
                {
                    List<ProbabilityDistribution> toreturn = new List<ProbabilityDistribution>();
                    foreach (var example in train_examples)
                    {
                        toreturn.Add(new ProbabilityDistribution(example.boardstate, reward * current_player != example.currplayer ? -1 : 1, example.probabilities));
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
                Train(trainexamples);
                var filename = "latest.pth";
                Save(".", filename);
            }
        }

        private void Train(List<ProbabilityDistribution> examples)
        {
            var optimizer = optim.Adam(model.parameters(), 0.0005);
            List<Tensor> pi_losses = new List<Tensor>();
            List<Tensor> v_losses = new List<Tensor>();

            Tensor LastPiExamples = null;
            Tensor LastProbExamples = null;

            for (int epoch = 0; epoch < args["epochs"]; ++epoch)
            {
                model.train();
                int batchidx = 0;

                while (batchidx < (int)(examples.Count / args["batch_size"]))
                {
                    NDarray<int> sample_ids = np.random.randint(examples.Count, args["batch_size"]);

                    List<ProbabilityDistribution> list = (from i in sample_ids.ToList() select examples[i]).ToList().ToList();

                    var boards = from_array(list.Select(i => i.boardstate.board).ToArray());
                    var probabilities = from_array(list.Select(i => tensor(i.probabilities)).ToArray());
                    var values = from_array(list.Select(i => tensor(i.currplayer)).ToArray());
                    //Create tensors for all the NN inputs

                    //Predict
                    var b_cuda = boards.contiguous().cuda();
                    var p_cuda = probabilities.contiguous().cuda();
                    var v_cuda = values.contiguous().cuda();

                    //Compute output
                    var data = model.Foward(boards);
                    var l_pi = Loss_pi(probabilities, data.tensor1);
                    var l_v = Loss_pi(values, data.tensor2);
                    var totalloss = l_pi + l_v;

                    pi_losses.Add(l_pi);
                    v_losses.Add(l_v);

                    optimizer.zero_grad();
                    totalloss.backward();
                    optimizer.step();

                    batchidx += 1;

                    LastPiExamples = data.tensor1.detach();
                    LastProbExamples = probabilities[0];
                }
            }

            Console.WriteLine();
            var pl = np.mean(np.array(pi_losses.ToArray()));
            var vl = np.mean(np.array(v_losses.ToArray()));
            Console.WriteLine(String.Format("Policy Loss", pl));
            Console.WriteLine(String.Format("Value Loss", vl));
            Console.WriteLine("Examples:");
            Console.WriteLine(LastPiExamples);
            Console.WriteLine(LastProbExamples);
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
        private Tensor Loss_pi(Tensor targets, Tensor outputs)
        {
            var loss = -(targets * torch.log(outputs)).sum(1);
            return loss.mean();
        }

        private Tensor Loss_v(Tensor targets, Tensor outputs) {
            Tensor t = targets - outputs.view(-1);
            var loss = torch.sum(t*t) / targets.size()[0];
            return loss;
        }
    }
}
