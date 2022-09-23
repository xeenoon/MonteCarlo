using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarlo
{
    public struct GameResult
    {
        public bool finished;
        public int player_won;

        public GameResult(bool finished, int player_won)
        {
            this.finished = finished;
            this.player_won = player_won;
        }
    }
    public class BackendBoard
    {
        public int height;
        public int width;
        public int win_requirement;

        public int[] board;

        public List<int> empty_squares = new List<int>();

        public BackendBoard(int height, int width, int win_requirement)
        {
            this.height = height;
            this.width = width;
            board = new int[height * width];
            this.win_requirement = win_requirement;
            empty_squares = Enumerable.Range(0, height * width).ToList();
        }
        public void Move(int position, int hasmove)
        {
            board[position] = hasmove;
            empty_squares.Remove(position);
        }
        public void UndoMove(int position)
        {
            board[position] = 0;
            empty_squares.Add(position);
        }

        public bool IsFinished()
        {
            for (int col = 0; col < width; ++col)
            {
                for (int row = 0; row < height; ++row)
                {
                    var side = board[col + row * 8];
                    if (side == 0) //Empty square
                    {
                        continue;
                    }
                    if (col >= win_requirement - 1) //Look for duplicates towards the left
                    {
                        int inarow = FindDuplicates(col + row * 8, row * 8, side, -1); //Look towards the right
                        if (inarow >= win_requirement)
                        {
                            return true; //Win
                        }
                    }
                }
            }
            if (empty_squares.Count == 0)
            {
                return true; //Draw
            }
            return false; //Unfinished game
        }
        public bool IsWin(int player)
        {
            for (int col = 0; col < width; ++col)
            {
                for (int row = 0; row < height; ++row)
                {
                    var side = board[col + row * 8];
                    if (side != player) //Empty square or opponent
                    {
                        continue;
                    }
                    if (col >= win_requirement - 1) //Look for duplicates towards the left
                    {
                        int inarow = FindDuplicates(col + row * 8, row * 8, side, -1); //Look towards the right
                        if (inarow >= win_requirement)
                        {
                            return true; //Win
                        }
                    }
                }
            }
            return false; //Unfinished game OR draw
        }
        public GameResult GameResult()
        {
            for (int col = 0; col < width; ++col)
            {
                for (int row = 0; row < height; ++row)
                {
                    var side = board[col + row * 8];
                    if (side == 0) //Empty square
                    {
                        continue;
                    }
                    if (col >= win_requirement - 1) //Look for duplicates towards the left
                    {
                        int inarow = FindDuplicates(col + row * 8, row * 8, side, -1); //Look towards the right
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

        private int FindDuplicates(int startposition, int endposition, int side, int direction, int duplicatesfound = 0)
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
    }
}
