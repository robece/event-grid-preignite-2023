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
            lstViewWebhook = new ListView();
            label2 = new Label();
            btnClearEventHub = new Button();
            label4 = new Label();
            lstViewPull = new ListView();
            btnStartPull = new Button();
            timerPull = new System.Windows.Forms.Timer(components);
            btnStopPull = new Button();
            btnClearPull = new Button();
            btnArchitecture = new Button();
            rdbCustomEvents = new RadioButton();
            rdbSystemEvents = new RadioButton();
            pbCustomEvents = new PictureBox();
            pbStorageEvents = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbCustomEvents).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbStorageEvents).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(3266, 130);
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
            statusStrip.Location = new Point(0, 2144);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(2, 0, 26, 0);
            statusStrip.Size = new Size(3409, 42);
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
            progressBarPublish.Size = new Size(3100, 86);
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
            btnStartPublish.Font = new Font("Segoe UI", 15F);
            btnStartPublish.Location = new Point(58, 228);
            btnStartPublish.Margin = new Padding(6);
            btnStartPublish.Name = "btnStartPublish";
            btnStartPublish.Size = new Size(279, 98);
            btnStartPublish.TabIndex = 6;
            btnStartPublish.Text = "Start";
            btnStartPublish.UseVisualStyleBackColor = true;
            btnStartPublish.Click += btnPublishStart_Click;
            // 
            // btnStopPublish
            // 
            btnStopPublish.Enabled = false;
            btnStopPublish.Font = new Font("Segoe UI", 15F);
            btnStopPublish.Location = new Point(349, 228);
            btnStopPublish.Margin = new Padding(6);
            btnStopPublish.Name = "btnStopPublish";
            btnStopPublish.Size = new Size(279, 98);
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
            lblPublishedEvents.Font = new Font("Segoe UI", 15F);
            lblPublishedEvents.ForeColor = Color.DarkGreen;
            lblPublishedEvents.Location = new Point(3278, 56);
            lblPublishedEvents.Margin = new Padding(6, 0, 6, 0);
            lblPublishedEvents.Name = "lblPublishedEvents";
            lblPublishedEvents.Size = new Size(45, 54);
            lblPublishedEvents.TabIndex = 8;
            lblPublishedEvents.Text = "0";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15F);
            label3.ForeColor = Color.DarkGreen;
            label3.Location = new Point(2952, 56);
            label3.Margin = new Padding(6, 0, 6, 0);
            label3.Name = "label3";
            label3.Size = new Size(331, 54);
            label3.TabIndex = 9;
            label3.Text = "Published Events:";
            // 
            // btnClearPublish
            // 
            btnClearPublish.Font = new Font("Segoe UI", 15F);
            btnClearPublish.Location = new Point(640, 228);
            btnClearPublish.Margin = new Padding(6);
            btnClearPublish.Name = "btnClearPublish";
            btnClearPublish.Size = new Size(279, 98);
            btnClearPublish.TabIndex = 10;
            btnClearPublish.Text = "Clear";
            btnClearPublish.UseVisualStyleBackColor = true;
            btnClearPublish.Click += btnPublishClear_Click;
            // 
            // lstViewWebhook
            // 
            lstViewWebhook.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lstViewWebhook.Location = new Point(58, 486);
            lstViewWebhook.Margin = new Padding(6);
            lstViewWebhook.Name = "lstViewWebhook";
            lstViewWebhook.Size = new Size(3290, 723);
            lstViewWebhook.TabIndex = 11;
            lstViewWebhook.UseCompatibleStateImageBehavior = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label2.Location = new Point(58, 398);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(1079, 54);
            label2.TabIndex = 12;
            label2.Text = "Push delivery to On-Premise Webhook with Azure Relay";
            // 
            // btnClearEventHub
            // 
            btnClearEventHub.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClearEventHub.Font = new Font("Segoe UI", 15F);
            btnClearEventHub.Location = new Point(3069, 376);
            btnClearEventHub.Margin = new Padding(6);
            btnClearEventHub.Name = "btnClearEventHub";
            btnClearEventHub.Size = new Size(279, 98);
            btnClearEventHub.TabIndex = 15;
            btnClearEventHub.Text = "Clear";
            btnClearEventHub.UseVisualStyleBackColor = true;
            btnClearEventHub.Click += btnClearEventHub_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label4.Location = new Point(58, 1286);
            label4.Margin = new Padding(6, 0, 6, 0);
            label4.Name = "label4";
            label4.Size = new Size(697, 54);
            label4.TabIndex = 16;
            label4.Text = "Pull delivery from Namespace topic";
            // 
            // lstViewPull
            // 
            lstViewPull.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lstViewPull.Location = new Point(58, 1373);
            lstViewPull.Margin = new Padding(6);
            lstViewPull.Name = "lstViewPull";
            lstViewPull.Size = new Size(3290, 723);
            lstViewPull.TabIndex = 17;
            lstViewPull.UseCompatibleStateImageBehavior = false;
            // 
            // btnStartPull
            // 
            btnStartPull.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnStartPull.Font = new Font("Segoe UI", 15F);
            btnStartPull.Location = new Point(2490, 1264);
            btnStartPull.Margin = new Padding(6);
            btnStartPull.Name = "btnStartPull";
            btnStartPull.Size = new Size(279, 98);
            btnStartPull.TabIndex = 18;
            btnStartPull.Text = "Start";
            btnStartPull.UseVisualStyleBackColor = true;
            btnStartPull.Click += btnStartPull_Click;
            // 
            // timerPull
            // 
            timerPull.Interval = 2000;
            timerPull.Tick += timerPull_Tick;
            // 
            // btnStopPull
            // 
            btnStopPull.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnStopPull.Enabled = false;
            btnStopPull.Font = new Font("Segoe UI", 15F);
            btnStopPull.Location = new Point(2781, 1263);
            btnStopPull.Margin = new Padding(6);
            btnStopPull.Name = "btnStopPull";
            btnStopPull.Size = new Size(279, 98);
            btnStopPull.TabIndex = 19;
            btnStopPull.Text = "Stop";
            btnStopPull.UseVisualStyleBackColor = true;
            btnStopPull.Click += btnStopPull_Click;
            // 
            // btnClearPull
            // 
            btnClearPull.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClearPull.Font = new Font("Segoe UI", 15F);
            btnClearPull.Location = new Point(3072, 1263);
            btnClearPull.Margin = new Padding(6);
            btnClearPull.Name = "btnClearPull";
            btnClearPull.Size = new Size(279, 98);
            btnClearPull.TabIndex = 20;
            btnClearPull.Text = "Clear";
            btnClearPull.UseVisualStyleBackColor = true;
            btnClearPull.Click += btnClearPull_Click;
            // 
            // btnArchitecture
            // 
            btnArchitecture.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnArchitecture.Font = new Font("Segoe UI", 15F);
            btnArchitecture.Location = new Point(3069, 228);
            btnArchitecture.Margin = new Padding(6);
            btnArchitecture.Name = "btnArchitecture";
            btnArchitecture.Size = new Size(279, 98);
            btnArchitecture.TabIndex = 21;
            btnArchitecture.Text = "Diagram";
            btnArchitecture.UseVisualStyleBackColor = true;
            btnArchitecture.Click += btnArchitecture_Click;
            // 
            // rdbCustomEvents
            // 
            rdbCustomEvents.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            rdbCustomEvents.AutoSize = true;
            rdbCustomEvents.Checked = true;
            rdbCustomEvents.Font = new Font("Segoe UI", 15F);
            rdbCustomEvents.Location = new Point(2001, 248);
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
            rdbSystemEvents.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            rdbSystemEvents.AutoSize = true;
            rdbSystemEvents.Font = new Font("Segoe UI", 15F);
            rdbSystemEvents.Location = new Point(2346, 248);
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
            // frmPushPullWindow
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(3409, 2186);
            Controls.Add(pbStorageEvents);
            Controls.Add(pbCustomEvents);
            Controls.Add(rdbSystemEvents);
            Controls.Add(rdbCustomEvents);
            Controls.Add(btnArchitecture);
            Controls.Add(btnClearPull);
            Controls.Add(btnStopPull);
            Controls.Add(btnStartPull);
            Controls.Add(lstViewPull);
            Controls.Add(label4);
            Controls.Add(btnClearEventHub);
            Controls.Add(label2);
            Controls.Add(lstViewWebhook);
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
            WindowState = FormWindowState.Maximized;
            FormClosing += frmPushPullWindow_FormClosing;
            Load += frmPushPullWindow_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbCustomEvents).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbStorageEvents).EndInit();
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
        private ListView lstViewWebhook;
        private Label label2;
        private Button btnClearEventHub;
        private Label label4;
        private ListView lstViewPull;
        private Button btnStartPull;
        private System.Windows.Forms.Timer timerPull;
        private Button btnStopPull;
        private Button btnClearPull;
        private ToolStripStatusLabel lblVersion;
        private Button btnArchitecture;
        private RadioButton rdbCustomEvents;
        private RadioButton rdbSystemEvents;
        private PictureBox pbCustomEvents;
        private PictureBox pbStorageEvents;
        private ToolStripStatusLabel lblListener;
    }
}