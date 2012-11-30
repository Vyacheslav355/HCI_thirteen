﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Controls.Primitives;
using System.Threading;
using System.ComponentModel;
using System.Windows.Threading;

namespace phonecopy
{
    public partial class printOptionPage : PhoneApplicationPage
    {
        private Model model = Model.getInstance();
        private DispatcherTimer dispatcherTimer;
        private bool printing = false;
        private ProgressBar bar = null;

        public printOptionPage()
        {
            InitializeComponent();
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            bar = new ProgressBar();
            bar.IsIndeterminate = true;
            printing = true;           

            this.ContentPanel.Children.Add(bar);
            this.ApplicationBar.IsVisible = false;

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();

        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (printing == true)
            {
                printing = false;
                this.ContentPanel.Children.Remove(bar);
                this.ApplicationBar.IsVisible = true;
                e.Cancel = true;
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (printing == true)
            {
                this.ContentPanel.Children.Remove(bar);
                this.ApplicationBar.IsVisible = true;
                printing = false;
                NavigationService.GoBack();
            }
        }

    }
}