using System.Timers;
using TorchSharp;
using Game;

namespace MonteCarlo
{
    enum PlayerType
    {
        Human,
        ML,
        MCTS,
        MinMax
    }
    public partial class Form1 : Form
    {
        ConnectBoard connectBoard;

        public Form1()
        {
            InitializeComponent();

            GreenPlayerBox.SelectedIndex = 0;
            RedPlayerBox.SelectedIndex = 0;
        }
        System.Timers.Timer checkmateTimer = new System.Timers.Timer();
        int won = 0;
        bool runagain = true;

        int firstplayer = 1;
        PlayerType red
        {
            get
            {
                return (PlayerType)RedPlayerBox.SelectedIndex;
            }
        }
        PlayerType green
        {
            get
            {
                return (PlayerType)GreenPlayerBox.SelectedIndex;
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (connectBoard == null)
            {
                connectBoard = new ConnectBoard(6,7,50,new Point(10,20), 4);
                connectBoard.Draw(e.Graphics);
                hasturn = firstplayer;
            }
            else
            {
                connectBoard.Paint(e.Graphics);
            }
            if (connectBoard != null && connectBoard.backendBoard.IsFinished() && runagain)
            {
                won = connectBoard.backendBoard.GetReward(1);
                runagain = false;
                checkmateTimer = new System.Timers.Timer(100);
                checkmateTimer.Elapsed += new ElapsedEventHandler(TimerElapsed);
                checkmateTimer.Start();
                Invalidate();
            }
            if (waitfordraw)
            {
                waitfordraw = false;
                hasturn = HASTURN; //Run AI thing again.
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
                MessageBox.Show(String.Format("{0} won", hasturn == 1 ? "Green" : "Red"), "Victory");
            }
            messageshowing = false;
            connectBoard = null;
            runagain = true;
            won = 0;
            firstplayer = -firstplayer;
            Invalidate();
        }
        int HASTURN = 1;
        bool waitfordraw = false;
        int hasturn
        {
            get
            {
                return HASTURN;
            }
            set
            {
                HASTURN = value;
                PlayTurn(ref HASTURN);
                if (HASTURN != value)
                {
                    waitfordraw = true;
                    Invalidate();
                }
            }
        }

        private void PlayTurn(ref int value)
        {
            if (connectBoard != null && connectBoard.backendBoard.IsFinished())
            {
                return;
            }
            int position = -1;
            if (value == 1) //Red
            {
                switch (red)
                {
                    case PlayerType.Human:
                        break;
                    case PlayerType.ML:
                        position = MiniMax.ML_PROB_Move(connectBoard.backendBoard, value);
                        break;
                    case PlayerType.MinMax:
                        position = MiniMax.BestMove(connectBoard.backendBoard, value);
                        break;
                    case PlayerType.MCTS:
                        position = MiniMax.Best_ML_Move(connectBoard.backendBoard, value);
                        break;
                }
            }
            else
            {
                switch (green)
                {
                    case PlayerType.Human:
                        break;
                    case PlayerType.ML:
                        position = MiniMax.ML_PROB_Move(connectBoard.backendBoard, value);
                        break;
                    case PlayerType.MinMax:
                        position = MiniMax.BestMove(connectBoard.backendBoard, value);
                        break;
                    case PlayerType.MCTS:
                        position = MiniMax.Best_ML_Move(connectBoard.backendBoard, value);
                        break;
                }
            }
            if (position == -1) //No moves?
            {
                return;
            }
            else
            {
                var realpos = connectBoard.backendBoard.Move(position, value);
                connectBoard.squares[realpos].side = value;
                value = -value;
                Invalidate();
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            Square square = connectBoard.SquareAt(((MouseEventArgs)e).Location);
            if (square == null || messageshowing)
            {
                return;
            }
            if ((hasturn == 1 && red != PlayerType.Human) || (hasturn == 0 && green != PlayerType.Human)) //Are we trying to play an AI's turn
            {
                return;
            }
            square.Click(hasturn);
            hasturn = -hasturn;
            Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void PlayerChanged(object sender, EventArgs e)
        {
            hasturn = HASTURN;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }
        string[] lines = new string[100];
        public bool Log(string input)
        {
            textBox1.AppendText(input + "\r\n");
            return false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void SaveModel(object sender, EventArgs e)
        {
            saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "ML files (*.TML)|*.TML|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var path = Path.GetFullPath(saveFileDialog1.FileName);
                MiniMax.model.save(path);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "ML files (*.TML)|*.TML";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var path = Path.GetFullPath(openFileDialog1.FileName);
                MiniMax.model.load(path);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var iterations = -1;
            if (int.TryParse(IterationBox.Text,out iterations))
            {
                if (iterations <= 0)
                {
                    MessageBox.Show("Iterations cannot be negative or zero");
                }
                else
                {
                    panel2.Visible = false;
                    MessageBox.Show(String.Format("Training beggining for {0} iterations", iterations));
                    MiniMax.TrainML(iterations, Log);
                    MessageBox.Show("Training finished");
                }
            }
            else
            {
                MessageBox.Show("Iterations must be a number");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }
    }
}