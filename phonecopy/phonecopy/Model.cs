using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;


/*Singleton object for storing internal state data*/
namespace phonecopy
{
    public class Model
    {
        private static Model thisInst = null;

        private bool PDF = false;
        private bool printing = false;
        private bool scanning = false;
        private bool disconnected = false;
        private bool waitingpage = false;

        private Popup pUp;

        public static Model getInstance()
        {
            if (thisInst == null)
            {
                thisInst = new Model();
            }

            return thisInst;
        }

        public void setPDF(bool PDFvalue)
        {
            PDF = PDFvalue;
        }

        public bool getPDF()
        {
            return PDF;
        }

        public void Print()
        {
            printing = true;
            //Open print popup
            if(showPopup("Printing...") == false)
                return;
            pUp.IsOpen = true;

            //pUp.IsOpen = false;
            //printing = false;
            //pUp = null;
        }

        public void cancelPrinting()
        {
            if (printing == true)
            {
                printing = false;
                pUp.IsOpen = false;
                pUp = null;
            }
        }

        public bool checkPrinting()
        {
            return printing;
        }

        /*forms a popup showing the message parameter. Returns true if popup could be created, false otherwise*/
        public bool showPopup(string message)
        {
            double screenWidth = System.Windows.Application.Current.Host.Content.ActualWidth;
            double ScreenHeight = System.Windows.Application.Current.Host.Content.ActualHeight;

            if(pUp != null)
                return false;

            pUp = new Popup();
            Border border = new Border();
            border.BorderBrush = new SolidColorBrush(Colors.Black);
            border.BorderThickness = new Thickness(5.0);

            StackPanel panel1 = new StackPanel();
            panel1.Background = new SolidColorBrush(Colors.LightGray);

            TextBlock textblock1 = new TextBlock();
            textblock1.Text = message;
            textblock1.Margin = new Thickness(5.0);
            panel1.Children.Add(textblock1);
            border.Child = panel1;

            // Set the Child property of Popup to the border 
            // which contains a stackpanel, textblock and button.
            pUp.Child = border;

            // Set where the popup will show up on the screen.
            pUp.VerticalOffset = 400;
            pUp.HorizontalOffset = 250;

            return true;
        }
    }
}
