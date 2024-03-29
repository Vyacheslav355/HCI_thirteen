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
using Microsoft.Phone.Shell;

namespace phonecopy
{
    public partial class PreviewPage1 : PhoneApplicationPage
    {
        public PreviewPage1()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Model model = Model.getInstance();
            ApplicationBarIconButton b = null;

            if(ApplicationBar.Buttons.Count != 0)
                b = (ApplicationBarIconButton)ApplicationBar.Buttons[0];

            if (b == null)
            {
                if (model.getPDF() == true)
                {
                    ApplicationBarIconButton printButton = new ApplicationBarIconButton(new Uri("/icons/appbar.printer.blank.png", UriKind.Relative)) { Text = "Print" };
                    printButton.Click += printButton_Click;
                    ApplicationBar.Buttons.Add(printButton);
                }
                else
                {
                    ApplicationBarIconButton addButton = new ApplicationBarIconButton(new Uri("/icons/appbar.add.rest.png", UriKind.Relative)) { Text = "Scan next page" };
                    ApplicationBarIconButton printButton = new ApplicationBarIconButton(new Uri("/icons/appbar.printer.blank.png", UriKind.Relative)) { Text = "Print" };
                    ApplicationBarIconButton saveButton = new ApplicationBarIconButton(new Uri("/icons/appbar.save.rest.png", UriKind.Relative)) { Text = "Save or export" };
                    printButton.Click += printButton_Click;
                    addButton.Click += addButton_Click;
                    saveButton.Click += saveButton_Click;
                    ApplicationBar.Buttons.Add(addButton);
                    ApplicationBar.Buttons.Add(printButton);
                    ApplicationBar.Buttons.Add(saveButton);
                }
            }

            // Update page count
            PageCount.Text = "Page 1/" + model.PageCount;

            
            if (model.Abort)
            {
                NavigationService.GoBack();
            }
            else if (!model.getPDF() && model.PageCount < 1)
            {
                // Request first page when entering preview
                NavigationService.Navigate(new Uri("/insertPage.xaml", UriKind.Relative));
            }
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/printOptionPage.xaml", UriKind.Relative));
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/shareOptionPage.xaml", UriKind.Relative));
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/insertPage.xaml", UriKind.Relative));
       
        }
    }
}