using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PdfTemplator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.DocumentText = "<html><head></head><body><div id='divContainer'><table><tr><td>click</td><td></td></tr></table></div></body></html>";
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
          var item= webBrowser1.Document.GetElementById("divContainer");
        }

       
    }
}
