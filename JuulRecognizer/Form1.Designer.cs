namespace JuulRecognizer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.videoFrame = new Emgu.CV.UI.ImageBox();
            this.start_button = new System.Windows.Forms.Button();
            this.button_stop = new System.Windows.Forms.Button();
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.trainButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.videoFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // videoFrame
            // 
            this.videoFrame.Dock = System.Windows.Forms.DockStyle.Top;
            this.videoFrame.Location = new System.Drawing.Point(0, 0);
            this.videoFrame.Name = "videoFrame";
            this.videoFrame.Size = new System.Drawing.Size(1245, 499);
            this.videoFrame.TabIndex = 2;
            this.videoFrame.TabStop = false;
            // 
            // start_button
            // 
            this.start_button.Location = new System.Drawing.Point(1094, 505);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(139, 42);
            this.start_button.TabIndex = 3;
            this.start_button.Text = "Start";
            this.start_button.UseVisualStyleBackColor = true;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // button_stop
            // 
            this.button_stop.Location = new System.Drawing.Point(1094, 553);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(139, 43);
            this.button_stop.TabIndex = 4;
            this.button_stop.Text = "Stop";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(0, 505);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(713, 147);
            this.logBox.TabIndex = 5;
            this.logBox.Text = "";
            // 
            // trainButton
            // 
            this.trainButton.Location = new System.Drawing.Point(950, 505);
            this.trainButton.Name = "trainButton";
            this.trainButton.Size = new System.Drawing.Size(138, 42);
            this.trainButton.TabIndex = 6;
            this.trainButton.Text = "Train";
            this.trainButton.UseVisualStyleBackColor = true;
            this.trainButton.Click += new System.EventHandler(this.trainButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 664);
            this.Controls.Add(this.trainButton);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.button_stop);
            this.Controls.Add(this.start_button);
            this.Controls.Add(this.videoFrame);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.videoFrame)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Emgu.CV.UI.ImageBox videoFrame;
        private System.Windows.Forms.Button start_button;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.RichTextBox logBox;
        private System.Windows.Forms.Button trainButton;
    }
}

