using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TTMS_OOP.Helpers;
using TTMS_OOP.Models;
using TTMS_OOP.BLL;

namespace TTMS_OOP.Forms
{
    public partial class ManageTeachersForm : Form
    {
        private List<Teacher> teachers;
        private List<Subject> allSubjects;
        private List<Section> allSections;

        public ManageTeachersForm()
        {
            InitializeComponent();
            this.BackColor = AppColors.Background;
            lblTitle.Font = AppFonts.HeadlineLg;
            lblTitle.ForeColor = AppColors.OnSurface;
            lstTeachers.Font = AppFonts.BodyMd;
            lstTeachers.BackColor = AppColors.Surface;
            pnl.BackColor = AppColors.Surface;
            l1.Font = AppFonts.LabelMd; l1.ForeColor = AppColors.OnSurfaceVar;
            l2.Font = AppFonts.LabelMd; l2.ForeColor = AppColors.OnSurfaceVar;
            cmbDesignation.Font = AppFonts.BodyMd;
            txtName.Font = AppFonts.BodyMd;
            btnSave.Font = AppFonts.BodyMd; btnSave.BackColor = AppColors.Primary; btnSave.FlatAppearance.BorderSize = 0; btnSave.Cursor = Cursors.Hand;
            btnDelete.Font = AppFonts.BodyMd; btnDelete.BackColor = AppColors.Error; btnDelete.FlatAppearance.BorderSize = 0; btnDelete.Cursor = Cursors.Hand;
            LoadData();
        }

        private void LoadData()
        {
            teachers = DataManager.GetTeachers();
            allSubjects = DataManager.GetSubjects();
            allSections = DataManager.GetSections();
            RefreshList();
        }

        private void RefreshList()
        {
            teachers.Sort((a, b) => string.Compare(a.Name ?? "", b.Name ?? "", StringComparison.OrdinalIgnoreCase));
            lstTeachers.Items.Clear();
            foreach (Teacher t in teachers)
                lstTeachers.Items.Add(t);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            { MessageBox.Show("Enter teacher name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (lstTeachers.SelectedItems.Count == 1 && lstTeachers.SelectedItem is Teacher selTeacher)
            {
                selTeacher.Name = txtName.Text.Trim();
                selTeacher.Designation = cmbDesignation.SelectedItem.ToString();
            }
            else
            {
                int nextId = teachers.Count > 0 ? (teachers.Max(t => t.TeacherId) + 1) : 1;
                teachers.Add(new Teacher { TeacherId = nextId, Name = txtName.Text.Trim(), Designation = cmbDesignation.SelectedItem.ToString() });
            }
            DataManager.SaveTeachers(teachers);
            RefreshList();
            ClearForm();
        }

        private void List_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTeachers.SelectedItems.Count == 0)
            {
                ClearForm();
                return;
            }

            if (lstTeachers.SelectedItems.Count > 1)
            {
                txtName.Text = "";
                cmbDesignation.SelectedIndex = 0;
                txtSubjects.Text = "";
                btnDelete.Enabled = true;
                btnDelete.Text = $"Delete Selected ({lstTeachers.SelectedItems.Count})";
                btnSave.Enabled = false;
                btnSave.Text = "Save Teacher";
                return;
            }

            Teacher t = (Teacher)lstTeachers.SelectedItem;
            txtName.Text = t.Name;
            cmbDesignation.SelectedItem = t.Designation;
            btnDelete.Enabled = true;
            btnDelete.Text = "Delete";
            btnSave.Enabled = true;
            btnSave.Text = "Update Teacher";

            // Show teaching subjects
            List<string> subjLines = new List<string>();
            foreach (Subject s in allSubjects)
            {
                if (s.TeacherId == t.TeacherId)
                {
                    string secStr = "";
                    if (allSections != null)
                    {
                        var matchingSections = allSections.FindAll(sec => sec.Semester == s.Semester && !string.IsNullOrWhiteSpace(sec.Name));
                        if (matchingSections.Count > 0)
                        {
                            List<string> secNames = new List<string>();
                            foreach (var sec in matchingSections) secNames.Add(sec.Name);
                            secStr = " [Sections: " + string.Join(", ", secNames) + "]";
                        }
                    }
                    subjLines.Add(s.Name + (s.IsLab ? " (Lab)" : "") + " (" + (string.IsNullOrEmpty(s.Semester) ? "General" : s.Semester) + ")" + secStr);
                }
            }
            if (subjLines.Count == 0) txtSubjects.Text = "No subjects assigned.";
            else txtSubjects.Text = string.Join(Environment.NewLine, subjLines);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int count = lstTeachers.SelectedItems.Count;
            if (count == 0) return;

            string msg = count == 1
                ? "Delete this teacher?"
                : $"Delete these {count} selected teachers?";

            if (MessageBox.Show(msg, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                List<Teacher> toRemove = new List<Teacher>();
                foreach (object item in lstTeachers.SelectedItems)
                {
                    if (item is Teacher t)
                        toRemove.Add(t);
                }

                foreach (Teacher t in toRemove)
                {
                    teachers.Remove(t);
                }

                DataManager.SaveTeachers(teachers);
                RefreshList();
                ClearForm();
            }
        }

        private void ClearForm()
        {
            txtName.Text = "";
            if (cmbDesignation.Items.Count > 0) cmbDesignation.SelectedIndex = 0;
            lstTeachers.ClearSelected();
            txtSubjects.Text = "";
            btnDelete.Enabled = false;
            btnDelete.Text = "Delete";
            btnSave.Enabled = true;
            btnSave.Text = "Save Teacher";
        }
    }
}

