using System;
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
using System.Windows.Threading;
using System.ComponentModel;

namespace phonecopy
{
    public partial class shareOptionPage : PhoneApplicationPage
    {
        private ProgressBar bar = null;
        private Boolean exporting = false;
        private DispatcherTimer dispatcherTimer = null;

        public shareOptionPage()
        {
            InitializeComponent();
        }

        private void UriClick(object sender, RoutedEventArgs e)
        {
            bar = new ProgressBar();
            bar.IsIndeterminate = true;
            exporting = true;

            this.ContentPanel.Children.Add(bar);

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (exporting == true)
            {
                this.ContentPanel.Children.Remove(bar);
                exporting = false;
                NavigationService.GoBack();
            }
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (exporting == true)
            {
                exporting = false;
                this.ContentPanel.Children.Remove(bar);
                e.Cancel = true;
            }
        }
    }
}