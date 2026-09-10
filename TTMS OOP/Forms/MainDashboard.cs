using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TTMS_OOP.Helpers;
using TTMS_OOP.BLL;

namespace TTMS_OOP.Forms
{
    public partial class MainDashboard : Form
    {
        private List<Button> navButtons = new List<Button>();
        private string activeNav = "Dashboard";

        private Panel cardSections;
        private Panel cardTeachers;
        private Panel cardSubjects;
        private Panel cardSlots;
        private Panel cardConflicts;

        public MainDashboard()
        {
            InitializeComponent();
            this.BackColor = AppColors.Background;
            BuildContent();
            BuildSidebar();
        }

        // ─────────────────────────────────────────
        // SIDEBAR
        // ─────────────────────────────────────────
        private void BuildSidebar()
        {

            // Logo area
            Panel logoArea = new Panel();
            logoArea.Size = new Size(230, 80);
            logoArea.Location = new Point(0, 0);
            logoArea.BackColor = Color.FromArgb(12, 36, 84);

            Label appName = new Label();
            appName.Text = "TTMS";
            appName.Font = new Font("Segoe UI", 20f, FontStyle.Bold);
            appName.ForeColor = Color.White;
            appName.AutoSize = true;
            appName.Location = new Point(20, 14);
            logoArea.Controls.Add(appName);

            Label appSub = new Label();
            appSub.Text = "Academic Schedule Manager";
            appSub.Font = new Font("Segoe UI", 7.5f);
            appSub.ForeColor = Color.FromArgb(140, 180, 230);
            appSub.AutoSize = true;
            appSub.Location = new Point(20, 48);
            logoArea.Controls.Add(appSub);

            sidebar.Controls.Add(logoArea);

            // Section label
            Label lblNav = new Label();
            lblNav.Text = "NAVIGATION";
            lblNav.Font = new Font("Segoe UI", 7.5f, FontStyle.Bold);
            lblNav.ForeColor = Color.FromArgb(100, 140, 200);
            lblNav.AutoSize = true;
            lblNav.Location = new Point(20, 96);
            sidebar.Controls.Add(lblNav);

            // Nav items with icons
            string[,] navItems = {
                { "Dashboard",  "⊞" },
                { "Sections",   "◫" },
                { "Teachers",   "👤" },
                { "Subjects",   "📖" },
                { "Time Slots", "🕐" },
                { "Timetable",  "📅" },
                { "Print",      "🖨" }
            };

            for (int i = 0; i < navItems.GetLength(0); i++)
            {
                Button btn = CreateNavButton(
                    navItems[i, 0], navItems[i, 1]);
                btn.Location = new Point(0, 116 + (i * 50));
                navButtons.Add(btn);
                sidebar.Controls.Add(btn);
            }

            SetActiveNav(navButtons[0]);

            // Bottom info
            Panel bottomInfo = new Panel();
            bottomInfo.Size = new Size(230, 50);
            bottomInfo.Dock = DockStyle.Bottom;
            bottomInfo.BackColor = Color.FromArgb(12, 36, 84);

            Label lblVersion = new Label();
            lblVersion.Text = "TTMS System  ·  By Shahzaib (1258)";
            lblVersion.Font = new Font("Segoe UI", 8f);
            lblVersion.ForeColor = Color.FromArgb(80, 120, 180);
            lblVersion.AutoSize = true;
            lblVersion.Location = new Point(20, 16);
            bottomInfo.Controls.Add(lblVersion);

            sidebar.Controls.Add(bottomInfo);
        }

        private Button CreateNavButton(string text, string icon)
        {
            Button btn = new Button();
            btn.Text = "  " + icon + "   " + text;
            btn.Size = new Size(230, 48);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Font = new Font("Segoe UI", 10f);
            btn.ForeColor = Color.FromArgb(160, 200, 240);
            btn.BackColor = Color.Transparent;
            btn.Cursor = Cursors.Hand;
            btn.Tag = text;

            btn.MouseEnter += (s, e) => {
                if ((string)btn.Tag != activeNav)
                {
                    btn.BackColor = Color.FromArgb(30, 255, 255, 255);
                    btn.ForeColor = Color.White;
                }
            };
            btn.MouseLeave += (s, e) => {
                if ((string)btn.Tag != activeNav)
                {
                    btn.BackColor = Color.Transparent;
                    btn.ForeColor = Color.FromArgb(160, 200, 240);
                }
            };
            btn.Click += (s, e) => {
                SetActiveNav(btn);
                NavButton_Click(text);
            };
            return btn;
        }

