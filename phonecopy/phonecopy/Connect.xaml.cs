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

namespace phonecopy
{
    public partial class Connect : PhoneApplicationPage
    {
        private DispatcherTimer dispatcherTimer = null;
        private Model model = Model.getInstance();

        public Connect()
        {
            InitializeComponent();

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(checkConnection);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            dispatcherTimer.Start();
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            dispatcherTimer.Stop();
        }

        private void checkConnection(object sender, EventArgs e)
        {
            if (model.Connected)
            {
                NavigationService.GoBack();
            }
        }
    }
}