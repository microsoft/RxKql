namespace RxWinforms
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.noButtonTextBox = new System.Windows.Forms.TextBox();
            this.rightButtonTextBox = new System.Windows.Forms.TextBox();
            this.leftButtonTextBox = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleAllEventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllDataPointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetCountersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.noButtonTextBox);
            this.panel1.Controls.Add(this.rightButtonTextBox);
            this.panel1.Controls.Add(this.leftButtonTextBox);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(693, 438);
            this.panel1.TabIndex = 0;
            // 
            // noButtonTextBox
            // 
            this.noButtonTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.noButtonTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.noButtonTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.noButtonTextBox.ForeColor = System.Drawing.Color.Gainsboro;
            this.noButtonTextBox.Location = new System.Drawing.Point(626, 26);
            this.noButtonTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.noButtonTextBox.Name = "noButtonTextBox";
            this.noButtonTextBox.Size = new System.Drawing.Size(67, 13);
            this.noButtonTextBox.TabIndex = 2;
            this.noButtonTextBox.TabStop = false;
            this.noButtonTextBox.Text = "0";
            this.noButtonTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // rightButtonTextBox
            // 
            this.rightButtonTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rightButtonTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rightButtonTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rightButtonTextBox.ForeColor = System.Drawing.Color.Gold;
            this.rightButtonTextBox.Location = new System.Drawing.Point(626, 60);
            this.rightButtonTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.rightButtonTextBox.Name = "rightButtonTextBox";
            this.rightButtonTextBox.Size = new System.Drawing.Size(67, 13);
            this.rightButtonTextBox.TabIndex = 1;
            this.rightButtonTextBox.TabStop = false;
            this.rightButtonTextBox.Text = "0";
            this.rightButtonTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // leftButtonTextBox
            // 
            this.leftButtonTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.leftButtonTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.leftButtonTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.leftButtonTextBox.ForeColor = System.Drawing.Color.Goldenrod;
            this.leftButtonTextBox.Location = new System.Drawing.Point(626, 43);
            this.leftButtonTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.leftButtonTextBox.Name = "leftButtonTextBox";
            this.leftButtonTextBox.Size = new System.Drawing.Size(67, 13);
            this.leftButtonTextBox.TabIndex = 0;
            this.leftButtonTextBox.TabStop = false;
            this.leftButtonTextBox.Text = "0";
            this.leftButtonTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.clearAllDataPointsToolStripMenuItem,
            this.resetCountersToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(693, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleAllEventsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "&Settings";
            // 
            // toggleAllEventsToolStripMenuItem
            // 
            this.toggleAllEventsToolStripMenuItem.CheckOnClick = true;
            this.toggleAllEventsToolStripMenuItem.Name = "toggleAllEventsToolStripMenuItem";
            this.toggleAllEventsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.toggleAllEventsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.toggleAllEventsToolStripMenuItem.Text = "&Display All Events";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // clearAllDataPointsToolStripMenuItem
            // 
            this.clearAllDataPointsToolStripMenuItem.Name = "clearAllDataPointsToolStripMenuItem";
            this.clearAllDataPointsToolStripMenuItem.Size = new System.Drawing.Size(99, 20);
            this.clearAllDataPointsToolStripMenuItem.Text = "&Clear All Points";
            this.clearAllDataPointsToolStripMenuItem.Click += new System.EventHandler(this.clearAllDataPointsToolStripMenuItem_Click);
            // 
            // resetCountersToolStripMenuItem
            // 
            this.resetCountersToolStripMenuItem.Name = "resetCountersToolStripMenuItem";
            this.resetCountersToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.resetCountersToolStripMenuItem.Text = "&Reset Counters";
            this.resetCountersToolStripMenuItem.Click += new System.EventHandler(this.resetCountersToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(693, 438);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox leftButtonTextBox;
        private System.Windows.Forms.TextBox rightButtonTextBox;
        private System.Windows.Forms.TextBox noButtonTextBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleAllEventsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllDataPointsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetCountersToolStripMenuItem;
    }
}

