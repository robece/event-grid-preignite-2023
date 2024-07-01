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
            lblVersion = new ToolStripStatusLabel();
            progressBarPublish = new ProgressBar();
            label1 = new Label();
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
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(3430, 130);
            pictureBox1.Margin = new Padding(6);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(85, 98);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(32, 32);
            statusStrip.Items.AddRange(new ToolStripItem[] { lblVersion });
            statusStrip.Location = new Point(0, 1794);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(2, 0, 26, 0);
            statusStrip.Size = new Size(3573, 22);
            statusStrip.TabIndex = 2;
            statusStrip.Text = "statusStrip1";
            // 
            // lblVersion
            // 
            lblVersion.BackColor = SystemColors.Control;
            lblVersion.ForeColor = Color.SlateGray;
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(0, 12);
            // 
            // progressBarPublish
            // 
            progressBarPublish.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBarPublish.Location = new Point(58, 130);
            progressBarPublish.Margin = new Padding(6);
            progressBarPublish.Name = "progressBarPublish";
            progressBarPublish.Size = new Size(3361, 98);
            progressBarPublish.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(58, 43);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(674, 54);
            label1.TabIndex = 5;
            label1.Text = "Publish events to Azure Event Grid";
            // 
            // btnStartPublish
            // 
            btnStartPublish.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            btnStartPublish.Location = new Point(54, 241);
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
            btnStopPublish.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            btnStopPublish.Location = new Point(344, 241);
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
            lblPublishedEvents.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            lblPublishedEvents.ForeColor = Color.DarkGreen;
            lblPublishedEvents.Location = new Point(3442, 56);
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
            label3.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.DarkGreen;
            label3.Location = new Point(3116, 56);
            label3.Margin = new Padding(6, 0, 6, 0);
            label3.Name = "label3";
            label3.Size = new Size(331, 54);
            label3.TabIndex = 9;
            label3.Text = "Published Events:";
            // 
            // btnClearPublish
            // 
            btnClearPublish.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            btnClearPublish.Location = new Point(633, 241);
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
            lstViewWebhook.Location = new Point(58, 548);
            lstViewWebhook.Margin = new Padding(6);
            lstViewWebhook.Name = "lstViewWebhook";
            lstViewWebhook.Size = new Size(3454, 516);
            lstViewWebhook.TabIndex = 11;
            lstViewWebhook.UseCompatibleStateImageBehavior = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(58, 460);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(1079, 54);
            label2.TabIndex = 12;
            label2.Text = "Push delivery to On-Premise Webhook with Azure Relay";
            // 
            // btnClearEventHub
            // 
            btnClearEventHub.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClearEventHub.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            btnClearEventHub.Location = new Point(3233, 438);
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
            label4.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(58, 1116);
            label4.Margin = new Padding(6, 0, 6, 0);
            label4.Name = "label4";
            label4.Size = new Size(697, 54);
            label4.TabIndex = 16;
            label4.Text = "Pull delivery from Namespace topic";
            // 
            // lstViewPull
            // 
            lstViewPull.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lstViewPull.Location = new Point(58, 1204);
            lstViewPull.Margin = new Padding(6);
            lstViewPull.Name = "lstViewPull";
            lstViewPull.Size = new Size(3454, 516);
            lstViewPull.TabIndex = 17;
            lstViewPull.UseCompatibleStateImageBehavior = false;
            // 
            // btnStartPull
            // 
            btnStartPull.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnStartPull.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            btnStartPull.Location = new Point(2654, 1094);
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
            btnStopPull.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            btnStopPull.Location = new Point(2945, 1094);
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
            btnClearPull.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            btnClearPull.Location = new Point(3236, 1094);
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
            btnArchitecture.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            btnArchitecture.Location = new Point(2945, 241);
            btnArchitecture.Margin = new Padding(6);
            btnArchitecture.Name = "btnArchitecture";
            btnArchitecture.Size = new Size(573, 98);
            btnArchitecture.TabIndex = 21;
            btnArchitecture.Text = "Architecture Diagram";
            btnArchitecture.UseVisualStyleBackColor = true;
            btnArchitecture.Click += btnArchitecture_Click;
            // 
            // frmPushPullWindow
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(3573, 1816);
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
            Controls.Add(label1);
            Controls.Add(progressBarPublish);
            Controls.Add(statusStrip);
            Controls.Add(pictureBox1);
            Margin = new Padding(6);
            Name = "frmPushPullWindow";
            Text = "Azure Event Grid Standard Delivery Using Push and Pull";
            WindowState = FormWindowState.Maximized;
            FormClosing += frmPushPullWindow_FormClosing;
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
    }
}