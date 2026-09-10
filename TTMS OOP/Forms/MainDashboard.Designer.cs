namespace TTMS_OOP.Forms
{
    partial class MainDashboard
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.sidebar = new System.Windows.Forms.Panel();
            this.contentArea = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // sidebar
            this.sidebar.Width = 230;
            this.sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebar.Name = "sidebar";
            // contentArea
            this.contentArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentArea.Name = "contentArea";
            // MainDashboard
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.contentArea);
            this.Controls.Add(this.sidebar);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "MainDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TTMS \u2014 Academic Schedule Manager";
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel sidebar;
        private System.Windows.Forms.Panel contentArea;
    }
}
