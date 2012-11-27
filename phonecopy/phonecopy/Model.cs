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


/*Singleton object for storing internal state data*/
namespace phonecopy
{
    public class Model
    {
        private static Model thisInst = null;

        private bool PDF = false;

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
    }
}
