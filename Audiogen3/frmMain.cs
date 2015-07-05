using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using FlamedLib;
using System.IO;
using Telerik.WinControls.UI;
using AudiogenWaveWriter.AudiogenWaveWriter;
namespace Audiogen3 {
    public partial class frmMain : Telerik.WinControls.UI.RadForm {
        public FL_CDAudioWriter AudioCdWriter = new FL_CDAudioWriter();
        public FL_DriveInfo DriveInfo = new FL_DriveInfo();
        public FL_CDInfo CDInfo = new FL_CDInfo();
        public FL_Manager Manager = new FL_Manager();
        
        /// <summary>
        /// Entry Point
        /// </summary>
        public frmMain() {
            InitializeComponent();
        }
        /// <summary>
        /// Main Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e) {
            var init = new frmInit();
            lblDecodeMp3Status.Text = "";
            lblStatus.Text = "";
            init.Show(this);
            foreach (var drive in System.IO.DriveInfo.GetDrives()) {
                var b = false;
                //var di = new System.IO.DriveInfo(drive.Name);
                switch (drive.DriveType) {
                    case DriveType.Network:
                        b = true;
                        break;
                    case DriveType.Fixed:
                        b = true;
                        break;
                    case DriveType.Removable:
                        b = true;
                        break;
                    case DriveType.Unknown:
                        b = true;
                        break;
                }
                if (b) {
                    tvwDecodeMp3s.Nodes.Add(new RadTreeNode() {
                        Text = drive.Name
                    });
                }
            }
            this.Resize += new EventHandler(frmMain_Resize);
            tvwDecodeMp3s.DoubleClick += new EventHandler(tvwDecodeMp3s_DoubleClick);
            tvwDecodeMp3s.NodeMouseClick += new RadTreeView.TreeViewEventHandler(tvwDecodeMp3s_NodeMouseClick);
            AudioCdWriter.Finished += new __FL_CDAudioWriter_FinishedEventHandler(AudioCdWriter_Finished);
            AudioCdWriter.WriteProgress += new __FL_CDAudioWriter_WriteProgressEventHandler(AudioCdWriter_WriteProgress);
            init.ShowText("Detecting CD Drives");
            foreach (var drive in Manager.GetCDVDROMs()) {
                if (drive != null) {
                    var driveLetter = drive.ToString();
                    var info = DriveInfo.GetInfo(driveLetter);
                    if (DriveInfo.WriteCapabilities != 0) {
                        cboDrive.Items.Add(driveLetter + ": " + DriveInfo.Vendor + " " + DriveInfo.Product + " " + DriveInfo.Revision);
                    }
                    init.ShowText("Detecting Device " + driveLetter + ": " + DriveInfo.Vendor + " " + DriveInfo.Product + " " + DriveInfo.Revision);
                }
            }
            if (cboDrive.SelectedItem == null) { cboDrive.SelectedItem = cboDrive.Items.LastOrDefault(); }
            var driveLetter2 = cboDrive.SelectedItem.Text.Split(' ')[0];
            UpdateSpeed(driveLetter2);
            init.ShowText("All CD Drives Loaded.");
            init.Close();
           

        }


