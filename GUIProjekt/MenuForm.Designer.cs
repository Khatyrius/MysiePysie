namespace GUIProjekt
{
    partial class MenuForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.uZProfesorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uZStudentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uZSupportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uZProfesorsToolStripMenuItem,
            this.uZStudentsToolStripMenuItem,
            this.uZSupportToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // uZProfesorsToolStripMenuItem
            // 
            this.uZProfesorsToolStripMenuItem.Name = "uZProfesorsToolStripMenuItem";
            this.uZProfesorsToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.uZProfesorsToolStripMenuItem.Text = "UZ profesors";
            this.uZProfesorsToolStripMenuItem.Click += new System.EventHandler(this.uZProfesorsToolStripMenuItem_Click);
            // 
            // uZStudentsToolStripMenuItem
            // 
            this.uZStudentsToolStripMenuItem.Name = "uZStudentsToolStripMenuItem";
            this.uZStudentsToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.uZStudentsToolStripMenuItem.Text = "UZ Students";
            this.uZStudentsToolStripMenuItem.Click += new System.EventHandler(this.uZStudentsToolStripMenuItem_Click);
            // 
            // uZSupportToolStripMenuItem
            // 
            this.uZSupportToolStripMenuItem.Name = "uZSupportToolStripMenuItem";
            this.uZSupportToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.uZSupportToolStripMenuItem.Text = "UZ support";
            this.uZSupportToolStripMenuItem.Click += new System.EventHandler(this.uZSupportToolStripMenuItem_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MenuForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem uZProfesorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uZStudentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uZSupportToolStripMenuItem;
    }
}