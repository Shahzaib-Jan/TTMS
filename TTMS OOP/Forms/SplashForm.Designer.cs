namespace TTMS_OOP.Forms
{
    partial class SplashForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components  = new System.ComponentModel.Container();
            this.animTimer   = new System.Windows.Forms.Timer(this.components);
            this.iconBox     = new System.Windows.Forms.Panel();
            this.lblTitle1   = new System.Windows.Forms.Label();
            this.lblTitle2   = new System.Windows.Forms.Label();
            this.lblSub      = new System.Windows.Forms.Label();
            this.lblVer      = new System.Windows.Forms.Label();
            this.lblStatus   = new System.Windows.Forms.Label();
            this.divider     = new System.Windows.Forms.Panel();
            this.lblManage   = new System.Windows.Forms.Label();
            this.pnlDot0     = new System.Windows.Forms.Panel();
            this.pnlDot1     = new System.Windows.Forms.Panel();
            this.pnlDot2     = new System.Windows.Forms.Panel();
            this.pnlDot3     = new System.Windows.Forms.Panel();
            this.pnlDot4     = new System.Windows.Forms.Panel();
            this.pnlDot5     = new System.Windows.Forms.Panel();
            this.lblFeat0    = new System.Windows.Forms.Label();
            this.lblFeat1    = new System.Windows.Forms.Label();
            this.lblFeat2    = new System.Windows.Forms.Label();
            this.lblFeat3    = new System.Windows.Forms.Label();
            this.lblFeat4    = new System.Windows.Forms.Label();
            this.lblFeat5    = new System.Windows.Forms.Label();
            this.btnStart    = new System.Windows.Forms.Button();
            this.lblArrow    = new System.Windows.Forms.Label();
            this.btnClose    = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // animTimer
            this.animTimer.Interval = 500;
            this.animTimer.Tick += new System.EventHandler(this.AnimTimer_Tick);
            // iconBox
            this.iconBox.BackColor = System.Drawing.Color.FromArgb(40, 255, 255, 255);
            this.iconBox.Location  = new System.Drawing.Point(55, 65);
            this.iconBox.Name      = "iconBox";
            this.iconBox.Size      = new System.Drawing.Size(56, 56);
            this.iconBox.Paint    += new System.Windows.Forms.PaintEventHandler(this.IconBox_Paint);
            // lblTitle1
            this.lblTitle1.AutoSize  = true;
            this.lblTitle1.Font      = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblTitle1.ForeColor = System.Drawing.Color.White;
            this.lblTitle1.Location  = new System.Drawing.Point(55, 140);
            this.lblTitle1.Name      = "lblTitle1";
            this.lblTitle1.Text      = "Academic Schedule";
            // lblTitle2
            this.lblTitle2.AutoSize  = true;
            this.lblTitle2.Font      = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblTitle2.ForeColor = System.Drawing.Color.FromArgb(180, 255, 255, 255);
            this.lblTitle2.Location  = new System.Drawing.Point(55, 184);
            this.lblTitle2.Name      = "lblTitle2";
            this.lblTitle2.Text      = "Manager";
            // lblSub
            this.lblSub.AutoSize  = true;
            this.lblSub.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSub.ForeColor = System.Drawing.Color.FromArgb(160, 255, 255, 255);
            this.lblSub.Location  = new System.Drawing.Point(55, 238);
            this.lblSub.Name      = "lblSub";
            this.lblSub.Text      = "BSCS  \u2014  UET Lahore (FSD Campus)";
            // lblVer
            this.lblVer.AutoSize  = true;
            this.lblVer.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblVer.ForeColor = System.Drawing.Color.FromArgb(110, 255, 255, 255);
            this.lblVer.Location  = new System.Drawing.Point(55, 264);
            this.lblVer.Name      = "lblVer";
            this.lblVer.Text      = "v1.0   Spring 2026  ·  By Shahzaib";
            // lblStatus
            this.lblStatus.AutoSize  = true;
            this.lblStatus.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(110, 255, 255, 255);
            this.lblStatus.Location  = new System.Drawing.Point(55, 360);
            this.lblStatus.Name      = "lblStatus";
            this.lblStatus.Text      = "Initializing...";
            // divider
            this.divider.BackColor = System.Drawing.Color.FromArgb(50, 255, 255, 255);
            this.divider.Location  = new System.Drawing.Point(400, 60);
            this.divider.Name      = "divider";
            this.divider.Size      = new System.Drawing.Size(1, 340);
            // lblManage
            this.lblManage.AutoSize  = true;
            this.lblManage.Font      = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblManage.ForeColor = System.Drawing.Color.FromArgb(200, 255, 255, 255);
            this.lblManage.Location  = new System.Drawing.Point(430, 75);
            this.lblManage.Name      = "lblManage";
            this.lblManage.Text      = "What you can do:";
            // pnlDot0
            this.pnlDot0.BackColor = System.Drawing.Color.FromArgb(150, 255, 255, 255);
            this.pnlDot0.Location  = new System.Drawing.Point(430, 123);
            this.pnlDot0.Name      = "pnlDot0";
            this.pnlDot0.Size      = new System.Drawing.Size(6, 6);
            // pnlDot1
            this.pnlDot1.BackColor = System.Drawing.Color.FromArgb(150, 255, 255, 255);
            this.pnlDot1.Location  = new System.Drawing.Point(430, 159);
            this.pnlDot1.Name      = "pnlDot1";
            this.pnlDot1.Size      = new System.Drawing.Size(6, 6);
            // pnlDot2
            this.pnlDot2.BackColor = System.Drawing.Color.FromArgb(150, 255, 255, 255);
            this.pnlDot2.Location  = new System.Drawing.Point(430, 195);
            this.pnlDot2.Name      = "pnlDot2";
            this.pnlDot2.Size      = new System.Drawing.Size(6, 6);
            // pnlDot3
            this.pnlDot3.BackColor = System.Drawing.Color.FromArgb(150, 255, 255, 255);
            this.pnlDot3.Location  = new System.Drawing.Point(430, 231);
            this.pnlDot3.Name      = "pnlDot3";
            this.pnlDot3.Size      = new System.Drawing.Size(6, 6);
            // pnlDot4
            this.pnlDot4.BackColor = System.Drawing.Color.FromArgb(150, 255, 255, 255);
            this.pnlDot4.Location  = new System.Drawing.Point(430, 267);
            this.pnlDot4.Name      = "pnlDot4";
            this.pnlDot4.Size      = new System.Drawing.Size(6, 6);
            // pnlDot5
            this.pnlDot5.BackColor = System.Drawing.Color.FromArgb(150, 255, 255, 255);
            this.pnlDot5.Location  = new System.Drawing.Point(430, 303);
            this.pnlDot5.Name      = "pnlDot5";
            this.pnlDot5.Size      = new System.Drawing.Size(6, 6);
            // lblFeat0
            this.lblFeat0.Font      = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblFeat0.ForeColor = System.Drawing.Color.FromArgb(170, 255, 255, 255);
            this.lblFeat0.Location  = new System.Drawing.Point(446, 116);
            this.lblFeat0.Name      = "lblFeat0";
            this.lblFeat0.Size      = new System.Drawing.Size(350, 24);
            this.lblFeat0.Text      = "Manage Teachers and Subjects";
            // lblFeat1
            this.lblFeat1.Font      = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblFeat1.ForeColor = System.Drawing.Color.FromArgb(170, 255, 255, 255);
            this.lblFeat1.Location  = new System.Drawing.Point(446, 152);
            this.lblFeat1.Name      = "lblFeat1";
            this.lblFeat1.Size      = new System.Drawing.Size(350, 24);
            this.lblFeat1.Text      = "Create Sections and Time Slots";
            // lblFeat2
            this.lblFeat2.Font      = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblFeat2.ForeColor = System.Drawing.Color.FromArgb(170, 255, 255, 255);
            this.lblFeat2.Location  = new System.Drawing.Point(446, 188);
            this.lblFeat2.Name      = "lblFeat2";
            this.lblFeat2.Size      = new System.Drawing.Size(350, 24);
            this.lblFeat2.Text      = "Build Weekly Timetable Grid";
            // lblFeat3
            this.lblFeat3.Font      = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblFeat3.ForeColor = System.Drawing.Color.FromArgb(170, 255, 255, 255);
            this.lblFeat3.Location  = new System.Drawing.Point(446, 224);
            this.lblFeat3.Name      = "lblFeat3";
            this.lblFeat3.Size      = new System.Drawing.Size(350, 24);
            this.lblFeat3.Text      = "Detect Scheduling Conflicts";
            // lblFeat4
            this.lblFeat4.Font      = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblFeat4.ForeColor = System.Drawing.Color.FromArgb(170, 255, 255, 255);
            this.lblFeat4.Location  = new System.Drawing.Point(446, 260);
            this.lblFeat4.Name      = "lblFeat4";
            this.lblFeat4.Size      = new System.Drawing.Size(350, 24);
            this.lblFeat4.Text      = "Print and Export Timetables";
            // lblFeat5
            this.lblFeat5.Font      = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblFeat5.ForeColor = System.Drawing.Color.FromArgb(170, 255, 255, 255);
            this.lblFeat5.Location  = new System.Drawing.Point(446, 296);
            this.lblFeat5.Name      = "lblFeat5";
            this.lblFeat5.Size      = new System.Drawing.Size(350, 24);
            this.lblFeat5.Text      = "Support for Multiple Sections";
            // btnStart
            this.btnStart.BackColor              = System.Drawing.Color.White;
            this.btnStart.Cursor                 = System.Windows.Forms.Cursors.Hand;
            this.btnStart.FlatStyle              = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.Font                   = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnStart.ForeColor              = System.Drawing.Color.FromArgb(0, 88, 190);
            this.btnStart.Location               = new System.Drawing.Point(430, 360);
            this.btnStart.Name                   = "btnStart";
            this.btnStart.Size                   = new System.Drawing.Size(200, 48);
            this.btnStart.Text                   = "Get Started";
            this.btnStart.Click                 += new System.EventHandler(this.BtnStart_Click);
            this.btnStart.MouseEnter            += new System.EventHandler(this.BtnStart_MouseEnter);
            this.btnStart.MouseLeave            += new System.EventHandler(this.BtnStart_MouseLeave);
            // lblArrow
            this.lblArrow.AutoSize  = true;
            this.lblArrow.Font      = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblArrow.ForeColor = System.Drawing.Color.White;
            this.lblArrow.Location  = new System.Drawing.Point(644, 372);
            this.lblArrow.Name      = "lblArrow";
            this.lblArrow.Text      = "-->";
            // btnClose
            this.btnClose.BackColor                          = System.Drawing.Color.Transparent;
            this.btnClose.Cursor                             = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatStyle                          = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize          = 0;
            this.btnClose.FlatAppearance.MouseOverBackColor  = System.Drawing.Color.FromArgb(30, 255, 255, 255);
            this.btnClose.Font                               = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor                          = System.Drawing.Color.FromArgb(160, 255, 255, 255);
            this.btnClose.Location                           = new System.Drawing.Point(778, 12);
            this.btnClose.Name                               = "btnClose";
            this.btnClose.Size                               = new System.Drawing.Size(36, 36);
            this.btnClose.Text                               = "X";
            this.btnClose.Click                             += new System.EventHandler(this.BtnClose_Click);
            // SplashForm
            this.AutoScaleMode   = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor       = System.Drawing.Color.FromArgb(0, 88, 190);
            this.ClientSize      = new System.Drawing.Size(820, 460);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name            = "SplashForm";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text            = "TTMS";
            this.Paint          += new System.Windows.Forms.PaintEventHandler(this.Form_Paint);
            this.Controls.Add(this.iconBox);
            this.Controls.Add(this.lblTitle1);
            this.Controls.Add(this.lblTitle2);
            this.Controls.Add(this.lblSub);
            this.Controls.Add(this.lblVer);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.divider);
            this.Controls.Add(this.lblManage);
            this.Controls.Add(this.pnlDot0);
            this.Controls.Add(this.pnlDot1);
            this.Controls.Add(this.pnlDot2);
            this.Controls.Add(this.pnlDot3);
            this.Controls.Add(this.pnlDot4);
            this.Controls.Add(this.pnlDot5);
            this.Controls.Add(this.lblFeat0);
            this.Controls.Add(this.lblFeat1);
            this.Controls.Add(this.lblFeat2);
            this.Controls.Add(this.lblFeat3);
            this.Controls.Add(this.lblFeat4);
            this.Controls.Add(this.lblFeat5);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblArrow);
            this.Controls.Add(this.btnClose);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Timer   animTimer;
        private System.Windows.Forms.Panel   iconBox;
        private System.Windows.Forms.Label   lblTitle1;
        private System.Windows.Forms.Label   lblTitle2;
        private System.Windows.Forms.Label   lblSub;
        private System.Windows.Forms.Label   lblVer;
        private System.Windows.Forms.Label   lblStatus;
        private System.Windows.Forms.Panel   divider;
        private System.Windows.Forms.Label   lblManage;
        private System.Windows.Forms.Panel   pnlDot0;
        private System.Windows.Forms.Panel   pnlDot1;
        private System.Windows.Forms.Panel   pnlDot2;
        private System.Windows.Forms.Panel   pnlDot3;
        private System.Windows.Forms.Panel   pnlDot4;
        private System.Windows.Forms.Panel   pnlDot5;
        private System.Windows.Forms.Label   lblFeat0;
        private System.Windows.Forms.Label   lblFeat1;
        private System.Windows.Forms.Label   lblFeat2;
        private System.Windows.Forms.Label   lblFeat3;
        private System.Windows.Forms.Label   lblFeat4;
        private System.Windows.Forms.Label   lblFeat5;
        private System.Windows.Forms.Button  btnStart;
        private System.Windows.Forms.Label   lblArrow;
        private System.Windows.Forms.Button  btnClose;
    }
}
