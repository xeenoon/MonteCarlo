namespace MonteCarlo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.RedPlayerBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.GreenPlayerBox = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.IterationBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.AI_Settings_Panel = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.Name_textbox = new System.Windows.Forms.TextBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.TrainButton = new System.Windows.Forms.Button();
            this.CopyButton = new System.Windows.Forms.Button();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.Depth_Label = new System.Windows.Forms.Label();
            this.Depth_textbox = new System.Windows.Forms.TextBox();
            this.LR_Label = new System.Windows.Forms.Label();
            this.LR_textbox = new System.Windows.Forms.TextBox();
            this.AutosaveCheckbox = new System.Windows.Forms.CheckBox();
            this.SaveLabel = new System.Windows.Forms.Label();
            this.SavePath_textbox = new System.Windows.Forms.TextBox();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.ImportButton = new System.Windows.Forms.Button();
            this.NewButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.AI_List = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ModelPanel = new System.Windows.Forms.Panel();
            this.Apply_button = new System.Windows.Forms.Button();
            this.LayerIndex_textbox = new System.Windows.Forms.TextBox();
            this.LayerIndex_label = new System.Windows.Forms.Label();
            this.Delete_button = new System.Windows.Forms.Button();
            this.Close2_button = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Create_button = new System.Windows.Forms.Button();
            this.Convolution_label = new System.Windows.Forms.Label();
            this.Stride_textbox = new System.Windows.Forms.TextBox();
            this.Stride_label = new System.Windows.Forms.Label();
            this.TransformationType_combobox = new System.Windows.Forms.ComboBox();
            this.TransformationType_label = new System.Windows.Forms.Label();
            this.OutputSize_textbox = new System.Windows.Forms.TextBox();
            this.InputSize_textbox = new System.Windows.Forms.TextBox();
            this.OutputSize_label = new System.Windows.Forms.Label();
            this.InputSize_label = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Layer_listbox = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.AI_Settings_Panel.SuspendLayout();
            this.ModelPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(431, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Red player: ";
            // 
            // RedPlayerBox
            // 
            this.RedPlayerBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RedPlayerBox.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RedPlayerBox.FormattingEnabled = true;
            this.RedPlayerBox.Items.AddRange(new object[] {
            "Human",
            "ML",
            "MCTS",
            "Min-Max"});
            this.RedPlayerBox.Location = new System.Drawing.Point(563, 15);
            this.RedPlayerBox.Name = "RedPlayerBox";
            this.RedPlayerBox.Size = new System.Drawing.Size(121, 25);
            this.RedPlayerBox.TabIndex = 3;
            this.RedPlayerBox.SelectedIndexChanged += new System.EventHandler(this.PlayerChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(411, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 26);
            this.label3.TabIndex = 4;
            this.label3.Text = "Green player: ";
            // 
            // GreenPlayerBox
            // 
            this.GreenPlayerBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GreenPlayerBox.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GreenPlayerBox.FormattingEnabled = true;
            this.GreenPlayerBox.Items.AddRange(new object[] {
            "Human",
            "ML",
            "MCTS",
            "Min-Max"});
            this.GreenPlayerBox.Location = new System.Drawing.Point(563, 56);
            this.GreenPlayerBox.Name = "GreenPlayerBox";
            this.GreenPlayerBox.Size = new System.Drawing.Size(121, 25);
            this.GreenPlayerBox.TabIndex = 5;
            this.GreenPlayerBox.SelectedIndexChanged += new System.EventHandler(this.PlayerChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(411, 129);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(386, 267);
            this.panel1.TabIndex = 6;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Black;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(5, 36);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(372, 228);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(11, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Trainer logs";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button5);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.IterationBox);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(283, 155);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 100);
            this.panel2.TabIndex = 9;
            this.panel2.Visible = false;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(103, 67);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 3;
            this.button5.Text = "Cancel";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(22, 67);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 2;
            this.button4.Text = "Go";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // IterationBox
            // 
            this.IterationBox.Location = new System.Drawing.Point(50, 39);
            this.IterationBox.Name = "IterationBox";
            this.IterationBox.Size = new System.Drawing.Size(100, 22);
            this.IterationBox.TabIndex = 1;
            this.IterationBox.Text = "5";
            this.IterationBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "Training iterations";
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button6.Location = new System.Drawing.Point(701, 15);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(85, 25);
            this.button6.TabIndex = 10;
            this.button6.Text = "AI settings";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // AI_Settings_Panel
            // 
            this.AI_Settings_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AI_Settings_Panel.Controls.Add(this.label13);
            this.AI_Settings_Panel.Controls.Add(this.textBox5);
            this.AI_Settings_Panel.Controls.Add(this.label14);
            this.AI_Settings_Panel.Controls.Add(this.textBox6);
            this.AI_Settings_Panel.Controls.Add(this.NameLabel);
            this.AI_Settings_Panel.Controls.Add(this.Name_textbox);
            this.AI_Settings_Panel.Controls.Add(this.CloseButton);
            this.AI_Settings_Panel.Controls.Add(this.TrainButton);
            this.AI_Settings_Panel.Controls.Add(this.CopyButton);
            this.AI_Settings_Panel.Controls.Add(this.ApplyButton);
            this.AI_Settings_Panel.Controls.Add(this.Depth_Label);
            this.AI_Settings_Panel.Controls.Add(this.Depth_textbox);
            this.AI_Settings_Panel.Controls.Add(this.LR_Label);
            this.AI_Settings_Panel.Controls.Add(this.LR_textbox);
            this.AI_Settings_Panel.Controls.Add(this.AutosaveCheckbox);
            this.AI_Settings_Panel.Controls.Add(this.SaveLabel);
            this.AI_Settings_Panel.Controls.Add(this.SavePath_textbox);
            this.AI_Settings_Panel.Controls.Add(this.DeleteButton);
            this.AI_Settings_Panel.Controls.Add(this.ImportButton);
            this.AI_Settings_Panel.Controls.Add(this.NewButton);
            this.AI_Settings_Panel.Controls.Add(this.label6);
            this.AI_Settings_Panel.Controls.Add(this.AI_List);
            this.AI_Settings_Panel.Controls.Add(this.label5);
            this.AI_Settings_Panel.Location = new System.Drawing.Point(49, 12);
            this.AI_Settings_Panel.Name = "AI_Settings_Panel";
            this.AI_Settings_Panel.Size = new System.Drawing.Size(219, 466);
            this.AI_Settings_Panel.TabIndex = 11;
            this.AI_Settings_Panel.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Enabled = false;
            this.label13.Location = new System.Drawing.Point(138, 384);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(49, 14);
            this.label13.TabIndex = 31;
            this.label13.Text = "Ephocs";
            // 
            // textBox5
            // 
            this.textBox5.Enabled = false;
            this.textBox5.Location = new System.Drawing.Point(138, 401);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(73, 22);
            this.textBox5.TabIndex = 30;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Enabled = false;
            this.label14.Location = new System.Drawing.Point(7, 384);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(84, 14);
            this.label14.TabIndex = 29;
            this.label14.Text = "Temperature";
            // 
            // textBox6
            // 
            this.textBox6.Enabled = false;
            this.textBox6.Location = new System.Drawing.Point(7, 401);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 22);
            this.textBox6.TabIndex = 28;
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Enabled = false;
            this.NameLabel.Location = new System.Drawing.Point(7, 255);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(35, 14);
            this.NameLabel.TabIndex = 27;
            this.NameLabel.Text = "Name";
            // 
            // Name_textbox
            // 
            this.Name_textbox.Enabled = false;
            this.Name_textbox.Location = new System.Drawing.Point(7, 272);
            this.Name_textbox.Name = "Name_textbox";
            this.Name_textbox.Size = new System.Drawing.Size(204, 22);
            this.Name_textbox.TabIndex = 26;
            // 
            // CloseButton
            // 
            this.CloseButton.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CloseButton.Location = new System.Drawing.Point(147, 434);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(64, 24);
            this.CloseButton.TabIndex = 25;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // TrainButton
            // 
            this.TrainButton.Enabled = false;
            this.TrainButton.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TrainButton.Location = new System.Drawing.Point(77, 434);
            this.TrainButton.Name = "TrainButton";
            this.TrainButton.Size = new System.Drawing.Size(64, 24);
            this.TrainButton.TabIndex = 24;
            this.TrainButton.Text = "Train";
            this.TrainButton.UseVisualStyleBackColor = true;
            this.TrainButton.Click += new System.EventHandler(this.TrainButton_Click);
            // 
            // CopyButton
            // 
            this.CopyButton.Enabled = false;
            this.CopyButton.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CopyButton.Location = new System.Drawing.Point(147, 168);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(64, 24);
            this.CopyButton.TabIndex = 23;
            this.CopyButton.Text = "Copy";
            this.CopyButton.UseVisualStyleBackColor = true;
            // 
            // ApplyButton
            // 
            this.ApplyButton.Enabled = false;
            this.ApplyButton.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ApplyButton.Location = new System.Drawing.Point(7, 434);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(64, 24);
            this.ApplyButton.TabIndex = 22;
            this.ApplyButton.Text = "Apply";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // Depth_Label
            // 
            this.Depth_Label.AutoSize = true;
            this.Depth_Label.Enabled = false;
            this.Depth_Label.Location = new System.Drawing.Point(138, 330);
            this.Depth_Label.Name = "Depth_Label";
            this.Depth_Label.Size = new System.Drawing.Size(42, 14);
            this.Depth_Label.TabIndex = 21;
            this.Depth_Label.Text = "Depth";
            // 
            // Depth_textbox
            // 
            this.Depth_textbox.Enabled = false;
            this.Depth_textbox.Location = new System.Drawing.Point(138, 347);
            this.Depth_textbox.Name = "Depth_textbox";
            this.Depth_textbox.Size = new System.Drawing.Size(73, 22);
            this.Depth_textbox.TabIndex = 20;
            // 
            // LR_Label
            // 
            this.LR_Label.AutoSize = true;
            this.LR_Label.Enabled = false;
            this.LR_Label.Location = new System.Drawing.Point(7, 330);
            this.LR_Label.Name = "LR_Label";
            this.LR_Label.Size = new System.Drawing.Size(98, 14);
            this.LR_Label.TabIndex = 19;
            this.LR_Label.Text = "Learning rate";
            // 
            // LR_textbox
            // 
            this.LR_textbox.Enabled = false;
            this.LR_textbox.Location = new System.Drawing.Point(7, 347);
            this.LR_textbox.Name = "LR_textbox";
            this.LR_textbox.Size = new System.Drawing.Size(100, 22);
            this.LR_textbox.TabIndex = 18;
            // 
            // AutosaveCheckbox
            // 
            this.AutosaveCheckbox.AutoSize = true;
            this.AutosaveCheckbox.Enabled = false;
            this.AutosaveCheckbox.Location = new System.Drawing.Point(8, 304);
            this.AutosaveCheckbox.Name = "AutosaveCheckbox";
            this.AutosaveCheckbox.Size = new System.Drawing.Size(82, 18);
            this.AutosaveCheckbox.TabIndex = 17;
            this.AutosaveCheckbox.Text = "Autosave";
            this.AutosaveCheckbox.UseVisualStyleBackColor = true;
            // 
            // SaveLabel
            // 
            this.SaveLabel.AutoSize = true;
            this.SaveLabel.Enabled = false;
            this.SaveLabel.Location = new System.Drawing.Point(7, 204);
            this.SaveLabel.Name = "SaveLabel";
            this.SaveLabel.Size = new System.Drawing.Size(98, 14);
            this.SaveLabel.TabIndex = 16;
            this.SaveLabel.Text = "Save filepath";
            // 
            // SavePath_textbox
            // 
            this.SavePath_textbox.Enabled = false;
            this.SavePath_textbox.Location = new System.Drawing.Point(7, 221);
            this.SavePath_textbox.Name = "SavePath_textbox";
            this.SavePath_textbox.Size = new System.Drawing.Size(204, 22);
            this.SavePath_textbox.TabIndex = 15;
            // 
            // DeleteButton
            // 
            this.DeleteButton.Enabled = false;
            this.DeleteButton.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DeleteButton.Location = new System.Drawing.Point(77, 168);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(64, 24);
            this.DeleteButton.TabIndex = 14;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // ImportButton
            // 
            this.ImportButton.Enabled = false;
            this.ImportButton.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ImportButton.Location = new System.Drawing.Point(7, 168);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(64, 24);
            this.ImportButton.TabIndex = 13;
            this.ImportButton.Text = "Import";
            this.ImportButton.UseVisualStyleBackColor = true;
            this.ImportButton.Click += new System.EventHandler(this.ImportButton_Click);
            // 
            // NewButton
            // 
            this.NewButton.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NewButton.Location = new System.Drawing.Point(147, 50);
            this.NewButton.Name = "NewButton";
            this.NewButton.Size = new System.Drawing.Size(64, 24);
            this.NewButton.TabIndex = 12;
            this.NewButton.Text = "New";
            this.NewButton.UseVisualStyleBackColor = true;
            this.NewButton.Click += new System.EventHandler(this.NewButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(7, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 19);
            this.label6.TabIndex = 2;
            this.label6.Text = "Select AI";
            // 
            // AI_List
            // 
            this.AI_List.FormattingEnabled = true;
            this.AI_List.ItemHeight = 14;
            this.AI_List.Items.AddRange(new object[] {
            "Default_ML"});
            this.AI_List.Location = new System.Drawing.Point(7, 74);
            this.AI_List.Name = "AI_List";
            this.AI_List.Size = new System.Drawing.Size(204, 88);
            this.AI_List.TabIndex = 1;
            this.AI_List.SelectedIndexChanged += new System.EventHandler(this.AI_List_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(38, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(142, 24);
            this.label5.TabIndex = 0;
            this.label5.Text = "AI settings";
            // 
            // ModelPanel
            // 
            this.ModelPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ModelPanel.Controls.Add(this.Apply_button);
            this.ModelPanel.Controls.Add(this.LayerIndex_textbox);
            this.ModelPanel.Controls.Add(this.LayerIndex_label);
            this.ModelPanel.Controls.Add(this.Delete_button);
            this.ModelPanel.Controls.Add(this.Close2_button);
            this.ModelPanel.Controls.Add(this.button2);
            this.ModelPanel.Controls.Add(this.Create_button);
            this.ModelPanel.Controls.Add(this.Convolution_label);
            this.ModelPanel.Controls.Add(this.Stride_textbox);
            this.ModelPanel.Controls.Add(this.Stride_label);
            this.ModelPanel.Controls.Add(this.TransformationType_combobox);
            this.ModelPanel.Controls.Add(this.TransformationType_label);
            this.ModelPanel.Controls.Add(this.OutputSize_textbox);
            this.ModelPanel.Controls.Add(this.InputSize_textbox);
            this.ModelPanel.Controls.Add(this.OutputSize_label);
            this.ModelPanel.Controls.Add(this.InputSize_label);
            this.ModelPanel.Controls.Add(this.label8);
            this.ModelPanel.Controls.Add(this.Layer_listbox);
            this.ModelPanel.Controls.Add(this.label7);
            this.ModelPanel.Location = new System.Drawing.Point(286, 12);
            this.ModelPanel.Name = "ModelPanel";
            this.ModelPanel.Size = new System.Drawing.Size(200, 466);
            this.ModelPanel.TabIndex = 10;
            this.ModelPanel.Visible = false;
            // 
            // Apply_button
            // 
            this.Apply_button.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Apply_button.Location = new System.Drawing.Point(5, 434);
            this.Apply_button.Name = "Apply_button";
            this.Apply_button.Size = new System.Drawing.Size(58, 24);
            this.Apply_button.TabIndex = 47;
            this.Apply_button.Text = "Apply";
            this.Apply_button.UseVisualStyleBackColor = true;
            this.Apply_button.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // LayerIndex_textbox
            // 
            this.LayerIndex_textbox.Location = new System.Drawing.Point(7, 322);
            this.LayerIndex_textbox.Name = "LayerIndex_textbox";
            this.LayerIndex_textbox.Size = new System.Drawing.Size(187, 22);
            this.LayerIndex_textbox.TabIndex = 46;
            // 
            // LayerIndex_label
            // 
            this.LayerIndex_label.AutoSize = true;
            this.LayerIndex_label.Enabled = false;
            this.LayerIndex_label.Location = new System.Drawing.Point(7, 305);
            this.LayerIndex_label.Name = "LayerIndex_label";
            this.LayerIndex_label.Size = new System.Drawing.Size(84, 14);
            this.LayerIndex_label.TabIndex = 45;
            this.LayerIndex_label.Text = "Layer index";
            // 
            // Delete_button
            // 
            this.Delete_button.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Delete_button.Location = new System.Drawing.Point(71, 37);
            this.Delete_button.Name = "Delete_button";
            this.Delete_button.Size = new System.Drawing.Size(58, 24);
            this.Delete_button.TabIndex = 44;
            this.Delete_button.Text = "Delete";
            this.Delete_button.UseVisualStyleBackColor = true;
            this.Delete_button.Click += new System.EventHandler(this.Delete_button_Click);
            // 
            // Close2_button
            // 
            this.Close2_button.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Close2_button.Location = new System.Drawing.Point(135, 434);
            this.Close2_button.Name = "Close2_button";
            this.Close2_button.Size = new System.Drawing.Size(58, 24);
            this.Close2_button.TabIndex = 43;
            this.Close2_button.Text = "Close";
            this.Close2_button.UseVisualStyleBackColor = true;
            this.Close2_button.Click += new System.EventHandler(this.Close2_button_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button2.Location = new System.Drawing.Point(130, 37);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(64, 24);
            this.button2.TabIndex = 32;
            this.button2.Text = "New";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Create_button
            // 
            this.Create_button.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Create_button.Location = new System.Drawing.Point(70, 434);
            this.Create_button.Name = "Create_button";
            this.Create_button.Size = new System.Drawing.Size(58, 24);
            this.Create_button.TabIndex = 32;
            this.Create_button.Text = "Create";
            this.Create_button.UseVisualStyleBackColor = true;
            this.Create_button.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Convolution_label
            // 
            this.Convolution_label.AutoSize = true;
            this.Convolution_label.Enabled = false;
            this.Convolution_label.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Convolution_label.Location = new System.Drawing.Point(26, 354);
            this.Convolution_label.Name = "Convolution_label";
            this.Convolution_label.Size = new System.Drawing.Size(137, 25);
            this.Convolution_label.TabIndex = 37;
            this.Convolution_label.Text = "Conv settings";
            // 
            // Stride_textbox
            // 
            this.Stride_textbox.Location = new System.Drawing.Point(6, 401);
            this.Stride_textbox.Name = "Stride_textbox";
            this.Stride_textbox.Size = new System.Drawing.Size(187, 22);
            this.Stride_textbox.TabIndex = 36;
            // 
            // Stride_label
            // 
            this.Stride_label.AutoSize = true;
            this.Stride_label.Enabled = false;
            this.Stride_label.Location = new System.Drawing.Point(6, 384);
            this.Stride_label.Name = "Stride_label";
            this.Stride_label.Size = new System.Drawing.Size(84, 14);
            this.Stride_label.TabIndex = 35;
            this.Stride_label.Text = "Stride size";
            // 
            // TransformationType_combobox
            // 
            this.TransformationType_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TransformationType_combobox.FormattingEnabled = true;
            this.TransformationType_combobox.Items.AddRange(new object[] {
            "Linear",
            "Convolutional"});
            this.TransformationType_combobox.Location = new System.Drawing.Point(6, 176);
            this.TransformationType_combobox.Name = "TransformationType_combobox";
            this.TransformationType_combobox.Size = new System.Drawing.Size(187, 22);
            this.TransformationType_combobox.TabIndex = 34;
            this.TransformationType_combobox.SelectedIndexChanged += new System.EventHandler(this.TransformationType_combobox_SelectedIndexChanged);
            // 
            // TransformationType_label
            // 
            this.TransformationType_label.AutoSize = true;
            this.TransformationType_label.Enabled = false;
            this.TransformationType_label.Location = new System.Drawing.Point(6, 159);
            this.TransformationType_label.Name = "TransformationType_label";
            this.TransformationType_label.Size = new System.Drawing.Size(140, 14);
            this.TransformationType_label.TabIndex = 33;
            this.TransformationType_label.Text = "Transformation type";
            // 
            // OutputSize_textbox
            // 
            this.OutputSize_textbox.Location = new System.Drawing.Point(6, 272);
            this.OutputSize_textbox.Name = "OutputSize_textbox";
            this.OutputSize_textbox.Size = new System.Drawing.Size(187, 22);
            this.OutputSize_textbox.TabIndex = 32;
            // 
            // InputSize_textbox
            // 
            this.InputSize_textbox.Location = new System.Drawing.Point(6, 224);
            this.InputSize_textbox.Name = "InputSize_textbox";
            this.InputSize_textbox.Size = new System.Drawing.Size(187, 22);
            this.InputSize_textbox.TabIndex = 31;
            // 
            // OutputSize_label
            // 
            this.OutputSize_label.AutoSize = true;
            this.OutputSize_label.Enabled = false;
            this.OutputSize_label.Location = new System.Drawing.Point(6, 255);
            this.OutputSize_label.Name = "OutputSize_label";
            this.OutputSize_label.Size = new System.Drawing.Size(84, 14);
            this.OutputSize_label.TabIndex = 30;
            this.OutputSize_label.Text = "Output size";
            // 
            // InputSize_label
            // 
            this.InputSize_label.AutoSize = true;
            this.InputSize_label.Enabled = false;
            this.InputSize_label.Location = new System.Drawing.Point(6, 207);
            this.InputSize_label.Name = "InputSize_label";
            this.InputSize_label.Size = new System.Drawing.Size(77, 14);
            this.InputSize_label.TabIndex = 29;
            this.InputSize_label.Text = "Input size";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 14);
            this.label8.TabIndex = 28;
            this.label8.Text = "Layers";
            // 
            // Layer_listbox
            // 
            this.Layer_listbox.AllowDrop = true;
            this.Layer_listbox.FormattingEnabled = true;
            this.Layer_listbox.ItemHeight = 14;
            this.Layer_listbox.Items.AddRange(new object[] {
            "Linear (fixed)",
            "Linear (fixed)"});
            this.Layer_listbox.Location = new System.Drawing.Point(7, 63);
            this.Layer_listbox.Name = "Layer_listbox";
            this.Layer_listbox.Size = new System.Drawing.Size(187, 88);
            this.Layer_listbox.TabIndex = 1;
            this.Layer_listbox.SelectedIndexChanged += new System.EventHandler(this.Layer_listbox_SelectedIndexChanged);
            this.Layer_listbox.DragDrop += new System.Windows.Forms.DragEventHandler(this.Layer_listbox_DragDrop);
            this.Layer_listbox.DragEnter += new System.Windows.Forms.DragEventHandler(this.Layer_listbox_DragEnter);
            this.Layer_listbox.DragOver += new System.Windows.Forms.DragEventHandler(this.Layer_listbox_DragOver);
            this.Layer_listbox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Layer_listbox_MouseDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(23, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(149, 25);
            this.label7.TabIndex = 0;
            this.label7.Text = "Model settings";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 532);
            this.Controls.Add(this.ModelPanel);
            this.Controls.Add(this.AI_Settings_Panel);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.GreenPlayerBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RedPlayerBox);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.AI_Settings_Panel.ResumeLayout(false);
            this.AI_Settings_Panel.PerformLayout();
            this.ModelPanel.ResumeLayout(false);
            this.ModelPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label label1;
        private ComboBox RedPlayerBox;
        private Label label3;
        private ComboBox GreenPlayerBox;
        private Panel panel1;
        private Label label2;
        private TextBox textBox1;
        private SaveFileDialog saveFileDialog1;
        private OpenFileDialog openFileDialog1;
        private Panel panel2;
        private TextBox IterationBox;
        private Label label4;
        private Button button4;
        private Button button5;
        private Button button6;
        private Panel AI_Settings_Panel;
        private Label label6;
        private ListBox AI_List;
        private Label label5;
        private Button DeleteButton;
        private Button ImportButton;
        private Button NewButton;
        private CheckBox AutosaveCheckbox;
        private Label SaveLabel;
        private TextBox SavePath_textbox;
        private Label LR_Label;
        private TextBox LR_textbox;
        private Label Depth_Label;
        private TextBox Depth_textbox;
        private Button ApplyButton;
        private Button CopyButton;
        private Button TrainButton;
        private Button CloseButton;
        private Label NameLabel;
        private TextBox Name_textbox;
        private Panel ModelPanel;
        private Label label7;
        private Label label8;
        private ListBox Layer_listbox;
        private Label InputSize_label;
        private Label OutputSize_label;
        private TextBox InputSize_textbox;
        private TextBox OutputSize_textbox;
        private Label TransformationType_label;
        private ComboBox TransformationType_combobox;
        private Label Stride_label;
        private TextBox Stride_textbox;
        private Label label13;
        private TextBox textBox5;
        private Label label14;
        private TextBox textBox6;
        private Label Convolution_label;
        private Button Create_button;
        private Button button2;
        private Button Close2_button;
        private Button Delete_button;
        private TextBox LayerIndex_textbox;
        private Label LayerIndex_label;
        private Button Apply_button;
        private ToolTip toolTip1;
    }
}