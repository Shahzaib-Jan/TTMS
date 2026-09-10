namespace TTMS_OOP.Forms
{
    partial class ManageTeachersForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lstTeachers = new System.Windows.Forms.ListBox();
            this.pnl = new System.Windows.Forms.Panel();
            this.l1 = new System.Windows.Forms.Label();
            this.cmbDesignation = new System.Windows.Forms.ComboBox();
            this.l2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblSubjects = new System.Windows.Forms.Label();
            this.txtSubjects = new System.Windows.Forms.TextBox();
            this.pnl.SuspendLayout();
            this.SuspendLayout();
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "Teachers";
            this.lstTeachers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstTeachers.Location = new System.Drawing.Point(20, 60);
            this.lstTeachers.Name = "lstTeachers";
            this.lstTeachers.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstTeachers.Size = new System.Drawing.Size(240, 360);
            this.lstTeachers.SelectedIndexChanged += new System.EventHandler(this.List_SelectedIndexChanged);
            this.pnl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl.Controls.Add(this.l1);
            this.pnl.Controls.Add(this.cmbDesignation);
            this.pnl.Controls.Add(this.l2);
            this.pnl.Controls.Add(this.txtName);
            this.pnl.Controls.Add(this.btnSave);
            this.pnl.Controls.Add(this.btnDelete);
            this.pnl.Controls.Add(this.lblSubjects);
            this.pnl.Controls.Add(this.txtSubjects);
            this.pnl.Location = new System.Drawing.Point(280, 60);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(290, 360);
            this.l1.AutoSize = true;
            this.l1.Location = new System.Drawing.Point(15, 20);
            this.l1.Name = "l1";
            this.l1.Text = "Designation";
            this.cmbDesignation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDesignation.Items.AddRange(new object[] { "Mr.", "Dr.", "Ms.", "Mrs." });
            this.cmbDesignation.Location = new System.Drawing.Point(15, 40);
            this.cmbDesignation.Name = "cmbDesignation";
            this.cmbDesignation.Size = new System.Drawing.Size(100, 28);
            this.cmbDesignation.SelectedIndex = 0;
            this.l2.AutoSize = true;
            this.l2.Location = new System.Drawing.Point(15, 80);
            this.l2.Name = "l2";
            this.l2.Text = "Full Name";
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Location = new System.Drawing.Point(15, 100);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(255, 28);
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(15, 155);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(255, 36);
            this.btnSave.Text = "Save Teacher";
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(15, 205);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(255, 36);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            this.lblSubjects.AutoSize = true;
            this.lblSubjects.Location = new System.Drawing.Point(15, 255);
            this.lblSubjects.Name = "lblSubjects";
            this.lblSubjects.Text = "Teaching Subjects";
            this.txtSubjects.Location = new System.Drawing.Point(15, 275);
            this.txtSubjects.Name = "txtSubjects";
            this.txtSubjects.Multiline = true;
            this.txtSubjects.ReadOnly = true;
            this.txtSubjects.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSubjects.Size = new System.Drawing.Size(255, 70);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 450);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lstTeachers);
            this.Controls.Add(this.pnl);
            this.Name = "ManageTeachersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage Teachers";
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ListBox lstTeachers;
        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.Label l1;
        private System.Windows.Forms.ComboBox cmbDesignation;
        private System.Windows.Forms.Label l2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblSubjects;
        private System.Windows.Forms.TextBox txtSubjects;
    }
}
