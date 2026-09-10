namespace TTMS_OOP.Forms
{
    partial class AddEntryForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lbl0 = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.cmbSubject = new System.Windows.Forms.ComboBox();
            this.lblTeacher = new System.Windows.Forms.Label();
            this.cmbTeacher = new System.Windows.Forms.ComboBox();
            this.chkLockTeacher = new System.Windows.Forms.CheckBox();
            this.chkReserved = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkCustom = new System.Windows.Forms.CheckBox();
            this.txtCustom = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbl0
            // 
            this.lbl0.AutoSize = true;
            this.lbl0.Location = new System.Drawing.Point(20, 14);
            this.lbl0.Name = "lbl0";
            this.lbl0.Size = new System.Drawing.Size(46, 13);
            this.lbl0.TabIndex = 0;
            this.lbl0.Text = "Section:";
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(20, 42);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(76, 13);
            this.lbl1.TabIndex = 1;
            this.lbl1.Text = "Select Subject";
            // 
            // cmbSubject
            // 
            this.cmbSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubject.Location = new System.Drawing.Point(20, 60);
            this.cmbSubject.Name = "cmbSubject";
            this.cmbSubject.Size = new System.Drawing.Size(370, 28);
            this.cmbSubject.TabIndex = 2;
            this.cmbSubject.SelectedIndexChanged += new System.EventHandler(this.CmbSubject_SelectedIndexChanged);
            // 
            // lblTeacher
            // 
            this.lblTeacher.AutoSize = true;
            this.lblTeacher.Location = new System.Drawing.Point(20, 96);
            this.lblTeacher.Name = "lblTeacher";
            this.lblTeacher.Size = new System.Drawing.Size(80, 13);
            this.lblTeacher.TabIndex = 3;
            this.lblTeacher.Text = "Select Teacher";
            // 
            // cmbTeacher
            // 
            this.cmbTeacher.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTeacher.Location = new System.Drawing.Point(20, 114);
            this.cmbTeacher.Name = "cmbTeacher";
            this.cmbTeacher.Size = new System.Drawing.Size(370, 28);
            this.cmbTeacher.TabIndex = 4;
            // 
            // chkLockTeacher
            // 
            this.chkLockTeacher.AutoSize = true;
            this.chkLockTeacher.Checked = true;
            this.chkLockTeacher.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLockTeacher.Location = new System.Drawing.Point(20, 150);
            this.chkLockTeacher.Name = "chkLockTeacher";
            this.chkLockTeacher.Size = new System.Drawing.Size(347, 17);
            this.chkLockTeacher.TabIndex = 5;
            this.chkLockTeacher.Text = "Lock this Teacher for this Subject (auto-select in other slots)";
            // 
            // chkReserved
            // 
            this.chkReserved.AutoSize = true;
            this.chkReserved.Location = new System.Drawing.Point(20, 178);
            this.chkReserved.Name = "chkReserved";
            this.chkReserved.Size = new System.Drawing.Size(217, 17);
            this.chkReserved.TabIndex = 6;
            this.chkReserved.Text = "Mark as Reserved (Tutorial / Seminar)";
            this.chkReserved.CheckedChanged += new System.EventHandler(this.ChkReserved_CheckedChanged);
            // 
            // chkCustom
            // 
            this.chkCustom.AutoSize = true;
            this.chkCustom.Location = new System.Drawing.Point(20, 206);
            this.chkCustom.Name = "chkCustom";
            this.chkCustom.Size = new System.Drawing.Size(88, 17);
            this.chkCustom.TabIndex = 7;
            this.chkCustom.Text = "Custom Text:";
            this.chkCustom.CheckedChanged += new System.EventHandler(this.ChkCustom_CheckedChanged);
            // 
            // txtCustom
            // 
            this.txtCustom.Enabled = false;
            this.txtCustom.Location = new System.Drawing.Point(120, 204);
            this.txtCustom.Name = "txtCustom";
            this.txtCustom.Size = new System.Drawing.Size(270, 20);
            this.txtCustom.TabIndex = 8;
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(20, 245);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(160, 38);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(190, 245);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 38);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "Clear Cell";
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(300, 245);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 38);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // AddEntryForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 305);
            this.Controls.Add(this.lbl0);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.cmbSubject);
            this.Controls.Add(this.lblTeacher);
            this.Controls.Add(this.cmbTeacher);
            this.Controls.Add(this.chkLockTeacher);
            this.Controls.Add(this.chkReserved);
            this.Controls.Add(this.chkCustom);
            this.Controls.Add(this.txtCustom);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddEntryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lbl0;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.ComboBox cmbSubject;
        private System.Windows.Forms.Label lblTeacher;
        private System.Windows.Forms.ComboBox cmbTeacher;
        private System.Windows.Forms.CheckBox chkLockTeacher;
        private System.Windows.Forms.CheckBox chkReserved;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkCustom;
        private System.Windows.Forms.TextBox txtCustom;
    }
}
