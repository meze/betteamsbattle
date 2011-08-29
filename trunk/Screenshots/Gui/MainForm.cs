using System;
using System.Threading;
using System.Windows.Forms;
using BetTeamsBattle.Screenshots.BettScreenshotsManager.Interfaces;
using BetTeamsBattle.Screenshots.Gui.DI;
using NLog;
using Ninject;

namespace BetTeamsBattle.Screenshots.Gui
{
    public partial class MainForm : Form
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IScreenshotsMakingManager _screenshotsMakingManager;

        public MainForm()
        {
            InitializeComponent();

            var kernel = ScreenshotsGuiNinjectKernel.CreateKernel();

            _screenshotsMakingManager = kernel.Get<IScreenshotsMakingManager>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _screenshotsMakingManager.Run();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _screenshotsMakingManager.Stop();
        }
    }
}