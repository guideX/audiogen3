using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using FlamedLib;
using Telerik.WinControls.UI;
using Audiogen.Mp3Decoder.Controllers;
/// <summary>
/// Audiogen3
/// </summary>
namespace Audiogen3 {
    public partial class frmMain: Telerik.WinControls.UI.RadForm {
        #region "Variables"
        /// <summary>
        /// Flamed Components
        /// </summary>
        public FL_CDAudioWriter AudioCdWriter = new FL_CDAudioWriter();
        /// <summary>
        /// Drive Info
        /// </summary>
        public FL_DriveInfo DriveInfo = new FL_DriveInfo();
        /// <summary>
        /// CD Info
        /// </summary>
        public FL_CDInfo CDInfo = new FL_CDInfo();
        /// <summary>
        /// Manager
        /// </summary>
        public FL_Manager Manager = new FL_Manager();
        /// <summary>
        /// Decode Files Completed
        /// </summary>
        private int _decodeFilesCompleted;
        /// <summary>
        /// Decode Files
        /// </summary>
        private SelectedTreeNodeCollection _decodeFiles;
        /// <summary>
        /// Decode Controller
        /// </summary>
        private Mp3DecoderController _decodeController;
        #endregion
        /// <summary>
        /// Entry Point
        /// </summary>
        public frmMain() {
            try {
                InitializeComponent();
            } catch {
                throw;
            }     
        }
        /// <summary>
        /// Main Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e) {
            try {
                this.Width = Convert.ToInt32(IniFiles.ReadINI(Application.StartupPath + @"\settings.ini", "Settings", "Width", "600"));
                this.Height = Convert.ToInt32(IniFiles.ReadINI(Application.StartupPath + @"\settings.ini", "Settings", "Height", "400"));
                this.Left = Convert.ToInt32(IniFiles.ReadINI(Application.StartupPath + @"\settings.ini", "Settings", "Left", "0"));
                this.Top = Convert.ToInt32(IniFiles.ReadINI(Application.StartupPath + @"\settings.ini", "Settings", "Top", "0"));
                this.Resize += new EventHandler(frmMain_Resize);
                this.FormClosed += FrmMain_FormClosed;
                _decodeController = new Mp3DecoderController();
                _decodeController.PercentChanged += _decodeController_PercentChanged;
                tvwDecodeMp3s.DoubleClick += new EventHandler(tvwDecodeMp3s_DoubleClick);
                tvwDecodeMp3s.NodeMouseClick += new RadTreeView.TreeViewEventHandler(tvwDecodeMp3s_NodeMouseClick);
                AudioCdWriter.Finished += new __FL_CDAudioWriter_FinishedEventHandler(AudioCdWriter_Finished);
                AudioCdWriter.WriteProgress += new __FL_CDAudioWriter_WriteProgressEventHandler(AudioCdWriter_WriteProgress);
                ThisResize();
                this.Visible = true;
                Application.DoEvents();
                var init = new frmInit();
                rwbMakeAudioCD.WaitingStep = 5;
                rwbMakeAudioCD.StartWaiting();
                prgSpaceAvailable.Text = "Space Available";
                prgDecode.Text = "Decode";
                prgTotalProgress.Text = "Total Progress";
                prgTrackProgress.Text = "Track Progress";
                lblDecodeMp3Status.Text = "";
                lblStatus.Text = "";
                init.Show(this);
                init.BeginAbout();
                foreach (var drive in System.IO.DriveInfo.GetDrives()) {
                    var b = false;
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
                init.SetProgress(25);
                init.ShowText("Detecting CD Drives");
                init.SetProgress(30);
                foreach (var drive in Manager.GetCDVDROMs()) {
                    if (drive != null) {
                        var driveLetter = drive.ToString();
                        var info = DriveInfo.GetInfo(driveLetter);
                        if (DriveInfo.WriteCapabilities != 0) {
                            cboDrive.Items.Add(driveLetter + ": " + DriveInfo.Vendor + " " + DriveInfo.Product + " " + DriveInfo.Revision);
                            cboMakeDataCDDrive.Items.Add(driveLetter + ": " + DriveInfo.Vendor + " " + DriveInfo.Product + " " + DriveInfo.Revision);
                        }
                        init.ShowText("Detecting Device " + driveLetter + ": " + DriveInfo.Vendor + " " + DriveInfo.Product + " " + DriveInfo.Revision);
                    }
                }
                init.SetProgress(75);
                if (cboDrive.SelectedItem == null) { cboDrive.SelectedItem = cboDrive.Items.LastOrDefault(); }
                if (cboMakeDataCDDrive.SelectedItem == null) { cboMakeDataCDDrive.SelectedItem = cboMakeDataCDDrive.Items.LastOrDefault(); }
                var driveLetter2 = cboDrive.SelectedItem.Text.Split(' ')[0];
                UpdateSpeed(driveLetter2);
                init.SetProgress(85);
                init.ShowText("All CD Drives Loaded.");
                init.SetProgress(90);
                init.EndAbout();
                init.Close();
                UpdateUsedSpace(driveLetter2);
                rwbMakeAudioCD.StopWaiting();
            } catch {
                throw;
            }
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e) {
            try {
                IniFiles.WriteINI(Application.StartupPath + @"\settings.ini", "Settings", "Width", this.Width.ToString());
                IniFiles.WriteINI(Application.StartupPath + @"\settings.ini", "Settings", "Height", this.Height.ToString());
                IniFiles.WriteINI(Application.StartupPath + @"\settings.ini", "Settings", "Left", this.Left.ToString());
                IniFiles.WriteINI(Application.StartupPath + @"\settings.ini", "Settings", "Top", this.Top.ToString());
            } catch {
                throw;
            }
        }

