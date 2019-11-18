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

        void Capture()
        {
            timer.Tick += new EventHandler(updateImage);
            timer.Interval = 33;
            timer.Start();
        }

        void EndCapture()
        {
            timer.Stop();
        }

        void updateImage(object sender, EventArgs arg)
        {
            videoFrame.Image = cap.QueryFrame();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void start_button_Click(object sender, EventArgs e)
        {
            Capture();
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            EndCapture();
        }
    }
}