using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarlo
{
    public class MiniMax
    {
        public static int BestMove(BackendBoard backendBoard, Side s)
        {
            if (backendBoard.Victory() != Side.None)
            {
                return -1;
            }

            int bestmove = 0;
            int max = int.MinValue;
            int movecount = backendBoard.empty_squares.Count;
            for (int i = 0; i < backendBoard.empty_squares.Count; i++)
            {
                int move = backendBoard.empty_squares[0];
                backendBoard.Move(move, s);
                var score = -maxi(2, backendBoard, s == Side.Red ? Side.Green : Side.Red);
                backendBoard.UndoMove(move);
                if (score > max)
                {
                    max = score;
                    bestmove = move;
                }
            }
            return bestmove;
        }
        static int maxi(int depth, BackendBoard backendBoard, Side hasmove)
        {
            if (depth == 0 || backendBoard.empty_squares.Count == 0 || backendBoard.Victory() != Side.None)
            {
                return Evaluate(backendBoard);
            }
            int max = int.MinValue;
            for (int i = 0; i < backendBoard.empty_squares.Count; i++)
            {
                int move = backendBoard.empty_squares[0];
                backendBoard.Move(move, hasmove);
                var score = mini(depth - 1, backendBoard, hasmove == Side.Red ? Side.Green : Side.Red);
                backendBoard. UndoMove(move);
                if (score > max)
                {
                    max = score;
                }
            }
            return max;
        }

        static int mini(int depth, BackendBoard backendBoard, Side hasmove)
        {
            if (depth == 0 || backendBoard.empty_squares.Count == 0 || backendBoard.Victory() != Side.None)
            {
                return Evaluate(backendBoard);
            }
            int min = int.MaxValue;
            for (int i = 0; i < backendBoard.empty_squares.Count; i++)
            {
                int move = backendBoard.empty_squares[0];
                backendBoard.Move(move, hasmove);
                var score = maxi(depth - 1, backendBoard, hasmove == Side.Red ? Side.Green : Side.Red);
                backendBoard.UndoMove(move);
                if (score < min)
                {
                    min = score;
                }
            }
            return min;
        }
        public static int Evaluate(BackendBoard backendBoard)
        {
            Side winner = backendBoard.Victory();
            switch (winner)
            {
                case Side.None:
                    return 0;
                case Side.Red:
                    return 1;
                case Side.Green:
                    return -1;
            }
            return 0; //wot?
        }
    }
}