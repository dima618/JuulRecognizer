using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;

namespace JuulRecognizer
{
    public partial class Form1 : Form
    {
        private Timer timer = new Timer();
        private VideoCapture cap = new VideoCapture();

        public Form1()
        {
            InitializeComponent();
        }

        private void initTimer()
        {
            timer.Tick += new EventHandler();
        }

        private void updateFrames()
        {

        }
    }
}
