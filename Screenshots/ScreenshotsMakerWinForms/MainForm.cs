using System;
using System.Windows.Forms;
using BetTeamsBattle.BettScreenshotsManager.ScreenshotMakingManager.Interfaces;
using BetTeamsBattle.ScreenshotsMakerWinForms.DI;
using NLog;
using Ninject;

namespace BetTeamsBattle.ScreenshotsMakerWinForms
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