using System;
using System.Windows.Forms;
using BetTeamsBattle.Data.Repositories.DI;
using BetTeamsBattle.ScreenShotsMaker.DI;
using BetTeamsBattle.ScreenShotsMaker.ScreenShotMakingManager.Interfaces;
using NLog;
using Ninject;

namespace BetTeamsBattle.ScreenShotsMaker
{
    public partial class MainForm : Form
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var kernel = ScreenshotsMakerNinjectKernel.CreateKernel();

            var screenshotsMakingManager = kernel.Get<IScreenshotsMakingManager>();

            screenshotsMakingManager.Run();
        }
    }
}