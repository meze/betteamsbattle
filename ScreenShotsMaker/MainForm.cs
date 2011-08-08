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
            //try
            //{
                var kernel = new StandardKernel(new DataRepositoriesNinjectModule(), new ScreenShotsMakerNinjectModule());

                var screenShotsMakingManager = kernel.Get<IScreenShotsMakingManager>();

                screenShotsMakingManager.Run();
            //}
            //catch (Exception ex)
            //{
            //    _logger.ErrorException("Error running ScreenShotMakingManager", ex);
            //}
        }
    }
}