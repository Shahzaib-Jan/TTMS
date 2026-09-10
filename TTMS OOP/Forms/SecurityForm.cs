using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TTMS_OOP.Helpers;
using TTMS_OOP.BLL;
using TTMS_OOP.Models;

namespace TTMS_OOP.Forms
{
    public partial class SecurityForm : Form
    {
        private int failedAttempts = 0;

        public SecurityForm()
        {
            InitializeComponent();
            AuthManager.EnsureDefaultAdmin();
        }

        // ── Left panel decorative background ──
        private void LeftPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (SolidBrush b = new SolidBrush(Color.FromArgb(18, 255, 255, 255)))
            {
                e.Graphics.FillEllipse(b, 180, 300, 280, 280);
                e.Graphics.FillEllipse(b, 220, -80, 200, 200);
            }
        }

        // ── Form border ──
        private void Form_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(
                new Pen(Color.FromArgb(194, 198, 214), 1),
                0, 0, this.Width - 1, this.Height - 1);
        }

        // ── Icon box drawing ──
        private void IconBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawRectangle(new Pen(Color.White, 2), 2, 2, 52, 52);
            e.Graphics.DrawLine(new Pen(Color.White, 2),    12, 22, 44, 22);
            e.Graphics.DrawLine(new Pen(Color.White, 1.5f), 12, 32, 44, 32);
            e.Graphics.DrawLine(new Pen(Color.White, 1.5f), 12, 40, 30, 40);
        }

        // ── Close button ──
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // ── Show/hide password ──
        private void ChkShowPass_Changed(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPass.Checked ? '\0' : '*';
        }

        // ── Login button hover ──
        private void BtnLogin_MouseEnter(object sender, EventArgs e)
        {
            btnLogin.BackColor = Color.FromArgb(0, 67, 149); // PrimaryDark
        }

        private void BtnLogin_MouseLeave(object sender, EventArgs e)
        {
            btnLogin.BackColor = Color.FromArgb(0, 88, 190); // Primary
        }

        // ── Enter key submits login ──
        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnLogin_Click(sender, e);
        }

        // ── Change password link ──
        private void LnkChange_Click(object sender, EventArgs e)
        {
            new ChangePasswordForm().ShowDialog();
        }

        // ── Login logic ──
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            lblError.Text = "";

            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblError.Text = "Please enter username and password.";
                return;
            }

            if (failedAttempts >= 3)
            {
                lblError.Text     = "Too many failed attempts. Restart the app.";
                btnLogin.Enabled  = false;
                return;
            }

            UserCredential user = AuthManager.ValidateLogin(
                txtUsername.Text.Trim(),
                txtPassword.Text);

            if (user != null)
            {
                this.Close();
            }
            else
            {
                failedAttempts++;
                int remaining = 3 - failedAttempts;
                lblError.Text = remaining > 0
                    ? "Incorrect username or password.  " + remaining + " attempt(s) remaining."
                    : "Too many failed attempts. Restart the app.";

                if (failedAttempts >= 3)
                    btnLogin.Enabled = false;

                txtPassword.Clear();
                txtPassword.Focus();
            }
        }
    }
}

