using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;
using TTMS_OOP.Helpers;
using TTMS_OOP.Models;
using TTMS_OOP.BLL;

namespace TTMS_OOP.Forms
{
    public partial class PrintPreviewForm : Form
    {
        private bool isMerged = false;

        private List<Section> sections;
        private List<TimeSlot> slots;
        private List<TimetableEntry> entries;
        private List<Subject> subjects;
        private List<Teacher> teachers;

        private string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };

        private Color[] subjectColors = {
            Color.FromArgb(224, 238, 249), // Soft Sky Blue
            Color.FromArgb(220, 245, 232), // Soft Sage Green
            Color.FromArgb(240, 230, 248), // Soft Lavender
            Color.FromArgb(254, 245, 224), // Soft Peach/Warm Yellow
            Color.FromArgb(253, 232, 235), // Soft Rose Pink
            Color.FromArgb(225, 246, 244), // Soft Mint
            Color.FromArgb(235, 242, 252)  // Soft Ice Blue
        };
        private Color[] subjectTextColors = {
            Color.FromArgb(20,  70, 120),
            Color.FromArgb(15,  90,  60),
            Color.FromArgb(85,  35, 135),
            Color.FromArgb(140, 80,  15),
            Color.FromArgb(135, 35,  45),
            Color.FromArgb(15,  95,  80),
            Color.FromArgb(20,  70, 120)
        };

        private const int CELL_W    = 150;
        private const int CELL_H    = 62;
        private const int TIME_W    = 85;
        private const int DAY_ROW_H = 36;
        private const int HEADER_H  = 60;
        private const int TABLE_GAP = 50;

        public PrintPreviewForm()
        {
            InitializeComponent();
            this.BackColor = AppColors.Background;

            // Apply theme
            topBar.BackColor = AppColors.Surface;
            cmbSection.Font = AppFonts.BodyMd;
            lblStyle.Font = AppFonts.LabelMd;
            lblStyle.ForeColor = AppColors.OnSurfaceVar;
            cmbStyle.Font = AppFonts.BodyMd;
            cmbStyle.SelectedIndex = 0; // Default to Official UET Style
            chkMerge.Font = AppFonts.BodyMd;
            chkMerge.ForeColor = AppColors.OnSurface;

            lblTitleEdit.Font = AppFonts.LabelMd;
            lblTitleEdit.ForeColor = AppColors.OnSurfaceVar;
            txtTitle.Font = AppFonts.BodyMd;
            txtTitle.Text = "BSCS, UET Lahore (FSD Campus)";
            txtTitle.TextChanged += (s, e) => Refresh_Preview();

            lblDateTimeEdit.Font = AppFonts.LabelMd;
            lblDateTimeEdit.ForeColor = AppColors.OnSurfaceVar;
            lblDateTimeEdit.Text = "W.e.f Date:";
            txtDateTime.Font = AppFonts.BodyMd;
            txtDateTime.Text = "14-09-2026";
            txtDateTime.TextChanged += (s, e) => Refresh_Preview();

            lblInchargeEdit.Font = AppFonts.LabelMd;
            lblInchargeEdit.ForeColor = AppColors.OnSurfaceVar;
            txtIncharge.Font = AppFonts.BodyMd;
            txtIncharge.Text = "Mr. Mohsin Sheraz";
            txtIncharge.TextChanged += (s, e) => Refresh_Preview();

            btnRefresh.Font = AppFonts.LabelMd;
            btnRefresh.BackColor = AppColors.SurfaceHigh;
            btnRefresh.ForeColor = AppColors.OnSurface;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Cursor = Cursors.Hand;
            btnPrint.Font = AppFonts.BodyMd;
            btnPrint.BackColor = AppColors.Primary;
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.Cursor = Cursors.Hand;

            scrollPanel.BackColor = Color.FromArgb(235, 238, 245);
            previewPanel.BackColor = Color.FromArgb(235, 238, 245);
            previewPanel.Paint += PreviewPanel_Paint;
            scrollPanel.Resize += (s, e) => { UpdateScrollSize(); Refresh_Preview(); };

            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                null, previewPanel, new object[] { true });

