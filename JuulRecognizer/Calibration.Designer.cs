namespace JuulRecognizer
{
    partial class Calibration
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
            this.video = new Emgu.CV.UI.ImageBox();
            this.up = new System.Windows.Forms.Button();
            this.down = new System.Windows.Forms.Button();
            this.left = new System.Windows.Forms.Button();
            this.right = new System.Windows.Forms.Button();
            this.calibrate = new System.Windows.Forms.Button();
            this.type = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.video)).BeginInit();
            this.SuspendLayout();
            // 
            // video
            // 
            this.video.Location = new System.Drawing.Point(93, 61);
            this.video.Name = "video";
            this.video.Size = new System.Drawing.Size(613, 446);
            this.video.TabIndex = 2;
            this.video.TabStop = false;
            this.video.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.video_LoadCompleted);
            // 
            // up
            // 
            this.up.Location = new System.Drawing.Point(357, 32);
            this.up.Name = "up";
            this.up.Size = new System.Drawing.Size(75, 23);
            this.up.TabIndex = 3;
            this.up.Text = "Up";
            this.up.UseVisualStyleBackColor = true;
            this.up.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseDown);
            this.up.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseUp);
            // 
            // down
            // 
            this.down.Location = new System.Drawing.Point(357, 513);
            this.down.Name = "down";
            this.down.Size = new System.Drawing.Size(75, 23);
            this.down.TabIndex = 4;
            this.down.Text = "Down";
            this.down.UseVisualStyleBackColor = true;
            this.down.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseDown);
            this.down.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseUp);
            // 
            // left
            // 
            this.left.Location = new System.Drawing.Point(12, 259);
            this.left.Name = "left";
            this.left.Size = new System.Drawing.Size(75, 23);
            this.left.TabIndex = 5;
            this.left.Text = "Left";
            this.left.UseVisualStyleBackColor = true;
            this.left.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseDown);
            this.left.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseUp);
            // 
            // right
            // 
            this.right.Location = new System.Drawing.Point(712, 259);
            this.right.Name = "right";
            this.right.Size = new System.Drawing.Size(75, 23);
            this.right.TabIndex = 6;
            this.right.Text = "Right";
            this.right.UseVisualStyleBackColor = true;
            this.right.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseDown);
            this.right.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseUp);
            // 
            // calibrate
            // 
            this.calibrate.Location = new System.Drawing.Point(713, 538);
            this.calibrate.Name = "calibrate";
            this.calibrate.Size = new System.Drawing.Size(75, 23);
            this.calibrate.TabIndex = 7;
            this.calibrate.Text = "Calibrate";
            this.calibrate.UseVisualStyleBackColor = true;
            this.calibrate.Click += new System.EventHandler(this.calibrate_Click);
            // 
            // type
            // 
            this.type.FormattingEnabled = true;
            this.type.Items.AddRange(new object[] {
            "X-Left",
            "Y-Top",
            "X-Right",
            "Y-Bottom"});
            this.type.Location = new System.Drawing.Point(667, 511);
            this.type.Name = "type";
            this.type.Size = new System.Drawing.Size(121, 21);
            this.type.TabIndex = 8;
            // 
            // Calibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 573);
            this.Controls.Add(this.type);
            this.Controls.Add(this.calibrate);
            this.Controls.Add(this.right);
            this.Controls.Add(this.left);
            this.Controls.Add(this.down);
            this.Controls.Add(this.up);
            this.Controls.Add(this.video);
            this.Name = "Calibration";
            this.Text = "Calibration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Calibration_FormClosing);
            this.Load += new System.EventHandler(this.Calibration_Load);
            ((System.ComponentModel.ISupportInitialize)(this.video)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Emgu.CV.UI.ImageBox video;
        private System.Windows.Forms.Button up;
        private System.Windows.Forms.Button down;
        private System.Windows.Forms.Button left;
        private System.Windows.Forms.Button right;
        private System.Windows.Forms.Button calibrate;
        private System.Windows.Forms.ComboBox type;
    }
}