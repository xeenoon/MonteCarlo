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

        public List<int> empty_squares = new List<int>();
        public int[] ValidMoves()
        {
            int[] validMoves = new int[board.Length];
            for (int i = 0; i < board.Length; i++)
            {
                float token = board[i];
                if (token == 0)
                {
                    validMoves[i] = 1; //Available space
                }
                else
                {
                    validMoves[i] = 0;
                }
            }
            return validMoves;
        }
        public BackendBoard(int height, int width, int win_requirement)
        {
            this.height = height;
            this.width = width;
            board = new float[height * width];
            this.win_requirement = win_requirement;
            empty_squares = Enumerable.Range(0, height * width).ToList();
        }

        private BackendBoard(int height, int width, int win_requirement, float[] board, List<int> emptySquares)
        {
            this.height = height;
            this.width = width;
            this.board = board;
            this.win_requirement = win_requirement;
            this.empty_squares = emptySquares;
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
                    return true;
                }
            }
            return false;
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
                        inarow = FindDuplicates(col + row * width, (col + row * width) - col * (width - 1), side, -(width - 1));
                        if (inarow >= win_requirement)
                        {
                            return new GameResult(true, side); //Win
                        }
                    }
                }
            }
            if (empty_squares.Count == 0)
            {
                return new GameResult(true, 0);
            }
            return new GameResult(false, 0); //Unfinished game
        }

        public int GetReward(int player)
        {
            if (IsWin(player))
            {
                return 1; //We won
            }
            else if (IsWin(-player))
            {
                return -1; //We lost :(
            }
            return 0; //We drew, :\
        }

        private int FindDuplicates(int startposition, int endposition, float side, int direction, int duplicatesfound = 0)
        {
            if (board[startposition] == side)
            {
                ++duplicatesfound;
            }
            if (startposition == endposition || board[startposition] != side) //Found a match?
            {
                return duplicatesfound;
            }

            return FindDuplicates(startposition+direction, endposition, side, direction, duplicatesfound);
        }

        public BackendBoard Flipped()
        {
            return new BackendBoard(height, width, win_requirement, board.Select(n=>n*-1).ToArray(), empty_squares.Copy());
        }
        public BackendBoard Flipp(int player)
        {
            return new BackendBoard(height, width, win_requirement, board.Select(n => n * player).ToArray(), empty_squares.Copy());
        }

        public BackendBoard NextState(int player, int action)
        {
            float[] b = board.Select(n => n).ToArray(); //Copy board

            BackendBoard backendBoard = new BackendBoard(height, width, 2, b, empty_squares.Copy());
            backendBoard.Move(action, player);
            return backendBoard;
        }
    }
}