            LoadData();
        }

        public PrintPreviewForm(Section initialSection) : this()
        {
            if (initialSection != null && cmbSection.DataSource is List<Section> secList)
            {
                int idx = secList.FindIndex(s => s.SectionId == initialSection.SectionId);
                if (idx >= 0)
                    cmbSection.SelectedIndex = idx;
            }
        }

        // ── Named event handlers (wired in Designer) ──
        private void CmbSection_Changed(object sender, EventArgs e)
        {
            if (cmbSection.SelectedItem is Section sec)
            {
                string dept = !string.IsNullOrWhiteSpace(sec.Department) ? sec.Department : "BSCS";
                txtTitle.Text = dept + ", UET Lahore (FSD Campus)";
            }
            Refresh_Preview();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            UpdateScrollSize();
            Refresh_Preview();
        }

        private void ChkMerge_Changed(object sender, EventArgs e)
        {
            isMerged = chkMerge.Checked;
            cmbSection.Enabled = !isMerged;
            UpdateScrollSize();
            Refresh_Preview();
        }

        private void CmbStyle_Changed(object sender, EventArgs e)
        {
            UpdateScrollSize();
            Refresh_Preview();
        }

        private void LoadData()
        {
            sections = DataManager.GetSections();
            slots    = DataManager.GetSlots();
            entries  = DataManager.GetEntries();
            subjects = DataManager.GetSubjects();
            teachers = DataManager.GetTeachers();
            cmbSection.DataSource = null;
            cmbSection.DataSource = sections;
            if (cmbSection.SelectedItem is Section sec)
            {
                string dept = !string.IsNullOrWhiteSpace(sec.Department) ? sec.Department : "BSCS";
                txtTitle.Text = dept + ", UET Lahore (FSD Campus)";
            }
        }

        private int GetSingleTableHeight()
        {
            if (cmbStyle != null && cmbStyle.SelectedIndex == 0)
            {
                int normalCount = slots != null ? slots.FindAll(s => !IsBreakSlot(s)).Count : 7;
                int breakCount = slots != null ? slots.FindAll(s => IsBreakSlot(s)).Count : 1;
                return 75 + 24 + 24 + 24 + (normalCount * 68) + (breakCount * 24) + 48;
            }
            if (cmbStyle != null && cmbStyle.SelectedIndex == 1)
                return 145 + (slots != null ? slots.Count * CELL_H : 0) + 75;
            return HEADER_H + DAY_ROW_H + (slots != null ? slots.Count * CELL_H : 0) + 50;
        }

        private int GetTotalTableWidth()
        {
            if (cmbStyle != null && cmbStyle.SelectedIndex == 0)
                return 100 + (days.Length * 116); // 680 px for Official UET
            return TIME_W + (days.Length * CELL_W);
        }

        private void UpdateScrollSize()
        {
            if (previewPanel == null || scrollPanel == null) return;
            int tableW = GetTotalTableWidth();
            int tableH = isMerged && sections != null
                ? (sections.Count * (GetSingleTableHeight() + TABLE_GAP)) + 80
                : GetSingleTableHeight() + 80;

            int targetW = Math.Max(scrollPanel.ClientSize.Width - 4, tableW + 80);
            int targetH = Math.Max(scrollPanel.ClientSize.Height - 4, tableH);
            previewPanel.Size = new Size(targetW, targetH);
        }

        private void Refresh_Preview()
        {
            previewPanel?.Invalidate();
        }

        private void PreviewPanel_Paint(object sender, PaintEventArgs e)
        {
            if (sections == null || slots == null) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            int tableW = GetTotalTableWidth();
            int panelW = previewPanel.ClientSize.Width;
            int startX = Math.Max(24, (panelW - tableW) / 2);
            int startY = 24;
            if (isMerged)
            {
                int offsetY = startY;
                for (int p = 0; p < sections.Count; p++)
                {
                    DrawTimetable(e.Graphics, startX, offsetY, sections[p], p + 1);
                    offsetY += GetSingleTableHeight() + TABLE_GAP;
                }
            }
            else
            {
                if (cmbSection.SelectedItem == null) return;
                DrawTimetable(e.Graphics, startX, startY, (Section)cmbSection.SelectedItem, 1);
            }
        }

        private string FormatTime(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return raw;
            if (raw.Contains(":")) return raw;
            int hr;
            if (int.TryParse(raw.Trim(), out hr)) return hr.ToString("00") + ":00";
            return raw;
        }

        private bool IsBreakSlot(TimeSlot slot)
        {
            if (slot == null) return false;
            string start = (slot.StartTime ?? "").Trim().ToLower();
            if (start.StartsWith("12") || start.Contains("12:"))
                return true;
            return false;
        }

        private Color GetSubjectBg(int subjectId) { int i = (subjectId-1)%subjectColors.Length; return subjectColors[i<0?0:i]; }
        private Color GetSubjectFg(int subjectId) { int i = (subjectId-1)%subjectTextColors.Length; return subjectTextColors[i<0?0:i]; }

        private TimetableEntry GetEntry(Section sec, int slotId, string day)
            => entries.Find(en => en.SectionId == sec.SectionId && en.SlotId == slotId && en.Day == day);

        private bool IsSameEntry(TimetableEntry a, TimetableEntry b)
        {
            if (a == null || b == null) return false;
            if (a.IsReserved && b.IsReserved) return true;
            if (!string.IsNullOrEmpty(a.CustomText) && !string.IsNullOrEmpty(b.CustomText)) return a.CustomText == b.CustomText;
            if (!a.IsReserved && string.IsNullOrEmpty(a.CustomText) && !b.IsReserved && string.IsNullOrEmpty(b.CustomText)) return a.SubjectId == b.SubjectId;
            return false;
        }

        private void DrawCenteredString(Graphics g, string text, Font font, Brush brush, int x, int y, int w, int h)
        {
            StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString(text, font, brush, new RectangleF(x, y, w, h), sf);
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius, bool roundTopOnly = false)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;
            if (d > rect.Width) d = rect.Width;
            if (d > rect.Height) d = rect.Height;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            if (roundTopOnly)
            {
                path.AddLine(rect.Right, rect.Y + d, rect.Right, rect.Bottom);
                path.AddLine(rect.Right, rect.Bottom, rect.X, rect.Bottom);
                path.AddLine(rect.X, rect.Bottom, rect.X, rect.Y + d);
            }
            else
            {
                path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
                path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            }
            path.CloseFigure();
            return path;
        }

        private Image uetLogo = null;
        private Image GetUetLogo()
        {
            if (uetLogo != null) return uetLogo;
            string[] paths = {
                System.IO.Path.Combine(Application.StartupPath, "Resources", "uet_logo.png"),
                System.IO.Path.Combine(Application.StartupPath, "uet_logo.png"),
                System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "uet_logo.png"),
                @"d:\C#\TTMS BY ZAIB\TTMS OOP\Resources\uet_logo.png",
                @"d:\C#\TTMS BY ZAIB\TTMS OOP\bin\Debug\Resources\uet_logo.png"
            };
            foreach (var p in paths)
            {
                if (System.IO.File.Exists(p))
                {
                    try { uetLogo = Image.FromFile(p); break; } catch { }
                }
            }
            return uetLogo;
        }

        private int DataManager_ParseTime(string timeStr)
        {
            if (string.IsNullOrWhiteSpace(timeStr)) return 0;
            string[] parts = timeStr.Split(new char[] { ':', ' ', '-' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) return 0;
            if (int.TryParse(parts[0], out int h))
            {
                int m = 0;
                if (parts.Length > 1 && int.TryParse(parts[1], out int mParsed)) m = mParsed;
                if (h >= 1 && h <= 7) h += 12;
                return h * 100 + m;
            }
            return 0;
        }

        private void DrawTimetable(Graphics g, int sx, int sy, Section sec, int pageNum = 1)
        {
            if (cmbStyle != null && cmbStyle.SelectedIndex == 1)
            {
                DrawTimetableExecutive(g, sx, sy, sec);
            }
            else if (cmbStyle != null && cmbStyle.SelectedIndex == 2)
            {
                DrawTimetableClassic(g, sx, sy, sec);
            }
            else
            {
                DrawTimetableOfficial(g, sx, sy, sec, pageNum);
            }
        }

        // ─────────────────────────────────────────
        // TEMPLATE 0: OFFICIAL UET STYLE
        // ─────────────────────────────────────────
        private void DrawTimetableOfficial(Graphics g, int sx, int sy, Section sec, int pageNum = 1)
        {
            if (slots == null || slots.Count == 0) return;

            int timeColW = 100;
            int dayColW = 116;
            int tableW = timeColW + (days.Length * dayColW); // 680 px

            Font titleFont = new Font("Times New Roman", 11.5f, FontStyle.Bold);
            Font subHeaderFont = new Font("Calibri", 9f, FontStyle.Bold);
            Font pageFont = new Font("Calibri", 9.5f, FontStyle.Bold);
            Font sectionBigFont = new Font("Times New Roman", 13.5f, FontStyle.Bold);
            Font regularInfoFont = new Font("Calibri", 9f, FontStyle.Regular);
            Font bannerFont = new Font("Calibri", 9.5f, FontStyle.Bold);
            Font dayHeaderFont = new Font("Calibri", 9.5f, FontStyle.Bold);
            Font timeSlotFont = new Font("Calibri", 8.5f, FontStyle.Bold);
            Font cellCodeFont = new Font("Calibri", 8.5f, FontStyle.Bold);
            Font cellNameFont = new Font("Calibri", 8f, FontStyle.Regular);
            Font cellTeacherFont = new Font("Calibri", 8f, FontStyle.Regular);
            Font cellRoomFont = new Font("Calibri", 7.5f, FontStyle.Regular);
            Font reservedFont = new Font("Calibri", 7.5f, FontStyle.Bold);
            Font breakXFont = new Font("Calibri", 9.5f, FontStyle.Bold);
            Font inchargeFont = new Font("Calibri", 9.5f, FontStyle.Bold);

            Pen pen = Pens.Black;
            Brush brush = Brushes.Black;
            StringFormat centerSf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // White canvas fill for the table
            int totalExpectedH = GetSingleTableHeight();
            g.FillRectangle(Brushes.White, sx - 10, sy - 10, tableW + 20, totalExpectedH + 20);

            // 1. TOP HEADER BLOCK (Height = 75)
            // Logo Box (Column 0: Time)
            g.DrawRectangle(pen, sx, sy, timeColW, 75);
            Image logo = GetUetLogo();
            if (logo != null)
            {
                int logoW = 64, logoH = 58;
                int logoX = sx + (timeColW - logoW) / 2;
                int logoY = sy + (75 - logoH) / 2;
                g.DrawImage(logo, logoX, logoY, logoW, logoH);
            }

            // Center Box (Columns 1, 2, 3: width = dayColW * 3 = 348)
            int centerW = dayColW * 3;
            g.DrawRectangle(pen, sx + timeColW, sy, centerW, 75);
            // Split horizontally at sy + 36
            g.DrawLine(pen, sx + timeColW, sy + 36, sx + timeColW + centerW, sy + 36);

            // Department Title
            string dept = !string.IsNullOrWhiteSpace(sec.Department) ? sec.Department : "BSCS";
            string deptTitle = dept + ", UET Lahore (FSD Campus)";
            if (txtTitle != null && !string.IsNullOrWhiteSpace(txtTitle.Text) && !txtTitle.Text.StartsWith("DEPARTMENT"))
                deptTitle = txtTitle.Text.Trim();
            g.DrawString(deptTitle, titleFont, brush, new RectangleF(sx + timeColW, sy, centerW, 36), centerSf);

            // Sub-box divider at Col 3 start (after 2 day columns)
            int subBox1W = dayColW * 2;
            g.DrawLine(pen, sx + timeColW + subBox1W, sy + 36, sx + timeColW + subBox1W, sy + 75);

            // Left sub-box: Section Wise Time Table (Fall 2026)
            g.DrawString("Section Wise Time Table\n(Fall 2026)", subHeaderFont, brush, new RectangleF(sx + timeColW, sy + 36, subBox1W, 39), centerSf);

            // Right sub-box: Rev: 1.0
            g.DrawString("Rev: 1.0", subHeaderFont, brush, new RectangleF(sx + timeColW + subBox1W, sy + 36, dayColW, 39), centerSf);

            // Right Box (Column 4: Page 1)
            g.DrawRectangle(pen, sx + timeColW + centerW, sy, dayColW, 75);
            g.DrawString("Page " + pageNum, pageFont, brush, new RectangleF(sx + timeColW + centerW, sy, dayColW, 75), centerSf);

            // 2. SECTION & SEMESTER INFO ROW (curY = sy + 75, Height = 24)
            int curY = sy + 75;
            g.DrawRectangle(pen, sx, curY, tableW, 24);

            // Box 1: Section A (width = timeColW)
            g.DrawLine(pen, sx + timeColW, curY, sx + timeColW, curY + 24);
            string secName = "Section " + (sec.Name ?? "A");
            g.DrawString(secName, sectionBigFont, brush, new RectangleF(sx + 4, curY, timeColW - 6, 24), new StringFormat { LineAlignment = StringAlignment.Center });

            // Box 2: Semester (width = dayColW)
            g.DrawLine(pen, sx + timeColW + dayColW, curY, sx + timeColW + dayColW, curY + 24);
            g.DrawString(sec.Semester ?? "1st Semester", regularInfoFont, brush, new RectangleF(sx + timeColW, curY, dayColW, 24), centerSf);

            // Box 3: Empty divider (width = dayColW * 2)
            g.DrawLine(pen, sx + timeColW + dayColW * 3, curY, sx + timeColW + dayColW * 3, curY + 24);

            // Box 4: W.e.f date (width = dayColW * 2)
            string wefStr = txtDateTime != null && !string.IsNullOrWhiteSpace(txtDateTime.Text) && txtDateTime.Text.Contains("-") 
                ? txtDateTime.Text.Trim() 
                : "14-09-2026";
            g.DrawString("W.e.f: " + wefStr, regularInfoFont, brush, new RectangleF(sx + timeColW + dayColW * 3, curY, dayColW * 2, 24), centerSf);

            // 3. SESSION BANNER ROW (curY += 24, Height = 24)
            curY += 24;
            g.DrawRectangle(pen, sx, curY, tableW, 24);
            string sessionText = !string.IsNullOrWhiteSpace(sec.Session) ? sec.Session : "Session 2026 (1st semester)";
            g.DrawString(sessionText, bannerFont, brush, new RectangleF(sx, curY, tableW, 24), centerSf);

            // 4. DAYS HEADER ROW (curY += 24, Height = 24)
            curY += 24;
            g.DrawRectangle(pen, sx, curY, timeColW, 24);
            for (int d = 0; d < days.Length; d++)
            {
                int dx = sx + timeColW + (d * dayColW);
                g.DrawRectangle(pen, dx, curY, dayColW, 24);
                g.DrawString(days[d], dayHeaderFont, brush, new RectangleF(dx, curY, dayColW, 24), centerSf);
            }

            // 5. TIME SLOTS ROWS
            curY += 24;

            var sortedSlots = new List<TimeSlot>(slots);
            sortedSlots.Sort((a, b) => DataManager_ParseTime(a.StartTime).CompareTo(DataManager_ParseTime(b.StartTime)));

            bool[,] drawn = new bool[sortedSlots.Count, days.Length];

            for (int r = 0; r < sortedSlots.Count; r++)
            {
                var slot = sortedSlots[r];
                bool isBreak = IsBreakSlot(slot);
                int slotH = isBreak ? 24 : 68;

                // Time cell
                g.DrawRectangle(pen, sx, curY, timeColW, slotH);
                string timeStr = FormatTime(slot.StartTime) + " - " + FormatTime(slot.EndTime);
                g.DrawString(timeStr, timeSlotFont, brush, new RectangleF(sx, curY, timeColW, slotH), centerSf);

                for (int d = 0; d < days.Length; d++)
                {
                    if (drawn[r, d]) continue;

                    int cellX = sx + timeColW + (d * dayColW);

                    // 12:00 - 01:00 Break slot
                    if (isBreak)
                    {
                        g.DrawRectangle(pen, cellX, curY, dayColW, slotH);
                        g.DrawString("x", breakXFont, brush, new RectangleF(cellX, curY, dayColW, slotH), centerSf);
                        drawn[r, d] = true;
                        continue;
                    }

                    // Friday 01:00 - 02:00 prayer break
                    string slotStart = (slot.StartTime ?? "").Trim();
                    bool isFri1to2 = (d == 4 && (slotStart == "1" || slotStart == "01:00" || slotStart.StartsWith("1:")));
                    var entry = GetEntry(sec, slot.SlotId, days[d]);

                    if (isFri1to2 && entry == null)
                    {
                        g.DrawRectangle(pen, cellX, curY, dayColW, slotH);
                        g.DrawString("x", breakXFont, brush, new RectangleF(cellX, curY, dayColW, slotH), centerSf);
                        drawn[r, d] = true;
                        continue;
                    }

                    if (entry == null)
                    {
                        g.DrawRectangle(pen, cellX, curY, dayColW, slotH);
                        drawn[r, d] = true;
                        continue;
                    }

                    // Multi-hour merging
                    int span = 1;
                    int mergedH = slotH;
                    for (int nr = r + 1; nr < sortedSlots.Count; nr++)
                    {
                        var nextSlot = sortedSlots[nr];
                        if (IsBreakSlot(nextSlot)) break;
                        var nextEntry = GetEntry(sec, nextSlot.SlotId, days[d]);
                        if (IsSameEntry(entry, nextEntry))
                        {
                            span++;
                            drawn[nr, d] = true;
                            mergedH += 68;
                        }
                        else break;
                    }

                    drawn[r, d] = true;
                    Rectangle cellRect = new Rectangle(cellX, curY, dayColW, mergedH);
                    g.DrawRectangle(pen, cellRect);

                    if (entry.IsReserved)
                    {
                        g.DrawString("RESERVED FOR\nTUTORIAL/\nSEMINAR/\nCOUNSELLING\nSESSIONS", reservedFont, brush, new RectangleF(cellX + 2, curY, dayColW - 4, mergedH), centerSf);
                    }
                    else if (!string.IsNullOrEmpty(entry.CustomText))
                    {
                        g.DrawString(entry.CustomText, cellNameFont, brush, new RectangleF(cellX + 2, curY, dayColW - 4, mergedH), centerSf);
                    }
                    else
                    {
                        Subject subj = subjects.Find(s => s.SubjectId == entry.SubjectId);
                        int tid = entry.TeacherId > 0 ? entry.TeacherId : (subj != null ? subj.TeacherId : 0);
                        Teacher tchr = tid > 0 ? teachers.Find(t => t.TeacherId == tid) : null;

                        DrawOfficialCellText(g, cellRect, subj, tchr, cellCodeFont, cellNameFont, cellTeacherFont, cellRoomFont);
                    }
                }

                curY += slotH;
            }

            int totalH = curY;

            // 6. FOOTER
            string incharge = txtIncharge != null && !string.IsNullOrWhiteSpace(txtIncharge.Text) ? txtIncharge.Text.Trim() : "Mr. Mohsin Sheraz";
            string inchargeText = "Time table Incharge: " + incharge;
            SizeF inchargeSz = g.MeasureString(inchargeText, inchargeFont);
            g.DrawString(inchargeText, inchargeFont, brush, sx + tableW - inchargeSz.Width, totalH + 10);

            // Stealth watermark 1258
            Font hiddenFont = new Font("Segoe UI", 6.5f, FontStyle.Regular);
            Brush hiddenBrush = new SolidBrush(Color.FromArgb(195, 202, 215));
            g.DrawString("1258", hiddenFont, hiddenBrush, sx + tableW - 24, totalH + 28);
        }

        private void DrawOfficialCellText(Graphics g, Rectangle rect, Subject subj, Teacher tchr, Font codeFont, Font nameFont, Font teacherFont, Font roomFont)
        {
            if (subj == null) return;

            string code = (subj.CourseCode ?? "").Trim();
            string name = (subj.Name ?? "").Trim();
            string teacher = tchr != null ? tchr.ToString() : "";
            string room = "";

            if (name.Contains("TF-03") || name.Contains("TF- 03"))
            {
                room = "BSH TF-03";
                name = name.Replace("BSH TF-03", "").Replace("TF-03", "").Replace("TF- 03", "").Trim();
            }

            if (!string.IsNullOrEmpty(code) && name.StartsWith(code, StringComparison.OrdinalIgnoreCase))
            {
                name = name.Substring(code.Length).Trim();
            }

            List<Tuple<string, Font>> lines = new List<Tuple<string, Font>>();
            if (!string.IsNullOrEmpty(code))
                lines.Add(Tuple.Create(code, codeFont));
            if (!string.IsNullOrEmpty(name))
                lines.Add(Tuple.Create(name, nameFont));
            if (!string.IsNullOrEmpty(teacher))
                lines.Add(Tuple.Create(teacher, teacherFont));
            if (!string.IsNullOrEmpty(room))
                lines.Add(Tuple.Create(room, roomFont));

            float totalH = 0;
            float lineSpacing = 1.5f;
            List<float> heights = new List<float>();
            foreach (var item in lines)
            {
                SizeF sz = g.MeasureString(item.Item1, item.Item2, rect.Width - 4);
                heights.Add(sz.Height);
                totalH += sz.Height + lineSpacing;
            }

            float curY = rect.Y + Math.Max(2, (rect.Height - totalH) / 2);
            StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Near,
                Trimming = StringTrimming.EllipsisWord
            };

            for (int i = 0; i < lines.Count; i++)
            {
                RectangleF lineRect = new RectangleF(rect.X + 2, curY, rect.Width - 4, heights[i]);
                g.DrawString(lines[i].Item1, lines[i].Item2, Brushes.Black, lineRect, sf);
                curY += heights[i] + lineSpacing;
            }
        }

        // ─────────────────────────────────────────
        // TEMPLATE 1: CLASSIC STYLE
        // ─────────────────────────────────────────
        private void DrawTimetableClassic(Graphics g, int sx, int sy, Section sec)
        {
            if (slots == null || slots.Count == 0) return;

            Font timeFont    = new Font("Segoe UI",  8f, FontStyle.Bold);
            Font dayFont     = new Font("Segoe UI",  9f, FontStyle.Bold);
            Font subjFont    = new Font("Segoe UI",  8f, FontStyle.Bold);
            Font teacherFont = new Font("Segoe UI",  7.5f, FontStyle.Regular);
            Font titleFont   = new Font("Segoe UI", 13f, FontStyle.Bold);
            Font subtitleFont= new Font("Segoe UI",  8.5f, FontStyle.Regular);
            Font footerFont  = new Font("Segoe UI",  8f, FontStyle.Italic);
            Font badgeFont   = new Font("Segoe UI",  6f, FontStyle.Bold);
            Font spanFont    = new Font("Segoe UI",  7f, FontStyle.Italic);

            Pen   borderPen  = new Pen(Color.FromArgb(200, 210, 230), 1f);
            Brush headerBrush= new SolidBrush(AppColors.Primary);
            Brush whiteBrush = Brushes.White;
            Brush darkBrush  = new SolidBrush(Color.FromArgb(20, 50, 120));
            int   tableW     = TIME_W + (days.Length * CELL_W);

            // Header
            g.FillRectangle(headerBrush, sx, sy, tableW, HEADER_H);
            string mainTitle = txtTitle != null && !string.IsNullOrWhiteSpace(txtTitle.Text) ? txtTitle.Text.Trim() : "BSCS, UET Lahore (FSD Campus)";
            g.DrawString(mainTitle, titleFont, whiteBrush, sx + 10, sy + 8);
            
            string dtStr = txtDateTime != null && !string.IsNullOrWhiteSpace(txtDateTime.Text) ? txtDateTime.Text.Trim() : DateTime.Now.ToString("dd-MMM-yyyy · hh:mm tt");
            SizeF dtSize = g.MeasureString(dtStr, subtitleFont);
            g.DrawString(dtStr, subtitleFont, new SolidBrush(Color.FromArgb(210, 230, 255)), sx + tableW - dtSize.Width - 10, sy + 10);

            List<string> subParts = new List<string>();
            if (!string.IsNullOrWhiteSpace(sec.Name) && !sec.Name.Equals("section", StringComparison.OrdinalIgnoreCase))
                subParts.Add(sec.Name.StartsWith("Section", StringComparison.OrdinalIgnoreCase) ? sec.Name : "Section " + sec.Name);
            if (!string.IsNullOrWhiteSpace(sec.Session)) subParts.Add("Session " + sec.Session);
            if (!string.IsNullOrWhiteSpace(sec.Semester)) subParts.Add(sec.Semester);
            string subTitle = string.Join("  ·  ", subParts);
            g.DrawString(subTitle, subtitleFont, new SolidBrush(Color.FromArgb(200, 255, 255, 255)), sx + 10, sy + 36);

            int tableY = sy + HEADER_H;

            // Day header row
            g.FillRectangle(darkBrush, sx, tableY, TIME_W, DAY_ROW_H);
            g.DrawRectangle(borderPen, sx, tableY, TIME_W, DAY_ROW_H);
            DrawCenteredString(g, "TIME", dayFont, whiteBrush, sx, tableY, TIME_W, DAY_ROW_H);
            for (int d = 0; d < days.Length; d++)
            {
                int x = sx + TIME_W + (d * CELL_W);
                g.FillRectangle(darkBrush, x, tableY, CELL_W, DAY_ROW_H);
                g.DrawRectangle(borderPen, x, tableY, CELL_W, DAY_ROW_H);
                DrawCenteredString(g, days[d], dayFont, whiteBrush, x, tableY, CELL_W, DAY_ROW_H);
            }

            bool[,] drawn = new bool[slots.Count, days.Length];

            for (int r = 0; r < slots.Count; r++)
            {
                int y = tableY + DAY_ROW_H + (r * CELL_H);
                Color rowBg = r % 2 == 0 ? Color.White : Color.FromArgb(248, 250, 255);

                if (IsBreakSlot(slots[r]))
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(254, 246, 235)), sx, y, TIME_W, CELL_H);
                    g.DrawRectangle(borderPen, sx, y, TIME_W, CELL_H);
                    string sf2 = FormatTime(slots[r].StartTime), ef2 = FormatTime(slots[r].EndTime);
                    DrawCenteredString(g, sf2 + "\n-\n" + ef2, timeFont, new SolidBrush(Color.FromArgb(160, 95, 15)), sx, y, TIME_W, CELL_H);

                    int bannerW = days.Length * CELL_W;
                    for (int d = 0; d < days.Length; d++)
                    {
                        int bx = sx + TIME_W + (d * CELL_W);
                        g.FillRectangle(new SolidBrush(Color.FromArgb(255, 252, 245)), bx, y, CELL_W, CELL_H);
                        g.DrawRectangle(borderPen, bx, y, CELL_W, CELL_H);
                        drawn[r, d] = true;
                    }

                    Rectangle breakCardRect = new Rectangle(sx + TIME_W + 4, y + 4, bannerW - 8, CELL_H - 8);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(254, 243, 220)), breakCardRect);
                    g.DrawRectangle(new Pen(Color.FromArgb(235, 205, 150), 1.2f), breakCardRect);

                    Font breakFont = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                    DrawCenteredString(g, "L U N C H   &   P R A Y E R   B R E A K", breakFont, new SolidBrush(Color.FromArgb(145, 80, 10)), breakCardRect.X, breakCardRect.Y, breakCardRect.Width, breakCardRect.Height);
                    continue;
                }

                g.FillRectangle(new SolidBrush(Color.FromArgb(235, 240, 255)), sx, y, TIME_W, CELL_H);
                g.DrawRectangle(borderPen, sx, y, TIME_W, CELL_H);
                string sf3 = FormatTime(slots[r].StartTime), ef3 = FormatTime(slots[r].EndTime);
                SizeF ts = g.MeasureString(sf3, timeFont);
                float tx = sx + (TIME_W - ts.Width) / 2f;
                g.DrawString(sf3, timeFont, new SolidBrush(AppColors.Primary), tx, y + 8);
                g.DrawString("-", teacherFont, new SolidBrush(AppColors.OnSurfaceVar), sx + TIME_W/2 - 4, y + 24);
                g.DrawString(ef3, timeFont, new SolidBrush(AppColors.Primary), tx, y + 36);

                for (int d = 0; d < days.Length; d++)
                {
                    if (drawn[r, d]) continue;
                    int x = sx + TIME_W + (d * CELL_W);
                    TimetableEntry entry = GetEntry(sec, slots[r].SlotId, days[d]);
                    if (entry == null)
                    {
                        g.FillRectangle(new SolidBrush(rowBg), x, y, CELL_W, CELL_H);
                        g.DrawRectangle(borderPen, x, y, CELL_W, CELL_H);
                        continue;
                    }
                    int span = 1;
                    for (int nr = r + 1; nr < slots.Count; nr++)
                    {
                        TimetableEntry nx = GetEntry(sec, slots[nr].SlotId, days[d]);
                        if (IsSameEntry(entry, nx)) { span++; drawn[nr, d] = true; } else break;
                    }
                    int mergedH = span * CELL_H;
                    if (entry.IsReserved)
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(245, 245, 248)), x, y, CELL_W, mergedH);
                        g.DrawRectangle(borderPen, x, y, CELL_W, mergedH);
                        DrawCenteredString(g, "RESERVED\nTutorial/Seminar", teacherFont, new SolidBrush(Color.FromArgb(150,150,160)), x, y, CELL_W, mergedH);
                    }
                    else if (!string.IsNullOrEmpty(entry.CustomText))
                    {
                        Color bg = GetSubjectBg(100), fg = GetSubjectFg(100);
                        g.FillRectangle(new SolidBrush(bg), x, y, CELL_W, mergedH);
                        g.DrawRectangle(borderPen, x, y, CELL_W, mergedH);
                        g.FillRectangle(new SolidBrush(fg), x, y, 4, mergedH);
                        DrawCenteredString(g, entry.CustomText, subjFont, new SolidBrush(fg), x+8, y, CELL_W-16, mergedH);
                    }
                    else
                    {
                        Subject subj = subjects.Find(s => s.SubjectId == entry.SubjectId);
                        int tid = entry.TeacherId > 0 ? entry.TeacherId : (subj != null ? subj.TeacherId : 0);
                        Teacher tchr = tid > 0 ? teachers.Find(t => t.TeacherId == tid) : null;
                        if (subj != null)
                        {
                            Color bg = GetSubjectBg(subj.SubjectId), fg = GetSubjectFg(subj.SubjectId);
                            g.FillRectangle(new SolidBrush(bg), x, y, CELL_W, mergedH);
                            g.DrawRectangle(borderPen, x, y, CELL_W, mergedH);
                            g.FillRectangle(new SolidBrush(fg), x, y, 4, mergedH);
                            int labReserve = subj.IsLab ? 36 : 0;
                            RectangleF subjRect = new RectangleF(x + 8, y + 7, CELL_W - 16 - labReserve, 20);
                            using (StringFormat sf = new StringFormat { Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap, LineAlignment = StringAlignment.Center })
                            {
                                g.DrawString(subj.Name ?? "", subjFont, new SolidBrush(fg), subjRect, sf);
                            }

                            if (tchr != null)
                            {
                                int codeReserve = !string.IsNullOrWhiteSpace(subj.CourseCode) && span == 1 ? 55 : 0;
                                RectangleF teacherRect = new RectangleF(x + 8, y + 25, CELL_W - 16 - codeReserve, 18);
                                using (StringFormat sf = new StringFormat { Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap, LineAlignment = StringAlignment.Center })
                                {
                                    g.DrawString(tchr.ToString(), teacherFont, new SolidBrush(Color.FromArgb(160, fg.R, fg.G, fg.B)), teacherRect, sf);
                                }
                            }
                            if (subj.IsLab)
                            {
                                g.FillRectangle(new SolidBrush(fg), x + CELL_W - 32, y + 6, 26, 13);
                                g.DrawString("LAB", badgeFont, whiteBrush, x + CELL_W - 30, y + 7);
                            }
                            if (span > 1)
                            {
                                string dur = FormatTime(slots[r].StartTime) + " - " + FormatTime(slots[r+span-1].EndTime);
                                g.DrawString(dur, spanFont, new SolidBrush(Color.FromArgb(160, fg.R, fg.G, fg.B)), x + 8, y + mergedH - 16);
                                Pen bp = new Pen(fg, 1.5f);
                                g.DrawLine(bp, x + CELL_W - 8, y + 6, x + CELL_W - 8, y + mergedH - 6);
                                g.DrawLine(bp, x + CELL_W - 8, y + 6, x + CELL_W - 4, y + 6);
                                g.DrawLine(bp, x + CELL_W - 8, y + mergedH - 6, x + CELL_W - 4, y + mergedH - 6);
                            }
                            if (!string.IsNullOrWhiteSpace(subj.CourseCode))
                            {
                                string cCode = subj.CourseCode.Trim();
                                Font codeFont = new Font("Segoe UI", 7f, FontStyle.Bold);
                                SizeF codeSize = g.MeasureString(cCode, codeFont);
                                float codeX = x + CELL_W - codeSize.Width - 8;
                                float codeY = y + mergedH - 16;
                                g.DrawString(cCode, codeFont, new SolidBrush(Color.FromArgb(160, fg.R, fg.G, fg.B)), codeX, codeY);
                            }
                        }
                        else
                        {
                            g.FillRectangle(new SolidBrush(rowBg), x, y, CELL_W, mergedH);
                            g.DrawRectangle(borderPen, x, y, CELL_W, mergedH);
                        }
                    }
                }
            }

            int totalH = tableY + DAY_ROW_H + (slots.Count * CELL_H);
            g.DrawLine(new Pen(AppColors.Primary, 2), sx, totalH, sx+tableW, totalH);
            string incharge = txtIncharge != null && !string.IsNullOrWhiteSpace(txtIncharge.Text) ? txtIncharge.Text.Trim() : "Mr. Mohsin Sheraz";
            g.DrawString("Timetable Incharge:  " + incharge, footerFont, new SolidBrush(AppColors.OnSurfaceVar), sx, totalH+10);
            
            // Hidden roll number tag placed discreetly in the bottom-right corner
            Font hiddenFont = new Font("Segoe UI", 6.5f, FontStyle.Regular);
            Brush hiddenBrush = new SolidBrush(Color.FromArgb(195, 202, 215));
            SizeF rollSz = g.MeasureString("1258", hiddenFont);
            g.DrawString("1258", hiddenFont, hiddenBrush, sx + tableW - rollSz.Width - 4, totalH + 26);
        }

        // ─────────────────────────────────────────
        // TEMPLATE 2: EXECUTIVE MODERN
        // ─────────────────────────────────────────
        private void DrawTimetableExecutive(Graphics g, int sx, int sy, Section sec)
        {
            if (slots == null || slots.Count == 0) return;

            Font headerTitleFont = new Font("Segoe UI", 12f, FontStyle.Bold);
            Font headerSubFont   = new Font("Segoe UI",  8.5f, FontStyle.Regular);
            Font infoLabelFont   = new Font("Segoe UI",  9f, FontStyle.Regular);
            Font infoValFont     = new Font("Segoe UI",  9f, FontStyle.Bold);
            Font timeFont        = new Font("Segoe UI",  8f, FontStyle.Bold);
            Font dayFont         = new Font("Segoe UI",  9f, FontStyle.Bold);
            Font subjFont        = new Font("Segoe UI",  8.5f, FontStyle.Bold);
            Font teacherFont     = new Font("Segoe UI",  7.5f, FontStyle.Regular);
            Font badgeFont       = new Font("Segoe UI",  6.5f, FontStyle.Bold);
            Font spanFont        = new Font("Segoe UI",  7f, FontStyle.Italic);

            Pen borderPen = new Pen(Color.FromArgb(215, 222, 232), 1f);
            Brush darkHeaderBrush = new SolidBrush(Color.FromArgb(15, 34, 64));
            Brush whiteBrush = Brushes.White;
            int tableW = TIME_W + (days.Length * CELL_W);

            // 1. Top Executive Banner (Rounded Top Corners)
            int bannerH = 50;
            Rectangle bannerRect = new Rectangle(sx, sy, tableW, bannerH);
            using (GraphicsPath path = GetRoundedPath(bannerRect, 8, true))
            {
                g.FillPath(darkHeaderBrush, path);
            }
            string execTitle = txtTitle != null && !string.IsNullOrWhiteSpace(txtTitle.Text) ? txtTitle.Text.Trim() : "DEPARTMENT OF COMPUTER SCIENCE";
            g.DrawString(execTitle, headerTitleFont, whiteBrush, sx + 16, sy + 14);

            string execRight = txtDateTime != null && !string.IsNullOrWhiteSpace(txtDateTime.Text) ? txtDateTime.Text.Trim() : DateTime.Now.ToString("dddd, dd MMMM yyyy · hh:mm tt");
            SizeF rightSize = g.MeasureString(execRight, headerSubFont);
            g.DrawString(execRight, headerSubFont, new SolidBrush(Color.FromArgb(210, 225, 250)), sx + tableW - rightSize.Width - 16, sy + 17);

            // 2. Info Bar (Rounded Pill Card)
            int infoY = sy + bannerH + 10;
            int infoH = 36;
            Rectangle infoRect = new Rectangle(sx, infoY, tableW, infoH);
            using (GraphicsPath path = GetRoundedPath(infoRect, 8))
            {
                g.FillPath(new SolidBrush(Color.FromArgb(248, 250, 254)), path);
                g.DrawPath(borderPen, path);
            }

            string rawSem = string.IsNullOrEmpty(sec.Semester) ? "General" : sec.Semester.Trim();
            string cleanSem = rawSem;
            if (cleanSem.EndsWith(" Semester", StringComparison.OrdinalIgnoreCase))
            {
                cleanSem = cleanSem.Substring(0, cleanSem.Length - 9).Trim();
            }
            else if (cleanSem.EndsWith("Semester", StringComparison.OrdinalIgnoreCase) && cleanSem.Length > 8)
            {
                cleanSem = cleanSem.Substring(0, cleanSem.Length - 8).Trim();
            }
            else if (cleanSem.StartsWith("Semester ", StringComparison.OrdinalIgnoreCase))
            {
                cleanSem = cleanSem.Substring(9).Trim();
            }
            if (string.IsNullOrEmpty(cleanSem)) cleanSem = rawSem;

            string rawSecName = sec != null ? (sec.Name ?? "").Trim() : "";
            bool hasSectionName = !string.IsNullOrWhiteSpace(rawSecName) &&
                                  !rawSecName.Equals("section", StringComparison.OrdinalIgnoreCase) &&
                                  !rawSecName.Equals("n/a", StringComparison.OrdinalIgnoreCase) &&
                                  !rawSecName.Equals("none", StringComparison.OrdinalIgnoreCase) &&
                                  !rawSecName.Equals("-", StringComparison.OrdinalIgnoreCase);

            if (hasSectionName)
            {
                // Left info (Semester)
                int ix = sx + 16;
                string semLblText = "Semester: ";
                g.DrawString(semLblText, infoLabelFont, new SolidBrush(Color.FromArgb(80, 90, 110)), ix, infoY + 9);
                SizeF semLblSize = g.MeasureString(semLblText, infoLabelFont);
                g.DrawString(cleanSem, infoValFont, new SolidBrush(Color.FromArgb(15, 34, 64)), ix + semLblSize.Width, infoY + 9);

                // Right info (Section)
                string secNameDisplay = rawSecName.StartsWith("Section", StringComparison.OrdinalIgnoreCase) ? rawSecName : "Section: " + rawSecName;
                string secLblText = rawSecName.StartsWith("Section", StringComparison.OrdinalIgnoreCase) ? "" : "Section: ";
                if (!string.IsNullOrEmpty(secLblText))
                {
                    SizeF secLblSize = g.MeasureString(secLblText, infoLabelFont);
                    SizeF secValSize = g.MeasureString(rawSecName, infoValFont);
                    float rx = sx + tableW - 16 - secLblSize.Width - secValSize.Width;
                    g.DrawString(secLblText, infoLabelFont, new SolidBrush(Color.FromArgb(80, 90, 110)), rx, infoY + 9);
                    g.DrawString(rawSecName, infoValFont, new SolidBrush(Color.FromArgb(15, 34, 64)), rx + secLblSize.Width, infoY + 9);
                }
                else
                {
                    SizeF secValSize = g.MeasureString(rawSecName, infoValFont);
                    float rx = sx + tableW - 16 - secValSize.Width;
                    g.DrawString(rawSecName, infoValFont, new SolidBrush(Color.FromArgb(15, 34, 64)), rx, infoY + 9);
                }
            }
            else
            {
                // Center Semester info when no specific section exists
                string semLblText = "Semester: ";
                SizeF semLblSize = g.MeasureString(semLblText, infoLabelFont);
                SizeF semValSize = g.MeasureString(cleanSem, infoValFont);
                float totalW = semLblSize.Width + semValSize.Width;
                float startX = sx + (tableW - totalW) / 2f;

                g.DrawString(semLblText, infoLabelFont, new SolidBrush(Color.FromArgb(80, 90, 110)), startX, infoY + 9);
                g.DrawString(cleanSem, infoValFont, new SolidBrush(Color.FromArgb(15, 34, 64)), startX + semLblSize.Width, infoY + 9);
            }

            // 3. Day Headers Row
            int tableY = infoY + infoH + 12;
            g.FillRectangle(new SolidBrush(Color.FromArgb(240, 244, 250)), sx, tableY, TIME_W, DAY_ROW_H);
            g.DrawRectangle(borderPen, sx, tableY, TIME_W, DAY_ROW_H);
            DrawCenteredString(g, "Time", dayFont, new SolidBrush(Color.FromArgb(20, 50, 100)), sx, tableY, TIME_W, DAY_ROW_H);

            for (int d = 0; d < days.Length; d++)
            {
                int x = sx + TIME_W + (d * CELL_W);
                g.FillRectangle(new SolidBrush(Color.FromArgb(240, 244, 250)), x, tableY, CELL_W, DAY_ROW_H);
                g.DrawRectangle(borderPen, x, tableY, CELL_W, DAY_ROW_H);
                DrawCenteredString(g, days[d], dayFont, new SolidBrush(Color.FromArgb(20, 50, 100)), x, tableY, CELL_W, DAY_ROW_H);
            }

            // 4. Slots Grid
            bool[,] drawn = new bool[slots.Count, days.Length];

            for (int r = 0; r < slots.Count; r++)
            {
                int y = tableY + DAY_ROW_H + (r * CELL_H);
                Color rowBg = r % 2 == 0 ? Color.White : Color.FromArgb(250, 252, 255);

                if (IsBreakSlot(slots[r]))
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(254, 246, 235)), sx, y, TIME_W, CELL_H);
                    g.DrawRectangle(borderPen, sx, y, TIME_W, CELL_H);

                    string sf2 = FormatTime(slots[r].StartTime), ef2 = FormatTime(slots[r].EndTime);
                    string timeRange = sf2 + " -\n" + ef2;
                    DrawCenteredString(g, timeRange, timeFont, new SolidBrush(Color.FromArgb(160, 95, 15)), sx, y, TIME_W, CELL_H);

                    int bannerW = days.Length * CELL_W;
                    for (int d = 0; d < days.Length; d++)
                    {
                        int bx = sx + TIME_W + (d * CELL_W);
                        g.FillRectangle(new SolidBrush(Color.FromArgb(255, 252, 245)), bx, y, CELL_W, CELL_H);
                        g.DrawRectangle(borderPen, bx, y, CELL_W, CELL_H);
                        drawn[r, d] = true;
                    }

                    Rectangle breakCardRect = new Rectangle(sx + TIME_W + 5, y + 4, bannerW - 10, CELL_H - 8);
                    using (GraphicsPath path = GetRoundedPath(breakCardRect, 6))
                    {
                        using (LinearGradientBrush breakGrad = new LinearGradientBrush(breakCardRect, Color.FromArgb(255, 248, 232), Color.FromArgb(254, 238, 205), LinearGradientMode.Horizontal))
                        {
                            g.FillPath(breakGrad, path);
                        }
                        g.DrawPath(new Pen(Color.FromArgb(235, 205, 150), 1.2f), path);
                    }

                    Font breakFont = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                    DrawCenteredString(g, "L U N C H   &   P R A Y E R   B R E A K", breakFont, new SolidBrush(Color.FromArgb(145, 80, 10)), breakCardRect.X, breakCardRect.Y, breakCardRect.Width, breakCardRect.Height);
                    continue;
                }

                g.FillRectangle(new SolidBrush(Color.FromArgb(244, 247, 252)), sx, y, TIME_W, CELL_H);
                g.DrawRectangle(borderPen, sx, y, TIME_W, CELL_H);

                string sf3 = FormatTime(slots[r].StartTime), ef3 = FormatTime(slots[r].EndTime);
                string timeRangeStandard = sf3 + " -\n" + ef3;
                DrawCenteredString(g, timeRangeStandard, timeFont, new SolidBrush(Color.FromArgb(25, 60, 110)), sx, y, TIME_W, CELL_H);

                for (int d = 0; d < days.Length; d++)
                {
                    if (drawn[r, d]) continue;
                    int x = sx + TIME_W + (d * CELL_W);
                    TimetableEntry entry = GetEntry(sec, slots[r].SlotId, days[d]);
                    if (entry == null)
                    {
                        g.FillRectangle(new SolidBrush(rowBg), x, y, CELL_W, CELL_H);
                        g.DrawRectangle(borderPen, x, y, CELL_W, CELL_H);
                        continue;
                    }

                    int span = 1;
                    for (int nr = r + 1; nr < slots.Count; nr++)
                    {
                        TimetableEntry nx = GetEntry(sec, slots[nr].SlotId, days[d]);
                        if (IsSameEntry(entry, nx)) { span++; drawn[nr, d] = true; } else break;
                    }
                    int mergedH = span * CELL_H;

                    // Draw underlying cell border & background
                    g.FillRectangle(new SolidBrush(rowBg), x, y, CELL_W, mergedH);
                    g.DrawRectangle(borderPen, x, y, CELL_W, mergedH);

                    if (entry.IsReserved)
                    {
                        Rectangle cardRect = new Rectangle(x + 4, y + 4, CELL_W - 8, mergedH - 8);
                        using (GraphicsPath path = GetRoundedPath(cardRect, 6))
                        {
                            g.FillPath(new SolidBrush(Color.FromArgb(246, 247, 250)), path);
                            g.DrawPath(new Pen(Color.FromArgb(220, 222, 230), 1f), path);
                        }
                        DrawCenteredString(g, "RESERVED\nTutorial/Seminar", teacherFont, new SolidBrush(Color.FromArgb(140, 145, 155)), x, y, CELL_W, mergedH);
                    }
                    else if (!string.IsNullOrEmpty(entry.CustomText))
                    {
                        Color bg = GetSubjectBg(100), fg = GetSubjectFg(100);
                        Rectangle cardRect = new Rectangle(x + 4, y + 4, CELL_W - 8, mergedH - 8);
                        using (GraphicsPath path = GetRoundedPath(cardRect, 6))
                        {
                            g.FillPath(new SolidBrush(bg), path);
                            g.DrawPath(new Pen(Color.FromArgb(160, fg.R, fg.G, fg.B), 1f), path);
                        }

                        string text = entry.CustomText;
                        DrawCenteredString(g, text, subjFont, new SolidBrush(fg), cardRect.X, cardRect.Y, cardRect.Width, cardRect.Height);
                    }
                    else
                    {
                        Subject subj = subjects.Find(s => s.SubjectId == entry.SubjectId);
                        int tid = entry.TeacherId > 0 ? entry.TeacherId : (subj != null ? subj.TeacherId : 0);
                        Teacher tchr = tid > 0 ? teachers.Find(t => t.TeacherId == tid) : null;
                        if (subj != null)
                        {
                            Color bg = GetSubjectBg(subj.SubjectId), fg = GetSubjectFg(subj.SubjectId);
                            Rectangle cardRect = new Rectangle(x + 4, y + 4, CELL_W - 8, mergedH - 8);
                            using (GraphicsPath path = GetRoundedPath(cardRect, 6))
                            {
                                g.FillPath(new SolidBrush(bg), path);
                                g.DrawPath(new Pen(Color.FromArgb(150, fg.R, fg.G, fg.B), 1f), path);
                            }

                            int labReserve = subj.IsLab ? 36 : 0;
                            RectangleF subjRect = new RectangleF(cardRect.X + 8, cardRect.Y + 7, cardRect.Width - 16 - labReserve, 20);
                            using (StringFormat sf = new StringFormat { Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap, LineAlignment = StringAlignment.Center })
                            {
                                g.DrawString(subj.Name ?? "", subjFont, new SolidBrush(fg), subjRect, sf);
                            }

                            if (tchr != null)
                            {
                                int codeReserve = !string.IsNullOrWhiteSpace(subj.CourseCode) && span == 1 ? 55 : 0;
                                RectangleF teacherRect = new RectangleF(cardRect.X + 8, cardRect.Y + 25, cardRect.Width - 16 - codeReserve, 18);
                                using (StringFormat sf = new StringFormat { Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap, LineAlignment = StringAlignment.Center })
                                {
                                    g.DrawString(tchr.ToString(), teacherFont, new SolidBrush(Color.FromArgb(170, fg.R, fg.G, fg.B)), teacherRect, sf);
                                }
                            }

                            if (subj.IsLab)
                            {
                                Rectangle badgeRect = new Rectangle(cardRect.Right - 32, cardRect.Y + 6, 26, 13);
                                using (GraphicsPath badgePath = GetRoundedPath(badgeRect, 3))
                                {
                                    g.FillPath(new SolidBrush(fg), badgePath);
                                }
                                DrawCenteredString(g, "LAB", badgeFont, whiteBrush, badgeRect.X, badgeRect.Y, badgeRect.Width, badgeRect.Height);
                            }

                            if (span > 1)
                            {
                                string dur = FormatTime(slots[r].StartTime) + " - " + FormatTime(slots[r+span-1].EndTime);
                                g.DrawString(dur, spanFont, new SolidBrush(Color.FromArgb(170, fg.R, fg.G, fg.B)), cardRect.X + 8, cardRect.Bottom - 16);
                            }

                            // Down right corner: Course Code
                            if (!string.IsNullOrWhiteSpace(subj.CourseCode))
                            {
                                string cCode = subj.CourseCode.Trim();
                                Font codeFont = new Font("Segoe UI", 7f, FontStyle.Bold);
                                SizeF codeSize = g.MeasureString(cCode, codeFont);
                                float codeX = cardRect.Right - codeSize.Width - 6;
                                float codeY = cardRect.Bottom - 15;
                                g.DrawString(cCode, codeFont, new SolidBrush(Color.FromArgb(180, fg.R, fg.G, fg.B)), codeX, codeY);
                            }
                        }
                        else
                        {
                            g.FillRectangle(new SolidBrush(rowBg), x, y, CELL_W, mergedH);
                            g.DrawRectangle(borderPen, x, y, CELL_W, mergedH);
                        }
                    }
                }
            }

            // 5. Signature Section (Right Aligned - Fixed Glitch)
            int totalH = tableY + DAY_ROW_H + (slots.Count * CELL_H);
            string incharge = txtIncharge != null && !string.IsNullOrWhiteSpace(txtIncharge.Text) ? txtIncharge.Text.Trim() : "Mr. Mohsin Sheraz";

            int sigW = 280;
            int sigX = sx + tableW - sigW;
            int sigY = totalH + 30;

            // Signature line
            g.DrawLine(new Pen(Color.FromArgb(170, 180, 200), 1.5f), sigX + 20, sigY, sigX + sigW - 20, sigY);

            // Incharge Name under signature line with single line format to avoid wrap glitch
            StringFormat noWrapSf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.NoWrap
            };
            Font inchargeFont = new Font("Segoe UI", 9f, FontStyle.Bold);
            g.DrawString("Timetable Incharge: " + incharge, inchargeFont, new SolidBrush(Color.FromArgb(30, 45, 75)), new RectangleF(sigX, sigY + 6, sigW, 26), noWrapSf);

            // Hidden roll number tag placed discreetly in the bottom-right corner
            Font hiddenFont = new Font("Segoe UI", 6.5f, FontStyle.Regular);
            Brush hiddenBrush = new SolidBrush(Color.FromArgb(195, 202, 215));
            SizeF rollSz = g.MeasureString("1258", hiddenFont);
            g.DrawString("1258", hiddenFont, hiddenBrush, sx + tableW - rollSz.Width - 4, sigY + 28);
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            bool isOfficial = cmbStyle == null || cmbStyle.SelectedIndex == 0;
            if (isMerged)
            {
                if (sections == null || sections.Count == 0)
                { MessageBox.Show("No sections found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                int currentPage = 0;
                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.Landscape = !isOfficial;
                pd.BeginPrint += (s, ev) => { currentPage = 0; };
                pd.PrintPage += (s, ev) => {
                    ev.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    ev.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                    int pageW = ev.PageBounds.Width, tableW = GetTotalTableWidth();
                    int startX = Math.Max(20, (pageW - tableW) / 2);
                    DrawTimetable(ev.Graphics, startX, isOfficial ? 35 : 30, sections[currentPage], currentPage + 1);
                    currentPage++;
                    ev.HasMorePages = currentPage < sections.Count;
                };
                var ppd = new System.Windows.Forms.PrintPreviewDialog();
                ppd.Document = pd; ppd.Size = new Size(1000, 750); ppd.ShowDialog();
            }
            else
            {
                if (cmbSection.SelectedItem == null) return;
                Section sec = (Section)cmbSection.SelectedItem;
                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.Landscape = !isOfficial;
                pd.PrintPage += (s, ev) => {
                    ev.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    ev.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                    int pageW = ev.PageBounds.Width, tableW = GetTotalTableWidth();
                    int startX = Math.Max(20, (pageW - tableW) / 2);
                    DrawTimetable(ev.Graphics, startX, isOfficial ? 35 : 30, sec, 1);
                    ev.HasMorePages = false;
                };
                var ppd = new System.Windows.Forms.PrintPreviewDialog();
                ppd.Document = pd; ppd.Size = new Size(1000, 750); ppd.ShowDialog();
            }
        }

        protected override void OnShown(EventArgs e)  { base.OnShown(e);  UpdateScrollSize(); Refresh_Preview(); }
        protected override void OnResize(EventArgs e) { base.OnResize(e); Refresh_Preview(); }
    }
}
