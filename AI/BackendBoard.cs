using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Numpy;
using AI;

namespace Game
{
    public struct GameResult
    {
        public bool finished;
        public float player_won;

        public GameResult(bool finished, float player_won)
        {
            this.finished = finished;
            this.player_won = player_won;
        }
    }
    public struct BackendBoard
    {
        public int height;
        public int width;
        public int win_requirement;

        public float[] board;

        public int[] ValidMoveMask()
        {

            int[] result = new int[width];
            for (int i = 0; i < width; ++i)
            {
                if (!ColumnFull(i))
                {
                    result[i] = 1;
                }
            }
            return result;

        }
        public BackendBoard(int height, int width, int win_requirement)
        {
            this.height = height;
            this.width = width;
            board = new float[height * width];
            this.win_requirement = win_requirement;
        }

        private BackendBoard(int height, int width, int win_requirement, float[] board)
        {
            this.height = height;
            this.width = width;
            this.board = board;
            this.win_requirement = win_requirement;
        }
        public int Move(int col, int hasmove)
        {
            for (int row = 0; row < height; ++row)
            {
                if (board[col + row*width] == 0) //Empty square?
                {
                    board[col + row * width] = hasmove;
                    return col + row * width;
                }
            }
            return -1;
        }
        public void UndoMove(int position)
        {
            board[position] = 0;
        }

        public List<int> AvailableMoves()
        {
            List<int> result = new List<int>();
            for (int i = 0; i <width; ++i)
            {
                if (!ColumnFull(i))
                {
                    result.Add(i);
                }
            }
            return result;
        }

        public bool ColumnFull(int col)
        {
            for (int row = 0; row < height; ++row)
            {
                if (board[col + row * width] == 0) //Empty square?
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsFinished()
        {
            return GameResult().finished;
        }
        public bool IsWin(int player)
        {
            GameResult gameResult = GameResult();
            return gameResult.player_won == player;
        }
        public GameResult GameResult()
        {
            for (int col = 0; col < width; ++col)
            {
                for (int row = 0; row < height; ++row)
                {
                    var side = board[col + row * width];
                    if (side == 0) //Empty square
                    {
                        continue;
                    }
                    if (col >= win_requirement - 1) //Look for duplicates towards the left
                    {
                        int inarow = FindDuplicates(col + row * width, row * width, side, -1); //Look towards the right
                        if (inarow >= win_requirement)
                        {
                            return new GameResult(true, side); //Win
                        }
                        inarow = FindDuplicates(col + row * width, (col + row * width) - col * (width + 1), side, -(width + 1));
                        if (inarow >= win_requirement)
                        {
                            return new GameResult(true, side); //Win
                        }
                        inarow = FindDuplicates(col + row * width, (col + row * width) + col * (width - 1), side, (width - 1));
                        if (inarow >= win_requirement)
                        {
                            return new GameResult(true, side); //Win
                        }
                    }
                    if (row >= win_requirement - 1) //Look for duplicates downwards
                    {
                        int inarow = FindDuplicates(col + row * width, col, side, -(width));
                        if (inarow >= win_requirement)
                        {
                            return new GameResult(true, side); //Win
                        }
                    }
                }
            }
            if (AvailableMoves().Count == 0)
            {
                return new GameResult(true, 0);
            }
            return new GameResult(false, 0); //Unfinished game
        }

        public int GetReward(int player)
        {
            var result = GameResult();
            if (result.finished)
            {
                return (int)(result.player_won) == player ? 1 : -1;
            }
            return 0; //We drew, :\
        }

        private int FindDuplicates(int startposition, int endposition, float side, int direction, int duplicatesfound = 0)
        {
            if (startposition <= -1 || startposition >= board.Length) //Out of range
            {
                return duplicatesfound;
            }

            if (board[startposition] == side)
            {
                ++duplicatesfound;
            }
            if (startposition == endposition || board[startposition] != side) //Found a match?
            {
                return duplicatesfound;
            }

            return FindDuplicates(startposition + direction, endposition, side, direction, duplicatesfound);
        }

        public BackendBoard Flipped()
        {
            return new BackendBoard(height, width, win_requirement, board.Select(n=>n*-1).ToArray());
        }
        public BackendBoard Flipp(int player)
        {
            return new BackendBoard(height, width, win_requirement, board.Select(n => n * player).ToArray());
        }

        public BackendBoard NextState(int player, int action)
        {
            float[] b = board.Select(n => n).ToArray(); //Copy board

            BackendBoard backendBoard = new BackendBoard(height, width, win_requirement, b);
            backendBoard.Move(action, player);
            return backendBoard;
        }
    }
}