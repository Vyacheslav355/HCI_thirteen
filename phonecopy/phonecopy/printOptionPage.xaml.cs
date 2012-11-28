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

namespace phonecopy
{
    public partial class printOptionPage : PhoneApplicationPage
    {
        private Model model = Model.getInstance();

        public printOptionPage()
        {
            InitializeComponent();
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            this.ApplicationBar.IsVisible = false;
            model.Print();
            this.ApplicationBar.IsVisible = true;
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (model.checkPrinting() == true)
            {
                this.ApplicationBar.IsVisible = true;
                model.cancelPrinting();
                e.Cancel = true;
            }
        }

    }
}