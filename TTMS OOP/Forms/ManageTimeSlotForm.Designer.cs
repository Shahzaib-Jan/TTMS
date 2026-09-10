namespace TTMS_OOP.Forms
{
    partial class ManageTimeSlotsForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lstSlots = new System.Windows.Forms.ListBox();
            this.pnl = new System.Windows.Forms.Panel();
            this.l1 = new System.Windows.Forms.Label();
            this.txtStart = new System.Windows.Forms.TextBox();
            this.l2 = new System.Windows.Forms.Label();
            this.txtEnd = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.pnl.SuspendLayout();
            this.SuspendLayout();
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "Time Slots";
            this.lstSlots.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstSlots.Location = new System.Drawing.Point(20, 60);
            this.lstSlots.Name = "lstSlots";
            this.lstSlots.Size = new System.Drawing.Size(240, 280);
            this.lstSlots.SelectedIndexChanged += new System.EventHandler(this.List_SelectedIndexChanged);
            this.pnl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl.Controls.Add(this.l1);
            this.pnl.Controls.Add(this.txtStart);
            this.pnl.Controls.Add(this.l2);
            this.pnl.Controls.Add(this.txtEnd);
            this.pnl.Controls.Add(this.btnSave);
            this.pnl.Controls.Add(this.btnDelete);
            this.pnl.Location = new System.Drawing.Point(280, 60);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(290, 280);
            this.l1.AutoSize = true;
            this.l1.Location = new System.Drawing.Point(15, 20);
            this.l1.Name = "l1";
            this.l1.Text = "Start Time (e.g. 08:00)";
            this.txtStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStart.Location = new System.Drawing.Point(15, 40);
            this.txtStart.Name = "txtStart";
            this.txtStart.Size = new System.Drawing.Size(255, 28);
            this.l2.AutoSize = true;
            this.l2.Location = new System.Drawing.Point(15, 80);
            this.l2.Name = "l2";
            this.l2.Text = "End Time (e.g. 09:00)";
            this.txtEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEnd.Location = new System.Drawing.Point(15, 100);
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.Size = new System.Drawing.Size(255, 28);
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(15, 155);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(255, 36);
            this.btnSave.Text = "Save Slot";
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(15, 205);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(255, 36);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 420);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lstSlots);
            this.Controls.Add(this.pnl);
            this.Name = "ManageTimeSlotsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage Time Slots";
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ListBox lstSlots;
        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.Label l1;
        private System.Windows.Forms.TextBox txtStart;
        private System.Windows.Forms.Label l2;
        private System.Windows.Forms.TextBox txtEnd;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
    }
}
