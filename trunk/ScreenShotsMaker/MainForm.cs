using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BetTeamsBattle.Data.Repositories.DI;
using Ninject;
using ScreenShotsMaker.DI;
using ScreenShotsMaker.Interfaces;

namespace ScreenShotsMaker
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
