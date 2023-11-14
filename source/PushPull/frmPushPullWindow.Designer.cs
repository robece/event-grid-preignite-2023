namespace PushPull
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
            progressBarPublish = new ProgressBar();
            label1 = new Label();
            btnStartPublish = new Button();
            btnStopPublish = new Button();
            timerPublish = new System.Windows.Forms.Timer(components);
            lblPublishedEvents = new Label();
            label3 = new Label();
            btnClearPublish = new Button();
            lstViewEventHub = new ListView();
            label2 = new Label();
            btnStartEventHub = new Button();
            btnStopEventHub = new Button();
            btnClearEventHub = new Button();
            label4 = new Label();
            lstViewPull = new ListView();
            btnStartPull = new Button();
            timerPull = new System.Windows.Forms.Timer(components);
            btnStopPull = new Button();
            btnClearPull = new Button();
            lblVersion = new ToolStripStatusLabel();
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
            statusStrip.Text = "statusStrip1";
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
            label1.Size = new Size(280, 28);
            label1.TabIndex = 5;
            label1.Text = "Publish events to Event Grid";
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
            label3.Location = new Point(1664, 20);
            label3.Name = "label3";
            label3.Size = new Size(177, 28);
            label3.TabIndex = 9;
            label3.Text = "Published Events:";
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
            // lstViewEventHub
            // 
            lstViewEventHub.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstViewEventHub.Location = new Point(31, 220);
            lstViewEventHub.Name = "lstViewEventHub";
            lstViewEventHub.Size = new Size(919, 654);
            lstViewEventHub.TabIndex = 11;
            lstViewEventHub.UseCompatibleStateImageBehavior = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(31, 176);
            label2.Name = "label2";
            label2.Size = new Size(309, 28);
            label2.TabIndex = 12;
            label2.Text = "Push subscription to Event Hub";
            // 
            // btnStartEventHub
            // 
            btnStartEventHub.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnStartEventHub.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            btnStartEventHub.Location = new Point(31, 880);
            btnStartEventHub.Name = "btnStartEventHub";
            btnStartEventHub.Size = new Size(150, 46);
            btnStartEventHub.TabIndex = 13;
            btnStartEventHub.Text = "Start";
            btnStartEventHub.UseVisualStyleBackColor = true;
            btnStartEventHub.Click += btnStartEventHub_Click;
            // 
            // btnStopEventHub
            // 
            btnStopEventHub.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnStopEventHub.Enabled = false;
            btnStopEventHub.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            btnStopEventHub.Location = new Point(187, 880);
            btnStopEventHub.Name = "btnStopEventHub";
            btnStopEventHub.Size = new Size(150, 46);
            btnStopEventHub.TabIndex = 14;
            btnStopEventHub.Text = "Stop";
            btnStopEventHub.UseVisualStyleBackColor = true;
            btnStopEventHub.Click += btnStopEventHub_Click;
            // 
            // btnClearEventHub
            // 
            btnClearEventHub.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnClearEventHub.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            btnClearEventHub.Location = new Point(343, 880);
            btnClearEventHub.Name = "btnClearEventHub";
            btnClearEventHub.Size = new Size(150, 46);
            btnClearEventHub.TabIndex = 15;
            btnClearEventHub.Text = "Clear";
            btnClearEventHub.UseVisualStyleBackColor = true;
            btnClearEventHub.Click += btnClearEventHub_Click;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(956, 176);
            label4.Name = "label4";
            label4.Size = new Size(492, 28);
            label4.TabIndex = 16;
            label4.Text = "Pull subscription from Event Grid namespace topic";
            // 
            // lstViewPull
            // 
            lstViewPull.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            lstViewPull.Location = new Point(956, 220);
            lstViewPull.Name = "lstViewPull";
            lstViewPull.Size = new Size(937, 654);
            lstViewPull.TabIndex = 17;
            lstViewPull.UseCompatibleStateImageBehavior = false;
            // 
            // btnStartPull
            // 
            btnStartPull.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnStartPull.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            btnStartPull.Location = new Point(1431, 880);
            btnStartPull.Name = "btnStartPull";
            btnStartPull.Size = new Size(150, 46);
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
            btnStopPull.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnStopPull.Enabled = false;
            btnStopPull.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            btnStopPull.Location = new Point(1587, 880);
            btnStopPull.Name = "btnStopPull";
            btnStopPull.Size = new Size(150, 46);
            btnStopPull.TabIndex = 19;
            btnStopPull.Text = "Stop";
            btnStopPull.UseVisualStyleBackColor = true;
            btnStopPull.Click += btnStopPull_Click;
            // 
            // btnClearPull
            // 
            btnClearPull.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClearPull.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            btnClearPull.Location = new Point(1743, 880);
            btnClearPull.Name = "btnClearPull";
            btnClearPull.Size = new Size(150, 46);
            btnClearPull.TabIndex = 20;
            btnClearPull.Text = "Clear";
            btnClearPull.UseVisualStyleBackColor = true;
            btnClearPull.Click += btnClearPull_Click;
            // 
            // lblVersion
            // 
            lblVersion.BackColor = SystemColors.Control;
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(0, 17);
            // 
            // frmPushPullWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1924, 962);
            Controls.Add(btnClearPull);
            Controls.Add(btnStopPull);
            Controls.Add(btnStartPull);
            Controls.Add(lstViewPull);
            Controls.Add(label4);
            Controls.Add(btnClearEventHub);
            Controls.Add(btnStopEventHub);
            Controls.Add(btnStartEventHub);
            Controls.Add(label2);
            Controls.Add(lstViewEventHub);
            Controls.Add(btnClearPublish);
            Controls.Add(label3);
            Controls.Add(lblPublishedEvents);
            Controls.Add(btnStopPublish);
            Controls.Add(btnStartPublish);
            Controls.Add(label1);
            Controls.Add(progressBarPublish);
            Controls.Add(statusStrip);
            Controls.Add(pictureBox1);
            Name = "frmPushPullWindow";
            Text = "Push and Pull Delivery";
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
        private ListView lstViewEventHub;
        private Label label2;
        private Button btnStartEventHub;
        private Button btnStopEventHub;
        private Button btnClearEventHub;
        private Label label4;
        private ListView lstViewPull;
        private Button btnStartPull;
        private System.Windows.Forms.Timer timerPull;
        private Button btnStopPull;
        private Button btnClearPull;
        private ToolStripStatusLabel lblVersion;
    }
}