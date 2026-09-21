using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using TTMS_OOP.Helpers;
using TTMS_OOP.Models;
using TTMS_OOP.BLL;

namespace TTMS_OOP.Forms
{
    public partial class TimetableBuilderForm : Form
    {
        private List<Section> sections;
        private List<TimeSlot> slots;
        private List<TimetableEntry> entries;
        private List<Subject> subjects;
        private List<Teacher> teachers;

        private string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };

        // Drag and Drop & Interaction State
        private int dragFromRow = -1;
        private int dragFromCol = -1;
        private Point dragStartPoint = Point.Empty;
        private int dragTargetRow = -1;
        private int dragTargetCol = -1;
        private int hoverRow = -1;
        private int hoverCol = -1;

        public TimetableBuilderForm()
        {
            InitializeComponent();
            this.BackColor = AppColors.Background;

            // Apply theme to designer controls
            grid.BackgroundColor = Color.FromArgb(248, 250, 252);
            grid.BorderStyle = BorderStyle.None;
            grid.Font = AppFonts.BodyMd;
            grid.GridColor = Color.FromArgb(226, 232, 240);
            grid.ColumnHeadersDefaultCellStyle.BackColor = AppColors.Primary;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.EnableHeadersVisualStyles = false;
            grid.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
            grid.RowHeadersDefaultCellStyle.ForeColor = AppColors.OnSurfaceVar;
            grid.RowHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            grid.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            grid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.DefaultCellStyle.BackColor = AppColors.Surface;

            // Enable double buffering on DataGridView to eliminate flicker
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                null, grid, new object[] { true });

            topBar.BackColor = AppColors.Surface;
            lblTitle.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
            lblTitle.ForeColor = AppColors.Primary;

            lblStats.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            lblStats.ForeColor = Color.FromArgb(100, 116, 139);

            lblSes.Font = AppFonts.LabelMd;
            lblSes.ForeColor = AppColors.OnSurfaceVar;
            cmbSession.Font = AppFonts.BodyMd;

            lblSec.Font = AppFonts.LabelMd;
            lblSec.ForeColor = AppColors.OnSurfaceVar;
            cmbSection.Font = AppFonts.BodyMd;

            btnLoad.Font = AppFonts.BodyMd;
            btnLoad.BackColor = AppColors.Primary;
            btnLoad.FlatAppearance.BorderSize = 0;
            btnLoad.Cursor = Cursors.Hand;

            btnClearGrid.Font = AppFonts.BodyMd;
            btnClearGrid.BackColor = AppColors.Error;
            btnClearGrid.FlatAppearance.BorderSize = 0;
            btnClearGrid.Cursor = Cursors.Hand;

            btnPrint.Font = AppFonts.BodyMd;
            btnPrint.BackColor = Color.FromArgb(16, 149, 193);
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.Cursor = Cursors.Hand;

            lblInfo.Font = new Font("Segoe UI", 8.25f, FontStyle.Regular);
            lblInfo.ForeColor = AppColors.OnSurfaceVar;

