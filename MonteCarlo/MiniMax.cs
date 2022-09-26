using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarlo
{
    public class MiniMax
    {
        public static int BestMove(BackendBoard backendBoard, int s)
        {
            if (backendBoard.IsFinished())
            {
                return -1;
            }

            int bestmove = 0;
            float max = float.MinValue;
            for (int i = 0; i < backendBoard.empty_squares.Count; i++)
            {
                int move = backendBoard.empty_squares[0];
                backendBoard.Move(move, s);
                var score = -maxi(2, backendBoard, -s);
                backendBoard.UndoMove(move);
                if (score > max)
                {
                    max = score;
                    bestmove = move;
                }
            }
            return bestmove;
        }
        static float maxi(int depth, BackendBoard backendBoard, int hasmove)
        {
            if (depth == 0 || backendBoard.empty_squares.Count == 0 || backendBoard.IsFinished())
            {
                return Evaluate(backendBoard);
            }
            float max = int.MinValue;
            for (int i = 0; i < backendBoard.empty_squares.Count; i++)
            {
                int move = backendBoard.empty_squares[0];
                backendBoard.Move(move, hasmove);
                var score = mini(depth - 1, backendBoard, -hasmove);
                backendBoard. UndoMove(move);
                if (score > max)
                {
                    max = score;
                }
            }
            return max;
        }

        static float mini(int depth, BackendBoard backendBoard, int hasmove)
        {
            if (depth == 0 || backendBoard.empty_squares.Count == 0 || backendBoard.IsFinished())
            {
                return Evaluate(backendBoard);
            }
            float min = float.MaxValue;
            for (int i = 0; i < backendBoard.empty_squares.Count; i++)
            {
                int move = backendBoard.empty_squares[0];
                backendBoard.Move(move, hasmove);
                var score = maxi(depth - 1, backendBoard, -hasmove);
                backendBoard.UndoMove(move);
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