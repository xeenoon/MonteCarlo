using Numpy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MonteCarlo.MonteCarloSearch;
using static MonteCarlo.TorchNetwork;
using static TorchSharp.torch;

namespace MonteCarlo
{
    internal class MCTS_Tests
    {
        class MockModel
        {
            float[] mockdata;
            float mockvalue;
            public MockModel(string name, float[] mockdata, float mockvalue)
            {
                this.mockdata = mockdata;
                this.mockvalue = mockvalue;
            }

            public DoubleTuple Predict(int[] board)
            {
                //Ignore the board input, just there for formatting purposes
                //Starting board is: [0, 0, 1, -1]
                return new DoubleTuple(mockdata, mockvalue); //Sample NN output
            }
        }
        /*

        public static bool Test1()
        {
            var game = new BackendBoard(1, 4, 2);
            var args = new Dictionary<string, int>() { { "num_simulations", 50 } };

            var model = new MockModel(new double[4] { 0.26, 0.24, 0.24, 0.26 }, 0.0001);
            var mcts = new MCTS(game, model, args);
            var root = mcts.Run(model, game, 1);

            //Best move is play at indexes 1 & 2 even though their probabilities are lower
            var best_outer_move = Math.Max(root.children[0].visitCount, root.children[0].visitCount);
            var best_center_move = Math.Max(root.children[1].visitCount, root.children[2].visitCount);
            return best_center_move > best_outer_move; //Was it going to play the best move
        }
        public static bool Test2()
        {
            var game = new BackendBoard(1, 4, 2);
            var args = new Dictionary<string, int>() { { "num_simulations", 25 } };

            var model = new MockModel(new double[4] { 0.3, 0.7, 0, 0 }, 0.0001);
            var mcts = new MCTS(game, model, args);
            game.Move(2, 1);
            game.Move(3, -1); //[0, 0, 1, -1]
            var root = mcts.Run(model, game, 1);
            return root.children[1].visitCount > root.children[0].visitCount; //Did we choose the best move?
        }

        */
    }
}
