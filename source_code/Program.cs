using BeverageDistributor.Forms;
using BeverageDistributor.Services;
using BeverageDistributor.Models;

namespace BeverageDistributor
{
    /// <summary>
    /// Main program class for the Beverage Distributor application
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            try
            {
                // Show splash screen or loading message
                ShowSplashScreen();

                // Initialize data service
                var dataService = new DataService();

                // Show main application form
                using (var mainForm = new MainApplicationForm(dataService))
                {
                    Application.Run(mainForm);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Application error: {ex.Message}\n\nPlease contact system administrator.", 
                    "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Shows a splash screen while the application initializes
        /// </summary>
        private static void ShowSplashScreen()
        {
            var splashForm = new Form
            {
                Text = "Beverage Distributor",
                Size = new Size(400, 300),
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.FromArgb(0, 120, 215)
            };

            var lblTitle = new Label
            {
                Text = "Beverage Distributor",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(400, 50),
                Location = new Point(0, 80)
            };

            var lblSubtitle = new Label
            {
                Text = "Sales Representative Portal",
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(400, 30),
                Location = new Point(0, 130)
            };

            var lblLoading = new Label
            {
                Text = "Initializing...",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(400, 25),
                Location = new Point(0, 170)
            };

            var lblVersion = new Label
            {
                Text = "Version 2.0.0",
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(400, 20),
                Location = new Point(0, 250)
            };

            splashForm.Controls.AddRange(new Control[] { lblTitle, lblSubtitle, lblLoading, lblVersion });

            splashForm.Show();
            Application.DoEvents();

            // Simulate initialization time
            Thread.Sleep(2000);

            splashForm.Close();
        }
    }
} 