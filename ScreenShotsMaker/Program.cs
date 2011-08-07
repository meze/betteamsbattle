using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BetTeamsBattle.Data.Repositories.DI;
using Ninject;
using ScreenShotsMaker.DI;
using ScreenShotsMaker.Interfaces;

namespace ScreenShotsMaker
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MainForm());
        }
    }
}
