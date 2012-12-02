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
using System.Windows.Threading;
using System.Windows.Navigation;


/*Singleton object for storing internal state data*/
namespace phonecopy
{
    public class Model
    {
        private static Model thisInst = null;

        private bool connected = false;
        public bool Connected
        {
            get
            {
                return connected;
            }
        }

        private bool paper = false;
        public bool Paper
        {
            get
            {
                return paper;
            }
        }

        private int pageCount;
        public int PageCount
        {
            get
            {
                return pageCount;
            }
            set
            {
                pageCount = value;
            }
        }

        private bool abort = false;
        public bool Abort
        {
            get
            {
                return abort;
            }
            set
            {
                abort = value;
            }
        }

        private bool PDF = false;
        private bool printing = false;
        private bool scanning = false;
        private bool disconnected = false;
        private bool waitingpage = false;
        private bool firstPageScanned = false;

        private Popup pUp;
        private DispatcherTimer dispatcherTimer;
        private DispatcherTimer connectionPollTimer;
        private DispatcherTimer paperPollTimer;

        public static Model getInstance()
        {
            if (thisInst == null)
            {
                thisInst = new Model();
            }

            return thisInst;
        }

        public Model()
        {
            // Create connection poller
            connectionPollTimer = new System.Windows.Threading.DispatcherTimer();
            connectionPollTimer.Tick += new EventHandler(pollConnection);
            connectionPollTimer.Interval = new TimeSpan(0, 0, 3);
            connectionPollTimer.Start();

            // Poll paper status
            paperPollTimer = new System.Windows.Threading.DispatcherTimer();
            paperPollTimer.Tick += new EventHandler(pollPaper);
            paperPollTimer.Interval = new TimeSpan(0, 0, 2);
            paperPollTimer.Start();
        }

        public void reset(bool pdf = false, int initialPageCount = 0)
        {
            pageCount = initialPageCount;
            PDF = pdf;
            abort = false;
        }

        private void pollConnection(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            client.DownloadStringCompleted += (s, response) =>
                {
                    if (response.Result == "0")
                    {
                        connected = false;
                    }
                    else
                    {
                        connected = true;
                    }
                };
            client.DownloadStringAsync(new Uri("http://adii.hci.jit.su/connected?ts=" + DateTime.Now));
        }

        private void pollPaper(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            client.DownloadStringCompleted += (s, response) =>
            {
                if (response.Result == "0")
                {
                    paper = false;
                }
                else
                {
                    paper = true;
                }
            };
            client.DownloadStringAsync(new Uri("http://adii.hci.jit.su/paper?ts=" + DateTime.Now));
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

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
            dispatcherTimer.Start();

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

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (printing == true)
            {
                printing = false;
                pUp.IsOpen = false;
                pUp = null;
            }
        }

        public void setFirstScan()
        {
            firstPageScanned = true;
        }

        public bool firstScan()
        {
            return firstPageScanned;
        }

    }
}
