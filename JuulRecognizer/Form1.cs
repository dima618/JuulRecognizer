using System;
using System.IO;
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
        public Timer timer { get; set; }
        public VideoCapture cap { get; set; } //new VideoCapture();
        public EigenFaceRecognizer FaceRecognition { get; set; }//= new EigenFaceRecognizer();
        public CascadeClassifier FaceDetection { get; set; }//= new CascadeClassifier();

        public Mat Frame { get; set; }
        public List<Image<Gray, byte>> Faces { get; set; }
        public List<int> IDs { get; set; }

        public int ImageWidth { get; set; } = 128;
        public int ImageHeight { get; set; } = 150;


        public string YMLPath { get; set; } = @"../../Res/trainingData.yml";

        public Form1()
        {
            InitializeComponent();
            cap = new VideoCapture();
            FaceRecognition = new EigenFaceRecognizer(80, double.PositiveInfinity);
            FaceDetection = new CascadeClassifier(Path.GetFullPath(@"../../Res/face-detection-retail-0004.xml"));
            Frame = new Mat();
            Faces = new List<Image<Gray, byte>>();
            IDs = new List<int>();
            timer = new Timer();
            //Capture();
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
                foreach (var face in faces)
                {
                    img.Draw(face, new Bgr(0, double.MaxValue, 0), 3);
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

        private void trainButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}