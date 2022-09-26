using System.Timers;
using TorchSharp;

namespace MonteCarlo
{
    public partial class Form1 : Form
    {
        ConnectBoard connectBoard;

        public Form1()
        {
            InitializeComponent();


        }
        System.Timers.Timer checkmateTimer = new System.Timers.Timer();
        int won = 0;
        bool runagain = true;

        bool humanfirst = true;
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (connectBoard == null)
            {
                connectBoard = new ConnectBoard(1,4,50,new Point(10,20), 2);
                connectBoard.Draw(e.Graphics);
                if (!humanfirst)
                {
                    var otherturn = -hasturn;
                    int position = MiniMax.BestMove(connectBoard.backendBoard, otherturn);
                    if (position == -1)
                    {
                        return;
                    }
                    connectBoard.backendBoard.Move(position, otherturn);
                    connectBoard.squares[position].side = otherturn;
                    connectBoard.Paint(e.Graphics);
                }
            }
            else
            {
                connectBoard.Paint(e.Graphics);
            }
            if (connectBoard.backendBoard.IsFinished() && runagain)
            {
                won = connectBoard.backendBoard.GetReward(1);
                runagain = false;
                checkmateTimer = new System.Timers.Timer(100);
                checkmateTimer.Elapsed += new ElapsedEventHandler(TimerElapsed);
                checkmateTimer.Start();
                Invalidate();
            }
        }
        bool messageshowing = false;
        public void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            checkmateTimer.Stop();
            messageshowing = true;
            if (won == 0)
            {
                MessageBox.Show(String.Format("Tis a draw"), "Draw");
            }
            else
            {
                MessageBox.Show(String.Format("{0} won", won == -1 ? "Opponent" : "You"), "Victory");
            }
            messageshowing = false;
            connectBoard = null;
            runagain = true;
            won = 0;
            humanfirst = !humanfirst;
            Invalidate();
        }
        int hasturn = 1;
        private void Form1_Click(object sender, EventArgs e)
        {
            Square square = connectBoard.SquareAt(((MouseEventArgs)e).Location);
            if (square == null || messageshowing)
            {
                return;
            }
            square.Click(hasturn);
            Invalidate();
        }
    }
}