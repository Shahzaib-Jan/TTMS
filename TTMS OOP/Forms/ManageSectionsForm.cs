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
    public partial class ManageSectionsForm : Form
    {
        private List<Section> sections;

        public ManageSectionsForm()
        {
            InitializeComponent();
            this.BackColor = AppColors.Background;
            lblTitle.Font = AppFonts.HeadlineLg; lblTitle.ForeColor = AppColors.OnSurface;
            lstSections.Font = AppFonts.BodyMd; lstSections.BackColor = AppColors.Surface;
            pnl.BackColor = AppColors.Surface;
            l1.Font = AppFonts.LabelMd; l1.ForeColor = AppColors.OnSurfaceVar;
            l2.Font = AppFonts.LabelMd; l2.ForeColor = AppColors.OnSurfaceVar;
            txtName.Font = AppFonts.BodyMd;
            cmbSemester.Font = AppFonts.BodyMd;
            btnSave.Font = AppFonts.BodyMd; btnSave.BackColor = AppColors.Primary; btnSave.FlatAppearance.BorderSize = 0; btnSave.Cursor = Cursors.Hand;
            btnDelete.Font = AppFonts.BodyMd; btnDelete.BackColor = AppColors.Error; btnDelete.FlatAppearance.BorderSize = 0; btnDelete.Cursor = Cursors.Hand;
            // Fix btnSave/btnDelete positions to not overlap
            btnSave.Location = new System.Drawing.Point(15, 155);
            btnDelete.Location = new System.Drawing.Point(15, 205);
            LoadData();
        }

        private void LoadData()
        {
            sections = DataManager.GetSections();
            RefreshList();
        }

        private void RefreshList()
        {
            sections.Sort((a, b) => {
                int semA = TimetableManager.GetSemesterNumber(a.Semester);
                int semB = TimetableManager.GetSemesterNumber(b.Semester);
                int cmp = semA.CompareTo(semB);
                if (cmp != 0) return cmp;
                return string.Compare(a.Name ?? "", b.Name ?? "", StringComparison.OrdinalIgnoreCase);
            });
            lstSections.Items.Clear();
            foreach (Section s in sections)
                lstSections.Items.Add(s);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim().ToUpper();
            string sem = cmbSemester.SelectedItem != null ? cmbSemester.SelectedItem.ToString() : "";
            if (lstSections.SelectedItems.Count == 1 && lstSections.SelectedItem is Section selSection)
            {
                selSection.Name = name;
                selSection.Semester = sem;
            }
            else
            {
                bool exists = false;
                foreach (Section s in sections)
                    if (s.Name == name && s.Semester == sem) exists = true;
                if (exists) { MessageBox.Show("Section already exists!", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                
                int maxSec = sections.Count > 0 ? sections.Max(s => s.SectionId) : 0;
                var allEntries = DataManager.GetEntries();
                int maxEntrySec = allEntries.Count > 0 ? allEntries.Max(en => en.SectionId) : 0;
                int nextId = Math.Max(maxSec, maxEntrySec) + 1;

                sections.Add(new Section { SectionId = nextId, Name = name, Semester = sem });
            }
            DataManager.SaveSections(sections);
            RefreshList();
            ClearForm();
        }

        private void List_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSections.SelectedItems.Count == 0)
            {
                ClearForm();
                return;
            }

            if (lstSections.SelectedItems.Count > 1)
            {
                txtName.Text = "";
                btnDelete.Enabled = true;
                btnDelete.Text = $"Delete Selected ({lstSections.SelectedItems.Count})";
                btnSave.Enabled = false;
                btnSave.Text = "Save Section";
                return;
            }

            Section s = (Section)lstSections.SelectedItem;
            txtName.Text = s.Name;
            if (cmbSemester.Items.Contains(s.Semester))
                cmbSemester.SelectedItem = s.Semester;
            btnDelete.Enabled = true;
            btnDelete.Text = "Delete";
            btnSave.Enabled = true;
            btnSave.Text = "Update Section";
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int count = lstSections.SelectedItems.Count;
            if (count == 0) return;

            string msg = count == 1
                ? "Delete this section?"
                : $"Delete these {count} selected sections?";

            if (MessageBox.Show(msg, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                List<Section> toRemove = new List<Section>();
                foreach (object item in lstSections.SelectedItems)
                {
                    if (item is Section s)
                        toRemove.Add(s);
                }

                foreach (Section s in toRemove)
                {
                    sections.Remove(s);
                }

                DataManager.SaveSections(sections);

                // Cascade delete: remove all entries belonging to the deleted section(s)
                var allEntries = DataManager.GetEntries();
                int removedCount = allEntries.RemoveAll(en => toRemove.Any(rem => rem.SectionId == en.SectionId));
                if (removedCount > 0)
                {
                    DataManager.SaveEntries(allEntries);
                }

                RefreshList();
                ClearForm();
            }
        }

        private void ClearForm()
        {
            txtName.Text = "";
            if (cmbSemester.Items.Count > 1) cmbSemester.SelectedIndex = 1;
            lstSections.ClearSelected();
            btnDelete.Enabled = false;
            btnDelete.Text = "Delete";
            btnSave.Enabled = true;
            btnSave.Text = "Save Section";
        }
    }
}