        void AudioCdWriter_WriteProgress(short Percent, short Track) {
            lblStatus.Text = "Burn Audio CD " + Percent.ToString() + "%.";
            prgTotalProgress.Value1 = Percent;
            prgTotalProgress.Value2 = Percent;
            prgTrackProgress.Value1 = Track;
            prgTrackProgress.Value2 = Track;
        }
        void AudioCdWriter_Finished() {
            lblStatus.Text = "";
            prgTotalProgress.Value1 = 0;
            prgTotalProgress.Value2 = 0;
            prgTrackProgress.Value1 = 0;
            prgTrackProgress.Value2 = 0;
            prgSpaceAvailable.Value1 = 0;
            prgSpaceAvailable.Value2 = 0;
        }
        private void frmMain_Resize(object sender, EventArgs e) {
            cboDrive.Width = this.Width - (lblDriveDescriptions.Width + 45);
            cboSpeedAudioBurn.Width = this.Width - (lblSpeedDescription.Width + 40);
            lvwAudioFiles.Width = this.Width - 40;
            cmdBurnAudioFiles.Left = this.Width - 137;
        }
        private void cmdAddFiles_Click(object sender, EventArgs e) {
            openFileDialog1.Multiselect = true;
            openFileDialog1.Filter = "Wave Files (*.wav)|*.wav|All Files (*.*)|*.*";
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) {
                foreach (var file in openFileDialog1.FileNames) {
                    try {
                        var text = File.ReadAllText(file);
                        var size = text.Length;
                        if (!AudioCdWriter.AddFile(file)) {
                            MessageBox.Show("Could not add '" + file + "'");
                        }
                    } catch (IOException ex) {
                        MessageBox.Show("Could not add '" + file + "' because: '" + ex.Message + "'");
                    }
                }
            }
            ShowAudioFiles();
            var driveLetter = cboDrive.SelectedItem.Text.Split(' ')[0];
            UpdateUsedSpace(driveLetter);
            UpdateSpeed(driveLetter);
        }
        private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e) {

        }
        private void UpdateSpeed(string driveLetter) {
            var speeds = DriveInfo.GetWriteSpeeds(driveLetter);
            foreach (var speed in speeds) {
                cboSpeedAudioBurn.Items.Add(speed.ToString());
            }
            if (cboSpeedAudioBurn.SelectedItem == null) {
                cboSpeedAudioBurn.SelectedItem = cboSpeedAudioBurn.Items.LastOrDefault();
            }
        }
        private bool UpdateUsedSpace(string driveLetter) {
            var cd = new FL_MSF();
            var tracks = new FL_MSF();
            int l = 0;
            for (int i = 0; i < AudioCdWriter.FileCount; ++i) {
                l = l + AudioCdWriter.TrackLength((short)i);
            }
            int m = l / 60;
            int s = l - m * 60;
            tracks.set_M(m);
            tracks.set_s(s);
            CDInfo.GetInfo(driveLetter);
            cd.LBA = CDInfo.Capacity / 2352;
            lblStatus.Text = cd.LBA.ToString() + " available space " + tracks.LBA.ToString() + " total track(s) length";
            prgSpaceAvailable.Maximum = cd.LBA;
            prgSpaceAvailable.Value1 = tracks.LBA;
            prgSpaceAvailable.Value2 = tracks.LBA;
            var b = false;
            if (cd.get_M() < m) {
                b = true;
            }
            if (cd.get_M() == m && cd.get_s() < s) {
                b = true;
            }
            return b;
        }
        private void ShowAudioFiles() {
            lvwAudioFiles.ViewType = Telerik.WinControls.UI.ListViewType.DetailsView;
            lvwAudioFiles.Columns.Clear();
            lvwAudioFiles.Columns.Add("Track Number");
            lvwAudioFiles.Columns.Add("Length");
            lvwAudioFiles.Columns.Add("File");
            lvwAudioFiles.Items.Clear();
            for (int i = 0; i < AudioCdWriter.FileCount; ++i) {
                var item = new ListViewDataItem();
                var s = (short)i;
                item.SubItems.Add((i + 1).ToString());
                item.SubItems.Add(AudioCdWriter.TrackLength(s).ToString() + " sec(s)"); // Length
                item.SubItems.Add(AudioCdWriter.file[s]);
                lvwAudioFiles.Items.Add(item);
            }
        }
        private void cmdClearAudioFiles_Click(object sender, EventArgs e) {
            lvwAudioFiles.Items.Clear();
            AudioCdWriter.Clear();
        }

        private void cmdBurnAudioFiles_Click(object sender, EventArgs e) {
            var driveLetter = cboDrive.SelectedItem.Text.Split(' ')[0];
            if (UpdateUsedSpace(driveLetter)) {
                var msg = MessageBox.Show(this, "Data size exceeds disk capacity.", "audiogen", MessageBoxButtons.OKCancel);
                if (msg == System.Windows.Forms.DialogResult.Cancel) {
                    return;
                }
            }
            prgSpaceAvailable.Maximum = AudioCdWriter.FileCount;
            prgSpaceAvailable.Value1 = 0;
            prgSpaceAvailable.Value2 = 0;
            prgTotalProgress.Value1 = 0;
            prgTotalProgress.Value2 = 0;
            prgTrackProgress.Value1 = 0;
            prgTrackProgress.Value2 = 0;
            Manager.SetCDRomSpeed(driveLetter, int.Parse(cboSpeedAudioBurn.Text), int.Parse(cboSpeedAudioBurn.Text));
            var strMsg = "";
            var result = AudioCdWriter.WriteAudioToCD(driveLetter);
            switch (result) {
                case FL_BURNRET.BURNRET_CLOSE_SESSION:
                    strMsg = "Could not close session.";
                    break;
                case FL_BURNRET.BURNRET_CLOSE_TRACK:
                    strMsg = "Could not cloe track.";
                    break;
                case FL_BURNRET.BURNRET_FILE_ACCESS:
                    strMsg = "Failed to access a file.";
                    break;
                case FL_BURNRET.BURNRET_INVALID_MEDIA:
                    strMsg = "Invalid medium in drive.";
                    break;
                case FL_BURNRET.BURNRET_ISOCREATION:
                    strMsg = "ISO creation failed.";
                    break;
                case FL_BURNRET.BURNRET_NO_NEXT_WRITABLE_LBA:
                    strMsg = "Could not get next writable LBA.";
                    break;
                case FL_BURNRET.BURNRET_NOT_EMPTY:
                    strMsg = "Disk is finalized.";
                    break;
                case FL_BURNRET.BURNRET_OK:
                    strMsg = "Finished.";
                    break;
                case FL_BURNRET.BURNRET_SYNC_CACHE:
                    strMsg = "Could not synchronize cache.";
                    break;
                case FL_BURNRET.BURNRET_WPMP:
                    strMsg = "Write Parameters Page invalid";
                    break;
                case FL_BURNRET.BURNRET_WRITE:
                    strMsg = "Write error (Buffer Underrun?)";
                    break;
            }
            if (!string.IsNullOrEmpty(strMsg)) { MessageBox.Show(strMsg); }
        }

        private void pgeAudioCD_Paint(object sender, PaintEventArgs e) {

        }

        private void rpvFunctions_SelectedPageChanged(object sender, EventArgs e) {

        }

        private void radTreeView1_SelectedNodeChanged(object sender, RadTreeViewEventArgs e) {

        }

        private void pgeDecodeMP3s_Paint(object sender, PaintEventArgs e) {

        }
        void tvwDecodeMp3s_NodeMouseClick(object sender, RadTreeViewEventArgs e) {
            lblDecodeMp3Status.Text = e.Node.Text;
            try {
                var attr = File.GetAttributes(@e.Node.Text);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory) {
                    var dirs = Directory.GetDirectories(e.Node.Text);
                    if (System.IO.Directory.Exists(e.Node.Text)) {
                        foreach (var dir in dirs) {
                            var node = new RadTreeNode(dir);
                            e.Node.Nodes.Add(node);
                        }
                    }
                    var files = Directory.GetFiles(e.Node.Text, "*.mp3");
                    foreach (var file in files) {
                        var node = new RadTreeNode(file);
                        e.Node.Nodes.Add(node);
                    }
                    e.Node.ExpandAll();
                } else {
                    // File
                    if (e.Node.Text.ToLower().Contains(".mp3")) {
                        cmdDecodeNow.Enabled = true;
                    } else {
                        cmdDecodeNow.Enabled = false;
                    }
                }
            } catch {
                // I don't care, all kinds of things can happen
            }
        }
        void tvwDecodeMp3s_DoubleClick(object sender, EventArgs e) {
        }
        
        private void cmdDecodeNow_Click(object sender, EventArgs e) {
            int chunkSize = 8024;
            using (var mp3Stream = new Mp3Sharp.Mp3Stream(tvwDecodeMp3s.SelectedNode.Text)) {
                var bytes = new byte[mp3Stream.Length];
                int numBytesToRead = (int)mp3Stream.Length;
                int numBytesRead = 0;
                using (var writer = new WaveWriter(tvwDecodeMp3s.SelectedNode.Text.Replace(".mp3", ".wav"))) {
                    while (numBytesToRead > 0) {
                        var n = 0;
                        if (chunkSize > numBytesToRead) {
                            n = mp3Stream.Read(bytes, numBytesRead, numBytesToRead);
                        } else {
                            n = mp3Stream.Read(bytes, numBytesRead, chunkSize);
                        }
                        writer.Write(bytes, chunkSize);
                        if (n == 0) {
                            break;
                        }
                        numBytesRead += n;
                        numBytesToRead -= n;
                        lblDecodeMp3Status.Text = numBytesRead.ToString() + " - " + numBytesToRead.ToString();
                        this.Refresh();
                    }
                    writer.Close();
                    writer.Dispose();
                }
                numBytesToRead = bytes.Length;
            }
            //var decoder = new MPEGPLAYLib.Mp3Play();
            //decoder = new MPEGPLAYLib.Mp3Play();
            //decoder.ActFrame += new MPEGPLAYLib._DMp3PlayEvents_ActFrameEventHandler(decoder_ActFrame);
            //decoder.ThreadEnded += new MPEGPLAYLib._DMp3PlayEvents_ThreadEndedEventHandler(decoder_ThreadEnded);
            //decoder.Authorize("Leon J Aiossa", "812144397");
            //decoder.Open(tvwDecodeMp3s.SelectedNode.Text, tvwDecodeMp3s.SelectedNode.Text.Replace(".mp3", ".wav"));
        }
        void decoder_ThreadEnded() {
            prgDecode.Value1 = 0;
            prgDecode.Value2 = 0;
            lblDecodeMp3Status.Text = "";
        }
        void decoder_ActFrame(int ActFrame) {
            /*
            var i = (ActFrame * 100 / decoder.FrameCount) / 1;
            if (i < 101) {
                prgDecode.Value1 = i;
                prgDecode.Value2 = i;
                lblDecodeMp3Status.Text = "Decode " + i.ToString() + "%";
            }
             */
        }
    }
}