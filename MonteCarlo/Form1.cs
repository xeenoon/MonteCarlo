using System.Timers;
using TorchSharp;
using Game;
using static TorchSharp.torch;
using AI;

namespace MonteCarlo
{
    public partial class Form1 : Form
    {
        string playerType;

        ConnectBoard connectBoard;
        public static List<TorchNetwork> models = new List<TorchNetwork>();
        MiniMax minmax = new MiniMax();
        RandomAI randomAI = new RandomAI();
        MCTS_AI mcts = new MCTS_AI(10000);
        public Form1()
        {
            InitializeComponent();

            models.Add(new TorchNetwork("Default_ML", layers, string.Format(@"C:\Users\{0}\Downloads\Default.TML",Environment.UserName),true, 0.0005, Log,1000));

            GreenPlayerBox.Items.Clear();
            GreenPlayerBox.Items.Add("Human");
            GreenPlayerBox.Items.Add("Random");
            GreenPlayerBox.Items.Add("Brute force");
            GreenPlayerBox.Items.Add("Default_ML");
            GreenPlayerBox.Items.Add("MCTS");

            RedPlayerBox.Items.Clear();
            RedPlayerBox.Items.Add("Human");
            RedPlayerBox.Items.Add("Random");
            RedPlayerBox.Items.Add("Brute force");
            RedPlayerBox.Items.Add("Default_ML");
            RedPlayerBox.Items.Add("MCTS");

            GreenPlayerBox.SelectedIndex = 0;
            RedPlayerBox.SelectedIndex = 0;
        }
        System.Timers.Timer checkmateTimer = new System.Timers.Timer();
        int won = 0;
        bool runagain = true;

