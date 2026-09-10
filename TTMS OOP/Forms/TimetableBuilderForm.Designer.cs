namespace TTMS_OOP.Forms
{
    partial class TimetableBuilderForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.grid = new System.Windows.Forms.DataGridView();
            this.topBar = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblStats = new System.Windows.Forms.Label();
            this.lblSes = new System.Windows.Forms.Label();
            this.cmbSession = new System.Windows.Forms.ComboBox();
            this.lblSec = new System.Windows.Forms.Label();
            this.cmbSection = new System.Windows.Forms.ComboBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnClearGrid = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.topBar.SuspendLayout();
            this.SuspendLayout();
            // grid
            this.grid.AllowDrop = true;
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grid.ColumnHeadersHeight = 42;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersWidth = 120;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.grid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellClick);
            this.grid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_MouseDown);
            this.grid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Grid_MouseMove);
            this.grid.DragEnter += new System.Windows.Forms.DragEventHandler(this.Grid_DragEnter);
            this.grid.DragOver += new System.Windows.Forms.DragEventHandler(this.Grid_DragOver);
            this.grid.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
            this.grid.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.Grid_CellPainting);
            this.grid.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellMouseEnter);
            this.grid.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellMouseLeave);
            // topBar
            this.topBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.topBar.Controls.Add(this.lblTitle);
            this.topBar.Controls.Add(this.lblStats);
            this.topBar.Controls.Add(this.lblSes);
            this.topBar.Controls.Add(this.cmbSession);
            this.topBar.Controls.Add(this.lblSec);
            this.topBar.Controls.Add(this.cmbSection);
            this.topBar.Controls.Add(this.btnLoad);
            this.topBar.Controls.Add(this.btnClearGrid);
            this.topBar.Controls.Add(this.btnPrint);
            this.topBar.Controls.Add(this.lblInfo);
            this.topBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.topBar.Height = 74;
            this.topBar.Name = "topBar";
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(16, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "📅 Timetable Builder";
            // lblStats
            this.lblStats.AutoSize = true;
            this.lblStats.Location = new System.Drawing.Point(18, 44);
            this.lblStats.Name = "lblStats";
            this.lblStats.Text = "Ready";
            // lblSes
            this.lblSes.AutoSize = true;
            this.lblSes.Location = new System.Drawing.Point(210, 12);
            this.lblSes.Name = "lblSes";
            this.lblSes.Text = "Semester:";
            // cmbSession
            this.cmbSession.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSession.Location = new System.Drawing.Point(210, 32);
            this.cmbSession.Name = "cmbSession";
            this.cmbSession.Size = new System.Drawing.Size(150, 28);
            this.cmbSession.SelectedIndexChanged += new System.EventHandler(this.CmbSession_Changed);
            // lblSec
            this.lblSec.AutoSize = true;
            this.lblSec.Location = new System.Drawing.Point(372, 12);
            this.lblSec.Name = "lblSec";
            this.lblSec.Text = "Section:";
            // cmbSection
            this.cmbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSection.FormattingEnabled = true;
            this.cmbSection.Location = new System.Drawing.Point(372, 32);
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.Size = new System.Drawing.Size(130, 28);
            this.cmbSection.SelectedIndexChanged += new System.EventHandler(this.CmbSection_Changed);
            // btnLoad
            this.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoad.ForeColor = System.Drawing.Color.White;
            this.btnLoad.Location = new System.Drawing.Point(516, 30);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(85, 32);
            this.btnLoad.Text = "🔄 Reload";
            this.btnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
            // btnClearGrid
            this.btnClearGrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearGrid.ForeColor = System.Drawing.Color.White;
            this.btnClearGrid.Location = new System.Drawing.Point(610, 30);
            this.btnClearGrid.Name = "btnClearGrid";
            this.btnClearGrid.Size = new System.Drawing.Size(115, 32);
            this.btnClearGrid.Text = "🗑️ Clear Schedule";
            this.btnClearGrid.Click += new System.EventHandler(this.BtnClearGrid_Click);
            // btnPrint
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(734, 30);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(105, 32);
            this.btnPrint.Text = "🖨️ Print View";
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // lblInfo
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(855, 18);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Text = "🟦 Theory   🟩 Lab   🟨 Reserved   🟥 Conflict\n⇄ Drag & Drop to Move or Swap Slots!";
            // TimetableBuilderForm
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 680);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.topBar);
            this.Name = "TimetableBuilderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Timetable Builder";
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.topBar.ResumeLayout(false);
            this.topBar.PerformLayout();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Panel topBar;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Label lblSes;
        private System.Windows.Forms.ComboBox cmbSession;
        private System.Windows.Forms.Label lblSec;
        private System.Windows.Forms.ComboBox cmbSection;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnClearGrid;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label lblInfo;
    }
}
