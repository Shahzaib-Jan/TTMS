using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TTMS_OOP.Helpers;

namespace TTMS_OOP.Forms
{
    public partial class SplashForm : Form
    {
        private int animStep = 0;

        public SplashForm()
        {
            InitializeComponent();
            animTimer.Start();
        }

        // ── iconBox custom drawing ──
        private void IconBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawRectangle(new Pen(Color.White, 2), 2, 2, 52, 52);
            e.Graphics.DrawLine(new Pen(Color.White, 2),    12, 20, 44, 20);
            e.Graphics.DrawLine(new Pen(Color.White, 1.5f), 12, 30, 44, 30);
            e.Graphics.DrawLine(new Pen(Color.White, 1.5f), 12, 38, 30, 38);
        }

        // ── Form background drawing ──
        private void Form_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (SolidBrush b = new SolidBrush(Color.FromArgb(18, 255, 255, 255)))
            {
                e.Graphics.FillEllipse(b, 560, 200, 300, 300);
                e.Graphics.FillEllipse(b, 620, -60, 220, 220);
                e.Graphics.FillEllipse(b, -60, 300, 200, 200);
            }
            e.Graphics.DrawRectangle(
                new Pen(Color.FromArgb(40, 255, 255, 255), 1),
                0, 0, this.Width - 1, this.Height - 1);
        }

        // ── Button hover effects ──
        private void BtnStart_MouseEnter(object sender, EventArgs e)
        {
            btnStart.BackColor = Color.FromArgb(220, 235, 255);
        }

        private void BtnStart_MouseLeave(object sender, EventArgs e)
        {
            btnStart.BackColor = Color.White;
        }

        // ── Close button ──
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // ── Animation timer ──
        private void AnimTimer_Tick(object sender, EventArgs e)
        {
            animStep++;
            string[] states = {
                "Initializing...",
                "Loading data...",
                "Setting up...",
                "Almost ready...",
                "Ready"
            };

            if (animStep < states.Length)
                lblStatus.Text = states[animStep];
            else
            {
                animTimer.Stop();
                lblStatus.Text      = "Ready  -->";
                lblStatus.ForeColor = Color.FromArgb(200, 255, 255, 255);
            }
        }

        // ── Get Started button ──
        private void BtnStart_Click(object sender, EventArgs e)
        {
            animTimer.Stop();
            this.Close();
        }
    }
}
