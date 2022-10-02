using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game;

namespace MonteCarlo
{
    internal class ConnectBoard
    {
        public int height;
        public int width;
        public int size;
        public int winrequirement;

        public Point offset;

        public Square[] squares;
        public BackendBoard backendBoard;

        public ConnectBoard(int height, int width, int size, Point offset, int winrequirement)
        {
            this.height = height;
            this.width = width;
            this.size = size;
            this.offset = offset;

            squares = new Square[width * height];
            this.winrequirement = winrequirement;
            backendBoard = new BackendBoard(height, width, winrequirement);
        }
        internal void Paint(Graphics g)
        {
            foreach (var square in squares)
            {
                square.Draw(g);
            }
        }

        internal Square SquareAt(Point location)
        {
            location.X -= this.offset.X;
            location.Y -= this.offset.Y; //Account for the offset
            location.Y = ((height) * size) -location.Y;

            location.X /= size;
            location.Y /= size;

            if (location.X >= width)
            {
                return null;
            }
            if (location.Y >= height)
            {
                return null;
            }

            int squareloc = location.X + location.Y * width;
            try
            {
                return squares[squareloc];
            }
            catch
            {
                return null;
            }
        }

        public void Draw(Graphics g)
        {
            for (int i = 0; i < width * height; ++i)
            {
                int x = (i % width) * size + offset.X;
                int y = ((height-1)*size) - ((i / width) * size) + offset.Y;
                Rectangle bounds = new Rectangle(x, y, size, size);
                squares[i] = new Square(i, bounds, new Pen(Color.White), new Pen(Color.Blue), this);
                squares[i].Draw(g);
            }
        }
    }
    internal class Square
    {
        public int location;
        public Rectangle bounds;

        public Pen background;
        public Pen colour;

        public int side = 0;
        public ConnectBoard connectBoard;

        public Square(int location, Rectangle bounds, Pen background, Pen colour, ConnectBoard connectBoard)
        {
            this.location = location;
            this.bounds = bounds;

            this.background = background;
            this.colour = colour;
            this.connectBoard = connectBoard;
        }

        public void Draw(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.FillRectangle(colour.Brush, bounds);
            g.DrawRectangle(background, bounds);
            if (side == 0) 
            {
                g.FillEllipse(background.Brush, bounds.X + bounds.Width / 6f, bounds.Y + bounds.Height / 6f, bounds.Width / 1.5f, bounds.Height / 1.5f);
            }
            else if(side == 1)
            {
                g.FillEllipse(new Pen(Color.Red).Brush, bounds.X + bounds.Width / 6f, bounds.Y + bounds.Height / 6f, bounds.Width / 1.5f, bounds.Height / 1.5f);
            }
            else if (side == -1)
            {
                g.FillEllipse(new Pen(Color.Green).Brush, bounds.X + bounds.Width / 6f, bounds.Y + bounds.Height / 6f, bounds.Width / 1.5f, bounds.Height / 1.5f);
            }
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
        }

        internal void Click(int hasturn)
        {
            if (side == 0) //Currently empty
            {
                var pos = connectBoard.backendBoard.Move(location%connectBoard.width,hasturn);
                connectBoard.squares[pos].side = hasturn;
            }
        }
    }
}