        private void SetActiveNav(Button btn)
        {
            foreach (Button b in navButtons)
            {
                b.BackColor = Color.Transparent;
                b.ForeColor = Color.FromArgb(160, 200, 240);
                b.Font = new Font("Segoe UI", 10f);
            }
            btn.BackColor = Color.FromArgb(0, 88, 190);
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            activeNav = (string)btn.Tag;
        }

        private void NavButton_Click(string section)
        {
            switch (section)
            {
                case "Teachers":
                    new ManageTeachersForm().ShowDialog(); break;
                case "Subjects":
                    new ManageSubjectsForm().ShowDialog(); break;
                case "Sections":
                    new ManageSectionsForm().ShowDialog(); break;
                case "Time Slots":
                    new ManageTimeSlotsForm().ShowDialog(); break;
                case "Timetable":
                    new TimetableBuilderForm().ShowDialog(); break;
                case "Print":
                    new PrintPreviewForm().ShowDialog(); break;
            }
            RefreshCards();
        }

        // ─────────────────────────────────────────
        // CONTENT
        // ─────────────────────────────────────────
        private void BuildContent()
        {
            // contentArea already created in InitializeComponent

            // Top header bar
            Panel headerBar = new Panel();
            headerBar.Size = new Size(900, 80);
            headerBar.Location = new Point(0, 0);
            headerBar.BackColor = AppColors.Surface;
            headerBar.BorderStyle = BorderStyle.FixedSingle;
            headerBar.Dock = DockStyle.Top;
            contentArea.Controls.Add(headerBar);

            Label welcome = new Label();
            welcome.Text = "Good " + GetGreeting() + " 👋";
            welcome.Font = new Font("Segoe UI", 18f, FontStyle.Bold);
            welcome.ForeColor = AppColors.OnSurface;
            welcome.AutoSize = true;
            welcome.Location = new Point(24, 14);
            headerBar.Controls.Add(welcome);

            Label dateLbl = new Label();
            dateLbl.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            dateLbl.Font = new Font("Segoe UI", 9f);
            dateLbl.ForeColor = AppColors.OnSurfaceVar;
            dateLbl.AutoSize = true;
            dateLbl.Location = new Point(24, 48);
            headerBar.Controls.Add(dateLbl);

            // Quick action buttons in header
            Button btnTimetable = MakeHeaderBtn(
                "Open Timetable", AppColors.Primary);
            btnTimetable.Location = new Point(650, 20);
            btnTimetable.Click += (s, e) => {
                new TimetableBuilderForm().ShowDialog();
                RefreshCards();
            };
            headerBar.Controls.Add(btnTimetable);

            Button btnPrint = MakeHeaderBtn(
                "Print", AppColors.OnSurfaceVar);
            btnPrint.Location = new Point(790, 20);
            btnPrint.Click += (s, e) => {
                new PrintPreviewForm().ShowDialog();
                RefreshCards();
            };
            headerBar.Controls.Add(btnPrint);

            // Cards container
            Panel cardsPanel = new Panel();
            cardsPanel.Location = new Point(24, 100);
            cardsPanel.Size = new Size(830, 120);
            cardsPanel.BackColor = Color.Transparent;
            contentArea.Controls.Add(cardsPanel);

            cardSections = MakeCard("Sections", "0", "◫",
                AppColors.ChipBlue, 0, 0);
            cardTeachers = MakeCard("Teachers", "0", "👤",
                AppColors.ChipMint, 164, 0);
            cardSubjects = MakeCard("Subjects", "0", "📖",
                AppColors.ChipLavender, 328, 0);
            cardSlots = MakeCard("Time Slots", "0", "🕐",
                AppColors.ChipPeach, 492, 0);
            cardConflicts = MakeCard("Conflicts", "0", "⚠️",
                AppColors.ChipMint, 656, 0);

            cardsPanel.Controls.Add(cardSections);
            cardsPanel.Controls.Add(cardTeachers);
            cardsPanel.Controls.Add(cardSubjects);
            cardsPanel.Controls.Add(cardSlots);
            cardsPanel.Controls.Add(cardConflicts);

            // Quick guide
            Label lblGuide = new Label();
            lblGuide.Text = "Quick Guide";
            lblGuide.Font = new Font("Segoe UI", 13f, FontStyle.Bold);
            lblGuide.ForeColor = AppColors.OnSurface;
            lblGuide.AutoSize = true;
            lblGuide.Location = new Point(24, 240);
            contentArea.Controls.Add(lblGuide);

            // Guide cards row
            string[,] guides = {
                { "1", "Add Teachers",
                  "Start by adding teachers\nwith their designation" },
                { "2", "Add Subjects",
                  "Link subjects to teachers\nand mark labs" },
                { "3", "Add Sections",
                  "Create sections (A, B, C)\nwith semester info" },
                { "4", "Add Time Slots",
                  "Define daily time slots\neg. 08:00 - 09:00" },
                { "5", "Build Timetable",
                  "Click cells to assign\nsubjects to slots" }
            };

            for (int i = 0; i < guides.GetLength(0); i++)
            {
                Panel g = MakeGuideCard(
                    guides[i, 0], guides[i, 1], guides[i, 2]);
                g.Location = new Point(24 + (i * 156), 270);
                contentArea.Controls.Add(g);
            }

            RefreshCards();
        }

