namespace MQTTBroker
{
    partial class frmMQTTBrokerWindow
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMQTTBrokerWindow));
            pictureBox1 = new PictureBox();
            statusStrip = new StatusStrip();
            lblVersion = new ToolStripStatusLabel();
            progressBarPublish = new ProgressBar();
            label1 = new Label();
            btnStartPublish = new Button();
            btnStopPublish = new Button();
            timerPublish = new System.Windows.Forms.Timer(components);
            lblPublishedEvents = new Label();
            label3 = new Label();
            btnClearPublish = new Button();
            label2 = new Label();
            lstViewMQTTBroker = new ListView();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(1847, 61);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(46, 46);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { lblVersion });
            statusStrip.Location = new Point(0, 940);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1924, 22);
            statusStrip.TabIndex = 2;
            statusStrip.Text = "statusStrip";
            // 
            // lblVersion
            // 
            lblVersion.BackColor = SystemColors.Control;
            lblVersion.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblVersion.ForeColor = Color.SlateGray;
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(0, 17);
            // 
            // progressBarPublish
            // 
            progressBarPublish.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBarPublish.Location = new Point(31, 61);
            progressBarPublish.Name = "progressBarPublish";
            progressBarPublish.Size = new Size(1810, 46);
            progressBarPublish.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(31, 20);
            label1.Name = "label1";
            label1.Size = new Size(372, 28);
            label1.TabIndex = 5;
            label1.Text = "Publish MQTT Messages to Event Grid";
            // 
            // btnStartPublish
            // 
            btnStartPublish.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            btnStartPublish.Location = new Point(29, 113);
            btnStartPublish.Name = "btnStartPublish";
            btnStartPublish.Size = new Size(150, 46);
            btnStartPublish.TabIndex = 6;
            btnStartPublish.Text = "Start";
            btnStartPublish.UseVisualStyleBackColor = true;
            btnStartPublish.Click += btnPublishStart_Click;
            // 
            // btnStopPublish
            // 
            btnStopPublish.Enabled = false;
            btnStopPublish.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            btnStopPublish.Location = new Point(185, 113);
            btnStopPublish.Name = "btnStopPublish";
            btnStopPublish.Size = new Size(150, 46);
            btnStopPublish.TabIndex = 7;
            btnStopPublish.Text = "Stop";
            btnStopPublish.UseVisualStyleBackColor = true;
            btnStopPublish.Click += btnPublishStop_Click;
            // 
            // timerPublish
            // 
            timerPublish.Interval = 2000;
            timerPublish.Tick += timerPublish_Tick;
            // 
            // lblPublishedEvents
            // 
            lblPublishedEvents.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblPublishedEvents.AutoSize = true;
            lblPublishedEvents.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point);
            lblPublishedEvents.ForeColor = Color.DarkGreen;
            lblPublishedEvents.Location = new Point(1847, 20);
            lblPublishedEvents.Name = "lblPublishedEvents";
            lblPublishedEvents.Size = new Size(24, 28);
            lblPublishedEvents.TabIndex = 8;
            lblPublishedEvents.Text = "0";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.DarkGreen;
            label3.Location = new Point(1572, 20);
            label3.Name = "label3";
            label3.Size = new Size(269, 28);
            label3.TabIndex = 9;
            label3.Text = "Published MQTT Messages:";
            // 
            // btnClearPublish
            // 
            btnClearPublish.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            btnClearPublish.Location = new Point(341, 113);
            btnClearPublish.Name = "btnClearPublish";
            btnClearPublish.Size = new Size(150, 46);
            btnClearPublish.TabIndex = 10;
            btnClearPublish.Text = "Clear";
            btnClearPublish.UseVisualStyleBackColor = true;
            btnClearPublish.Click += btnPublishClear_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(31, 176);
            label2.Name = "label2";
            label2.Size = new Size(166, 28);
            label2.TabIndex = 12;
            label2.Text = "MQTT Messages";
            // 
            // lstViewMQTTBroker
            // 
            lstViewMQTTBroker.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstViewMQTTBroker.Location = new Point(31, 220);
            lstViewMQTTBroker.Name = "lstViewMQTTBroker";
            lstViewMQTTBroker.Size = new Size(1862, 654);
            lstViewMQTTBroker.TabIndex = 17;
            lstViewMQTTBroker.UseCompatibleStateImageBehavior = false;
            // 
            // frmMQTTBrokerWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1924, 962);
            Controls.Add(lstViewMQTTBroker);
            Controls.Add(label2);
            Controls.Add(btnClearPublish);
            Controls.Add(label3);
            Controls.Add(lblPublishedEvents);
            Controls.Add(btnStopPublish);
            Controls.Add(btnStartPublish);
            Controls.Add(label1);
            Controls.Add(progressBarPublish);
            Controls.Add(statusStrip);
            Controls.Add(pictureBox1);
            Name = "frmMQTTBrokerWindow";
            Text = "MQTT Broker";
            WindowState = FormWindowState.Maximized;
            Load += frmPushPullWindow_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox pictureBox1;
        private StatusStrip statusStrip;
        private ProgressBar progressBarPublish;
        private Label label1;
        private Button btnStartPublish;
        private Button btnStopPublish;
        private System.Windows.Forms.Timer timerPublish;
        private Label lblPublishedEvents;
        private Label label3;
        private Button btnClearPublish;
        private Label label2;
        private ListView lstViewMQTTBroker;
        private ToolStripStatusLabel lblVersion;
    }
}