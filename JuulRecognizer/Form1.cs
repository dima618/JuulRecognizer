using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Structure;

namespace JuulRecognizer
{
    public partial class Form1 : Form
    {
        public System.Windows.Forms.Timer timer { get; set; }
        public VideoCapture cap { get; set; } //new VideoCapture();
        public CascadeClassifier JuulDetection { get; set; }//= new EigenFaceRecognizer();
        public CascadeClassifier FaceDetection { get; set; }//= new CascadeClassifier();

        public Mat Frame { get; set; }
        public List<Image<Gray, byte>> Faces { get; set; }
        public List<int> IDs { get; set; }

        public int ImageWidth { get; set; } = 128;
        public int ImageHeight { get; set; } = 150;

        public SerialPort serial;


        public string YMLPath { get; set; } = @"../../Res/trainingData.yml";

        public Form1()
        {
            InitializeComponent();
            cap = new VideoCapture();
            JuulDetection = new CascadeClassifier(Path.GetFullPath(@"../../Res/juulcascade.xml"));
            FaceDetection = new CascadeClassifier(Path.GetFullPath(@"../../Res/haarcascade_frontalface_default.xml"));
            Frame = new Mat();
            Faces = new List<Image<Gray, byte>>();
            IDs = new List<int>();
            timer = new System.Windows.Forms.Timer();
            serial = new SerialPort("COM4", 9600);
            serial.Open();
            Capture();
        }


        private void Capture()
        {
            logBox.AppendText($"Capture Started... {Environment.NewLine}");
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
            var frame = cap.QueryFrame();

            if (frame != null)
            {
                var img = frame.ToImage<Bgr, byte>();
                Image<Gray, byte> grayframe = img.Convert<Gray, byte>();
                var faces = FaceDetection.DetectMultiScale(grayframe, 1.3, 5);
                var juuls = JuulDetection.DetectMultiScale(grayframe, 1.3, 5);
                if (faces.Count() > 0)
                {
                    serial.Write("1");
                }
                else
                {
                    serial.Write("0");
                }
                Console.WriteLine(juuls.Count());
                foreach (var face in faces)
                {
                    img.Draw(face, new Bgr(0, double.MaxValue, 0), 3);
                }
                foreach (var juul in juuls)
                {
                    img.Draw(juul, new Bgr(100, double.MaxValue, 10), 3);
                }
                videoFrame.Image = img;
            }
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