        private string GetGreeting()
        {
            int h = DateTime.Now.Hour;
            if (h < 12) return "Morning";
            if (h < 17) return "Afternoon";
            return "Evening";
        }

        private Button MakeHeaderBtn(string text, Color bg)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Size = new Size(130, 38);
            btn.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            btn.BackColor = bg;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
            return btn;
        }

        private Panel MakeCard(string title, string value,
            string icon, Color bg, int x, int y)
        {
            Panel card = new Panel();
            card.Size = new Size(150, 100);
            card.Location = new Point(x, y);
            card.BackColor = bg;
            card.BorderStyle = BorderStyle.FixedSingle;

            Label lIcon = new Label();
            lIcon.Text = icon;
            lIcon.Font = new Font("Segoe UI", 18f);
            lIcon.AutoSize = true;
            lIcon.Location = new Point(14, 10);
            card.Controls.Add(lIcon);

            Label lVal = new Label();
            lVal.Name = "val";
            lVal.Text = value;
            lVal.Font = new Font("Segoe UI", 22f, FontStyle.Bold);
            lVal.ForeColor = AppColors.OnSurface;
            lVal.AutoSize = true;
            lVal.Location = new Point(80, 10);
            card.Controls.Add(lVal);

            Label lTitle = new Label();
            lTitle.Text = title;
            lTitle.Font = AppFonts.LabelMd;
            lTitle.ForeColor = AppColors.OnSurfaceVar;
            lTitle.AutoSize = true;
            lTitle.Location = new Point(14, 72);
            card.Controls.Add(lTitle);

            return card;
        }

        private Panel MakeGuideCard(string num,
            string title, string desc)
        {
            Panel card = new Panel();
            card.Size = new Size(148, 110);
            card.BackColor = AppColors.Surface;
            card.BorderStyle = BorderStyle.FixedSingle;

            Label lNum = new Label();
            lNum.Text = num;
            lNum.Font = new Font("Segoe UI", 20f, FontStyle.Bold);
            lNum.ForeColor = AppColors.Primary;
            lNum.AutoSize = true;
            lNum.Location = new Point(12, 8);
            card.Controls.Add(lNum);

            Label lTitle = new Label();
            lTitle.Text = title;
            lTitle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            lTitle.ForeColor = AppColors.OnSurface;
            lTitle.AutoSize = true;
            lTitle.Location = new Point(12, 44);
            card.Controls.Add(lTitle);

            Label lDesc = new Label();
            lDesc.Text = desc;
            lDesc.Font = new Font("Segoe UI", 7.5f);
            lDesc.ForeColor = AppColors.OnSurfaceVar;
            lDesc.Size = new Size(124, 40);
            lDesc.Location = new Point(12, 64);
            card.Controls.Add(lDesc);

            return card;
        }

        private void RefreshCards()
        {
            UpdateCard(cardSections,
                DataManager.GetSections().Count.ToString());
            UpdateCard(cardTeachers,
                DataManager.GetTeachers().Count.ToString());
            UpdateCard(cardSubjects,
                DataManager.GetSubjects().Count.ToString());
            UpdateCard(cardSlots,
                DataManager.GetSlots().Count.ToString());

            int conflicts = TimetableManager.GetTotalConflicts();
            UpdateCard(cardConflicts, conflicts.ToString());
            cardConflicts.BackColor = conflicts > 0
                ? Color.FromArgb(255, 200, 200)
                : AppColors.ChipMint;
        }

        private void UpdateCard(Panel card, string value)
        {
            foreach (Control c in card.Controls)
                if (c is Label lbl && lbl.Name == "val")
                    lbl.Text = value;
        }


    }
}
