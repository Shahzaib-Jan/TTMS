using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TTMS_OOP.Helpers;
using TTMS_OOP.BLL;
using TTMS_OOP.Models;

namespace TTMS_OOP.Forms
{
    public partial class ChangePasswordForm : Form
    {
        public ChangePasswordForm()
        {
            InitializeComponent();
            this.BackColor = AppColors.Surface;
            lblTitle.ForeColor = AppColors.OnSurface;
            foreach (Control c in this.Controls)
            {
                if (c is TextBox tb) { tb.Font = AppFonts.BodyMd; tb.BackColor = AppColors.SurfaceLow; }
                if (c is Label lbl && lbl.Name != "lblMsg") lbl.ForeColor = AppColors.OnSurfaceVar;
            }
            btnSave.Font = AppFonts.BodyMd;
            btnSave.BackColor = AppColors.Primary;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Cursor = Cursors.Hand;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            lblMsg.ForeColor = AppColors.Error;
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtOldPass.Text) ||
                string.IsNullOrWhiteSpace(txtNewPass.Text) || string.IsNullOrWhiteSpace(txtConfirm.Text))
            { lblMsg.Text = "All fields are required."; return; }
            if (txtNewPass.Text != txtConfirm.Text)
            { lblMsg.Text = "New passwords do not match."; return; }
            if (txtNewPass.Text.Length < 6)
            { lblMsg.Text = "Password must be at least 6 characters."; return; }
            UserCredential user = AuthManager.ValidateLogin(txtUsername.Text.Trim(), txtOldPass.Text);
            if (user == null) { lblMsg.Text = "Current password is incorrect."; return; }
            List<UserCredential> creds = AuthManager.GetCredentials();
            foreach (UserCredential c in creds)
                if (c.Username.ToLower() == txtUsername.Text.Trim().ToLower())
                { c.PasswordHash = AuthManager.HashPassword(txtNewPass.Text); break; }
            AuthManager.UpdateCredentials(creds);
            lblMsg.ForeColor = Color.Green;
            lblMsg.Text = "Password changed successfully!";
        }
    }
}

