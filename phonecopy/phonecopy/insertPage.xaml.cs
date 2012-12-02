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
    public partial class insertPage : PhoneApplicationPage
    {
        //private ProgressBar bar;
        private bool scanning = false;
        private DispatcherTimer dispatcherTimer = null;

        private Model model = Model.getInstance();

        public insertPage()
        {
            InitializeComponent();

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(scanningCompleted);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 4);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (model.Paper)
            {
                // Paper is already in place
                // Start scanning automatically
                startScanning();
            }
            else
            {

            }
        }

        private void startScanning()
        {
            scanning = true;

            this.ScanBar.Visibility = System.Windows.Visibility.Visible;
            this.ScanText.Visibility = System.Windows.Visibility.Visible;
            this.ApplicationBar.IsVisible = false;

            dispatcherTimer.Start();
        }

        private void stopScanning()
        {
            dispatcherTimer.Stop();
            scanning = false;
            this.ScanBar.Visibility = System.Windows.Visibility.Collapsed;
            this.ScanText.Visibility = System.Windows.Visibility.Collapsed;
            this.ApplicationBar.IsVisible = true;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (model.Paper)
            {
                startScanning();
            }
            else
            {
                // Error, no paper
            }

        }

        private void scanningCompleted(object sender, EventArgs e)
        {
            if (scanning)
            {
                stopScanning();

                model.PageCount += 1;
                NavigationService.GoBack();
            }
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (scanning)
            {
                stopScanning();
                e.Cancel = true;
            }
            else
            {
                if (model.PageCount < 1)
                {
                    model.Abort = true;
                }
            }
        }
    }
}