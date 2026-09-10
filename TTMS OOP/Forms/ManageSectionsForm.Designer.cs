namespace TTMS_OOP.Forms
{
    partial class ManageSectionsForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lstSections = new System.Windows.Forms.ListBox();
            this.pnl = new System.Windows.Forms.Panel();
            this.l1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.l2 = new System.Windows.Forms.Label();
            this.cmbSemester = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.pnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(48, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Sections";
            // 
            // lstSections
            // 
            this.lstSections.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstSections.Location = new System.Drawing.Point(20, 60);
            this.lstSections.Name = "lstSections";
            this.lstSections.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstSections.Size = new System.Drawing.Size(240, 314);
            this.lstSections.TabIndex = 1;
            this.lstSections.SelectedIndexChanged += new System.EventHandler(this.List_SelectedIndexChanged);
            // 
            // pnl
            // 
            this.pnl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl.Controls.Add(this.l1);
            this.pnl.Controls.Add(this.txtName);
            this.pnl.Controls.Add(this.l2);
            this.pnl.Controls.Add(this.cmbSemester);
            this.pnl.Controls.Add(this.btnSave);
            this.pnl.Controls.Add(this.btnDelete);
            this.pnl.Location = new System.Drawing.Point(280, 60);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(290, 314);
            this.pnl.TabIndex = 2;
            // 
            // l1
            // 
            this.l1.AutoSize = true;
            this.l1.Location = new System.Drawing.Point(15, 20);
            this.l1.Name = "l1";
            this.l1.Size = new System.Drawing.Size(116, 13);
            this.l1.TabIndex = 0;
            this.l1.Text = "Section Name (A, B, C)";
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Location = new System.Drawing.Point(15, 40);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(255, 20);
            this.txtName.TabIndex = 1;
            // 
            // l2
            // 
            this.l2.AutoSize = true;
            this.l2.Location = new System.Drawing.Point(15, 75);
            this.l2.Name = "l2";
            this.l2.Size = new System.Drawing.Size(51, 13);
            this.l2.TabIndex = 2;
            this.l2.Text = "Semester";
            // 
            // cmbSemester
            // 
            this.cmbSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSemester.Items.AddRange(new object[] {
            "1st Semester",
            "2nd Semester",
            "3rd Semester",
            "4th Semester",
            "5th Semester",
            "6th Semester",
            "7th Semester",
            "8th Semester"});
            this.cmbSemester.Location = new System.Drawing.Point(15, 95);
            this.cmbSemester.Name = "cmbSemester";
            this.cmbSemester.Size = new System.Drawing.Size(255, 21);
            this.cmbSemester.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(15, 155);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(255, 36);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save Section";
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(15, 205);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(255, 36);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // ManageSectionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lstSections);
            this.Controls.Add(this.pnl);
            this.Name = "ManageSectionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage Sections";
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ListBox lstSections;
        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.Label l1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label l2;
        private System.Windows.Forms.ComboBox cmbSemester;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
    }
}