            LoadData();
        }

        private void LoadData()
        {
            sections = DataManager.GetSections();
            slots = DataManager.GetSlots().FindAll(s => !IsBreakSlot(s));
            entries = DataManager.GetEntries();
            subjects = DataManager.GetSubjects();
            teachers = DataManager.GetTeachers();

            List<string> semesters = new List<string>();
            foreach (Section sec in sections)
                if (!string.IsNullOrWhiteSpace(sec.Semester) && !semesters.Contains(sec.Semester))
                    semesters.Add(sec.Semester);

            semesters.Sort((a, b) => TimetableManager.GetSemesterNumber(a).CompareTo(TimetableManager.GetSemesterNumber(b)));

            cmbSession.Items.Clear();
            foreach (string sem in semesters)
                cmbSession.Items.Add(sem);

            if (cmbSession.Items.Count > 0)
                cmbSession.SelectedIndex = 0;
        }

        private void CmbSession_Changed(object sender, EventArgs e)
        {
            if (cmbSession.SelectedItem == null) return;
            string selectedSem = cmbSession.SelectedItem.ToString();
            List<Section> filtered = sections.FindAll(s => s.Semester == selectedSem);
            filtered.Sort((a, b) => string.Compare(a.Name ?? "", b.Name ?? "", StringComparison.OrdinalIgnoreCase));
            cmbSection.DataSource = filtered;
            if (filtered.Count > 0) BuildGrid();
        }

        private void CmbSection_Changed(object sender, EventArgs e)
        {
            BuildGrid();
        }

        private void BtnClearGrid_Click(object sender, EventArgs e)
        {
            if (cmbSection.SelectedItem == null) return;
            Section selected = (Section)cmbSection.SelectedItem;
            if (MessageBox.Show($"Are you sure you want to clear all scheduled slots for {selected}?",
                "Clear Schedule", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                entries = DataManager.GetEntries();
                entries.RemoveAll(en => en.SectionId == selected.SectionId);
                DataManager.SaveEntries(entries);
                RefreshGrid();
            }
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            slots = DataManager.GetSlots().FindAll(s => !IsBreakSlot(s));
            entries = DataManager.GetEntries();
            subjects = DataManager.GetSubjects();
            teachers = DataManager.GetTeachers();
            BuildGrid();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (cmbSection.SelectedItem is Section selected)
            {
                PrintPreviewForm preview = new PrintPreviewForm(selected);
                preview.ShowDialog(this);
            }
            else
            {
                MessageBox.Show("Please select a section first.", "No Section Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BuildGrid()
        {
            grid.Columns.Clear();
            grid.Rows.Clear();
            grid.ColumnHeadersVisible = true;
            grid.RowHeadersVisible = true;

            if (slots.Count == 0)
            {
                MessageBox.Show("Please add Time Slots first.", "No Slots", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (string day in days)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.Name = day;
                col.HeaderText = day;
                col.Width = 220;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                grid.Columns.Add(col);
            }

            foreach (TimeSlot slot in slots)
            {
                int rowIndex = grid.Rows.Add();
                grid.Rows[rowIndex].HeaderCell.Value = slot.StartTime + "\n" + slot.EndTime;
                grid.Rows[rowIndex].Height = 80;
                grid.Rows[rowIndex].Tag = slot.SlotId;
            }
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            if (cmbSection.SelectedItem is Section selected)
            {
                int totalCapacity = slots.Count * days.Length;
                int scheduledCount = 0;
                foreach (TimetableEntry en in entries)
                {
                    if (en.SectionId == selected.SectionId)
                        scheduledCount++;
                }
                lblStats.Text = $"📊 {scheduledCount} of {totalCapacity} slots scheduled";
            }
            else
            {
                lblStats.Text = "Ready";
            }
            grid.Invalidate();
        }

        // ─────────────────────────────────────────────────────────────
        // INTERACTIVE CELL PAINTING (Modern Cards & Badges)
        // ─────────────────────────────────────────────────────────────
        private void Grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Allow default rendering for headers
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            Rectangle cellBounds = e.CellBounds;
            bool isTarget = (dragTargetRow == e.RowIndex && dragTargetCol == e.ColumnIndex);
            bool isHovered = (hoverRow == e.RowIndex && hoverCol == e.ColumnIndex);
            bool isDraggingSource = (dragFromRow == e.RowIndex && dragFromCol == e.ColumnIndex);

            // 1. Paint cell background
            using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(248, 250, 252)))
            {
                g.FillRectangle(bgBrush, cellBounds);
            }

            // 2. Draw cell grid lines
            using (Pen gridPen = new Pen(Color.FromArgb(226, 232, 240), 1))
            {
                g.DrawLine(gridPen, cellBounds.Right - 1, cellBounds.Top, cellBounds.Right - 1, cellBounds.Bottom);
                g.DrawLine(gridPen, cellBounds.Left, cellBounds.Bottom - 1, cellBounds.Right, cellBounds.Bottom - 1);
            }

            Section selected = cmbSection.SelectedItem as Section;
            if (selected == null || e.RowIndex >= slots.Count)
            {
                e.Handled = true;
                return;
            }

            TimeSlot slot = slots[e.RowIndex];
            string day = days[e.ColumnIndex];
            TimetableEntry entry = entries.Find(en => en.SectionId == selected.SectionId && en.SlotId == slot.SlotId && en.Day == day);

            Rectangle cardRect = new Rectangle(cellBounds.X + 4, cellBounds.Y + 4, cellBounds.Width - 8, cellBounds.Height - 8);

            // 3. If cell is EMPTY
            if (entry == null)
            {
                if (isTarget)
                {
                    // Target drop highlight on empty slot
                    using (GraphicsPath targetPath = GetRoundedPath(cardRect, 6))
                    using (SolidBrush fillBrush = new SolidBrush(Color.FromArgb(230, 243, 255)))
                    using (Pen borderPen = new Pen(AppColors.Primary, 2f) { DashStyle = DashStyle.Dash })
                    {
                        g.FillPath(fillBrush, targetPath);
                        g.DrawPath(borderPen, targetPath);
                    }
                    using (Font targetFont = new Font("Segoe UI", 8.5f, FontStyle.Bold))
                    using (SolidBrush textBrush = new SolidBrush(AppColors.Primary))
                    using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    {
                        g.DrawString("📥 Drop to Move Here", targetFont, textBrush, cardRect, sf);
                    }
                }
                else if (isHovered)
                {
                    // Hover pill indicator
                    using (GraphicsPath hoverPath = GetRoundedPath(cardRect, 6))
                    using (SolidBrush hoverBrush = new SolidBrush(Color.FromArgb(241, 245, 249)))
                    using (Pen hoverPen = new Pen(Color.FromArgb(203, 213, 225), 1f) { DashStyle = DashStyle.Dash })
                    {
                        g.FillPath(hoverBrush, hoverPath);
                        g.DrawPath(hoverPen, hoverPath);
                    }
                    using (Font addFont = new Font("Segoe UI", 8.5f, FontStyle.Regular))
                    using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(148, 163, 184)))
                    using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    {
                        g.DrawString("+ Assign Class", addFont, textBrush, cardRect, sf);
                    }
                }
                e.Handled = true;
                return;
            }

            // 4. If cell is OCCUPIED — configure card appearance
            Color cardBg;
            Color cardBorder;
            Color accentColor;
            Color titleColor;
            Color subtitleColor = Color.FromArgb(71, 85, 105);

            string titleText = "";
            string subtitleText = "";
            string courseCode = "";
            string badgeText = "";
            Color badgeBg = Color.Empty;
            Color badgeFg = Color.Empty;

            if (entry.IsReserved)
            {
                cardBg = Color.FromArgb(243, 244, 246);
                cardBorder = Color.FromArgb(209, 213, 219);
                accentColor = Color.FromArgb(156, 163, 175);
                titleColor = Color.FromArgb(55, 65, 81);
                titleText = "RESERVED";
                subtitleText = "Tutorial / Seminar / Meeting";
                badgeText = "RESERVED";
                badgeBg = Color.FromArgb(229, 231, 235);
                badgeFg = Color.FromArgb(75, 85, 99);
            }
            else if (!string.IsNullOrEmpty(entry.CustomText))
            {
                cardBg = Color.FromArgb(240, 245, 255);
                cardBorder = Color.FromArgb(191, 219, 254);
                accentColor = AppColors.Primary;
                titleColor = AppColors.Primary;
                titleText = entry.CustomText;
                subtitleText = "Special Scheduled Slot";
            }
            else
            {
                Subject subj = subjects.Find(s => s.SubjectId == entry.SubjectId);
                int tid = entry.TeacherId > 0 ? entry.TeacherId : (subj != null ? subj.TeacherId : 0);
                Teacher tchr = tid > 0 ? teachers.Find(t => t.TeacherId == tid) : null;
                bool hasConflict = tid > 0 && TimetableManager.IsTeacherBusy(tid, slot.SlotId, day, selected.SectionId);
                bool isLab = subj != null && subj.IsLab;

                courseCode = (subj != null && !string.IsNullOrWhiteSpace(subj.CourseCode)) ? subj.CourseCode.Trim() : "";
                titleText = subj != null ? subj.Name : "Unknown Subject";
                subtitleText = tchr != null ? "👨‍🏫 " + tchr.ToString() : "No Teacher Assigned";

                if (hasConflict)
                {
                    cardBg = Color.FromArgb(254, 242, 242);
                    cardBorder = Color.FromArgb(248, 113, 113);
                    accentColor = Color.FromArgb(220, 38, 38);
                    titleColor = Color.FromArgb(153, 27, 27);
                    subtitleColor = Color.FromArgb(185, 28, 28);
                    badgeText = "⚠️ CONFLICT";
                    badgeBg = Color.FromArgb(254, 226, 226);
                    badgeFg = Color.FromArgb(185, 28, 28);
                }
                else if (isLab)
                {
                    cardBg = Color.FromArgb(240, 253, 244);
                    cardBorder = Color.FromArgb(134, 239, 172);
                    accentColor = Color.FromArgb(22, 163, 74);
                    titleColor = Color.FromArgb(20, 83, 45);
                    subtitleColor = Color.FromArgb(21, 128, 61);
                    badgeText = "LAB";
                    badgeBg = Color.FromArgb(220, 252, 231);
                    badgeFg = Color.FromArgb(21, 128, 61);
                }
                else
                {
                    cardBg = Color.FromArgb(240, 247, 255);
                    cardBorder = Color.FromArgb(186, 220, 255);
                    accentColor = Color.FromArgb(2, 132, 199);
                    titleColor = Color.FromArgb(30, 58, 138);
                    subtitleColor = Color.FromArgb(71, 85, 105);
                }
            }

            // Dragged source appearance
            if (isDraggingSource)
            {
                cardBg = Color.FromArgb(235, 238, 245);
                cardBorder = Color.FromArgb(148, 163, 184);
            }

            // Target drop swap highlight
            if (isTarget)
            {
                cardBorder = AppColors.Primary;
                badgeText = "⇄ SWAP";
                badgeBg = AppColors.Primary;
                badgeFg = Color.White;
            }

            // Draw Card Body
            using (GraphicsPath cardPath = GetRoundedPath(cardRect, 6))
            using (SolidBrush bgBrush = new SolidBrush(cardBg))
            using (Pen borderPen = new Pen(isTarget ? AppColors.Primary : cardBorder, isTarget ? 2f : 1f))
            {
                g.FillPath(bgBrush, cardPath);
                g.DrawPath(borderPen, cardPath);
            }

            // Draw Left Color Accent Strip
            using (SolidBrush accentBrush = new SolidBrush(accentColor))
            {
                Rectangle accentRect = new Rectangle(cardRect.X, cardRect.Y + 4, 4, cardRect.Height - 8);
                using (GraphicsPath accentPath = GetRoundedPath(accentRect, 2))
                {
                    g.FillPath(accentBrush, accentPath);
                }
            }

            // Draw Badge (if present)
            int badgeWidth = 0;
            if (!string.IsNullOrEmpty(badgeText))
            {
                using (Font badgeFont = new Font("Segoe UI", 7f, FontStyle.Bold))
                {
                    SizeF badgeSize = g.MeasureString(badgeText, badgeFont);
                    badgeWidth = (int)badgeSize.Width + 10;
                    int badgeHeight = 16;
                    Rectangle badgeRect = new Rectangle(cardRect.Right - badgeWidth - 6, cardRect.Y + 6, badgeWidth, badgeHeight);
                    using (GraphicsPath badgePath = GetRoundedPath(badgeRect, 3))
                    using (SolidBrush badgeBgBrush = new SolidBrush(badgeBg))
                    using (SolidBrush badgeFgBrush = new SolidBrush(badgeFg))
                    using (StringFormat bsf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    {
                        g.FillPath(badgeBgBrush, badgePath);
                        g.DrawString(badgeText, badgeFont, badgeFgBrush, badgeRect, bsf);
                    }
                }
            }

            // Draw Subject / Course Code Title (Auto-fit font and wrap so full name always displays)
            int textLeft = cardRect.X + 10;
            int textWidth = cardRect.Width - 16 - (badgeWidth > 0 ? badgeWidth + 4 : 0);
            Rectangle titleRect = new Rectangle(textLeft, cardRect.Y + 6, textWidth, 34);

            float titleFontSize = 8.5f;
            Font titleFont = new Font("Segoe UI", titleFontSize, FontStyle.Bold);
            SizeF titleSize = g.MeasureString(titleText, titleFont, textWidth);
            while (titleSize.Height > 36 && titleFontSize > 6.8f)
            {
                titleFont.Dispose();
                titleFontSize -= 0.4f;
                titleFont = new Font("Segoe UI", titleFontSize, FontStyle.Bold);
                titleSize = g.MeasureString(titleText, titleFont, textWidth);
            }

            using (titleFont)
            using (SolidBrush titleBrush = new SolidBrush(titleColor))
            using (StringFormat sf = new StringFormat
            {
                Trimming = StringTrimming.EllipsisWord,
                LineAlignment = StringAlignment.Near
            })
            {
                g.DrawString(titleText, titleFont, titleBrush, titleRect, sf);
            }

            // Draw Teacher / Subtitle
            int subWidth = cardRect.Width - 16 - (!string.IsNullOrEmpty(courseCode) ? 65 : 0);
            float teacherY = cardRect.Y + Math.Max(26, titleSize.Height + 5);
            if (teacherY + 18 > cardRect.Bottom - 4)
                teacherY = cardRect.Bottom - 22;
            Rectangle subRect = new Rectangle(textLeft, (int)teacherY, subWidth, 18);
            using (Font subFont = new Font("Segoe UI", 7.8f, FontStyle.Regular))
            using (SolidBrush subBrush = new SolidBrush(subtitleColor))
            using (StringFormat sf = new StringFormat
            {
                Trimming = StringTrimming.EllipsisCharacter,
                FormatFlags = StringFormatFlags.NoWrap,
                LineAlignment = StringAlignment.Center
            })
            {
                g.DrawString(subtitleText, subFont, subBrush, subRect, sf);
            }

            // Draw Course Code in down right corner
            if (!string.IsNullOrEmpty(courseCode))
            {
                using (Font codeFont = new Font("Segoe UI", 7.5f, FontStyle.Bold))
                {
                    SizeF codeSize = g.MeasureString(courseCode, codeFont);
                    float codeX = cardRect.Right - codeSize.Width - 8;
                    float codeY = cardRect.Bottom - 18;
                    using (SolidBrush codeBrush = new SolidBrush(Color.FromArgb(160, titleColor.R, titleColor.G, titleColor.B)))
                    {
                        g.DrawString(courseCode, codeFont, codeBrush, codeX, codeY);
                    }
                }
            }

            e.Handled = true;
        }

        // ─────────────────────────────────────────────────────────────
        // MOUSE & DRAG-AND-DROP HANDLERS (Move & Swap)
        // ─────────────────────────────────────────────────────────────
        private void Grid_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hit = grid.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0 && hit.ColumnIndex >= 0 && hit.RowIndex < slots.Count)
                {
                    if (cmbSection.SelectedItem is Section sec)
                    {
                        TimeSlot slot = slots[hit.RowIndex];
                        string day = days[hit.ColumnIndex];
                        TimetableEntry existing = entries.Find(en => en.SectionId == sec.SectionId && en.SlotId == slot.SlotId && en.Day == day);
                        if (existing != null)
                        {
                            dragFromRow = hit.RowIndex;
                            dragFromCol = hit.ColumnIndex;
                            dragStartPoint = e.Location;
                            return;
                        }
                    }
                }
            }
            dragFromRow = -1;
            dragFromCol = -1;
            dragStartPoint = Point.Empty;
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && dragFromRow >= 0 && dragFromCol >= 0)
            {
                if (dragStartPoint != Point.Empty)
                {
                    Size dragSize = SystemInformation.DragSize;
                    Rectangle dragRect = new Rectangle(
                        new Point(dragStartPoint.X - (dragSize.Width / 2), dragStartPoint.Y - (dragSize.Height / 2)),
                        dragSize);

                    if (!dragRect.Contains(e.Location))
                    {
                        Point sourceCell = new Point(dragFromCol, dragFromRow);
                        grid.DoDragDrop(sourceCell, DragDropEffects.Move);

                        // Reset drag state after drop completion
                        dragFromRow = -1;
                        dragFromCol = -1;
                        dragTargetRow = -1;
                        dragTargetCol = -1;
                        dragStartPoint = Point.Empty;
                        grid.Invalidate();
                    }
                }
            }
        }

        private void Grid_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Point)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Grid_DragOver(object sender, DragEventArgs e)
        {
            Point clientPoint = grid.PointToClient(new Point(e.X, e.Y));
            DataGridView.HitTestInfo hit = grid.HitTest(clientPoint.X, clientPoint.Y);

            if (hit.RowIndex >= 0 && hit.ColumnIndex >= 0 && hit.RowIndex < slots.Count)
            {
                e.Effect = DragDropEffects.Move;
                if (dragTargetRow != hit.RowIndex || dragTargetCol != hit.ColumnIndex)
                {
                    int oldRow = dragTargetRow;
                    int oldCol = dragTargetCol;
                    dragTargetRow = hit.RowIndex;
                    dragTargetCol = hit.ColumnIndex;

                    if (oldRow >= 0 && oldCol >= 0 && oldRow < grid.RowCount && oldCol < grid.ColumnCount)
                        grid.InvalidateCell(oldCol, oldRow);

                    grid.InvalidateCell(dragTargetCol, dragTargetRow);
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Grid_DragDrop(object sender, DragEventArgs e)
        {
            Point clientPoint = grid.PointToClient(new Point(e.X, e.Y));
            DataGridView.HitTestInfo hit = grid.HitTest(clientPoint.X, clientPoint.Y);

            if (hit.RowIndex < 0 || hit.ColumnIndex < 0 || hit.RowIndex >= slots.Count || cmbSection.SelectedItem == null)
                return;

            if (!e.Data.GetDataPresent(typeof(Point)))
                return;

            Point source = (Point)e.Data.GetData(typeof(Point));
            int srcCol = source.X;
            int srcRow = source.Y;
            int dstCol = hit.ColumnIndex;
            int dstRow = hit.RowIndex;

            if (srcCol == dstCol && srcRow == dstRow)
                return;

            Section sec = (Section)cmbSection.SelectedItem;
            TimeSlot srcSlot = slots[srcRow];
            string srcDay = days[srcCol];

            TimeSlot dstSlot = slots[dstRow];
            string dstDay = days[dstCol];

            TimetableEntry srcEntry = entries.Find(en => en.SectionId == sec.SectionId && en.SlotId == srcSlot.SlotId && en.Day == srcDay);
            if (srcEntry == null) return;

            TimetableEntry dstEntry = entries.Find(en => en.SectionId == sec.SectionId && en.SlotId == dstSlot.SlotId && en.Day == dstDay);

            if (dstEntry == null)
            {
                // MOVE to empty slot
                srcEntry.SlotId = dstSlot.SlotId;
                srcEntry.Day = dstDay;
            }
            else
            {
                // SWAP both entries
                srcEntry.SlotId = dstSlot.SlotId;
                srcEntry.Day = dstDay;

                dstEntry.SlotId = srcSlot.SlotId;
                dstEntry.Day = srcDay;
            }

            DataManager.SaveEntries(entries);
            RefreshGrid();
        }

        private void Grid_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                hoverRow = e.RowIndex;
                hoverCol = e.ColumnIndex;
                grid.InvalidateCell(e.ColumnIndex, e.RowIndex);
            }
        }

        private void Grid_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (hoverRow == e.RowIndex && hoverCol == e.ColumnIndex)
                {
                    hoverRow = -1;
                    hoverCol = -1;
                }
                grid.InvalidateCell(e.ColumnIndex, e.RowIndex);
            }
        }

        private void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex >= slots.Count) return;
            if (cmbSection.SelectedItem == null) return;
            Section sec = (Section)cmbSection.SelectedItem;
            TimeSlot slot = slots[e.RowIndex];
            string day = days[e.ColumnIndex];
            TimetableEntry existing = entries.Find(en => en.SectionId == sec.SectionId && en.SlotId == slot.SlotId && en.Day == day);

            // Fresh data reload
            subjects = DataManager.GetSubjects();
            teachers = DataManager.GetTeachers();

            AddEntryForm modal = new AddEntryForm(sec, slot, day, subjects, teachers, existing, entries);
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                if (modal.DeleteEntry && existing != null)
                {
                    entries.Remove(existing);
                }
                else if (modal.ResultEntry != null)
                {
                    if (existing != null) entries.Remove(existing);
                    entries.Add(modal.ResultEntry);
                }
                DataManager.SaveEntries(entries);
                RefreshGrid();
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;
            if (rect.Width <= d || rect.Height <= d)
            {
                path.AddRectangle(rect);
                return path;
            }
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private int GetDayIndex(string day)
        {
            for (int i = 0; i < days.Length; i++)
                if (days[i] == day) return i;
            return -1;
        }

        private bool IsBreakSlot(TimeSlot slot)
        {
            if (slot == null) return false;
            string start = (slot.StartTime ?? "").Trim();
            string end = (slot.EndTime ?? "").Trim();

            // Check if start is 12 (or 12:00, 12:00 PM, 12 PM) and end is 1 (or 1:00, 1:00 PM, 1 PM, 13, 13:00)
            bool isStart12 = start == "12" || start.StartsWith("12:") || start.StartsWith("12 ") || start.Equals("12 PM", StringComparison.OrdinalIgnoreCase);
            bool isEnd1 = end == "1" || end == "13" || end.StartsWith("1:") || end.StartsWith("13:") || end.StartsWith("1 ") || end.Equals("1 PM", StringComparison.OrdinalIgnoreCase);

            if (isStart12 && isEnd1) return true;

            int sH = ParseSlotHour(start);
            int eH = ParseSlotHour(end);
            if (sH == 12 && (eH == 1 || eH == 13)) return true;

            return false;
        }

        private int ParseSlotHour(string timeStr)
        {
            if (string.IsNullOrWhiteSpace(timeStr)) return -1;
            string[] parts = timeStr.Split(new char[] { ':', ' ', '-' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 0 && int.TryParse(parts[0], out int h))
                return h;
            return -1;
        }
    }
}

