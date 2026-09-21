namespace TTMS_OOP.Forms
{
    partial class PrintPreviewForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.topBar = new System.Windows.Forms.Panel();
            this.cmbSection = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lblStyle = new System.Windows.Forms.Label();
            this.cmbStyle = new System.Windows.Forms.ComboBox();
            this.chkMerge = new System.Windows.Forms.CheckBox();
            this.lblTitleEdit = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblDateTimeEdit = new System.Windows.Forms.Label();
            this.txtDateTime = new System.Windows.Forms.TextBox();
            this.lblInchargeEdit = new System.Windows.Forms.Label();
            this.txtIncharge = new System.Windows.Forms.TextBox();
            this.scrollPanel = new System.Windows.Forms.Panel();
            this.previewPanel = new System.Windows.Forms.Panel();
            this.topBar.SuspendLayout();
            this.scrollPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // topBar
            // 
            this.topBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.topBar.Controls.Add(this.cmbSection);
            this.topBar.Controls.Add(this.lblStyle);
            this.topBar.Controls.Add(this.cmbStyle);
            this.topBar.Controls.Add(this.chkMerge);
            this.topBar.Controls.Add(this.btnRefresh);
            this.topBar.Controls.Add(this.btnPrint);
            this.topBar.Controls.Add(this.lblTitleEdit);
            this.topBar.Controls.Add(this.txtTitle);
            this.topBar.Controls.Add(this.lblDateTimeEdit);
            this.topBar.Controls.Add(this.txtDateTime);
            this.topBar.Controls.Add(this.lblInchargeEdit);
            this.topBar.Controls.Add(this.txtIncharge);
            this.topBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.topBar.Height = 84;
            this.topBar.Name = "topBar";
            // 
            // cmbSection
            // 
            this.cmbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSection.FormattingEnabled = true;
            this.cmbSection.Location = new System.Drawing.Point(12, 10);
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.Size = new System.Drawing.Size(145, 25);
            this.cmbSection.SelectedIndexChanged += new System.EventHandler(this.CmbSection_Changed);
            // 
            // lblStyle
            // 
            this.lblStyle.AutoSize = true;
            this.lblStyle.Location = new System.Drawing.Point(168, 14);
            this.lblStyle.Name = "lblStyle";
            this.lblStyle.Size = new System.Drawing.Size(54, 13);
            this.lblStyle.Text = "Template:";
            // 
            // cmbStyle
            // 
            this.cmbStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStyle.FormattingEnabled = true;
            this.cmbStyle.Items.AddRange(new object[] {
            "Classic Style",
            "Executive Modern"});
            this.cmbStyle.Location = new System.Drawing.Point(226, 10);
            this.cmbStyle.Name = "cmbStyle";
            this.cmbStyle.Size = new System.Drawing.Size(130, 25);
            this.cmbStyle.SelectedIndexChanged += new System.EventHandler(this.CmbStyle_Changed);
            // 
            // chkMerge
            // 
            this.chkMerge.AutoSize = true;
            this.chkMerge.Location = new System.Drawing.Point(370, 14);
            this.chkMerge.Name = "chkMerge";
            this.chkMerge.Size = new System.Drawing.Size(71, 17);
            this.chkMerge.Text = "Merge All";
            this.chkMerge.CheckedChanged += new System.EventHandler(this.ChkMerge_Changed);
            // 
            // btnRefresh
            // 
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(460, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(80, 28);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(550, 8);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(90, 28);
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // lblTitleEdit
            // 
            this.lblTitleEdit.AutoSize = true;
            this.lblTitleEdit.Location = new System.Drawing.Point(12, 45);
            this.lblTitleEdit.Name = "lblTitleEdit";
            this.lblTitleEdit.Size = new System.Drawing.Size(68, 13);
            this.lblTitleEdit.Text = "Header Title:";
            // 
            // txtTitle
            // 
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTitle.Location = new System.Drawing.Point(85, 43);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(250, 20);
            // 
            // lblDateTimeEdit
            // 
            this.lblDateTimeEdit.AutoSize = true;
            this.lblDateTimeEdit.Location = new System.Drawing.Point(348, 45);
            this.lblDateTimeEdit.Name = "lblDateTimeEdit";
            this.lblDateTimeEdit.Size = new System.Drawing.Size(65, 13);
            this.lblDateTimeEdit.Text = "Date & Time:";
            // 
            // txtDateTime
            // 
            this.txtDateTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDateTime.Location = new System.Drawing.Point(420, 43);
            this.txtDateTime.Name = "txtDateTime";
            this.txtDateTime.Size = new System.Drawing.Size(240, 20);
            // 
            // lblInchargeEdit
            // 
            this.lblInchargeEdit.AutoSize = true;
            this.lblInchargeEdit.Location = new System.Drawing.Point(675, 45);
            this.lblInchargeEdit.Name = "lblInchargeEdit";
            this.lblInchargeEdit.Size = new System.Drawing.Size(52, 13);
            this.lblInchargeEdit.Text = "Incharge:";
            // 
            // txtIncharge
            // 
            this.txtIncharge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIncharge.Location = new System.Drawing.Point(732, 43);
            this.txtIncharge.Name = "txtIncharge";
            this.txtIncharge.Size = new System.Drawing.Size(160, 20);
            // 
            // scrollPanel
            // 
            this.scrollPanel.AutoScroll = true;
            this.scrollPanel.Controls.Add(this.previewPanel);
            this.scrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scrollPanel.Name = "scrollPanel";
            // 
            // previewPanel
            // 
            this.previewPanel.Name = "previewPanel";
            this.previewPanel.Size = new System.Drawing.Size(800, 600);
            // 
            // PrintPreviewForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 720);
            this.Controls.Add(this.scrollPanel);
            this.Controls.Add(this.topBar);
            this.Name = "PrintPreviewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Print Preview";
            this.topBar.ResumeLayout(false);
            this.topBar.PerformLayout();
            this.scrollPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel topBar;
        private System.Windows.Forms.ComboBox cmbSection;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.TextBox txtIncharge;
        private System.Windows.Forms.CheckBox chkMerge;
        private System.Windows.Forms.Label lblStyle;
        private System.Windows.Forms.ComboBox cmbStyle;
        private System.Windows.Forms.Label lblTitleEdit;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblDateTimeEdit;
        private System.Windows.Forms.TextBox txtDateTime;
        private System.Windows.Forms.Label lblInchargeEdit;
        private System.Windows.Forms.Panel scrollPanel;
        private System.Windows.Forms.Panel previewPanel;
    }
}
