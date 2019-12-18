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
using System.Diagnostics;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using Newtonsoft.Json;

namespace JuulRecognizer
{
    public partial class Form1 : Form
    {
        public System.Windows.Forms.Timer timer { get; set; }
        public Stopwatch juulTimer { get; set; }
        public Rectangle target { get; set; }
        public VideoCapture cap { get; set; }
        public CascadeClassifier JuulDetection { get; set; }
        public CascadeClassifier FaceDetection { get; set; }

        private Settings settings;
        private float xDivConst;
        private float yDivConst;
        private bool Aim = false;
        private bool Fire = false;
        private float locationRange = 50;

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
            JuulDetection = new CascadeClassifier(Path.GetFullPath(@"../../Res/cascade_8.xml"));
            FaceDetection = new CascadeClassifier(Path.GetFullPath(@"../../Res/haarcascade_frontalface_default.xml"));
            Frame = new Mat();
            Faces = new List<Image<Gray, byte>>();
            IDs = new List<int>();
            timer = new System.Windows.Forms.Timer();
            juulTimer = new Stopwatch();
            initSerial();
            Capture();
        }

        private void initSerial()
        {
            try
            {
                serial = new SerialPort("COM4", 9600);
                serial.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            //videoFrame.Image = null;
        }

        void updateImage(object sender, EventArgs arg)
        {
            var frame = cap.QueryFrame();

            if (frame != null)
            {
                var img = frame.ToImage<Bgr, byte>();
                //img = img.Flip(Emgu.CV.CvEnum.FlipType.Horizontal);
                Image<Gray, byte> grayframe = img.Convert<Gray, byte>();
                var faces = FaceDetection.DetectMultiScale(grayframe, 1.3, 5);
                
                var juuls = JuulDetection.DetectMultiScale(grayframe, 1.5, 5);
                
                foreach (var face in faces)
                {
                    foreach (var juul in juuls)
                    {
                        PointF centerFace = new PointF(face.Location.X + face.Width / 2, face.Location.Y + face.Height / 2);
                        calculatePoint(centerFace, false);
                        if (juul.IntersectsWith(face) && !Aim && !Fire)
                        {
                            Aim = true;
                            juulTimer.Start();
                            //PointF centerFace = new PointF(face.Location.X + face.Width / 2, face.Location.Y + face.Height / 2);
                            //calculatePoint(centerFace, false);
                        }
                        else if (juul.IntersectsWith(face) && Aim && !Fire)
                        {
                            if (juulTimer.Elapsed.Seconds > 2)
                            {
                                Fire = true;
                            }
                            //PointF centerFace = new PointF(face.Location.X + face.Width / 2, face.Location.Y + face.Height / 2);
                            //calculatePoint(centerFace, false);
                        }
                        else if (juul.IntersectsWith(face) && Aim && Fire)
                        {
                            Console.WriteLine("AIMING AND FIRING!!!!!");
                            //PointF centerFace = new PointF(face.Location.X + face.Width / 2, face.Location.Y + face.Height / 2);
                            //calculatePoint(centerFace, true);
                        }
                        else
                        {
                            Aim = false;
                            Fire = false;
                        }
                    }
                    img.Draw(face, new Bgr(0, double.MaxValue, 0), 3);
                }
                foreach (var juul in juuls)
                {
                    img.Draw(juul, new Bgr(100, double.MaxValue, 10), 3);
                }
                videoFrame.Image = img;
            }
        }


        private void calculatePoint(PointF point, bool fire)
        {
            sendSignal(new Point(((int)(settings.xRightCalibration - (point.X / xDivConst))), ((int)(settings.yTopCalibration - (point.Y / yDivConst)))), fire);
        }

        private void sendSignal(Point currentPoint, bool fire)
        {
            if (fire)
            {
                serial.Write("X" + (currentPoint.X + settings.xShift).ToString() + ":Y" + (currentPoint.Y + settings.yShift).ToString() + ":F"); //Data stream format
            }
            else
            {
                serial.Write("X" + (currentPoint.X + settings.xShift).ToString() + ":Y" + (currentPoint.Y + settings.yShift).ToString()); //Data stream format
            }
            logBox.AppendText("Auto: X" + (currentPoint.X + settings.xShift).ToString() + "    Y" + (currentPoint.Y + settings.yShift).ToString() + Environment.NewLine); //Display detection position

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initializeJson();
            calculateDivisionConstants();
        }

        private void start_button_Click(object sender, EventArgs e)
        {
            Capture();
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            EndCapture();
        }

        private void Calibrate_Click(object sender, EventArgs e)
        {
            
            try
            {

                serial.Close();

            } catch
            {

            }
            try
            {
                EndCapture();
                Calibration calibrate = new Calibration(serial);
                calibrate.ShowDialog();
                calibrate.Dispose();
            }
            catch
            {

            }
        }

        private void calculateDivisionConstants()
        {
            xDivConst = (cap.QueryFrame().Width) / (settings.xRightCalibration - settings.xLeftCalibration);
            yDivConst = (cap.QueryFrame().Height) / (settings.yTopCalibration - settings.yBotCalibration);
        }

        private void initializeJson()
        {
            try
            {
                settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Properties.Resources.settingsFileName));
            }

            catch (FileNotFoundException e)
            {
                MessageBox.Show("No memory file detected, generating one");
                settings = new Settings();
                saveSettings();

            }

            catch (JsonReaderException e)
            {
                MessageBox.Show("Corrupt Memory File");
                File.Delete(Properties.Resources.settingsFileName);
                this.Close();
            }
        }

        private void saveSettings()
        {
            File.WriteAllText(Properties.Resources.settingsFileName, JsonConvert.SerializeObject(settings));
        }
    }
}