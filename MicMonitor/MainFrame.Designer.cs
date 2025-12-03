namespace MicMonitor
{
    partial class MainFrame
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrame));
            menuStrip1 = new MenuStrip();
            mnuMic = new ToolStripMenuItem();
            mnuExit = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            lblRms = new Label();
            pnlProgress = new Panel();
            pnlValue = new Panel();
            menuStrip1.SuspendLayout();
            pnlProgress.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.AutoSize = false;
            menuStrip1.Dock = DockStyle.Right;
            menuStrip1.Items.AddRange(new ToolStripItem[] { mnuMic, mnuExit, toolStripMenuItem1 });
            menuStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            menuStrip1.Location = new Point(321, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(0);
            menuStrip1.Size = new Size(49, 20);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // mnuMic
            // 
            mnuMic.DisplayStyle = ToolStripItemDisplayStyle.Image;
            mnuMic.Font = new Font("Segoe UI", 9F);
            mnuMic.Image = Properties.Resources.mic_logo_icon_png_svg;
            mnuMic.Name = "mnuMic";
            mnuMic.Padding = new Padding(0);
            mnuMic.Size = new Size(20, 20);
            mnuMic.Text = "toolStripMenuItem1";
            // 
            // mnuExit
            // 
            mnuExit.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            mnuExit.ForeColor = Color.Brown;
            mnuExit.Image = Properties.Resources._75519_copy;
            mnuExit.Name = "mnuExit";
            mnuExit.Padding = new Padding(0);
            mnuExit.Size = new Size(20, 20);
            mnuExit.TextAlign = ContentAlignment.TopCenter;
            mnuExit.TextImageRelation = TextImageRelation.Overlay;
            mnuExit.Click += mnuExit_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(12, 20);
            // 
            // lblRms
            // 
            lblRms.Dock = DockStyle.Left;
            lblRms.Location = new Point(0, 0);
            lblRms.Name = "lblRms";
            lblRms.Size = new Size(47, 20);
            lblRms.TabIndex = 4;
            lblRms.Text = "0";
            lblRms.TextAlign = ContentAlignment.MiddleCenter;
            lblRms.MouseDown += pbarMain_MouseDown;
            lblRms.MouseMove += pbarMain_MouseMove;
            // 
            // pnlProgress
            // 
            pnlProgress.Controls.Add(pnlValue);
            pnlProgress.Dock = DockStyle.Fill;
            pnlProgress.Location = new Point(47, 0);
            pnlProgress.Name = "pnlProgress";
            pnlProgress.Size = new Size(274, 20);
            pnlProgress.TabIndex = 5;
            pnlProgress.MouseDown += pbarMain_MouseDown;
            pnlProgress.MouseMove += pbarMain_MouseMove;
            // 
            // pnlValue
            // 
            pnlValue.BackColor = Color.Goldenrod;
            pnlValue.Dock = DockStyle.Left;
            pnlValue.Location = new Point(0, 0);
            pnlValue.Name = "pnlValue";
            pnlValue.Size = new Size(257, 20);
            pnlValue.TabIndex = 6;
            pnlValue.LocationChanged += pnlValue_LocationChanged;
            pnlValue.MouseDown += pbarMain_MouseDown;
            pnlValue.MouseMove += pbarMain_MouseMove;
            // 
            // MainFrame
            // 
            BackColor = Color.White;
            ClientSize = new Size(370, 20);
            ControlBox = false;
            Controls.Add(pnlProgress);
            Controls.Add(menuStrip1);
            Controls.Add(lblRms);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MaximumSize = new Size(1000, 50);
            MinimumSize = new Size(300, 36);
            Name = "MainFrame";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            FormClosing += MainFrame_FormClosing;
            Load += MainFrame_Load;
            Shown += MainFrame_Shown;
            LocationChanged += MainFrame_LocationChanged;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            pnlProgress.ResumeLayout(false);
            ResumeLayout(false);
        }
        private MenuStrip menuStrip1;
        private ToolStripMenuItem mnuMic;
        private ToolStripMenuItem mnuExit;
        private ToolStripMenuItem toolStripMenuItem1;
        private Label lblRms;
        private Panel pnlProgress;
        private Panel pnlValue;
    }
}