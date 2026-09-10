using System;
using System.Windows.Forms;
using TTMS_OOP.Forms;

namespace TTMS_OOP
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 1. Splash
            new SplashForm().ShowDialog();

            // 2. Login
            SecurityForm login = new SecurityForm();
            login.ShowDialog();

            // 3. Main App
            Application.Run(new MainDashboard());
        }
    }
}