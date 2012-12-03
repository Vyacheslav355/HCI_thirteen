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
using System.ComponentModel;

namespace phonecopy
{
    public partial class DropboxMock : PhoneApplicationPage
    {
        public DropboxMock()
        {
            InitializeComponent();
        }

        private void selectPdf(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PreviewPage1.xaml", UriKind.Relative));
        }

        //protected override void OnBackKeyPress(CancelEventArgs e)
        //{
        //    NavigationService.GoBack();
        //    NavigationService.GoBack();
        //}
    }
}