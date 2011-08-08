using System;
using System.Windows.Forms;
using BetTeamsBattle.Data.Repositories.DI;
using BetTeamsBattle.ScreenShotsMaker.DI;
using BetTeamsBattle.ScreenShotsMaker.ScreenShotMakingManager.Interfaces;
using Ninject;

namespace BetTeamsBattle.ScreenShotsMaker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var kernel = new StandardKernel(new DataRepositoriesNinjectModule(), new ScreenShotsMakerNinjectModule());
            var screenShotsMakingManager = kernel.Get<IScreenShotsMakingManager>();

            screenShotsMakingManager.Run();
        }
    }
}