using System;
using System.Drawing;
using System.Windows.Forms;
using MicMonitor.Properties;
using NAudio.Wave;

namespace MicMonitor
{
    public partial class MainFrame : Form
    {
        private WaveInEvent waveIn;
        private Point MouseDownLocation;
        private NotifyIcon trayIcon = new NotifyIcon();
        private List<Icon> icons = new List<Icon>();

        public MainFrame()
        {
            InitializeComponent();
        }

        private void LoadInputDevices()
        {
            mnuMic.DropDownItems.Clear();
            for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                var deviceInfo = WaveIn.GetCapabilities(i);
                ToolStripMenuItem item = new ToolStripMenuItem(deviceInfo.ProductName);
                item.Name = deviceInfo.ProductName;

                item.Tag = i;
                item.Click += (s, e) =>
                {
                    StartCapture((int)item.Tag);
                    foreach (var item in mnuMic.DropDownItems)
                    {
                        ((ToolStripMenuItem)item).Checked = false;
                    }
                    item.Checked = true;
                };
                mnuMic.DropDownItems.Add(item);
            }

            if (mnuMic.DropDownItems.Count > 0)
            {
                ((ToolStripMenuItem)mnuMic.DropDownItems[0]).Checked = true;
                StartCapture(0);
            }

            var selectedDevice = mnuMic.DropDownItems.Find(Settings.Default.Device, false);
            if (selectedDevice.Length > 0)
            {
                foreach (var item in mnuMic.DropDownItems)
                {
                    ((ToolStripMenuItem)item).Checked = false;
                }
                ((ToolStripMenuItem)selectedDevice[0]).Checked = true;
                StartCapture((int)((ToolStripMenuItem)selectedDevice[0]).Tag);
            }        
        }


        private void StartCapture(int deviceNumber)
        {
            StopCapture();
            waveIn = new WaveInEvent();
            waveIn.DeviceNumber = deviceNumber;
            waveIn.WaveFormat = new WaveFormat(44100, 1); // 44.1 kHz mono  
            waveIn.DataAvailable += WaveIn_DataAvailable;
            waveIn.StartRecording();
        }

        private void StopCapture()
        {
            if (waveIn != null)
            {
                waveIn.DataAvailable -= WaveIn_DataAvailable;
                waveIn.StopRecording();
                waveIn.Dispose();
                waveIn = null;
            }
        }

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            // Calculate RMS volume  
            int bytesPerSample = waveIn.WaveFormat.BitsPerSample / 8;
            int sampleCount = e.BytesRecorded / bytesPerSample;
            float sum = 0;
            for (int i = 0; i < e.BytesRecorded; i += bytesPerSample)
            {
                short sample = BitConverter.ToInt16(e.Buffer, i);
                float sample32 = sample / 32768f;
                sum += sample32 * sample32;
            }
            double rms = Math.Sqrt(sum / sampleCount);
            Invoke(() =>
            {
                value.Add((int)(pnlProgress.Width * rms * 3.8));
                if (value.Count > 5) value.RemoveAt(0);

                pnlValue.Width = (int)value.Average();

                pnlValue.BackColor = Color.PaleGreen;
                trayIcon.Icon = icons[1];

                if (pnlValue.Width > (int)(pnlProgress.Width * 0.9))
                {
                    pnlValue.BackColor = Color.Yellow;
                    trayIcon.Icon = icons[2];
                }
                if (pnlValue.Width > pnlProgress.Width)
                {
                    pnlValue.BackColor = Color.LightPink;
                    trayIcon.Icon = icons[3];
                }


                lblRms.Text = pnlValue.Width.ToString();

            });
        }

        List<Int32> value = new();

        private void pbarMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }

        private void pbarMain_MouseMove(object sender, MouseEventArgs e)
        {
            Control sending = sender as Control;
            if (e.Button == MouseButtons.Left)
            {
                this.Left = e.X + this.Left - MouseDownLocation.X;
                this.Top = e.Y + this.Top - MouseDownLocation.Y;
            }
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void MainFrame_Load(object sender, EventArgs e)
        {
            // Load input devices
            LoadInputDevices();

            // Create Icons from resources
            icons.Add(new Icon(new MemoryStream(Resources.MainIcon)));
            icons.Add(new Icon(new MemoryStream(Resources.IconGreen)));
            icons.Add(new Icon(new MemoryStream(Resources.IconYellow)));
            icons.Add(new Icon(new MemoryStream(Resources.IconRed)));

            // Add systray icon
            ContextMenuStrip trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("Show", null, (s, ev) =>
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            });
            trayMenu.Items.Add("Exit", null, (s, ev) =>
            {
                Application.Exit();
            });

            trayIcon.Icon = icons[0];
            trayIcon.Visible = true;
            trayIcon.ContextMenuStrip = trayMenu;

            trayIcon.DoubleClick += (s, args) =>
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            };
        }

        private void pnlValue_LocationChanged(object sender, EventArgs e)
        {

        }

        private void MainFrame_Shown(object sender, EventArgs e)
        {
            if (Settings.Default.Top > -1)
            {
                this.Left = Settings.Default.Left;
                this.Top = Settings.Default.Top;
            }
        }

        private void MainFrame_LocationChanged(object sender, EventArgs e)
        {
        }

        private void MainFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopCapture();
            Settings.Default.Device = ((ToolStripMenuItem)mnuMic.DropDownItems.Cast<ToolStripMenuItem>().FirstOrDefault(i => i.Checked))?.Name;
            Settings.Default.Left = this.Left;
            Settings.Default.Top = this.Top;
            Settings.Default.Save();
        }
    }
}