namespace RubikGUI
{
    partial class CubeView
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.rotateRightButton = new System.Windows.Forms.Button();
            this.rotateRightInvertedButton = new System.Windows.Forms.Button();
            this.rotateLeftButton = new System.Windows.Forms.Button();
            this.rotateLeftInvertedButton = new System.Windows.Forms.Button();
            this.rotateDownButton = new System.Windows.Forms.Button();
            this.rotateDownInvertedButton = new System.Windows.Forms.Button();
            this.rotateUpButton = new System.Windows.Forms.Button();
            this.rotateUpInvertedButton = new System.Windows.Forms.Button();
            this.rotateBackButton = new System.Windows.Forms.Button();
            this.rotateBackInvertedButton = new System.Windows.Forms.Button();
            this.rotateFrontButton = new System.Windows.Forms.Button();
            this.rotateFrontInvertedButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(193, 89);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(595, 237);
            this.textBox1.TabIndex = 0;
            // 
            // rotateRightButton
            // 
            this.rotateRightButton.Location = new System.Drawing.Point(794, 177);
            this.rotateRightButton.Name = "rotateRightButton";
            this.rotateRightButton.Size = new System.Drawing.Size(75, 23);
            this.rotateRightButton.TabIndex = 1;
            this.rotateRightButton.Text = "R";
            this.rotateRightButton.UseVisualStyleBackColor = true;
            this.rotateRightButton.Click += new System.EventHandler(this.rotateRightButton_Click);
            // 
            // rotateRightInvertedButton
            // 
            this.rotateRightInvertedButton.Location = new System.Drawing.Point(794, 206);
            this.rotateRightInvertedButton.Name = "rotateRightInvertedButton";
            this.rotateRightInvertedButton.Size = new System.Drawing.Size(75, 23);
            this.rotateRightInvertedButton.TabIndex = 1;
            this.rotateRightInvertedButton.Text = "Ri";
            this.rotateRightInvertedButton.UseVisualStyleBackColor = true;
            this.rotateRightInvertedButton.Click += new System.EventHandler(this.rotateRightInvertedButton_Click);
            // 
            // rotateLeftButton
            // 
            this.rotateLeftButton.Location = new System.Drawing.Point(112, 177);
            this.rotateLeftButton.Name = "rotateLeftButton";
            this.rotateLeftButton.Size = new System.Drawing.Size(75, 23);
            this.rotateLeftButton.TabIndex = 1;
            this.rotateLeftButton.Text = "L";
            this.rotateLeftButton.UseVisualStyleBackColor = true;
            this.rotateLeftButton.Click += new System.EventHandler(this.rotateLeftButton_Click);
            // 
            // rotateLeftInvertedButton
            // 
            this.rotateLeftInvertedButton.Location = new System.Drawing.Point(112, 206);
            this.rotateLeftInvertedButton.Name = "rotateLeftInvertedButton";
            this.rotateLeftInvertedButton.Size = new System.Drawing.Size(75, 23);
            this.rotateLeftInvertedButton.TabIndex = 1;
            this.rotateLeftInvertedButton.Text = "Li";
            this.rotateLeftInvertedButton.UseVisualStyleBackColor = true;
            this.rotateLeftInvertedButton.Click += new System.EventHandler(this.rotateLeftInvertedButton_Click);
            // 
            // rotateDownButton
            // 
            this.rotateDownButton.Location = new System.Drawing.Point(455, 339);
            this.rotateDownButton.Name = "rotateDownButton";
            this.rotateDownButton.Size = new System.Drawing.Size(75, 23);
            this.rotateDownButton.TabIndex = 1;
            this.rotateDownButton.Text = "D";
            this.rotateDownButton.UseVisualStyleBackColor = true;
            this.rotateDownButton.Click += new System.EventHandler(this.rotateDownButton_Click);
            // 
            // rotateDownInvertedButton
            // 
            this.rotateDownInvertedButton.Location = new System.Drawing.Point(455, 368);
            this.rotateDownInvertedButton.Name = "rotateDownInvertedButton";
            this.rotateDownInvertedButton.Size = new System.Drawing.Size(75, 23);
            this.rotateDownInvertedButton.TabIndex = 1;
            this.rotateDownInvertedButton.Text = "Di";
            this.rotateDownInvertedButton.UseVisualStyleBackColor = true;
            this.rotateDownInvertedButton.Click += new System.EventHandler(this.rotateDownInvertedButton_Click);
            // 
            // rotateUpButton
            // 
            this.rotateUpButton.Location = new System.Drawing.Point(455, 31);
            this.rotateUpButton.Name = "rotateUpButton";
            this.rotateUpButton.Size = new System.Drawing.Size(75, 23);
            this.rotateUpButton.TabIndex = 1;
            this.rotateUpButton.Text = "U";
            this.rotateUpButton.UseVisualStyleBackColor = true;
            this.rotateUpButton.Click += new System.EventHandler(this.rotateUpButton_Click);
            // 
            // rotateUpInvertedButton
            // 
            this.rotateUpInvertedButton.Location = new System.Drawing.Point(455, 60);
            this.rotateUpInvertedButton.Name = "rotateUpInvertedButton";
            this.rotateUpInvertedButton.Size = new System.Drawing.Size(75, 23);
            this.rotateUpInvertedButton.TabIndex = 1;
            this.rotateUpInvertedButton.Text = "Ui";
            this.rotateUpInvertedButton.UseVisualStyleBackColor = true;
            this.rotateUpInvertedButton.Click += new System.EventHandler(this.rotateUpInvertedButton_Click);
            // 
            // rotateBackButton
            // 
            this.rotateBackButton.Location = new System.Drawing.Point(816, 339);
            this.rotateBackButton.Name = "rotateBackButton";
            this.rotateBackButton.Size = new System.Drawing.Size(75, 23);
            this.rotateBackButton.TabIndex = 1;
            this.rotateBackButton.Text = "B";
            this.rotateBackButton.UseVisualStyleBackColor = true;
            this.rotateBackButton.Click += new System.EventHandler(this.rotateBackButton_Click);
            // 
            // rotateBackInvertedButton
            // 
            this.rotateBackInvertedButton.Location = new System.Drawing.Point(816, 368);
            this.rotateBackInvertedButton.Name = "rotateBackInvertedButton";
            this.rotateBackInvertedButton.Size = new System.Drawing.Size(75, 23);
            this.rotateBackInvertedButton.TabIndex = 1;
            this.rotateBackInvertedButton.Text = "Bi";
            this.rotateBackInvertedButton.UseVisualStyleBackColor = true;
            this.rotateBackInvertedButton.Click += new System.EventHandler(this.rotateBackInvertedButton_Click);
            // 
            // rotateFrontButton
            // 
            this.rotateFrontButton.Location = new System.Drawing.Point(112, 339);
            this.rotateFrontButton.Name = "rotateFrontButton";
            this.rotateFrontButton.Size = new System.Drawing.Size(75, 23);
            this.rotateFrontButton.TabIndex = 1;
            this.rotateFrontButton.Text = "F";
            this.rotateFrontButton.UseVisualStyleBackColor = true;
            this.rotateFrontButton.Click += new System.EventHandler(this.rotateFrontButton_Click);
            // 
            // rotateFrontInvertedButton
            // 
            this.rotateFrontInvertedButton.Location = new System.Drawing.Point(112, 368);
            this.rotateFrontInvertedButton.Name = "rotateFrontInvertedButton";
            this.rotateFrontInvertedButton.Size = new System.Drawing.Size(75, 23);
            this.rotateFrontInvertedButton.TabIndex = 1;
            this.rotateFrontInvertedButton.Text = "Fi";
            this.rotateFrontInvertedButton.UseVisualStyleBackColor = true;
            this.rotateFrontInvertedButton.Click += new System.EventHandler(this.rotateFrontInvertedButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 403);
            this.Controls.Add(this.rotateFrontInvertedButton);
            this.Controls.Add(this.rotateBackInvertedButton);
            this.Controls.Add(this.rotateFrontButton);
            this.Controls.Add(this.rotateBackButton);
            this.Controls.Add(this.rotateUpInvertedButton);
            this.Controls.Add(this.rotateUpButton);
            this.Controls.Add(this.rotateDownInvertedButton);
            this.Controls.Add(this.rotateDownButton);
            this.Controls.Add(this.rotateLeftInvertedButton);
            this.Controls.Add(this.rotateLeftButton);
            this.Controls.Add(this.rotateRightInvertedButton);
            this.Controls.Add(this.rotateRightButton);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button rotateRightButton;
        private System.Windows.Forms.Button rotateRightInvertedButton;
        private System.Windows.Forms.Button rotateLeftButton;
        private System.Windows.Forms.Button rotateLeftInvertedButton;
        private System.Windows.Forms.Button rotateDownButton;
        private System.Windows.Forms.Button rotateDownInvertedButton;
        private System.Windows.Forms.Button rotateUpButton;
        private System.Windows.Forms.Button rotateUpInvertedButton;
        private System.Windows.Forms.Button rotateBackButton;
        private System.Windows.Forms.Button rotateBackInvertedButton;
        private System.Windows.Forms.Button rotateFrontButton;
        private System.Windows.Forms.Button rotateFrontInvertedButton;
    }
}

