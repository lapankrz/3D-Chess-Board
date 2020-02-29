namespace GK_Lab_4_v2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FOVLabel = new System.Windows.Forms.Label();
            this.FOVTrackBar = new System.Windows.Forms.TrackBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.phongShading = new System.Windows.Forms.RadioButton();
            this.gouraudShading = new System.Windows.Forms.RadioButton();
            this.flatShading = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lightMoveButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.movingRadioButton = new System.Windows.Forms.RadioButton();
            this.followingRadioButton = new System.Windows.Forms.RadioButton();
            this.stillRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lightTimer = new System.Windows.Forms.Timer(this.components);
            this.rotationTimer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FOVTrackBar)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1382, 753);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 93);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1376, 657);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            this.pictureBox1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_Scroll);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Controls.Add(this.groupBox3);
            this.flowLayoutPanel1.Controls.Add(this.groupBox4);
            this.flowLayoutPanel1.Controls.Add(this.groupBox5);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1376, 84);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.FOVLabel);
            this.groupBox1.Controls.Add(this.FOVTrackBar);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 77);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FOV";
            // 
            // FOVLabel
            // 
            this.FOVLabel.AutoSize = true;
            this.FOVLabel.Location = new System.Drawing.Point(161, 32);
            this.FOVLabel.Name = "FOVLabel";
            this.FOVLabel.Size = new System.Drawing.Size(36, 20);
            this.FOVLabel.TabIndex = 1;
            this.FOVLabel.Text = "100";
            // 
            // FOVTrackBar
            // 
            this.FOVTrackBar.Location = new System.Drawing.Point(6, 21);
            this.FOVTrackBar.Maximum = 180;
            this.FOVTrackBar.Minimum = 10;
            this.FOVTrackBar.Name = "FOVTrackBar";
            this.FOVTrackBar.Size = new System.Drawing.Size(149, 56);
            this.FOVTrackBar.TabIndex = 0;
            this.FOVTrackBar.Value = 100;
            this.FOVTrackBar.ValueChanged += new System.EventHandler(this.FOVTrackBar_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.phongShading);
            this.groupBox2.Controls.Add(this.gouraudShading);
            this.groupBox2.Controls.Add(this.flatShading);
            this.groupBox2.Location = new System.Drawing.Point(209, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(188, 77);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rodzaj cieniowania";
            // 
            // phongShading
            // 
            this.phongShading.AutoSize = true;
            this.phongShading.Location = new System.Drawing.Point(102, 21);
            this.phongShading.Name = "phongShading";
            this.phongShading.Size = new System.Drawing.Size(86, 24);
            this.phongShading.TabIndex = 2;
            this.phongShading.Text = "Phonga";
            this.phongShading.UseVisualStyleBackColor = true;
            // 
            // gouraudShading
            // 
            this.gouraudShading.AutoSize = true;
            this.gouraudShading.Location = new System.Drawing.Point(7, 48);
            this.gouraudShading.Name = "gouraudShading";
            this.gouraudShading.Size = new System.Drawing.Size(103, 24);
            this.gouraudShading.TabIndex = 1;
            this.gouraudShading.Text = "Gourauda";
            this.gouraudShading.UseVisualStyleBackColor = true;
            // 
            // flatShading
            // 
            this.flatShading.AutoSize = true;
            this.flatShading.Checked = true;
            this.flatShading.Location = new System.Drawing.Point(7, 21);
            this.flatShading.Name = "flatShading";
            this.flatShading.Size = new System.Drawing.Size(66, 24);
            this.flatShading.TabIndex = 0;
            this.flatShading.TabStop = true;
            this.flatShading.Text = "stałe";
            this.flatShading.UseVisualStyleBackColor = true;
            this.flatShading.CheckedChanged += new System.EventHandler(this.flatShading_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lightMoveButton);
            this.groupBox3.Location = new System.Drawing.Point(403, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(156, 77);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ruch źródła światła";
            // 
            // lightMoveButton
            // 
            this.lightMoveButton.Location = new System.Drawing.Point(36, 26);
            this.lightMoveButton.Name = "lightMoveButton";
            this.lightMoveButton.Size = new System.Drawing.Size(83, 37);
            this.lightMoveButton.TabIndex = 0;
            this.lightMoveButton.Text = "Start";
            this.lightMoveButton.UseVisualStyleBackColor = true;
            this.lightMoveButton.Click += new System.EventHandler(this.lightMoveButton_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.movingRadioButton);
            this.groupBox4.Controls.Add(this.followingRadioButton);
            this.groupBox4.Controls.Add(this.stillRadioButton);
            this.groupBox4.Location = new System.Drawing.Point(565, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 77);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Tryb kamery";
            // 
            // movingRadioButton
            // 
            this.movingRadioButton.AutoSize = true;
            this.movingRadioButton.Location = new System.Drawing.Point(110, 22);
            this.movingRadioButton.Name = "movingRadioButton";
            this.movingRadioButton.Size = new System.Drawing.Size(95, 24);
            this.movingRadioButton.TabIndex = 2;
            this.movingRadioButton.TabStop = true;
            this.movingRadioButton.Text = "ruchoma";
            this.movingRadioButton.UseVisualStyleBackColor = true;
            // 
            // followingRadioButton
            // 
            this.followingRadioButton.AutoSize = true;
            this.followingRadioButton.Location = new System.Drawing.Point(6, 50);
            this.followingRadioButton.Name = "followingRadioButton";
            this.followingRadioButton.Size = new System.Drawing.Size(97, 24);
            this.followingRadioButton.TabIndex = 1;
            this.followingRadioButton.TabStop = true;
            this.followingRadioButton.Text = "śledząca";
            this.followingRadioButton.UseVisualStyleBackColor = true;
            // 
            // stillRadioButton
            // 
            this.stillRadioButton.AutoSize = true;
            this.stillRadioButton.Checked = true;
            this.stillRadioButton.Location = new System.Drawing.Point(6, 22);
            this.stillRadioButton.Name = "stillRadioButton";
            this.stillRadioButton.Size = new System.Drawing.Size(117, 24);
            this.stillRadioButton.TabIndex = 0;
            this.stillRadioButton.TabStop = true;
            this.stillRadioButton.Text = "nieruchoma";
            this.stillRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button1);
            this.groupBox5.Controls.Add(this.numericUpDown);
            this.groupBox5.Location = new System.Drawing.Point(771, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 77);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Stopień rekurencji kuli";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(95, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 31);
            this.button1.TabIndex = 1;
            this.button1.Text = "Aktualizuj";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // numericUpDown
            // 
            this.numericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDown.Location = new System.Drawing.Point(17, 32);
            this.numericUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.Size = new System.Drawing.Size(72, 28);
            this.numericUpDown.TabIndex = 0;
            this.numericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // lightTimer
            // 
            this.lightTimer.Interval = 10;
            this.lightTimer.Tick += new System.EventHandler(this.lightTimer_Tick);
            // 
            // rotationTimer
            // 
            this.rotationTimer.Interval = 20;
            this.rotationTimer.Tick += new System.EventHandler(this.rotationTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1382, 753);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chess 3D";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FOVTrackBar)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label FOVLabel;
        private System.Windows.Forms.TrackBar FOVTrackBar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton phongShading;
        private System.Windows.Forms.RadioButton gouraudShading;
        private System.Windows.Forms.RadioButton flatShading;
        private System.Windows.Forms.Timer lightTimer;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button lightMoveButton;
        private System.Windows.Forms.Timer rotationTimer;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton movingRadioButton;
        private System.Windows.Forms.RadioButton followingRadioButton;
        private System.Windows.Forms.RadioButton stillRadioButton;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numericUpDown;
    }
}

