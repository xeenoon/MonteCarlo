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
    public class MiniMax : ComputerPlayer
    {
        public int BestMove(BackendBoard backendBoard, int s)
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

        float maxi(int depth, BackendBoard backendBoard, int hasmove)
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

        float mini(int depth, BackendBoard backendBoard, int hasmove)
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
    public class RandomAI : ComputerPlayer
    {
        public int BestMove(BackendBoard backendBoard, int s)
        {
            var availablemoves = backendBoard.AvailableMoves().ToArray();
            return availablemoves.RanChoice();
        }
    }
}