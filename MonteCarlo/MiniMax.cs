using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI;
using static AI.MonteCarloSearch;
using Game;

namespace MonteCarlo
{
    public class MiniMax
    {
        public static TorchNetwork model = new TorchNetwork("bob", 42, 7);

        public static int BestMove(BackendBoard backendBoard, int s)
        {
            if (backendBoard.IsFinished())
            {
                return -1;
            }
            var game = backendBoard.Flipp(s); //Keep same if 1, flip it is -1
            if (s == -1)
            {
                s = 1; //Reset s to 1
            }

            int bestmove = 0;
            float max = float.MinValue;
            var moves = game.AvailableMoves().Shuffle();
            for (int i = 0; i < moves.Count; i++)
            {
                int move = moves[i];
                var pos = game.Move(move, s);
                var score = mini(6, game, -s);
                game.UndoMove(pos);
                if (score > max)
                {
                    max = score;
                    bestmove = move;
                }
            }
            return bestmove;
        }
        public static int Best_ML_Move(BackendBoard backendBoard, int s)
        {
            var game = backendBoard.Flipp(s); //Keep same if 1, flip it is -1
            if (s == -1)
            {
                s = 1; //Reset s to 1
            }
            var args = new Dictionary<string, int>() { { "num_simulations", 100 } };

            var mcts = new MCTS(args);
            var root = mcts.Run(model, game, s);
            return root.SelectChild().Key;
        }
        public static int ML_PROB_Move(BackendBoard backendBoard, int s)
        {
            var game = backendBoard.Flipp(s); //Keep same if 1, flip it is -1
            if (s == -1)
            {
                s = 1; //Reset s to 1
            }
            var probabilities = model.Predict(game.board).probabilities.Multiply(game.ValidMoveMask());
            double max = double.MinValue;
            int bestmove = 0;
            for (int i = 0; i < probabilities.Length; ++i)
            {
                if (probabilities[i] >max)
                {
                    max = probabilities[i];
                    bestmove = i;
                }
            }
            return bestmove;
        }

        public static void TrainML(int trainingdepth, Func<string, bool> logger)
        {
            Dictionary<string, int> arguments = new Dictionary<string, int>()
            {
                {"batch_size",64},
                {"numIters",trainingdepth},
                {"num_simulations",100},
                {"numEps",100},
                {"numItersForTrainExamplesHistory",20},
                {"epochs",10},
            };
            var game = new BackendBoard(6, 7, 4);
            
            var trainer = new Trainer(game, model, arguments, logger);
            trainer.Learn();
        }


        static float maxi(int depth, BackendBoard backendBoard, int hasmove)
        {
            if (depth == 0 || backendBoard.IsFinished())
            {
                return Evaluate(backendBoard);
            }
            float max = int.MinValue;
            var moves = backendBoard.AvailableMoves();
            for (int i = 0; i < moves.Count; i++)
            {
                int move = moves[i];
                var pos = backendBoard.Move(move, hasmove);
                var score = mini(depth - 1, backendBoard, -hasmove);
                backendBoard. UndoMove(pos);
                if (score > max)
                {
                    max = score;
                }
            }
            return max;
        }

        static float mini(int depth, BackendBoard backendBoard, int hasmove)
        {
            if (depth == 0 || backendBoard.IsFinished())
            {
                return Evaluate(backendBoard);
            }
            float min = float.MaxValue;
            var moves = backendBoard.AvailableMoves();
            for (int i = 0; i < moves.Count; i++)
            {
                int move = moves[i];
                var pos = backendBoard.Move(move, hasmove);
                var score = maxi(depth - 1, backendBoard, -hasmove);
                backendBoard.UndoMove(pos);
                if (score < min)
                {
                    min = score;
                }
            }
            return min;
        }
        public static float Evaluate(BackendBoard backendBoard)
        {
            var winner = backendBoard.GameResult();
            if (winner.finished)
            {
                return winner.player_won;
            }

            return 0; //Game unfinished
        }
    }
}