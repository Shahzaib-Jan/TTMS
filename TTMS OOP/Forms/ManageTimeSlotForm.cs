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
    public partial class ManageTimeSlotsForm : Form
    {
        private List<TimeSlot> slots;

        public ManageTimeSlotsForm()
        {
            InitializeComponent();
            this.BackColor = AppColors.Background;
            lblTitle.Font = AppFonts.HeadlineLg; lblTitle.ForeColor = AppColors.OnSurface;
            lstSlots.Font = AppFonts.BodyMd; lstSlots.BackColor = AppColors.Surface;
            pnl.BackColor = AppColors.Surface;
            l1.Font = AppFonts.LabelMd; l1.ForeColor = AppColors.OnSurfaceVar;
            l2.Font = AppFonts.LabelMd; l2.ForeColor = AppColors.OnSurfaceVar;
            txtStart.Font = AppFonts.BodyMd; txtEnd.Font = AppFonts.BodyMd;
            btnSave.Font = AppFonts.BodyMd; btnSave.BackColor = AppColors.Primary; btnSave.FlatAppearance.BorderSize = 0; btnSave.Cursor = Cursors.Hand;
            btnDelete.Font = AppFonts.BodyMd; btnDelete.BackColor = AppColors.Error; btnDelete.FlatAppearance.BorderSize = 0; btnDelete.Cursor = Cursors.Hand;
            LoadData();
        }

        private void LoadData()
        {
            slots = DataManager.GetSlots();
            RefreshList();
        }
        private void RefreshList()
        {
            lstSlots.Items.Clear();
            foreach (TimeSlot s in slots) lstSlots.Items.Add(s);
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStart.Text) || string.IsNullOrWhiteSpace(txtEnd.Text))
            { MessageBox.Show("Enter both start and end time.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (lstSlots.SelectedIndex >= 0)
            { slots[lstSlots.SelectedIndex].StartTime = txtStart.Text.Trim(); slots[lstSlots.SelectedIndex].EndTime = txtEnd.Text.Trim(); }
            else
            {
                int nextId = slots.Count > 0 ? (slots.Max(s => s.SlotId) + 1) : 1;
                slots.Add(new TimeSlot { SlotId = nextId, StartTime = txtStart.Text.Trim(), EndTime = txtEnd.Text.Trim() });
            }
            DataManager.SaveSlots(slots);
            slots = DataManager.GetSlots();
            RefreshList();
            ClearForm();
        }
        private void List_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSlots.SelectedIndex < 0) return;
            TimeSlot s = slots[lstSlots.SelectedIndex];
            txtStart.Text = s.StartTime; txtEnd.Text = s.EndTime;
            btnDelete.Enabled = true; btnSave.Text = "Update Slot";
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (lstSlots.SelectedIndex < 0) return;
            if (MessageBox.Show("Delete this slot?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            { slots.RemoveAt(lstSlots.SelectedIndex); DataManager.SaveSlots(slots); RefreshList(); ClearForm(); }
        }
        private void ClearForm()
        {
            txtStart.Text = ""; txtEnd.Text = "";
            lstSlots.ClearSelected();
            btnDelete.Enabled = false; btnSave.Text = "Save Slot";
        }
    }
}

