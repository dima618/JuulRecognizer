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
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Newtonsoft.Json;
using System.IO;
using System.IO.Ports;
using System.Timers;

namespace JuulRecognizer
{
    public partial class Calibration : Form
    {

        private buttonState button = new buttonState();
        private System.Timers.Timer buttonHoldtimer;
        private System.Windows.Forms.Timer timer;
        private VideoCapture capture;
        private SerialPort port = new SerialPort();
        private Settings settings = new Settings();
        private Point virtualPoint = new Point(90, 90);

        public enum buttonState
        {
            Up, Down, Right, Left
        }

        public Calibration(SerialPort serial)
        {

            InitializeComponent();
            initializePerepherals(serial);
            initializeSettings();
            initializeTimer();


        }

        private void initializeTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(displayImage);
            timer.Interval = 33;
            timer.Start();
            buttonHoldtimer = new System.Timers.Timer();
            buttonHoldtimer.Interval = 1;
            buttonHoldtimer.Enabled = false;
            buttonHoldtimer.Elapsed += timerEllapsed;
            buttonHoldtimer.AutoReset = true;
        }

        private void initializeSettings()
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

        private void displayImage(object sender, EventArgs arg)
        {
            video.Image = capture.QueryFrame();
        }

        private void timerEllapsed(object sender, ElapsedEventArgs e)
        {
            switch (button)
            {
                case buttonState.Right:
                    virtualPoint.X--;
                    break;

                case buttonState.Up:
                    virtualPoint.Y++;
                    break;

                case buttonState.Left:
                    virtualPoint.X++;
                    break;

                case buttonState.Down:
                    virtualPoint.Y--;
                    break;
            }
            sendSerial();

            buttonHoldtimer.Interval = 100;
        }


        private void initializePerepherals(SerialPort serial)
        {
            try
            {
                capture = new VideoCapture();
            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot initialize camera");
                //this.Close();
            }

            try
            {
                port = serial;
                port.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot initialize on COM port");
                //this.Close();
            }
        }

        private void sendSerial()
        {
            if (virtualPoint.X < 180 && virtualPoint.X > 0 && virtualPoint.Y < 120 && virtualPoint.Y > 0 && port.IsOpen)
            {
                port.Write("X" + (virtualPoint.X).ToString() + ":Y" + (virtualPoint.Y).ToString()); //Data stream format
            }

        }


        private void mouseDown(object sender, MouseEventArgs e)
        {
            Button button1 = (Button)sender;

            if (button1.Equals(up))
            {
                button = buttonState.Up;
            }
            else if (button1.Equals(right))
            {
                button = buttonState.Right;
            }
            else if (button1.Equals(down))
            {
                button = buttonState.Down;
            }
            else if (button1.Equals(left))
            {
                button = buttonState.Left;
            }


            buttonHoldtimer.Enabled = true;
            buttonHoldtimer.Start();
        }

        private void mouseUp(object sender, MouseEventArgs e)
        {
            buttonHoldtimer.Enabled = false;
            buttonHoldtimer.Stop();
        }

        private void calibrate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Calibrate " + type.Text + "?", "", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                if (type.SelectedIndex != -1)
                {
                    if (type.SelectedIndex == 0)
                    {
                        settings.xLeftCalibration = virtualPoint.X;
                    }
                    else if (type.SelectedIndex == 1)
                    {
                        settings.yTopCalibration = virtualPoint.Y;
                    }
                    else if (type.SelectedIndex == 2)
                    {
                        settings.xRightCalibration = virtualPoint.X;
                    }
                    else if (type.SelectedIndex == 3)
                    {
                        settings.yBotCalibration = virtualPoint.Y;
                    }

                    saveToFile();
                }
            }
        }

        private void saveToFile()
        {
            File.WriteAllText(Properties.Resources.settingsFileName, JsonConvert.SerializeObject(settings));

        }

        private void Calibration_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            capture.Dispose();
            resetPos();
            //this.Dispose();
        }

        private void resetPos()
        {
            if (port.IsOpen)
                port.Write("X90:Y90");
        }

        private void Calibration_Load(object sender, EventArgs e)
        {
            //displayImage(sender, e);
            
        }

        private void video_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            timer.Start();
        }
    }

    public class Settings
    {
        public int xShift { get; set; }
        public int yShift { get; set; }
        public int xLeftCalibration { get; set; }
        public int xRightCalibration { get; set; }
        public int yTopCalibration { get; set; }
        public int yBotCalibration { get; set; }


        public Settings()
        {
            xShift = 0;
            yShift = 0;
            xLeftCalibration = 59;
            xRightCalibration = 118;
            yTopCalibration = 110;
            yBotCalibration = 70;
        }

    }
}
