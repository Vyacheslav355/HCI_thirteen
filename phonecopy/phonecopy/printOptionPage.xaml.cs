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

        public printOptionPage()
        {
            InitializeComponent();
            //List<int> source = new List<int>();
            //for (int i = 1; i < 100; i++)
            //{
            //    source.Add(i);
            //}
            //this.CopyCount.ItemsSource = source;
            this.CopyCount.DataSource = new IntLoopingDataSource() { MinValue = 1, MaxValue = 60, SelectedItem = 1 };
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 4);
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            startPrinting();
        }

        private void startPrinting()
        {
            if (!printing)
            {
                this.PrintingBar.Visibility = System.Windows.Visibility.Visible;
                this.PrintingText.Visibility = System.Windows.Visibility.Visible;
                this.ApplicationBar.IsVisible = false;
                printing = true;
                dispatcherTimer.Start();
            }
        }

        private void stopPrinting()
        {
            if (printing)
            {
                this.PrintingBar.Visibility = System.Windows.Visibility.Collapsed;
                this.PrintingText.Visibility = System.Windows.Visibility.Collapsed;
                this.ApplicationBar.IsVisible = true;
                printing = false;
                dispatcherTimer.Stop();
            }

        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (printing)
            {
                stopPrinting();
                e.Cancel = true;
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            stopPrinting();
            NavigationService.GoBack();
        }

    }
}