        int firstplayer = 1;
        ComputerPlayer red
        {
            get
            {
                var selected = (string)RedPlayerBox.SelectedItem;
                if (selected.ToLower() == "brute force") //Default AI
                {
                    return minmax;
                }
                if (selected.ToLower() == "random")
                {
                    return randomAI;
                }
                if (selected.ToLower() == "mcts")
                {
                    return mcts;
                }
                var ML = models.FirstOrDefault(m=>m.nameID == selected);
                return ML; //If ML is null, then the computer player will be null, i.e. it is a humans turn
            }
        }
        ComputerPlayer green
        {
            get
            {
                var selected = (string)GreenPlayerBox.SelectedItem;
                if (selected.ToLower() == "brute force") //Default AI
                {
                    return minmax;
                }
                if (selected.ToLower() == "random")
                {
                    return randomAI;
                }
                if (selected.ToLower() == "mcts")
                {
                    return mcts;
                }
                var ML = models.FirstOrDefault(m => m.nameID == selected);
                return ML; //If ML is null, then the computer player will be null, i.e. it is a humans turn
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
            if (connectBoard == null)
            {
                return;
            }
            if (connectBoard.backendBoard.IsFinished())
            {
                return;
            }
            int position = -1;
            if (value == 1) //Red
            {
                if (red != null) //Computer playing
                {
                    position = red.BestMove(connectBoard.backendBoard, value);
                }
            }
            else
            {
                if (green != null) //Computer playing
                {
                    position = green.BestMove(connectBoard.backendBoard, value);
                }
            }
            if (position == -1) //No moves?
            {
                return;
            }
            else if (position >= 7)
            {

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
            if ((hasturn == 1 && red != null) || (hasturn == 0 && green != null)) //Are we trying to play an AI's turn
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

        TorchNetwork selectedModel;
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
                    selectedModel.Train(iterations);
                    MessageBox.Show("Training finished");
                    torch.NewDisposeScope();
                    MessageBox.Show("Tensors: " + Tensor.TotalCount);
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

        private void button6_Click(object sender, EventArgs e)
        {
            AI_Settings_Panel.Visible = true;
        }
        public void ShowAIData()
        {
            string selected_AI = (string)AI_List.SelectedItem;
            selectedModel = models.FirstOrDefault(n=>n.nameID == selected_AI);
            if (selectedModel == null)
            {
                ImportButton.Enabled = false;
                DeleteButton.Enabled = false;
                CopyButton.Enabled = false;
                SaveLabel.Enabled = false;
                SavePath_textbox.Enabled = false;

                NameLabel.Enabled = false;
                Name_textbox.Enabled = false;

                AutosaveCheckbox.Enabled = false;

                LR_Label.Enabled = false;
                LR_textbox.Enabled = false;

                Depth_Label.Enabled = false;
                Depth_textbox.Enabled = false;

                ApplyButton.Enabled = false;
                TrainButton.Enabled = false;
                CloseButton.Enabled = false;

                Name_textbox.Text = "";
                AutosaveCheckbox.Checked = false;
                LR_textbox.Text = "";
                Depth_textbox.Text = "";
                SavePath_textbox.Text = "";

                //Hide all the stuff

                return;
            }
            ImportButton.Enabled = true;
            DeleteButton.Enabled = true;
            CopyButton.Enabled = true;
            SaveLabel.Enabled = true;
            SavePath_textbox.Enabled = true;

            NameLabel.Enabled = true;
            Name_textbox.Enabled = true;

            AutosaveCheckbox.Enabled = true;

            LR_Label.Enabled = true;
            LR_textbox.Enabled = true;

            Depth_Label.Enabled = true;
            Depth_textbox.Enabled = true;

            ApplyButton.Enabled = true;
            TrainButton.Enabled = true;
            CloseButton.Enabled = true; //Show all the stuff

            Name_textbox.Text = selected_AI;
            AutosaveCheckbox.Checked = selectedModel.autosave;
            LR_textbox.Text = selectedModel.learnrate.ToString();
            Depth_textbox.Text = selectedModel.depth.ToString();
            SavePath_textbox.Text = selectedModel.filepath;
        }
        bool dontupdate = false;
        private void AI_List_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dontupdate)
            {
                return;
            }
            if (selectedModel == null || SaveChanges())
            {
                ShowAIData();
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (SaveChanges())
            {
                AI_Settings_Panel.Visible = false;
                AI_List.SelectedIndex = -1;
            }
        }
        public bool SaveChanges()
        {
            if (selectedModel == null)
            {
                return false;
            }

            selectedModel.autosave = AutosaveCheckbox.Checked;
            if (selectedModel.nameID != Name_textbox.Text)
            {
                if (AI_List.Items.Contains(Name_textbox.Text))
                {
                    var result = MessageBox.Show("No duplicate names allowed", "Invalid input", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                        return false; //Allow user to enter again
                    }
                    //Otherwise continue with other results
                }
                dontupdate = true;

                RedPlayerBox.Items.Add(Name_textbox.Text);
                GreenPlayerBox.Items.Add(Name_textbox.Text);

                RedPlayerBox.Items.Remove(selectedModel.nameID);
                GreenPlayerBox.Items.Remove(selectedModel.nameID);


                AI_List.Items.Remove(selectedModel.nameID);
                selectedModel.nameID = Name_textbox.Text;
                AI_List.Items.Add(selectedModel.nameID);


                dontupdate = false;
                AI_List.SelectedIndex = AI_List.Items.IndexOf(selectedModel.nameID);
            }
            double lr = 0;
            if (!double.TryParse(LR_textbox.Text, out lr))
            {
                var result = MessageBox.Show("Please enter a valid decimal for \"learn rate\"", "Invalid input",MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    return false; //Allow user to enter again
                }
                //Otherwise continue with other results
            }
            else
            {
                selectedModel.learnrate = lr;
            }

            int depth = 0;
            if (!int.TryParse(Depth_textbox.Text, out depth))
            {
                var result = MessageBox.Show("Please enter a valid decimal for \"depth\"", "Invalid input", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    return false; //Allow user to enter again
                }
                //Otherwise continue with other results
            }
            else
            {
                selectedModel.depth = depth;
            }

            var possiblePath = SavePath_textbox.Text.IndexOfAny(Path.GetInvalidPathChars()) == -1;
            if (possiblePath)
            {
                selectedModel.filepath = SavePath_textbox.Text;
            }
            else
            {
                var result = MessageBox.Show("Please enter a valid path for \"Save filepath\"", "Invalid input", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    return false; //Allow user to enter again
                }
                //Otherwise continue with other results
            }

            return true; //Successfully changed stuff
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            if (SaveChanges())
            {
                ShowAIData();
            }
        }
        Random r = new Random();
        private void NewButton_Click(object sender, EventArgs e)
        {
            ModelPanel.Visible = true;
            layers = new List<Layer>() { Layer.BeginLinear, Layer.EndLinear };
            Layer_listbox.Items.Clear();
            Layer_listbox.Items.Add("Linear (fixed)");
            Layer_listbox.Items.Add("Linear (fixed)");
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (selectedModel == null)
            {
                return;
            }
            AI_List.Items.Remove(selectedModel.nameID);
            models.Remove(selectedModel);
            ShowAIData();
        }

        private void TrainButton_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "ML files (*.TML)|*.TML";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var path = Path.GetFullPath(openFileDialog1.FileName);
                TorchNetwork toadd = new TorchNetwork(path.Split("\\").Last(), layers, path, true, 0.0005, Log, 1000);
                selectedModel.load(path);
                ShowAIData();
            }
        }
        List<Layer> layers = new List<Layer>() { Layer.BeginLinear, Layer.EndLinear };
        private void button2_Click(object sender, EventArgs e)
        {
            Layer_listbox.Items.Insert(1,"Linear (fc1)");
            Layer item = new Layer(Layer.LayerType.Linear, layers[0].outputsize, layers[1].inputsize);
            layers.Insert(1, item);

            ShowModelData(1, item);
        }

