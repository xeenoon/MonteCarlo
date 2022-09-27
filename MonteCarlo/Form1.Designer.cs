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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.RedPlayerBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.GreenPlayerBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(713, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Train";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(431, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Red player: ";
            // 
            // RedPlayerBox
            // 
            this.RedPlayerBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RedPlayerBox.FormattingEnabled = true;
            this.RedPlayerBox.Items.AddRange(new object[] {
            "Human",
            "ML",
            "MCTS",
            "Min-Max"});
            this.RedPlayerBox.Location = new System.Drawing.Point(563, 16);
            this.RedPlayerBox.Name = "RedPlayerBox";
            this.RedPlayerBox.Size = new System.Drawing.Size(121, 23);
            this.RedPlayerBox.TabIndex = 3;
            this.RedPlayerBox.SelectedIndexChanged += new System.EventHandler(this.PlayerChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(411, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 26);
            this.label3.TabIndex = 4;
            this.label3.Text = "Green player: ";
            // 
            // GreenPlayerBox
            // 
            this.GreenPlayerBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GreenPlayerBox.FormattingEnabled = true;
            this.GreenPlayerBox.Items.AddRange(new object[] {
            "Human",
            "ML",
            "MCTS",
            "Min-Max"});
            this.GreenPlayerBox.Location = new System.Drawing.Point(563, 60);
            this.GreenPlayerBox.Name = "GreenPlayerBox";
            this.GreenPlayerBox.Size = new System.Drawing.Size(121, 23);
            this.GreenPlayerBox.TabIndex = 5;
            this.GreenPlayerBox.SelectedIndexChanged += new System.EventHandler(this.PlayerChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.GreenPlayerBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RedPlayerBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private Label label1;
        private ComboBox RedPlayerBox;
        private Label label3;
        private ComboBox GreenPlayerBox;
    }
}