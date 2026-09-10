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
    public partial class AddEntryForm : Form
    {
        public TimetableEntry ResultEntry { get; private set; }
        public bool DeleteEntry { get; private set; }
        public bool ToggleLock { get; private set; }

        private Section section;
        private TimeSlot slot;
        private string day;
        private List<Subject> allSubjects;
        private List<Subject> subjects;
        private List<Teacher> teachers;
        private TimetableEntry existing;
        private List<TimetableEntry> sectionEntries;

        public AddEntryForm(Section sec, TimeSlot slt, string dy,
            List<Subject> subjs, List<Teacher> tchrs, TimetableEntry existingEntry,
            List<TimetableEntry> currentEntries = null)
        {
            this.section = sec;
            this.slot = slt;
            this.day = dy;
            this.allSubjects = subjs;
            this.teachers = tchrs ?? new List<Teacher>();
            this.existing = existingEntry;
            this.sectionEntries = currentEntries ?? DataManager.GetEntries();

            // Count how many hours/slots each subject is already scheduled in this section (excluding this slot)
            var countMap = new Dictionary<int, int>();
            foreach (var en in this.sectionEntries)
            {
                if (en.SectionId == sec.SectionId && en.SubjectId > 0 && !en.IsReserved && string.IsNullOrEmpty(en.CustomText))
                {
                    if (existingEntry != null && en.SlotId == existingEntry.SlotId && en.Day == existingEntry.Day)
                        continue;

                    if (!countMap.ContainsKey(en.SubjectId)) countMap[en.SubjectId] = 0;
                    countMap[en.SubjectId]++;
                }
            }

            // A 3-credit subject cannot have 4 hours (max 3 hours per section).
            // If already added 3 times, remove it from the list.
            this.subjects = (subjs ?? new List<Subject>())
                .FindAll(s => TimetableManager.IsSameSemester(s.Semester, sec.Semester) &&
                              (!countMap.ContainsKey(s.SubjectId) || countMap[s.SubjectId] < 3));

            this.subjects.Sort((a, b) => {
                string codeA = a.CourseCode ?? "";
                string codeB = b.CourseCode ?? "";
                int cmp = string.Compare(codeA, codeB, StringComparison.OrdinalIgnoreCase);
                if (cmp != 0) return cmp;
                cmp = string.Compare(a.Name ?? "", b.Name ?? "", StringComparison.OrdinalIgnoreCase);
                if (cmp != 0) return cmp;
                return a.IsLab.CompareTo(b.IsLab);
            });

            this.teachers.Sort((a, b) => string.Compare(a.Name ?? "", b.Name ?? "", StringComparison.OrdinalIgnoreCase));

            InitializeComponent();
            this.Text = dy + "  |  " + slt.ToString();
            this.BackColor = AppColors.Surface;

            lbl0.Text = "Section: " + sec.ToString();
            lbl0.Font = AppFonts.LabelMd;
            lbl0.ForeColor = AppColors.OnSurfaceVar;

            if (this.subjects.Count == 0)
            {
                lbl1.Text = $"Select Subject ({sec.Semester ?? "Semester"}) - All subjects at max (3 hrs)";
                lbl1.ForeColor = AppColors.Error;
            }
            else
            {
                lbl1.Text = $"Select Subject ({sec.Semester ?? "Semester"})";
                lbl1.Font = AppFonts.LabelMd;
                lbl1.ForeColor = AppColors.OnSurfaceVar;
            }

            cmbSubject.Font = AppFonts.BodyMd;
            cmbSubject.DataSource = subjects;

            lblTeacher.Font = AppFonts.LabelMd;
            lblTeacher.ForeColor = AppColors.OnSurfaceVar;
            cmbTeacher.Font = AppFonts.BodyMd;
            cmbTeacher.DataSource = new List<Teacher>(teachers);

            chkLockTeacher.Font = AppFonts.BodyMd;
            chkLockTeacher.ForeColor = AppColors.OnSurface;

            chkReserved.Font = AppFonts.BodyMd;
            chkReserved.ForeColor = AppColors.OnSurface;

            chkCustom.Font = AppFonts.BodyMd;
            chkCustom.ForeColor = AppColors.OnSurface;
            txtCustom.Font = AppFonts.BodyMd;
            txtCustom.BackColor = AppColors.Background;
            txtCustom.ForeColor = AppColors.OnSurface;

            btnSave.Font = AppFonts.BodyMd;
            btnSave.BackColor = AppColors.Primary;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Cursor = Cursors.Hand;

            btnDelete.Font = AppFonts.LabelMd;
            btnDelete.BackColor = AppColors.Error;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.Enabled = existing != null;

            btnCancel.Font = AppFonts.LabelMd;
            btnCancel.BackColor = AppColors.SurfaceHigh;
            btnCancel.ForeColor = AppColors.OnSurface;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Cursor = Cursors.Hand;

            FillExisting();
            UpdateInputs();
        }

        private void CmbSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSubject.SelectedItem is Subject sel)
            {
                // If this subject already has a locked/remembered teacher for this semester, auto-select them
                if (sel.TeacherId > 0)
                {
                    foreach (Teacher t in teachers)
                    {
                        if (t.TeacherId == sel.TeacherId)
                        {
                            cmbTeacher.SelectedItem = t;
                            break;
                        }
                    }
                }
            }
        }

        private void ChkReserved_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReserved.Checked) chkCustom.Checked = false;
            UpdateInputs();
        }

        private void ChkCustom_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCustom.Checked) chkReserved.Checked = false;
            UpdateInputs();
        }

        private void UpdateInputs()
        {
            bool isNormal = !chkReserved.Checked && !chkCustom.Checked;
            cmbSubject.Enabled = isNormal;
            cmbTeacher.Enabled = isNormal;
            chkLockTeacher.Enabled = isNormal;
            txtCustom.Enabled = chkCustom.Checked;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void FillExisting()
        {
            if (existing == null)
            {
                // Trigger auto-selection for the initial subject
                CmbSubject_SelectedIndexChanged(this, EventArgs.Empty);
                return;
            }

            chkReserved.Checked = existing.IsReserved;
            if (!string.IsNullOrEmpty(existing.CustomText))
            {
                chkCustom.Checked = true;
                txtCustom.Text = existing.CustomText;
            }

            if (!existing.IsReserved && string.IsNullOrEmpty(existing.CustomText))
            {
                Subject matchedSubj = null;
                foreach (Subject s in subjects)
                {
                    if (s.SubjectId == existing.SubjectId)
                    {
                        cmbSubject.SelectedItem = s;
                        matchedSubj = s;
                        break;
                    }
                }

                int tid = existing.TeacherId > 0 ? existing.TeacherId : (matchedSubj != null ? matchedSubj.TeacherId : 0);
                if (tid > 0)
                {
                    foreach (Teacher t in teachers)
                    {
                        if (t.TeacherId == tid)
                        {
                            cmbTeacher.SelectedItem = t;
                            break;
                        }
                    }
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!chkReserved.Checked && !chkCustom.Checked)
            {
                if (cmbSubject.SelectedItem == null)
                {
                    MessageBox.Show("Please select a subject.",
                        "Subject Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbTeacher.SelectedItem == null)
                {
                    MessageBox.Show("Please select a teacher for this class.",
                        "Teacher Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Subject selSubj = (Subject)cmbSubject.SelectedItem;
                Teacher selTchr = (Teacher)cmbTeacher.SelectedItem;

                // Validate 3 credit / 3 hours limit per section
                int currentUsage = sectionEntries.Count(en => en.SectionId == section.SectionId && en.SubjectId == selSubj.SubjectId &&
                    !en.IsReserved && string.IsNullOrEmpty(en.CustomText) &&
                    !(existing != null && en.SlotId == existing.SlotId && en.Day == existing.Day));

                if (currentUsage >= 3)
                {
                    MessageBox.Show($"\"{selSubj.Name}\" already has 3 hours scheduled in this section.\nA 3 credit hour subject cannot have more than 3 hours per week.",
                        "3 Hours Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool conflict = TimetableManager.IsTeacherBusy(selTchr.TeacherId, slot.SlotId, day, section.SectionId);
                if (conflict)
                {
                    string tName = selTchr.ToString();
                    DialogResult res = MessageBox.Show(tName + " is already scheduled at\n" + day + "  |  " + slot.ToString() + " in another section.\n\nAssign anyway?",
                        "Schedule Conflict", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (res == DialogResult.No) return;
                }

                // If lock teacher is checked, remember/lock this teacher for this subject across this semester
                if (chkLockTeacher.Checked)
                {
                    selSubj.TeacherId = selTchr.TeacherId;
                    DataManager.SaveSubjects(allSubjects);
                }
            }

            if (chkCustom.Checked && string.IsNullOrWhiteSpace(txtCustom.Text))
            {
                MessageBox.Show("Please enter custom text.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int finalSubjId = (chkReserved.Checked || chkCustom.Checked) ? 0 : ((Subject)cmbSubject.SelectedItem).SubjectId;
            int finalTchrId = (chkReserved.Checked || chkCustom.Checked) ? 0 : ((Teacher)cmbTeacher.SelectedItem).TeacherId;

            ResultEntry = new TimetableEntry
            {
                EntryId = existing != null ? existing.EntryId : 0,
                SectionId = section.SectionId,
                SlotId = slot.SlotId,
                Day = day,
                IsReserved = chkReserved.Checked,
                CustomText = chkCustom.Checked ? txtCustom.Text.Trim() : null,
                IsLocked = false, // Don't show lock icons on timetable
                SubjectId = finalSubjId,
                TeacherId = finalTchrId
            };
            this.DialogResult = DialogResult.OK;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DeleteEntry = true;
            this.DialogResult = DialogResult.OK;
        }
    }
}

