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
    public partial class ManageSubjectsForm : Form
    {
        private List<Subject> subjects;
        private int currentSemester = 1;
        private Button[] semButtons = new Button[8];

        private readonly string[] semesterNames = new string[]
        {
            "1st Semester", "2nd Semester", "3rd Semester", "4th Semester",
            "5th Semester", "6th Semester", "7th Semester", "8th Semester"
        };

        public ManageSubjectsForm()
        {
            InitializeComponent();
            this.BackColor = AppColors.Background;
            lblTitle.Font = AppFonts.HeadlineLg;
            lblTitle.ForeColor = AppColors.OnSurface;
            lblSub.Font = AppFonts.BodyMd;
            lblSub.ForeColor = AppColors.OnSurfaceVar;

            lblListTitle.Font = AppFonts.HeadlineMd;
            lblListTitle.ForeColor = AppColors.OnSurface;
            lstSubjects.Font = AppFonts.BodyMd;
            lstSubjects.BackColor = AppColors.Surface;

            pnlEdit.BackColor = AppColors.Surface;
            lblEditTitle.Font = AppFonts.HeadlineMd;
            lblEditTitle.ForeColor = AppColors.OnSurface;

            lblCode.Font = AppFonts.LabelMd;
            lblCode.ForeColor = AppColors.OnSurfaceVar;
            txtCourseCode.Font = AppFonts.BodyMd;

            lblName.Font = AppFonts.LabelMd;
            lblName.ForeColor = AppColors.OnSurfaceVar;
            txtName.Font = AppFonts.BodyMd;

            lblSem.Font = AppFonts.LabelMd;
            lblSem.ForeColor = AppColors.OnSurfaceVar;
            cmbSemester.Font = AppFonts.BodyMd;
            cmbSemester.Items.Clear();
            cmbSemester.Items.AddRange(semesterNames);
            cmbSemester.SelectedIndex = 0;

            chkIsLab.Font = AppFonts.BodyMd;
            chkIsLab.ForeColor = AppColors.OnSurface;

            btnSave.Font = AppFonts.BodyMd;
            btnSave.BackColor = AppColors.Primary;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Cursor = Cursors.Hand;

            btnClear.Font = AppFonts.LabelMd;
            btnClear.BackColor = AppColors.SurfaceHigh;
            btnClear.ForeColor = AppColors.OnSurface;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.Cursor = Cursors.Hand;

            btnDelete.Font = AppFonts.LabelMd;
            btnDelete.BackColor = AppColors.Error;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Cursor = Cursors.Hand;

            BuildSemesterNav();
            LoadData();
        }

        private void BuildSemesterNav()
        {
            pnlSemNav.Controls.Clear();
            int btnW = 86;
            int btnH = 34;
            int gap = 6;

            for (int i = 0; i < 8; i++)
            {
                int semNum = i + 1;
                Button btn = new Button();
                btn.Text = "Sem " + semNum;
                btn.Tag = semNum;
                btn.Size = new Size(btnW, btnH);
                btn.Location = new Point(i * (btnW + gap), 4);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                btn.Font = AppFonts.LabelMd;
                btn.Cursor = Cursors.Hand;
                btn.Click += (s, e) =>
                {
                    currentSemester = (int)((Button)s).Tag;
                    cmbSemester.SelectedIndex = currentSemester - 1;
                    UpdateSemButtons();
                    RefreshList();
                    ClearForm();
                };

                semButtons[i] = btn;
                pnlSemNav.Controls.Add(btn);
            }
            UpdateSemButtons();
        }

        private void UpdateSemButtons()
        {
            for (int i = 0; i < 8; i++)
            {
                int semNum = i + 1;
                Button btn = semButtons[i];
                if (semNum == currentSemester)
                {
                    btn.BackColor = AppColors.Primary;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = AppColors.Primary;
                    btn.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                }
                else
                {
                    btn.BackColor = AppColors.Surface;
                    btn.ForeColor = AppColors.OnSurface;
                    btn.FlatAppearance.BorderColor = Color.FromArgb(200, 210, 225);
                    btn.Font = new Font("Segoe UI", 9f, FontStyle.Regular);
                }
            }
        }

        private void LoadData()
        {
            subjects = DataManager.GetSubjects();
            EnsurePreSeededCurriculum();
            RefreshList();
        }

        private void EnsurePreSeededCurriculum()
        {
            bool needsSave = false;

            // Pre-seed official curriculum from syllabus images
            var curriculum = new (string Code, string Name, int Sem, bool IsLab)[]
            {
                // Year 1 - Semester 1
                ("CSC102", "Programming Fundamentals", 1, false),
                ("CSC102", "Programming Fundamentals", 1, true),
                ("CSC100", "Application of ICT", 1, false),
                ("CSC100", "Application of ICT", 1, true),
                ("CSC101", "Discrete Mathematics", 1, false),
                ("MA-123", "Calculus", 1, false),
                ("PHY-111", "Applied Physics", 1, false),
                ("PHY-111", "Applied Physics", 1, true),

                // Year 1 - Semester 2
                ("CSC103", "Object Oriented Programming", 2, false),
                ("CSC103", "Object Oriented Programming", 2, true),
                ("CSC104", "Database Systems", 2, false),
                ("CSC104", "Database Systems", 2, true),
                ("CSC105", "Digital Logic Design", 2, false),
                ("CSC105", "Digital Logic Design", 2, true),
                ("MA-104", "Calculus II", 2, false),
                ("MA-205", "Applied Statistics and Probability", 2, false),
                ("HU-111L", "Communication Skills", 2, true),
                ("QT-101", "Translation of the Holy Quran-I", 2, false),

                // Year 2 - Semester 3
                ("CSC200", "Data Structures and Algorithms", 3, false),
                ("CSC200", "Data Structures and Algorithms", 3, true),
                ("MA-234", "Linear Algebra", 3, false),
                ("HU-102", "Functional English", 3, false),
                ("CSC203", "Computer Networks", 3, false),
                ("CSC203", "Computer Networks", 3, true),
                ("CSC204", "Software Engineering", 3, false),
                ("IS-102", "Islamic Studies / Ethics", 3, false),

                // Year 2 - Semester 4
                ("CSC205", "Computer Org & Assembly Language", 4, false),
                ("CSC205", "Computer Org & Assembly Language", 4, true),
                ("CSC201", "Information Security", 4, false),
                ("CSC201", "Information Security", 4, true),
                ("CSC202", "Artificial Intelligence", 4, false),
                ("CSC202", "Artificial Intelligence", 4, true),
                ("CSC206", "Theory of Automata", 4, false),
                ("CSC208", "Design and Analysis of Algorithms", 4, false),
                ("CSC207", "Advanced Database Management Systems", 4, false),
                ("CSC207", "Advanced Database Management Systems", 4, true),
                ("QT-201", "Translation of the Holy Quran-II", 4, false),

                // Year 3 - Semester 5
                ("CSC300", "Operating Systems", 5, false),
                ("CSC300", "Operating Systems", 5, true),
                ("CSC301", "Human Computer Interaction", 5, false),
                ("CSC302", "Computer Architecture", 5, false),
                ("CSCXXX", "Domain Elective 1", 5, false),
                ("CSCXXX", "Domain Elective 2", 5, false),
                ("XX-XXX", "Social Science Elective", 5, false),

                // Year 3 - Semester 6
                ("CSC303", "Compiler Construction", 6, false),
                ("CSC303", "Compiler Construction", 6, true),
                ("CSC304", "Parallel and Distributed Computing", 6, false),
                ("CSC304", "Parallel and Distributed Computing", 6, true),
                ("CSCXXX", "Domain Elective 3", 6, false),
                ("CSCXXX", "Domain Elective 4", 6, false),
                ("XX-XXX", "Management Science Elective", 6, false),
                ("HU-222", "Expository Writing", 6, false),
                ("QT-301", "Translation of the Holy Quran-III", 6, false),

                // Year 4 - Semester 7
                ("CSC401", "Final Year Project - I", 7, true),
                ("CSCXXX", "Domain Elective 5", 7, false),
                ("CSCXXX", "Domain Elective 6", 7, false),
                ("XX-XXX", "Social Responsibility / Community", 7, false),
                ("HU-XXX", "Technical Report Writing", 7, false),
                ("MGT-318", "Entrepreneurship and Management", 7, false),

                // Year 4 - Semester 8
                ("CSC402", "Final Year Project - II", 8, true),
                ("CSCXXX", "Domain Elective 7", 8, false),
                ("IS-202", "Ideology and Constitution of Pakistan", 8, false),
                ("CSC403", "Professional Practices in Software Dev", 8, false),
                ("QT-401", "Translation of the Holy Quran-IV", 8, false),
                ("HU-XXX", "International Language", 8, false)
            };

            // First: Update existing subjects if they lack course codes
            foreach (var s in subjects)
            {
                if (string.IsNullOrWhiteSpace(s.CourseCode))
                {
                    int semNum = TimetableManager.GetSemesterNumber(s.Semester);
                    var match = curriculum.FirstOrDefault(c => c.Sem == semNum && 
                        c.IsLab == s.IsLab && 
                        (s.Name.ToLower().Contains(c.Name.ToLower()) || c.Name.ToLower().Contains(s.Name.ToLower())));
                    
                    if (match != default)
                    {
                        s.CourseCode = match.Code;
                        needsSave = true;
                    }
                }
            }

            // Second: If a curriculum subject is missing entirely from that semester, add it
            int maxId = subjects.Count > 0 ? subjects.Max(s => s.SubjectId) : 0;
            foreach (var item in curriculum)
            {
                string targetSemStr = semesterNames[item.Sem - 1];
                bool exists = subjects.Any(s => TimetableManager.IsSameSemester(s.Semester, targetSemStr) &&
                    string.Equals(s.CourseCode, item.Code, StringComparison.OrdinalIgnoreCase) &&
                    s.IsLab == item.IsLab);

                if (!exists)
                {
                    maxId++;
                    subjects.Add(new Subject
                    {
                        SubjectId = maxId,
                        CourseCode = item.Code,
                        Name = item.Name,
                        Semester = targetSemStr,
                        IsLab = item.IsLab,
                        TeacherId = 0
                    });
                    needsSave = true;
                }
            }

            if (needsSave)
            {
                DataManager.SaveSubjects(subjects);
            }
        }

        private void RefreshList()
        {
            lstSubjects.Items.Clear();
            string curSemStr = semesterNames[currentSemester - 1];
            List<Subject> semSubjs = subjects.FindAll(s => TimetableManager.IsSameSemester(s.Semester, curSemStr));

            semSubjs.Sort((a, b) => {
                string codeA = a.CourseCode ?? "";
                string codeB = b.CourseCode ?? "";
                int cmp = string.Compare(codeA, codeB, StringComparison.OrdinalIgnoreCase);
                if (cmp != 0) return cmp;
                cmp = string.Compare(a.Name ?? "", b.Name ?? "", StringComparison.OrdinalIgnoreCase);
                if (cmp != 0) return cmp;
                return a.IsLab.CompareTo(b.IsLab);
            });

            foreach (Subject s in semSubjs)
            {
                lstSubjects.Items.Add(s);
            }

            lblListTitle.Text = $"Semester {currentSemester} Subjects ({semSubjs.Count})";
        }

        private void List_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSubjects.SelectedItems.Count == 0)
            {
                ClearForm();
                return;
            }

            if (lstSubjects.SelectedItems.Count > 1)
            {
                txtCourseCode.Text = "";
                txtName.Text = "";
                chkIsLab.Checked = false;
                btnDelete.Enabled = true;
                btnDelete.Text = $"Delete Selected ({lstSubjects.SelectedItems.Count})";
                btnSave.Enabled = false;
                btnSave.Text = "Save Subject";
                return;
            }

            Subject s = (Subject)lstSubjects.SelectedItem;
            txtCourseCode.Text = s.CourseCode ?? "";
            txtName.Text = s.Name ?? "";
            chkIsLab.Checked = s.IsLab;

            int semNum = TimetableManager.GetSemesterNumber(s.Semester);
            if (semNum >= 1 && semNum <= 8)
                cmbSemester.SelectedIndex = semNum - 1;

            btnDelete.Enabled = true;
            btnDelete.Text = "Delete";
            btnSave.Enabled = true;
            btnSave.Text = "Update Subject";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string code = txtCourseCode.Text.Trim().ToUpper();
            string name = txtName.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please enter a Subject Name.", "Subject Name Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            string semStr = cmbSemester.SelectedItem != null ? cmbSemester.SelectedItem.ToString() : semesterNames[currentSemester - 1];
            bool isLab = chkIsLab.Checked;

            if (lstSubjects.SelectedItems.Count == 1 && lstSubjects.SelectedItem is Subject sel)
            {
                sel.CourseCode = code;
                sel.Name = name;
                sel.Semester = semStr;
                sel.IsLab = isLab;
            }
            else
            {
                int newId = subjects.Count > 0 ? (subjects.Max(s => s.SubjectId) + 1) : 1;
                subjects.Add(new Subject
                {
                    SubjectId = newId,
                    CourseCode = code,
                    Name = name,
                    Semester = semStr,
                    IsLab = isLab,
                    TeacherId = 0
                });
            }

            DataManager.SaveSubjects(subjects);
            RefreshList();
            ClearForm();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int count = lstSubjects.SelectedItems.Count;
            if (count == 0) return;

            string msg = count == 1
                ? "Delete this subject?"
                : $"Delete these {count} selected subjects?";

            if (MessageBox.Show(msg, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                List<Subject> toRemove = new List<Subject>();
                foreach (object item in lstSubjects.SelectedItems)
                {
                    if (item is Subject s) toRemove.Add(s);
                }

                foreach (Subject s in toRemove)
                {
                    subjects.Remove(s);
                }

                DataManager.SaveSubjects(subjects);
                RefreshList();
                ClearForm();
            }
        }

        private void ClearForm()
        {
            txtCourseCode.Text = "";
            txtName.Text = "";
            chkIsLab.Checked = false;
            cmbSemester.SelectedIndex = currentSemester - 1;
            lstSubjects.ClearSelected();
            btnDelete.Enabled = false;
            btnDelete.Text = "Delete";
            btnSave.Enabled = true;
            btnSave.Text = "Save Subject";
        }
    }
}

