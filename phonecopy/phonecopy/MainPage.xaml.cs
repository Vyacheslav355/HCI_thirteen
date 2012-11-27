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

namespace phonecopy
{
   
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

       
        private void paperInputButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/previewPage1.xaml", UriKind.Relative));
        }

        private void printPdfButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/filelist.xaml", UriKind.Relative));
        }
    }
}