        private void ShowModelData(int index, Layer layer)
        {
            TransformationType_combobox.Items.Remove("");
            
            TransformationType_label.Enabled = true;
            TransformationType_combobox.Enabled = true;
            TransformationType_combobox.SelectedIndex = layer.type == Layer.LayerType.Convolutional ? 1 : 0;

            InputSize_label.Enabled = true;
            InputSize_textbox.Enabled = true;
            InputSize_textbox.Text = layer.inputsize.ToString();

            OutputSize_label.Enabled = true;
            OutputSize_textbox.Enabled = true;
            OutputSize_textbox.Text = layer.outputsize.ToString();

            LayerIndex_label.Enabled = true;
            LayerIndex_textbox.Enabled = true;
            LayerIndex_textbox.Text = index.ToString();

            if (layer.type == Layer.LayerType.Convolutional) 
            {
                Convolution_label.Enabled = true;
                Stride_label.Enabled = true;
                Stride_textbox.Enabled = true;
                Stride_textbox.Text = layer.stridesize.ToString();
            }

            if (index == 0)
            {
                InputSize_textbox.Text = "42";
                InputSize_textbox.Enabled = false;
                InputSize_label.Enabled = false;
                layer.inputsize = 42;
                LayerIndex_label.Enabled = false;
                LayerIndex_textbox.Enabled = false;
            }
            if (index == Layer_listbox.Items.Count - 1)
            {
                OutputSize_textbox.Text = "7";
                OutputSize_textbox.Enabled = false;
                OutputSize_label.Enabled = false;
                layer.outputsize = 7;
                TransformationType_combobox.Enabled = false;
                TransformationType_combobox.SelectedIndex = 0;
                TransformationType_label.Enabled = false;
                layer.type = Layer.LayerType.Linear;
                LayerIndex_label.Enabled = false;
                LayerIndex_textbox.Enabled = false;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int random = 1;
            string randomname = "RandomName";
            while (models.Any(m => m.nameID == randomname))
            {
                random = r.Next();
                randomname = "RandomName" + random;
            }
            var model = new TorchNetwork(randomname, layers, string.Format(@"C:\Users\{0}\Downloads\{1}.TML", Environment.UserName, randomname), true, 0.0005, Log, 1000);

            AI_List.Items.Add(randomname);
            models.Add(model);
            AI_List.SelectedIndex = AI_List.Items.IndexOf(randomname);
            RedPlayerBox.Items.Add(randomname);
            GreenPlayerBox.Items.Add(randomname);
            ShowAIData();
        }

        private void Close2_button_Click(object sender, EventArgs e)
        {
            ModelPanel.Visible = false;

            TransformationType_label.Enabled = false;

            if (TransformationType_combobox.SelectedIndex != -1)
            {
                TransformationType_combobox.Enabled = true; //Make sure it is active, allowing us to do the operation

                TransformationType_combobox.Items.Add("");
                TransformationType_combobox.SelectedIndex = 2;
            }
            TransformationType_combobox.Enabled = false;
            
            InputSize_label.Enabled = false;
            InputSize_textbox.Enabled = false;
            InputSize_textbox.Text = "";

            OutputSize_label.Enabled = false;
            OutputSize_textbox.Enabled = false;
            OutputSize_textbox.Text = "";

            LayerIndex_label.Enabled = false;
            LayerIndex_textbox.Enabled = false;
            LayerIndex_textbox.Text = "";


            Convolution_label.Enabled = false;
            Stride_label.Enabled = false;
            Stride_textbox.Enabled = false;
            Stride_textbox.Text = "";

            lastindex = -1;
        }
        int lastindex = -1;
        bool ignore = false;
        private void Layer_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignore)
            {
                return;
            }
            int select_idx = Layer_listbox.SelectedIndex;
            if (SaveModelData() || lastindex == -1)
            {
                ignore = true;
                Layer_listbox.SelectedIndex = select_idx; //Just in case savemodeldata changed it while saving

                ShowModelData(Layer_listbox.SelectedIndex, layers[Layer_listbox.SelectedIndex]);
                ignore = false;
                lastindex = Layer_listbox.SelectedIndex;
            }
            else
            {
                Layer_listbox.SelectedIndex = lastindex;
            }
        }

