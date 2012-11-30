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
        private ProgressBar bar = null;
        private bool scanning = false;
        private DispatcherTimer dispatcherTimer = null;

        private Model model = Model.getInstance();

        public insertPage()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            bar = new ProgressBar();
            bar.IsIndeterminate = true;
            scanning = true;

            this.ContentPanel.Children.Add(bar);
            this.ApplicationBar.IsVisible = false;

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (scanning == true)
            {
                this.ContentPanel.Children.Remove(bar);
                this.ApplicationBar.IsVisible = true;
                scanning = false;
                if (model.firstScan() == true)
                    NavigationService.GoBack();
                else
                {
                    model.setFirstScan();
                    NavigationService.Navigate(new Uri("/PreviewPage1.xaml", UriKind.Relative));
                }
            }
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (scanning == true)
            {
                scanning = false;
                this.ContentPanel.Children.Remove(bar);
                this.ApplicationBar.IsVisible = true;
                e.Cancel = true;
            }
        }
    }
}