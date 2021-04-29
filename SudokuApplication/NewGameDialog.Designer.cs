namespace SudokuApplication
{
    partial class NewGameDialog
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
            this.radioHard = new System.Windows.Forms.RadioButton();
            this.radioMedium = new System.Windows.Forms.RadioButton();
            this.radioEasy = new System.Windows.Forms.RadioButton();
            this.radioExpert = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label16x16 = new System.Windows.Forms.Label();
            this.label12x12 = new System.Windows.Forms.Label();
            this.picture16x16 = new System.Windows.Forms.PictureBox();
            this.picture12x12 = new System.Windows.Forms.PictureBox();
            this.label9x9 = new System.Windows.Forms.Label();
            this.label6x6 = new System.Windows.Forms.Label();
            this.label4x4 = new System.Windows.Forms.Label();
            this.picture9x9 = new System.Windows.Forms.PictureBox();
            this.picture6x6 = new System.Windows.Forms.PictureBox();
            this.picture4x4 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.generateBoard = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture16x16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture12x12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture9x9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture6x6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture4x4)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioHard
            // 
            this.radioHard.AutoSize = true;
            this.radioHard.Location = new System.Drawing.Point(221, 23);
            this.radioHard.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioHard.Name = "radioHard";
            this.radioHard.Size = new System.Drawing.Size(60, 21);
            this.radioHard.TabIndex = 97;
            this.radioHard.TabStop = true;
            this.radioHard.Text = "Hard";
            this.radioHard.UseVisualStyleBackColor = true;
            this.radioHard.CheckedChanged += new System.EventHandler(this.boardDifficulty_CheckedChanged);
            // 
            // radioMedium
            // 
            this.radioMedium.AutoSize = true;
            this.radioMedium.Checked = true;
            this.radioMedium.Location = new System.Drawing.Point(135, 23);
            this.radioMedium.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioMedium.Name = "radioMedium";
            this.radioMedium.Size = new System.Drawing.Size(78, 21);
            this.radioMedium.TabIndex = 96;
            this.radioMedium.TabStop = true;
            this.radioMedium.Text = "Medium";
            this.radioMedium.UseVisualStyleBackColor = true;
            this.radioMedium.CheckedChanged += new System.EventHandler(this.boardDifficulty_CheckedChanged);
            // 
            // radioEasy
            // 
            this.radioEasy.AutoSize = true;
            this.radioEasy.Location = new System.Drawing.Point(59, 23);
            this.radioEasy.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioEasy.Name = "radioEasy";
            this.radioEasy.Size = new System.Drawing.Size(60, 21);
            this.radioEasy.TabIndex = 95;
            this.radioEasy.Text = "Easy";
            this.radioEasy.UseVisualStyleBackColor = true;
            this.radioEasy.CheckedChanged += new System.EventHandler(this.boardDifficulty_CheckedChanged);
            // 
            // radioExpert
            // 
            this.radioExpert.AutoSize = true;
            this.radioExpert.Location = new System.Drawing.Point(289, 23);
            this.radioExpert.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioExpert.Name = "radioExpert";
            this.radioExpert.Size = new System.Drawing.Size(69, 21);
            this.radioExpert.TabIndex = 100;
            this.radioExpert.Text = "Expert";
            this.radioExpert.UseVisualStyleBackColor = true;
            this.radioExpert.CheckedChanged += new System.EventHandler(this.boardDifficulty_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label16x16);
            this.groupBox1.Controls.Add(this.label12x12);
            this.groupBox1.Controls.Add(this.picture16x16);
            this.groupBox1.Controls.Add(this.picture12x12);
            this.groupBox1.Controls.Add(this.label9x9);
            this.groupBox1.Controls.Add(this.label6x6);
            this.groupBox1.Controls.Add(this.label4x4);
            this.groupBox1.Controls.Add(this.picture9x9);
            this.groupBox1.Controls.Add(this.picture6x6);
            this.groupBox1.Controls.Add(this.picture4x4);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(440, 431);
            this.groupBox1.TabIndex = 101;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Board Size:";
            // 
            // label16x16
            // 
            this.label16x16.AutoSize = true;
            this.label16x16.Location = new System.Drawing.Point(287, 399);
            this.label16x16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16x16.Name = "label16x16";
            this.label16x16.Size = new System.Drawing.Size(46, 17);
            this.label16x16.TabIndex = 9;
            this.label16x16.Text = "16x16";
            // 
            // label12x12
            // 
            this.label12x12.AutoSize = true;
            this.label12x12.Location = new System.Drawing.Point(79, 399);
            this.label12x12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12x12.Name = "label12x12";
            this.label12x12.Size = new System.Drawing.Size(46, 17);
            this.label12x12.TabIndex = 8;
            this.label12x12.Text = "12x12";
            // 
            // picture16x16
            // 
            this.picture16x16.Location = new System.Drawing.Point(196, 183);
            this.picture16x16.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picture16x16.Name = "picture16x16";
            this.picture16x16.Size = new System.Drawing.Size(229, 212);
            this.picture16x16.TabIndex = 7;
            this.picture16x16.TabStop = false;
            this.picture16x16.Click += new System.EventHandler(this.boardPicture_Click);
            // 
            // picture12x12
            // 
            this.picture12x12.Location = new System.Drawing.Point(15, 233);
            this.picture12x12.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picture12x12.Name = "picture12x12";
            this.picture12x12.Size = new System.Drawing.Size(176, 162);
            this.picture12x12.TabIndex = 6;
            this.picture12x12.TabStop = false;
            this.picture12x12.Click += new System.EventHandler(this.boardPicture_Click);
            // 
            // label9x9
            // 
            this.label9x9.AutoSize = true;
            this.label9x9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9x9.Location = new System.Drawing.Point(341, 158);
            this.label9x9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9x9.Name = "label9x9";
            this.label9x9.Size = new System.Drawing.Size(33, 17);
            this.label9x9.TabIndex = 5;
            this.label9x9.Text = "9x9";
            // 
            // label6x6
            // 
            this.label6x6.AutoSize = true;
            this.label6x6.Location = new System.Drawing.Point(180, 158);
            this.label6x6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6x6.Name = "label6x6";
            this.label6x6.Size = new System.Drawing.Size(30, 17);
            this.label6x6.TabIndex = 4;
            this.label6x6.Text = "6x6";
            // 
            // label4x4
            // 
            this.label4x4.AutoSize = true;
            this.label4x4.Location = new System.Drawing.Point(33, 158);
            this.label4x4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4x4.Name = "label4x4";
            this.label4x4.Size = new System.Drawing.Size(30, 17);
            this.label4x4.TabIndex = 3;
            this.label4x4.Text = "4x4";
            // 
            // picture9x9
            // 
            this.picture9x9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picture9x9.Location = new System.Drawing.Point(289, 28);
            this.picture9x9.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picture9x9.Name = "picture9x9";
            this.picture9x9.Size = new System.Drawing.Size(135, 125);
            this.picture9x9.TabIndex = 2;
            this.picture9x9.TabStop = false;
            this.picture9x9.Click += new System.EventHandler(this.boardPicture_Click);
            // 
            // picture6x6
            // 
            this.picture6x6.Location = new System.Drawing.Point(147, 65);
            this.picture6x6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picture6x6.Name = "picture6x6";
            this.picture6x6.Size = new System.Drawing.Size(96, 89);
            this.picture6x6.TabIndex = 1;
            this.picture6x6.TabStop = false;
            this.picture6x6.Click += new System.EventHandler(this.boardPicture_Click);
            // 
            // picture4x4
            // 
            this.picture4x4.Location = new System.Drawing.Point(15, 90);
            this.picture4x4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picture4x4.Name = "picture4x4";
            this.picture4x4.Size = new System.Drawing.Size(69, 64);
            this.picture4x4.TabIndex = 0;
            this.picture4x4.TabStop = false;
            this.picture4x4.Click += new System.EventHandler(this.boardPicture_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.generateBoard);
            this.groupBox2.Controls.Add(this.radioEasy);
            this.groupBox2.Controls.Add(this.radioMedium);
            this.groupBox2.Controls.Add(this.radioHard);
            this.groupBox2.Controls.Add(this.radioExpert);
            this.groupBox2.Location = new System.Drawing.Point(16, 457);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(440, 181);
            this.groupBox2.TabIndex = 102;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Difficulty:";
            // 
            // generateBoard
            // 
            this.generateBoard.Location = new System.Drawing.Point(106, 62);
            this.generateBoard.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.generateBoard.Name = "generateBoard";
            this.generateBoard.Size = new System.Drawing.Size(200, 28);
            this.generateBoard.TabIndex = 104;
            this.generateBoard.Text = "Generate board";
            this.generateBoard.UseVisualStyleBackColor = true;
            this.generateBoard.Click += new System.EventHandler(this.generateBoard_Click);
            // 
            // NewGameDialog
            // 
            this.AcceptButton = this.generateBoard;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 577);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "NewGameDialog";
            this.Text = "New Game";
            this.Load += new System.EventHandler(this.NewGameDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture16x16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture12x12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture9x9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture6x6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture4x4)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RadioButton radioHard;
        private System.Windows.Forms.RadioButton radioMedium;
        private System.Windows.Forms.RadioButton radioEasy;
        private System.Windows.Forms.RadioButton radioExpert;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button generateBoard;
        private System.Windows.Forms.PictureBox picture4x4;
        private System.Windows.Forms.PictureBox picture9x9;
        private System.Windows.Forms.PictureBox picture6x6;
        private System.Windows.Forms.Label label6x6;
        private System.Windows.Forms.Label label4x4;
        private System.Windows.Forms.Label label9x9;
        private System.Windows.Forms.PictureBox picture16x16;
        private System.Windows.Forms.PictureBox picture12x12;
        private System.Windows.Forms.Label label16x16;
        private System.Windows.Forms.Label label12x12;
    }
}