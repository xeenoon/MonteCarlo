using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarlo
{
    public class BackendBoard
    {
        public int height;
        public int width;
        public int win_requirement;

        public Side[] board;

        public List<int> empty_squares = new List<int>();

        public BackendBoard(int height, int width, int win_requirement)
        {
            this.height = height;
            this.width = width;
            board = new Side[height * width];
            this.win_requirement = win_requirement;
            empty_squares = Enumerable.Range(0,height*width).ToList();
        }
        public void Move(int position, Side s)
        {
            board[position] = s;
            empty_squares.Remove(position);
        }
        public void UndoMove(int position)
        {
            board[position] = Side.None;
            empty_squares.Add(position);
        }

        public Side Victory()
        {
            for (int col = 0; col < width; ++col)
            {
                for (int row = 0; row < height; ++row)
                {
                    Side side = board[col + row * 8];
                    if (side == Side.None)
                    {
                        continue;
                    }
                    if (col >= win_requirement-1) //Look for duplicates towards the left
                    {
                        int inarow = FindDuplicates(col + row*8, row*8, side, -1); //Look towards the right
                        if (inarow >= win_requirement)
                        {
                            return side;
                        }
                    }
                }
            }
            if (board.All(s=>s != Side.None))
            {
                return Side.Red | Side.Green;
            }
            return Side.None;
        }

        private int FindDuplicates(int startposition, int endposition, Side side, int direction, int duplicatesfound = 0)
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