        private bool SaveModelData()
        {
            if (lastindex == -1)
            {
                return false;
            }
            var layer = layers[lastindex];
            if (!int.TryParse(InputSize_textbox.Text, out int inputsize))
            {
                var response = MessageBox.Show("Invalid input for \"input size\", continue anyway?", "Invalid input", MessageBoxButtons.YesNo);
                if (response == DialogResult.Yes)
                {
                    return false;
                }
            }
            else
            {
                layer.inputsize = inputsize;
            }

            if (!int.TryParse(OutputSize_textbox.Text, out int outputsize))
            {
                var response = MessageBox.Show("Invalid input for \"output size\", continue anyway?", "Invalid input", MessageBoxButtons.YesNo);
                if (response == DialogResult.Yes)
                {
                    return false;
                }
            }
            else
            {
                layer.outputsize = outputsize;
            }

            if (!int.TryParse(Stride_textbox.Text, out int stridesize) && Stride_textbox.Text != "")
            {
                var response = MessageBox.Show("Invalid input for \"stride size\", continue anyway?", "Invalid input", MessageBoxButtons.YesNo);
                if (response == DialogResult.Yes)
                {
                    return false;
                }
            }
            else
            {
                layer.stridesize = stridesize;
            }

            if (TransformationType_combobox.SelectedIndex == 0) //Linear
            {
                layer.type = Layer.LayerType.Linear;
                ignore = true;

                Layer_listbox.Items.RemoveAt(lastindex); //remove the element
                Layer_listbox.Items.Insert(lastindex, "Linear (fc1)"); //re-insert the replacement element
                Layer_listbox.SelectedIndex = lastindex;

                ignore = false;
            }
            else
            {
                layer.type = Layer.LayerType.Convolutional;
                ignore = true;
                
                Layer_listbox.Items.RemoveAt(lastindex); //remove the element
                Layer_listbox.Items.Insert(lastindex, "Convolutional (fc1)"); //re-insert the replacement element
                Layer_listbox.SelectedIndex = lastindex;

                ignore = false;
            }

            if (lastindex == 0) //First item?
            {
                if (TransformationType_combobox.SelectedIndex == 0) //Linear
                {
                    layer.type = Layer.LayerType.Linear;
                    ignore = true;

                    Layer_listbox.Items.RemoveAt(lastindex); //remove the element
                    Layer_listbox.Items.Insert(lastindex, "Linear (fixed)"); //re-insert the replacement element
                    Layer_listbox.SelectedIndex = lastindex;

                    ignore = false;
                }
                else
                {
                    layer.type = Layer.LayerType.Convolutional;
                    ignore = true;

                    Layer_listbox.Items.RemoveAt(lastindex); //remove the element
                    Layer_listbox.Items.Insert(lastindex, "Convolutional (fixed)"); //re-insert the replacement element
                    Layer_listbox.SelectedIndex = lastindex;

                    ignore = false;
                }
            }
            else if(lastindex == Layer_listbox.Items.Count-1)
            {
                layer.type = Layer.LayerType.Linear;
                ignore = true;

                Layer_listbox.Items.RemoveAt(lastindex); //remove the element
                Layer_listbox.Items.Insert(lastindex, "Linear (fixed)"); //re-insert the replacement element
                Layer_listbox.SelectedIndex = lastindex;

                ignore = false;
            }

            if (!int.TryParse(LayerIndex_textbox.Text, out int layerindex))
            {
                var response = MessageBox.Show("Invalid input for \"layer index\", continue anyway?", "Invalid input", MessageBoxButtons.YesNo);
                if (response == DialogResult.Yes)
                {
                    return false;
                }
            }
            else
            {
                if (layerindex != lastindex)
                {
                    ignore = true;
                    if (lastindex <= layerindex) //Placing thing below (an index higher)
                    {
                        Layer_listbox.Items.Insert(layerindex, Layer_listbox.Items[lastindex]);
                        layers.Insert(layerindex, layers[lastindex]);
                        //Indexes dont matter, as the item was placed above the selected index
                        Layer_listbox.Items.RemoveAt(lastindex); //Remove the item
                        layers.RemoveAt(lastindex);
                    }
                    else
                    {
                        Layer_listbox.Items.Insert(layerindex, Layer_listbox.Items[lastindex]);
                        layers.Insert(layerindex, layers[lastindex]);
                        //All indexes shifted up by 1
                        Layer_listbox.Items.RemoveAt(lastindex + 1); //Remove the item
                        layers.RemoveAt(lastindex+1);
                    }
                    lastindex = Layer_listbox.SelectedIndex;
                    ignore = false; //Stop onselectindexchanged running for listbox
                }
            }

            return true;
        }

        private void TransformationType_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TransformationType_combobox.SelectedIndex == 1)
            {
                Convolution_label.Enabled = true;
                Stride_label.Enabled = true;
                Stride_textbox.Enabled = true;
                layers[TransformationType_combobox.SelectedIndex].stridesize = 1;
                Stride_textbox.Text = "1";
            }
            else
            {
                Convolution_label.Enabled = false;
                Stride_label.Enabled = false;
                Stride_textbox.Enabled = false;
                layers[TransformationType_combobox.SelectedIndex].stridesize = 0;
                Stride_textbox.Text = "";
            }

            if (!ignore)
            {
                SaveModelData();
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            lastindex = Layer_listbox.SelectedIndex;
            SaveModelData();
        }
    }
}