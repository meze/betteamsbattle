﻿using System;
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

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var kernel = ScreenshotsGuiNinjectKernel.CreateKernel();

            var screenshotsMakingManager = kernel.Get<IScreenshotsMakingManager>();

            screenshotsMakingManager.Run();
        }
    }
}