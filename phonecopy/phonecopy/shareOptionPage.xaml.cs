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
        private Boolean exporting = false;
        private DispatcherTimer dispatcherTimer = null;

        public shareOptionPage()
        {
            InitializeComponent();

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
        }

        private void UriClick(object sender, RoutedEventArgs e)
        {
            startExporting();
        }

        private void startExporting()
        {
            if (!exporting)
            {
                dispatcherTimer.Start();
                exporting = true;
                ExportingBar.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void finishExporting()
        {
            if (exporting)
            {
                exporting = false;
                dispatcherTimer.Stop();
                ExportingBar.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            finishExporting();
            NavigationService.GoBack();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (exporting)
            {
                finishExporting();
                e.Cancel = true;
            }
        }
    }
}