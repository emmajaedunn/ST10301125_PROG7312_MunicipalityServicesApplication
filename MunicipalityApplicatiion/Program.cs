using System;
using System.Windows.Forms;
using MunicipalityApplicatiion.UI;

namespace MunicipalityApplicatiion
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Main Menu Form
            Application.Run(new MainMenu());
        }
    }
}