        /// <summary>
        /// Decode Controller
        /// </summary>
        /// <param name="percent"></param>
        private void _decodeController_PercentChanged(long percent) {
            try {
                var msg = "Decode [" + (_decodeFilesCompleted + 1).ToString() + "/" + _decodeFiles.Count() + "] '" + _decodeController.Model.CurrentFileName + "' : " + percent.ToString() + "%";
                if (msg != lblDecodeMp3Status.Text) {
                    lblDecodeMp3Status.Text = msg;
                    prgDecode.Value1 = (int)percent;
                    prgDecode.Value2 = (int)percent;
                    this.Refresh();
                    prgDecode.Refresh();
                    Application.DoEvents();
                    if (lblDecodeMp3Status.BackColor != System.Drawing.Color.Transparent) {
                        lblDecodeMp3Status.BackColor = System.Drawing.Color.Transparent;
                    }
                }
            } catch {
                throw;
            }
        }

        /// <summary>
        /// Write Progress
        /// </summary>
        /// <param name="Percent"></param>
        /// <param name="Track"></param>
        void AudioCdWriter_WriteProgress(short Percent, short Track) {
            lblStatus.Text = "Burn Audio CD " + Percent.ToString() + "%.";
            prgTotalProgress.Value1 = Percent;
            prgTotalProgress.Value2 = Percent;
            prgTrackProgress.Value1 = Track;
            prgTrackProgress.Value2 = Track;
        }
        /// <summary>
        /// Audio CD Writer Finished
        /// </summary>
        void AudioCdWriter_Finished() {
            lblStatus.Text = "";
            prgTotalProgress.Value1 = 0;
            prgTotalProgress.Value2 = 0;
            prgTrackProgress.Value1 = 0;
            prgTrackProgress.Value2 = 0;
            prgSpaceAvailable.Value1 = 0;
            prgSpaceAvailable.Value2 = 0;
        }
        /// <summary>
        /// Resize
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Resize(object sender, EventArgs e) {
            ThisResize();
        }
        /// <summary>
        /// This Resize
        /// </summary>
        private void ThisResize() {
            var w = this.Width;
            var h = this.Height;
            rwbMakeAudioCD.Left = w - 374;
            rwbMakeCD.Left = w - 374;
            rwbMakeAudioCD.Top = h - 108;
            rwbMakeCD.Top = h - 108;
            rwbDecodeWait.Left = (this.Width / 2);
            rwbDecodeWait.Top = h - 143;
            rwbDecodeWait.Width = (this.Width / 2) - 30;
            tvwDecodeMp3s.Height = h - 155;
            lblDecodeMp3Status.Top = h - 100;
            lblMakeDataCDStatus.Top = h - 106;
            cmdDecodeNow.Left = w - 165;
            cmdDecodeNow.Top = h - 110;
            cboMakeDataCDDrive.Width = w - (lblMakeDataCDDrive.Width + 41);
            cboMakeDataCDSpeed.Width = w - (lblMakeDataCDSpeed.Width + 36);
            cboDrive.Width = w - (lblDriveDescriptions.Width + 45);
            cboSpeedAudioBurn.Width = w - (lblSpeedDescription.Width + 40);
            cmdBurnAudioFiles.Left = w - 95;
            cmdBurnDataCD.Left = w - 95;
            cmdAddFiles.Top = h - 108;
            cmdDataCDAdd.Top = h - 108;
            cmdBurnAudioFiles.Top = h - 108;
            cmdBurnDataCD.Top = h - 108;
            cmdClearAudioFiles.Top = cmdBurnAudioFiles.Top;
            cmdBurnDataCDClear.Top = cmdBurnAudioFiles.Top;
            cmdDeleteFiles.Top = cmdBurnAudioFiles.Top;
            cmdDataCDDelete.Top = cmdBurnAudioFiles.Top;
            cmdRefresh.Top = cmdBurnAudioFiles.Top;
            cmdBurnDataCDRefresh.Top = cmdBurnAudioFiles.Top;
            cmdRefresh.Left = this.Width - 165;
            cmdBurnDataCDRefresh.Left = this.Width - 165;
            lblStatus.Top = cmdBurnAudioFiles.Top + 3;
            lblStatus.Left = 210;
            prgDecode.Top = h - 143;
            prgTotalProgress.Top = h - 140;
            prgDataCDSpaceAvailable.Top = h - 140;
            //prgDataCDTrackProgress.Top = h - 167;
            prgDataCDTotalProgress.Top = h - 167;
            prgTrackProgress.Top = h - 165;
            prgSpaceAvailable.Top = h - 190;
            lvwAudioFiles.Height = h - 255;
            lvwAudioFiles.Width = w - 38;
            tvwDataCDFiles.Height = h - 231;
            tvwDataCDFiles.Width = w - 38;
            prgDataCDTotalProgress.Width = lvwAudioFiles.Width;
            //prgDataCDTrackProgress.Width = lvwAudioFiles.Width;
            prgDataCDSpaceAvailable.Width = lvwAudioFiles.Width;
            prgDecode.Width = (this.Width / 2) - 6;
            prgSpaceAvailable.Width = lvwAudioFiles.Width;
            prgTrackProgress.Width = lvwAudioFiles.Width;
            prgTotalProgress.Width = lvwAudioFiles.Width;
            tvwDecodeMp3s.Width = lvwAudioFiles.Width;
            if (w < 700) {
                rwbMakeAudioCD.Visible = false;
                rwbMakeCD.Visible = false;
            } else {
                rwbMakeAudioCD.Visible = true;
                rwbMakeCD.Visible = true;
            }
            if (w < 450) {
                lblStatus.Visible = false;
                lblMakeDataCDStatus.Visible = false;
            } else {
                lblStatus.Visible = true;
                lblMakeDataCDStatus.Visible = true;
            }
            if (w < 570) {
                cmdRefresh.Visible = false;
                cmdBurnDataCDRefresh.Visible = false;
            } else {
                cmdRefresh.Visible = true;
                cmdBurnDataCDRefresh.Visible = true;
            }
            if (w < 320) {
                cmdClearAudioFiles.Visible = false;
                cmdBurnDataCDClear.Visible = false;
            } else {
                cmdClearAudioFiles.Visible = true;
                cmdBurnDataCDClear.Visible = true;
            }
            if (w < 240) {
                cmdDeleteFiles.Visible = false;
                cmdDataCDDelete.Visible = false;
            } else {
                cmdDeleteFiles.Visible = true;
                cmdDataCDDelete.Visible = true;
            }
            if (w < 170) {
                cmdAddFiles.Visible = false;
                cmdDataCDAdd.Visible = false;
            } else {
                cmdAddFiles.Visible = true;
                cmdDataCDAdd.Visible = true;
            }
        }
        /// <summary>
        /// Add Files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAddFiles_Click(object sender, EventArgs e) {
            lblStatus.Text = "Prompting ...";
            lblStatus.Refresh();
            this.Refresh();
            Application.DoEvents();
            openFileDialog1.Multiselect = true;
            openFileDialog1.Filter = "Wave Files (*.wav)|*.wav|All Files (*.*)|*.*";
            var result = openFileDialog1.ShowDialog();
            lblStatus.Text = "Processing File(s) ...";
            lblStatus.Refresh();
            this.Refresh();
            rwbMakeAudioCD.StartWaiting();
            Application.DoEvents();
            if (result == DialogResult.OK) {
                foreach (var file in openFileDialog1.FileNames) {
                    try {
                        var text = File.ReadAllText(file);
                        var size = text.Length;
                        lblStatus.Text = "Adding: '" + System.IO.Path.GetFileNameWithoutExtension(file) + "'.";
                        lblStatus.Refresh();
                        this.Refresh();
                        Application.DoEvents();
                        if (!AudioCdWriter.AddFile(file)) {
                            MessageBox.Show("Could not add '" + file + "'");
                        }
                    } catch (IOException ex) {
                        MessageBox.Show("Could not add '" + file + "' because: '" + ex.Message + "'");
                    }
                }
            }
            lblStatus.Text = "";
            lblStatus.Refresh();
            this.Refresh();
            Application.DoEvents();
            ShowAudioFiles();
            var driveLetter = cboDrive.SelectedItem.Text.Split(' ')[0];
            UpdateUsedSpace(driveLetter);
            UpdateSpeed(driveLetter);
            rwbMakeAudioCD.StopWaiting();
        }
        /// <summary>
        /// Update Speed
        /// </summary>
        /// <param name="driveLetter"></param>
        private void UpdateSpeed(string driveLetter) {
            var speeds = DriveInfo.GetWriteSpeeds(driveLetter);
            foreach (var speed in speeds) {
                cboSpeedAudioBurn.Items.Add(speed.ToString());
                cboMakeDataCDSpeed.Items.Add(speed.ToString());
            }
            if (cboSpeedAudioBurn.SelectedItem == null) {
                cboSpeedAudioBurn.SelectedItem = cboSpeedAudioBurn.Items.LastOrDefault();
            }
            if (cboMakeDataCDSpeed.SelectedItem == null) {
                cboMakeDataCDSpeed.SelectedItem = cboMakeDataCDSpeed.Items.LastOrDefault();
            }
        }
        /// <summary>
        /// Update Used Space
        /// </summary>
        /// <param name="driveLetter"></param>
        /// <returns></returns>
        private bool UpdateUsedSpace(string driveLetter) {
            lblStatus.Text = "Updating Used Space ...";
            this.Refresh();
            Application.DoEvents();
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
            if (cd.LBA == 0) {
                lblStatus.Text = "CD has no available space.";
                cmdAddFiles.Enabled = false;
                cmdDeleteFiles.Enabled = false;
                cmdClearAudioFiles.Enabled = false;
                cmdBurnAudioFiles.Enabled = false;
                //lblStatus.Text = "";
                return false;
            } else {
                cmdAddFiles.Enabled = true;
                cmdDeleteFiles.Enabled = true;
                cmdClearAudioFiles.Enabled = true;
                cmdBurnAudioFiles.Enabled = true;
                prgSpaceAvailable.Maximum = cd.LBA;
                if (tracks.LBA > -1) {
                    lblStatus.Text = cd.LBA.ToString() + " available space " + tracks.LBA.ToString() + " total track(s) length";
                    prgSpaceAvailable.Value1 = tracks.LBA;
                    //prgSpaceAvailable.Value2 = tracks.LBA;
                } else {
                    lblStatus.Text = cd.LBA.ToString() + " available space";
                    prgSpaceAvailable.Value1 = 0;
                    prgSpaceAvailable.Value2 = 0;
                }
            }
            //lblStatus.Text = "";
            this.Refresh();
            Application.DoEvents();
            var b = false;
            if (cd.get_M() < m) {
                b = true;
            }
            if (cd.get_M() == m && cd.get_s() < s) {
                b = true;
            }
            return b;
        }
        /// <summary>
        /// Show Audio Files
        /// </summary>
        private void ShowAudioFiles() {
            lblStatus.Text = "Showing Audio File(s) ...";
            lblStatus.Refresh();
            this.Refresh();
            Application.DoEvents();
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
                lvwAudioFiles.Refresh();
                this.Refresh();
                Application.DoEvents();
            }
            lblStatus.Text = "";
            lblStatus.Refresh();
            this.Refresh();
            Application.DoEvents();
        }
        /// <summary>
        /// Clear Audio Files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdClearAudioFiles_Click(object sender, EventArgs e) {
            lvwAudioFiles.Items.Clear();
            AudioCdWriter.Clear();
            var driveLetter = cboDrive.SelectedItem.Text.Split(' ')[0];
            if (UpdateUsedSpace(driveLetter)) {
                var msg = MessageBox.Show(this, "Data size exceeds disk capacity.", "audiogen", MessageBoxButtons.OKCancel);
                if (msg == System.Windows.Forms.DialogResult.Cancel) {
                    return;
                }
            }
        }
        /// <summary>
        /// Burn Audio Files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            if (!string.IsNullOrEmpty(strMsg)) { lblStatus.Text = "Error: " + strMsg; }
        }
        /// <summary>
        /// Paint
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pgeAudioCD_Paint(object sender, PaintEventArgs e) {
        }
        /// <summary>
        /// rpvFunctions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rpvFunctions_SelectedPageChanged(object sender, EventArgs e) {
        }
        /// <summary>
        /// Radtreeview1 Selected Node Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radTreeView1_SelectedNodeChanged(object sender, RadTreeViewEventArgs e) {
        }
        /// <summary>
        /// Page Decode Mp3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pgeDecodeMP3s_Paint(object sender, PaintEventArgs e) {
        }
        /// <summary>
        /// Decode Mp3's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tvwDecodeMp3s_NodeMouseClick(object sender, RadTreeViewEventArgs e) {
            lblDecodeMp3Status.Text = e.Node.Text;
            try {
                if (e.Node.Nodes.Count() == 0) {
                    var fullPath = e.Node.FullPath;
                    var di = new DirectoryInfo(fullPath);
                    var attr = File.GetAttributes(fullPath);
                    if ((attr & FileAttributes.Directory) == FileAttributes.Directory) {
                        var dirs = Directory.GetDirectories(fullPath);
                        if (System.IO.Directory.Exists(fullPath)) {
                            foreach (var dir in dirs) {
                                var di2 = new DirectoryInfo(dir);
                                var node = new RadTreeNode(dir);
                                e.Node.Nodes.Add(di2.Name);
                            }
                        }
                        var files = Directory.GetFiles(fullPath, "*.mp3");
                        foreach (var file in files) {
                            var di3 = new DirectoryInfo(file);
                            var node = new RadTreeNode(di3.Name);
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
                }
            } catch {
                // I don't care, all kinds of things can happen
            }
        }
        /// <summary>
        /// Decode Mp3s Double Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tvwDecodeMp3s_DoubleClick(object sender, EventArgs e) {
        }
        /// <summary>
        /// Decode Now
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDecodeNow_Click(object sender, EventArgs e) {
            try {
                rwbDecodeWait.StartWaiting();
                _decodeFilesCompleted = 0;
                _decodeFiles = tvwDecodeMp3s.SelectedNodes;
                foreach (var file in _decodeFiles) {
                    _decodeController.Model.CurrentFile = file.FullPath;
                    _decodeController.DecodeFile();
                    _decodeFilesCompleted++;
                    prgDecode.Value1 = 0;
                    prgDecode.Value2 = 0;
                    lblDecodeMp3Status.Text = "";
                    Application.DoEvents();
                }
                lblDecodeMp3Status.Text = "Decode File(s) completed";
                rwbDecodeWait.StopWaiting();
            } catch {
                throw;
            }
        }

        /// <summary>
        /// Decoder Thread Ended
        /// </summary>
        void decoder_ThreadEnded() {
            prgDecode.Value1 = 0;
            prgDecode.Value2 = 0;
            lblDecodeMp3Status.Text = "";
        }
        /// <summary>
        /// Decoder Act Frame
        /// </summary>
        /// <param name="ActFrame"></param>
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
        /// <summary>
        /// cmdRefresh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdRefresh_Click(object sender, EventArgs e) {
            lblStatus.Text = "";
            lblStatus.Refresh();
            this.Refresh();
            Application.DoEvents();
            Thread.Sleep(200);
            var driveLetter2 = cboDrive.SelectedItem.Text.Split(' ')[0];
            UpdateUsedSpace(driveLetter2);
        }
        /// <summary>
        /// Delete Files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDeleteFiles_Click(object sender, EventArgs e) {
            try {
                if (lvwAudioFiles.SelectedItems.Count != 0) {
                    var itemsToRemove = new List<ListViewDataItem>();
                    foreach (var item in lvwAudioFiles.SelectedItems) {
                        itemsToRemove.Add(item);
                    }
                    foreach (var item in itemsToRemove) {
                        AudioCdWriter.RemFile((short)Convert.ToInt32(item.SubItems[0].ToString()));
                        lvwAudioFiles.Items.Remove(item);
                    }
                    var driveLetter = cboDrive.SelectedItem.Text.Split(' ')[0];
                    UpdateUsedSpace(driveLetter);
                }
            } catch {
                throw;
            }
        }
        private void pgeDataCD_Paint(object sender, PaintEventArgs e) {
        }
    }
}