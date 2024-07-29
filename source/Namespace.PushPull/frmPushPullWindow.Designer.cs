namespace Namespace.PushPull
{
    partial class frmPushPullWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPushPullWindow));
            pictureBox1 = new PictureBox();
            statusStrip = new StatusStrip();
            lblVersion = new ToolStripStatusLabel();
            lblListener = new ToolStripStatusLabel();
            progressBarPublish = new ProgressBar();
            lblPublish = new Label();
            btnStartPublish = new Button();
            btnStopPublish = new Button();
            timerPublish = new System.Windows.Forms.Timer(components);
            lblPublishedEvents = new Label();
            label3 = new Label();
            btnClearPublish = new Button();
            lstViewPush = new ListView();
            label2 = new Label();
            btnClearPush = new Button();
            label4 = new Label();
            lstViewPull = new ListView();
            btnStartPull = new Button();
            timerPull = new System.Windows.Forms.Timer(components);
            btnStopPull = new Button();
            btnClearPull = new Button();
            btnDiagram = new Button();
            rdbCustomEvents = new RadioButton();
            rdbSystemEvents = new RadioButton();
            pbCustomEvents = new PictureBox();
            pbStorageEvents = new PictureBox();
            rdbRelaySDK = new RadioButton();
            rdbBridge = new RadioButton();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            label1 = new Label();
            lblPulledEvents = new Label();
            label5 = new Label();
            lblPushedEvents = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbCustomEvents).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbStorageEvents).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(3272, 130);
            pictureBox1.Margin = new Padding(6);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(85, 86);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(32, 32);
            statusStrip.Items.AddRange(new ToolStripItem[] { lblVersion, lblListener });
            statusStrip.Location = new Point(0, 1896);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(2, 0, 26, 0);
            statusStrip.Size = new Size(3402, 42);
            statusStrip.TabIndex = 2;
            statusStrip.Text = "statusStrip1";
            // 
            // lblVersion
            // 
            lblVersion.BackColor = SystemColors.Control;
            lblVersion.ForeColor = Color.SlateGray;
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(0, 32);
            // 
            // lblListener
            // 
            lblListener.BackColor = SystemColors.Control;
            lblListener.ForeColor = Color.SlateGray;
            lblListener.Name = "lblListener";
            lblListener.Size = new Size(123, 32);
            lblListener.Text = "lblListener";
            // 
            // progressBarPublish
            // 
            progressBarPublish.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBarPublish.Location = new Point(155, 130);
            progressBarPublish.Margin = new Padding(6);
            progressBarPublish.Name = "progressBarPublish";
            progressBarPublish.Size = new Size(3105, 86);
            progressBarPublish.TabIndex = 4;
            // 
            // lblPublish
            // 
            lblPublish.AutoSize = true;
            lblPublish.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblPublish.Location = new Point(58, 43);
            lblPublish.Margin = new Padding(6, 0, 6, 0);
            lblPublish.Name = "lblPublish";
            lblPublish.Size = new Size(1053, 54);
            lblPublish.TabIndex = 5;
            lblPublish.Text = "Publish custom events to Azure Event Grid Namespace";
            // 
            // btnStartPublish
            // 
            btnStartPublish.BackColor = Color.MediumAquamarine;
            btnStartPublish.Font = new Font("Segoe UI", 15F);
            btnStartPublish.Location = new Point(58, 242);
            btnStartPublish.Margin = new Padding(6);
            btnStartPublish.Name = "btnStartPublish";
            btnStartPublish.Size = new Size(279, 98);
            btnStartPublish.TabIndex = 6;
            btnStartPublish.Text = "Start";
            btnStartPublish.UseVisualStyleBackColor = false;
            btnStartPublish.Click += btnPublishStart_Click;
            // 
            // btnStopPublish
            // 
            btnStopPublish.BackColor = Color.MistyRose;
            btnStopPublish.Enabled = false;
            btnStopPublish.Font = new Font("Segoe UI", 15F);
            btnStopPublish.Location = new Point(349, 242);
            btnStopPublish.Margin = new Padding(6);
            btnStopPublish.Name = "btnStopPublish";
            btnStopPublish.Size = new Size(279, 98);
            btnStopPublish.TabIndex = 7;
            btnStopPublish.Text = "Stop";
            btnStopPublish.UseVisualStyleBackColor = false;
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
            lblPublishedEvents.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblPublishedEvents.ForeColor = Color.DarkGreen;
            lblPublishedEvents.Location = new Point(3287, 56);
            lblPublishedEvents.Margin = new Padding(6, 0, 6, 0);
            lblPublishedEvents.Name = "lblPublishedEvents";
            lblPublishedEvents.Size = new Size(46, 54);
            lblPublishedEvents.TabIndex = 8;
            lblPublishedEvents.Text = "0";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label3.ForeColor = Color.DarkGreen;
            label3.Location = new Point(2922, 56);
            label3.Margin = new Padding(6, 0, 6, 0);
            label3.Name = "label3";
            label3.Size = new Size(353, 54);
            label3.TabIndex = 9;
            label3.Text = "Published Events:";
            // 
            // btnClearPublish
            // 
            btnClearPublish.BackColor = Color.LemonChiffon;
            btnClearPublish.Font = new Font("Segoe UI", 15F);
            btnClearPublish.Location = new Point(640, 242);
            btnClearPublish.Margin = new Padding(6);
            btnClearPublish.Name = "btnClearPublish";
            btnClearPublish.Size = new Size(279, 98);
            btnClearPublish.TabIndex = 10;
            btnClearPublish.Text = "Clear";
            btnClearPublish.UseVisualStyleBackColor = false;
            btnClearPublish.Click += btnPublishClear_Click;
            // 
            // lstViewPush
            // 
            lstViewPush.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lstViewPush.Location = new Point(58, 458);
            lstViewPush.Margin = new Padding(6);
            lstViewPush.Name = "lstViewPush";
            lstViewPush.Size = new Size(3299, 625);
            lstViewPush.TabIndex = 11;
            lstViewPush.UseCompatibleStateImageBehavior = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label2.Location = new Point(58, 371);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(1079, 54);
            label2.TabIndex = 12;
            label2.Text = "Push delivery to On-Premise Webhook with Azure Relay";
            // 
            // btnClearPush
            // 
            btnClearPush.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClearPush.BackColor = Color.LemonChiffon;
            btnClearPush.Font = new Font("Segoe UI", 15F);
            btnClearPush.Location = new Point(3078, 350);
            btnClearPush.Margin = new Padding(6);
            btnClearPush.Name = "btnClearPush";
            btnClearPush.Size = new Size(279, 96);
            btnClearPush.TabIndex = 15;
            btnClearPush.Text = "Clear";
            btnClearPush.UseVisualStyleBackColor = false;
            btnClearPush.Click += btnClearPush_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label4.Location = new Point(58, 1117);
            label4.Margin = new Padding(6, 0, 6, 0);
            label4.Name = "label4";
            label4.Size = new Size(697, 54);
            label4.TabIndex = 16;
            label4.Text = "Pull delivery from Namespace topic";
            // 
            // lstViewPull
            // 
            lstViewPull.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lstViewPull.Location = new Point(58, 1205);
            lstViewPull.Margin = new Padding(6);
            lstViewPull.Name = "lstViewPull";
            lstViewPull.Size = new Size(3299, 625);
            lstViewPull.TabIndex = 17;
            lstViewPull.UseCompatibleStateImageBehavior = false;
            // 
            // btnStartPull
            // 
            btnStartPull.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnStartPull.BackColor = Color.MediumAquamarine;
            btnStartPull.Font = new Font("Segoe UI", 15F);
            btnStartPull.Location = new Point(2496, 1095);
            btnStartPull.Margin = new Padding(6);
            btnStartPull.Name = "btnStartPull";
            btnStartPull.Size = new Size(279, 98);
            btnStartPull.TabIndex = 18;
            btnStartPull.Text = "Start";
            btnStartPull.UseVisualStyleBackColor = false;
            btnStartPull.Click += btnStartPull_Click;
            // 
            // timerPull
            // 
            timerPull.Interval = 1000;
            timerPull.Tick += timerPull_Tick;
            // 
            // btnStopPull
            // 
            btnStopPull.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnStopPull.BackColor = Color.MistyRose;
            btnStopPull.Enabled = false;
            btnStopPull.Font = new Font("Segoe UI", 15F);
            btnStopPull.Location = new Point(2787, 1095);
            btnStopPull.Margin = new Padding(6);
            btnStopPull.Name = "btnStopPull";
            btnStopPull.Size = new Size(279, 98);
            btnStopPull.TabIndex = 19;
            btnStopPull.Text = "Stop";
            btnStopPull.UseVisualStyleBackColor = false;
            btnStopPull.Click += btnStopPull_Click;
            // 
            // btnClearPull
            // 
            btnClearPull.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClearPull.BackColor = Color.LemonChiffon;
            btnClearPull.Font = new Font("Segoe UI", 15F);
            btnClearPull.Location = new Point(3078, 1095);
            btnClearPull.Margin = new Padding(6);
            btnClearPull.Name = "btnClearPull";
            btnClearPull.Size = new Size(279, 98);
            btnClearPull.TabIndex = 20;
            btnClearPull.Text = "Clear";
            btnClearPull.UseVisualStyleBackColor = false;
            btnClearPull.Click += btnClearPull_Click;
            // 
            // btnDiagram
            // 
            btnDiagram.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDiagram.BackColor = Color.LightSteelBlue;
            btnDiagram.Font = new Font("Segoe UI", 15F);
            btnDiagram.Location = new Point(3078, 240);
            btnDiagram.Margin = new Padding(6);
            btnDiagram.Name = "btnDiagram";
            btnDiagram.Size = new Size(279, 98);
            btnDiagram.TabIndex = 21;
            btnDiagram.Text = "Diagram";
            btnDiagram.UseVisualStyleBackColor = false;
            btnDiagram.Click += btnDiagram_Click;
            // 
            // rdbCustomEvents
            // 
            rdbCustomEvents.AutoSize = true;
            rdbCustomEvents.Checked = true;
            rdbCustomEvents.Font = new Font("Segoe UI", 15F);
            rdbCustomEvents.Location = new Point(30, 34);
            rdbCustomEvents.Name = "rdbCustomEvents";
            rdbCustomEvents.Size = new Size(315, 58);
            rdbCustomEvents.TabIndex = 23;
            rdbCustomEvents.TabStop = true;
            rdbCustomEvents.Text = "Custom Events";
            rdbCustomEvents.UseVisualStyleBackColor = true;
            rdbCustomEvents.CheckedChanged += rdbCustomEvents_CheckedChanged;
            // 
            // rdbSystemEvents
            // 
            rdbSystemEvents.AutoSize = true;
            rdbSystemEvents.Font = new Font("Segoe UI", 15F);
            rdbSystemEvents.Location = new Point(361, 38);
            rdbSystemEvents.Name = "rdbSystemEvents";
            rdbSystemEvents.Size = new Size(714, 58);
            rdbSystemEvents.TabIndex = 24;
            rdbSystemEvents.Text = "Azure System Events (Storage events)";
            rdbSystemEvents.UseVisualStyleBackColor = true;
            rdbSystemEvents.CheckedChanged += rdbSystemEvents_CheckedChanged;
            // 
            // pbCustomEvents
            // 
            pbCustomEvents.Image = (Image)resources.GetObject("pbCustomEvents.Image");
            pbCustomEvents.Location = new Point(58, 130);
            pbCustomEvents.Margin = new Padding(6);
            pbCustomEvents.Name = "pbCustomEvents";
            pbCustomEvents.Size = new Size(85, 86);
            pbCustomEvents.SizeMode = PictureBoxSizeMode.StretchImage;
            pbCustomEvents.TabIndex = 25;
            pbCustomEvents.TabStop = false;
            // 
            // pbStorageEvents
            // 
            pbStorageEvents.Image = (Image)resources.GetObject("pbStorageEvents.Image");
            pbStorageEvents.Location = new Point(58, 130);
            pbStorageEvents.Margin = new Padding(6);
            pbStorageEvents.Name = "pbStorageEvents";
            pbStorageEvents.Size = new Size(85, 86);
            pbStorageEvents.SizeMode = PictureBoxSizeMode.StretchImage;
            pbStorageEvents.TabIndex = 26;
            pbStorageEvents.TabStop = false;
            pbStorageEvents.Visible = false;
            // 
            // rdbRelaySDK
            // 
            rdbRelaySDK.AutoSize = true;
            rdbRelaySDK.Checked = true;
            rdbRelaySDK.Font = new Font("Segoe UI", 15F);
            rdbRelaySDK.Location = new Point(30, 30);
            rdbRelaySDK.Name = "rdbRelaySDK";
            rdbRelaySDK.Size = new Size(343, 58);
            rdbRelaySDK.TabIndex = 27;
            rdbRelaySDK.TabStop = true;
            rdbRelaySDK.Text = "Azure Relay SDK";
            rdbRelaySDK.UseVisualStyleBackColor = true;
            rdbRelaySDK.CheckedChanged += rdbRelaySDK_CheckedChanged;
            // 
            // rdbBridge
            // 
            rdbBridge.AutoSize = true;
            rdbBridge.Font = new Font("Segoe UI", 15F);
            rdbBridge.Location = new Point(388, 30);
            rdbBridge.Name = "rdbBridge";
            rdbBridge.Size = new Size(387, 58);
            rdbBridge.TabIndex = 28;
            rdbBridge.Text = "Azure Relay Bridge";
            rdbBridge.UseVisualStyleBackColor = true;
            rdbBridge.CheckedChanged += rdbBridge_CheckedChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rdbCustomEvents);
            groupBox1.Controls.Add(rdbSystemEvents);
            groupBox1.Location = new Point(977, 230);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1109, 110);
            groupBox1.TabIndex = 29;
            groupBox1.TabStop = false;
            groupBox1.Text = "Event types";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(rdbRelaySDK);
            groupBox2.Controls.Add(rdbBridge);
            groupBox2.Location = new Point(2111, 230);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(781, 110);
            groupBox2.TabIndex = 30;
            groupBox2.TabStop = false;
            groupBox2.Text = "Azure Relay mode ";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label1.ForeColor = Color.DarkGreen;
            label1.Location = new Point(2140, 1117);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(286, 54);
            label1.TabIndex = 31;
            label1.Text = "Pulled Events:";
            label1.Visible = false;
            // 
            // lblPulledEvents
            // 
            lblPulledEvents.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblPulledEvents.AutoSize = true;
            lblPulledEvents.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblPulledEvents.ForeColor = Color.DarkGreen;
            lblPulledEvents.Location = new Point(2438, 1117);
            lblPulledEvents.Margin = new Padding(6, 0, 6, 0);
            lblPulledEvents.Name = "lblPulledEvents";
            lblPulledEvents.Size = new Size(46, 54);
            lblPulledEvents.TabIndex = 32;
            lblPulledEvents.Text = "0";
            lblPulledEvents.Visible = false;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label5.ForeColor = Color.DarkGreen;
            label5.Location = new Point(2702, 371);
            label5.Margin = new Padding(6, 0, 6, 0);
            label5.Name = "label5";
            label5.Size = new Size(306, 54);
            label5.TabIndex = 33;
            label5.Text = "Pushed Events:";
            label5.Visible = false;
            // 
            // lblPushedEvents
            // 
            lblPushedEvents.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblPushedEvents.AutoSize = true;
            lblPushedEvents.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblPushedEvents.ForeColor = Color.DarkGreen;
            lblPushedEvents.Location = new Point(3020, 371);
            lblPushedEvents.Margin = new Padding(6, 0, 6, 0);
            lblPushedEvents.Name = "lblPushedEvents";
            lblPushedEvents.Size = new Size(46, 54);
            lblPushedEvents.TabIndex = 34;
            lblPushedEvents.Text = "0";
            lblPushedEvents.Visible = false;
            // 
            // frmPushPullWindow
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(3402, 1938);
            Controls.Add(lblPushedEvents);
            Controls.Add(label5);
            Controls.Add(lblPulledEvents);
            Controls.Add(label1);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(pbStorageEvents);
            Controls.Add(pbCustomEvents);
            Controls.Add(btnDiagram);
            Controls.Add(btnClearPull);
            Controls.Add(btnStopPull);
            Controls.Add(btnStartPull);
            Controls.Add(lstViewPull);
            Controls.Add(label4);
            Controls.Add(btnClearPush);
            Controls.Add(label2);
            Controls.Add(lstViewPush);
            Controls.Add(btnClearPublish);
            Controls.Add(label3);
            Controls.Add(lblPublishedEvents);
            Controls.Add(btnStopPublish);
            Controls.Add(btnStartPublish);
            Controls.Add(lblPublish);
            Controls.Add(progressBarPublish);
            Controls.Add(statusStrip);
            Controls.Add(pictureBox1);
            Margin = new Padding(6);
            Name = "frmPushPullWindow";
            Text = "Azure Event Grid Namespace Delivery Using Push and Pull";
            FormClosing += frmPushPullWindow_FormClosing;
            Load += frmPushPullWindow_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbCustomEvents).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbStorageEvents).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox pictureBox1;
        private StatusStrip statusStrip;
        private ProgressBar progressBarPublish;
        private Label lblPublish;
        private Button btnStartPublish;
        private Button btnStopPublish;
        private System.Windows.Forms.Timer timerPublish;
        private Label lblPublishedEvents;
        private Label label3;
        private Button btnClearPublish;
        private ListView lstViewPush;
        private Label label2;
        private Button btnClearPush;
        private Label label4;
        private ListView lstViewPull;
        private Button btnStartPull;
        private System.Windows.Forms.Timer timerPull;
        private Button btnStopPull;
        private Button btnClearPull;
        private ToolStripStatusLabel lblVersion;
        private Button btnDiagram;
        private RadioButton rdbCustomEvents;
        private RadioButton rdbSystemEvents;
        private PictureBox pbCustomEvents;
        private PictureBox pbStorageEvents;
        private ToolStripStatusLabel lblListener;
        private RadioButton rdbRelaySDK;
        private RadioButton rdbBridge;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label lblPulledEvents;
        private Label label5;
        private Label lblPushedEvents;
    }
}