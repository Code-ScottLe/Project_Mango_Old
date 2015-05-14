namespace Mango_WinForm
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
            this.SourceUrl_Box = new System.Windows.Forms.TextBox();
            this.SourceUrl_Label = new System.Windows.Forms.Label();
            this.SourcesList_ComboBox = new System.Windows.Forms.ComboBox();
            this.SourcesList_Label = new System.Windows.Forms.Label();
            this.SaveTo_TextBox = new System.Windows.Forms.TextBox();
            this.SaveTo_Label = new System.Windows.Forms.Label();
            this.SaveTo_Button = new System.Windows.Forms.Button();
            this.DetailedProgress_Box = new System.Windows.Forms.TextBox();
            this.DetailedProgress_TextBox = new System.Windows.Forms.Label();
            this.Download_Button = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SourceUrl_Box
            // 
            this.SourceUrl_Box.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SourceUrl_Box.Location = new System.Drawing.Point(13, 56);
            this.SourceUrl_Box.Name = "SourceUrl_Box";
            this.SourceUrl_Box.Size = new System.Drawing.Size(693, 22);
            this.SourceUrl_Box.TabIndex = 0;
            // 
            // SourceUrl_Label
            // 
            this.SourceUrl_Label.AutoSize = true;
            this.SourceUrl_Label.Location = new System.Drawing.Point(12, 31);
            this.SourceUrl_Label.Name = "SourceUrl_Label";
            this.SourceUrl_Label.Size = new System.Drawing.Size(27, 13);
            this.SourceUrl_Label.TabIndex = 1;
            this.SourceUrl_Label.Text = "URL";
            // 
            // SourcesList_ComboBox
            // 
            this.SourcesList_ComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SourcesList_ComboBox.FormattingEnabled = true;
            this.SourcesList_ComboBox.Location = new System.Drawing.Point(784, 56);
            this.SourcesList_ComboBox.Name = "SourcesList_ComboBox";
            this.SourcesList_ComboBox.Size = new System.Drawing.Size(203, 21);
            this.SourcesList_ComboBox.TabIndex = 2;
            // 
            // SourcesList_Label
            // 
            this.SourcesList_Label.AutoSize = true;
            this.SourcesList_Label.Location = new System.Drawing.Point(780, 31);
            this.SourcesList_Label.Name = "SourcesList_Label";
            this.SourcesList_Label.Size = new System.Drawing.Size(42, 13);
            this.SourcesList_Label.TabIndex = 3;
            this.SourcesList_Label.Text = "Source";
            // 
            // SaveTo_TextBox
            // 
            this.SaveTo_TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveTo_TextBox.Location = new System.Drawing.Point(13, 146);
            this.SaveTo_TextBox.Name = "SaveTo_TextBox";
            this.SaveTo_TextBox.Size = new System.Drawing.Size(868, 22);
            this.SaveTo_TextBox.TabIndex = 4;
            // 
            // SaveTo_Label
            // 
            this.SaveTo_Label.AutoSize = true;
            this.SaveTo_Label.Location = new System.Drawing.Point(12, 113);
            this.SaveTo_Label.Name = "SaveTo_Label";
            this.SaveTo_Label.Size = new System.Drawing.Size(75, 13);
            this.SaveTo_Label.TabIndex = 5;
            this.SaveTo_Label.Text = "Save location";
            // 
            // SaveTo_Button
            // 
            this.SaveTo_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveTo_Button.Location = new System.Drawing.Point(897, 145);
            this.SaveTo_Button.Name = "SaveTo_Button";
            this.SaveTo_Button.Size = new System.Drawing.Size(90, 30);
            this.SaveTo_Button.TabIndex = 6;
            this.SaveTo_Button.Text = "...";
            this.SaveTo_Button.UseVisualStyleBackColor = true;
            this.SaveTo_Button.Click += new System.EventHandler(this.SaveTo_Button_Click);
            // 
            // DetailedProgress_Box
            // 
            this.DetailedProgress_Box.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DetailedProgress_Box.Location = new System.Drawing.Point(13, 231);
            this.DetailedProgress_Box.Multiline = true;
            this.DetailedProgress_Box.Name = "DetailedProgress_Box";
            this.DetailedProgress_Box.ReadOnly = true;
            this.DetailedProgress_Box.Size = new System.Drawing.Size(977, 336);
            this.DetailedProgress_Box.TabIndex = 7;
            // 
            // DetailedProgress_TextBox
            // 
            this.DetailedProgress_TextBox.AutoSize = true;
            this.DetailedProgress_TextBox.Location = new System.Drawing.Point(12, 197);
            this.DetailedProgress_TextBox.Name = "DetailedProgress_TextBox";
            this.DetailedProgress_TextBox.Size = new System.Drawing.Size(97, 13);
            this.DetailedProgress_TextBox.TabIndex = 8;
            this.DetailedProgress_TextBox.Text = "Detailed Progress";
            // 
            // Download_Button
            // 
            this.Download_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Download_Button.Location = new System.Drawing.Point(13, 596);
            this.Download_Button.Name = "Download_Button";
            this.Download_Button.Size = new System.Drawing.Size(403, 36);
            this.Download_Button.TabIndex = 9;
            this.Download_Button.Text = "Download";
            this.Download_Button.UseVisualStyleBackColor = true;
            this.Download_Button.Click += new System.EventHandler(this.Download_Button_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(624, 596);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(366, 36);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 10;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1002, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.aboutToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 644);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.Download_Button);
            this.Controls.Add(this.DetailedProgress_TextBox);
            this.Controls.Add(this.DetailedProgress_Box);
            this.Controls.Add(this.SaveTo_Button);
            this.Controls.Add(this.SaveTo_Label);
            this.Controls.Add(this.SaveTo_TextBox);
            this.Controls.Add(this.SourcesList_Label);
            this.Controls.Add(this.SourcesList_ComboBox);
            this.Controls.Add(this.SourceUrl_Label);
            this.Controls.Add(this.SourceUrl_Box);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Project Mango";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SourceUrl_Box;
        private System.Windows.Forms.Label SourceUrl_Label;
        private System.Windows.Forms.ComboBox SourcesList_ComboBox;
        private System.Windows.Forms.Label SourcesList_Label;
        private System.Windows.Forms.TextBox SaveTo_TextBox;
        private System.Windows.Forms.Label SaveTo_Label;
        private System.Windows.Forms.Button SaveTo_Button;
        private System.Windows.Forms.TextBox DetailedProgress_Box;
        private System.Windows.Forms.Label DetailedProgress_TextBox;
        private System.Windows.Forms.Button Download_Button;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
    }
}

