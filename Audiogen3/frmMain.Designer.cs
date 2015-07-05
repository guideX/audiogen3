namespace Audiogen3
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.office2007BlackTheme1 = new Telerik.WinControls.Themes.Office2007BlackTheme();
            this.aquaTheme1 = new Telerik.WinControls.Themes.AquaTheme();
            this.rpvFunctions = new Telerik.WinControls.UI.RadPageView();
            this.pgeAudioCD = new Telerik.WinControls.UI.RadPageViewPage();
            this.prgTotalProgress = new Telerik.WinControls.UI.RadProgressBar();
            this.prgTrackProgress = new Telerik.WinControls.UI.RadProgressBar();
            this.lblStatus = new Telerik.WinControls.UI.RadLabel();
            this.lblSpeedDescription = new Telerik.WinControls.UI.RadLabel();
            this.cboSpeedAudioBurn = new Telerik.WinControls.UI.RadDropDownList();
            this.cmdBurnAudioFiles = new Telerik.WinControls.UI.RadButton();
            this.cmdClearAudioFiles = new Telerik.WinControls.UI.RadButton();
            this.prgSpaceAvailable = new Telerik.WinControls.UI.RadProgressBar();
            this.lvwAudioFiles = new Telerik.WinControls.UI.RadListView();
            this.radButton7 = new Telerik.WinControls.UI.RadButton();
            this.cmdAddFiles = new Telerik.WinControls.UI.RadButton();
            this.lblDriveDescriptions = new Telerik.WinControls.UI.RadLabel();
            this.cboDrive = new Telerik.WinControls.UI.RadDropDownList();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pgeDataCD = new Telerik.WinControls.UI.RadPageViewPage();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radDropDownList1 = new Telerik.WinControls.UI.RadDropDownList();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radDropDownList2 = new Telerik.WinControls.UI.RadDropDownList();
            this.pgeDecodeMP3s = new Telerik.WinControls.UI.RadPageViewPage();
            this.tvwDecodeMp3s = new Telerik.WinControls.UI.RadTreeView();
            this.lblDecodeMp3Status = new Telerik.WinControls.UI.RadLabel();
            this.cmdDecodeNow = new Telerik.WinControls.UI.RadButton();
            this.prgDecode = new Telerik.WinControls.UI.RadProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.rpvFunctions)).BeginInit();
            this.rpvFunctions.SuspendLayout();
            this.pgeAudioCD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.prgTotalProgress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prgTrackProgress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSpeedDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSpeedAudioBurn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmdBurnAudioFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmdClearAudioFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prgSpaceAvailable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lvwAudioFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmdAddFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDriveDescriptions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDrive)).BeginInit();
            this.pgeDataCD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList2)).BeginInit();
            this.pgeDecodeMP3s.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tvwDecodeMp3s)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDecodeMp3Status)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmdDecodeNow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prgDecode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // rpvFunctions
            // 
            this.rpvFunctions.AutoScroll = true;
            this.rpvFunctions.Controls.Add(this.pgeAudioCD);
            this.rpvFunctions.Controls.Add(this.pgeDataCD);
            this.rpvFunctions.Controls.Add(this.pgeDecodeMP3s);
            this.rpvFunctions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpvFunctions.Location = new System.Drawing.Point(0, 0);
            this.rpvFunctions.Name = "rpvFunctions";
            this.rpvFunctions.SelectedPage = this.pgeDecodeMP3s;
            this.rpvFunctions.Size = new System.Drawing.Size(872, 383);
            this.rpvFunctions.TabIndex = 0;
            this.rpvFunctions.Text = "Audio CD";
            this.rpvFunctions.ThemeName = "Office2007Black";
            this.rpvFunctions.SelectedPageChanged += new System.EventHandler(this.rpvFunctions_SelectedPageChanged);
            // 
            // pgeAudioCD
            // 
            this.pgeAudioCD.BackColor = System.Drawing.Color.White;
            this.pgeAudioCD.Controls.Add(this.prgTotalProgress);
            this.pgeAudioCD.Controls.Add(this.prgTrackProgress);
            this.pgeAudioCD.Controls.Add(this.lblStatus);
            this.pgeAudioCD.Controls.Add(this.lblSpeedDescription);
            this.pgeAudioCD.Controls.Add(this.cboSpeedAudioBurn);
            this.pgeAudioCD.Controls.Add(this.cmdBurnAudioFiles);
            this.pgeAudioCD.Controls.Add(this.cmdClearAudioFiles);
            this.pgeAudioCD.Controls.Add(this.prgSpaceAvailable);
            this.pgeAudioCD.Controls.Add(this.lvwAudioFiles);
            this.pgeAudioCD.Controls.Add(this.radButton7);
            this.pgeAudioCD.Controls.Add(this.cmdAddFiles);
            this.pgeAudioCD.Controls.Add(this.lblDriveDescriptions);
            this.pgeAudioCD.Controls.Add(this.cboDrive);
            this.pgeAudioCD.Location = new System.Drawing.Point(10, 37);
            this.pgeAudioCD.Name = "pgeAudioCD";
            this.pgeAudioCD.Size = new System.Drawing.Size(851, 335);
            this.pgeAudioCD.Text = "Audio CD";
            this.pgeAudioCD.Paint += new System.Windows.Forms.PaintEventHandler(this.pgeAudioCD_Paint);
            // 
            // prgTotalProgress
            // 
            this.prgTotalProgress.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.prgTotalProgress.ImageIndex = -1;
            this.prgTotalProgress.ImageKey = "";
            this.prgTotalProgress.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.prgTotalProgress.Location = new System.Drawing.Point(6, 279);
            this.prgTotalProgress.Name = "prgTotalProgress";
            this.prgTotalProgress.SeparatorColor1 = System.Drawing.Color.White;
            this.prgTotalProgress.SeparatorColor2 = System.Drawing.Color.White;
            this.prgTotalProgress.SeparatorColor3 = System.Drawing.Color.White;
            this.prgTotalProgress.SeparatorColor4 = System.Drawing.Color.White;
            this.prgTotalProgress.Size = new System.Drawing.Size(842, 23);
            this.prgTotalProgress.TabIndex = 13;
            this.prgTotalProgress.ThemeName = "Office2007Black";
            // 
            // prgTrackProgress
            // 
            this.prgTrackProgress.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.prgTrackProgress.ImageIndex = -1;
            this.prgTrackProgress.ImageKey = "";
            this.prgTrackProgress.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.prgTrackProgress.Location = new System.Drawing.Point(6, 250);
            this.prgTrackProgress.Name = "prgTrackProgress";
            this.prgTrackProgress.SeparatorColor1 = System.Drawing.Color.White;
            this.prgTrackProgress.SeparatorColor2 = System.Drawing.Color.White;
            this.prgTrackProgress.SeparatorColor3 = System.Drawing.Color.White;
            this.prgTrackProgress.SeparatorColor4 = System.Drawing.Color.White;
            this.prgTrackProgress.Size = new System.Drawing.Size(842, 23);
            this.prgTrackProgress.TabIndex = 12;
            this.prgTrackProgress.ThemeName = "Office2007Black";
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblStatus.Location = new System.Drawing.Point(327, 311);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(404, 18);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "(                                                                                " +
    "                                              )";
            this.lblStatus.ThemeName = "Office2007Black";
            // 
            // lblSpeedDescription
            // 
            this.lblSpeedDescription.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblSpeedDescription.Location = new System.Drawing.Point(6, 32);
            this.lblSpeedDescription.Name = "lblSpeedDescription";
            this.lblSpeedDescription.Size = new System.Drawing.Size(40, 18);
            this.lblSpeedDescription.TabIndex = 10;
            this.lblSpeedDescription.Text = "Speed:";
            this.lblSpeedDescription.ThemeName = "Office2007Black";
            // 
            // cboSpeedAudioBurn
            // 
            this.cboSpeedAudioBurn.DropDownAnimationEnabled = true;
            this.cboSpeedAudioBurn.Location = new System.Drawing.Point(47, 32);
            this.cboSpeedAudioBurn.Name = "cboSpeedAudioBurn";
            this.cboSpeedAudioBurn.ShowImageInEditorArea = true;
            this.cboSpeedAudioBurn.Size = new System.Drawing.Size(801, 22);
            this.cboSpeedAudioBurn.TabIndex = 9;
            this.cboSpeedAudioBurn.ThemeName = "Office2007Black";
            // 
            // cmdBurnAudioFiles
            // 
            this.cmdBurnAudioFiles.Location = new System.Drawing.Point(747, 308);
            this.cmdBurnAudioFiles.Name = "cmdBurnAudioFiles";
            this.cmdBurnAudioFiles.Size = new System.Drawing.Size(101, 24);
            this.cmdBurnAudioFiles.TabIndex = 8;
            this.cmdBurnAudioFiles.Text = "Burn";
            this.cmdBurnAudioFiles.ThemeName = "Office2007Black";
            this.cmdBurnAudioFiles.Click += new System.EventHandler(this.cmdBurnAudioFiles_Click);
            // 
            // cmdClearAudioFiles
            // 
            this.cmdClearAudioFiles.Location = new System.Drawing.Point(220, 308);
            this.cmdClearAudioFiles.Name = "cmdClearAudioFiles";
            this.cmdClearAudioFiles.Size = new System.Drawing.Size(101, 24);
            this.cmdClearAudioFiles.TabIndex = 7;
            this.cmdClearAudioFiles.Text = "Clear";
            this.cmdClearAudioFiles.ThemeName = "Office2007Black";
            this.cmdClearAudioFiles.Click += new System.EventHandler(this.cmdClearAudioFiles_Click);
            // 
            // prgSpaceAvailable
            // 
            this.prgSpaceAvailable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.prgSpaceAvailable.ImageIndex = -1;
            this.prgSpaceAvailable.ImageKey = "";
            this.prgSpaceAvailable.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.prgSpaceAvailable.Location = new System.Drawing.Point(6, 221);
            this.prgSpaceAvailable.Name = "prgSpaceAvailable";
            this.prgSpaceAvailable.SeparatorColor1 = System.Drawing.Color.White;
            this.prgSpaceAvailable.SeparatorColor2 = System.Drawing.Color.White;
            this.prgSpaceAvailable.SeparatorColor3 = System.Drawing.Color.White;
            this.prgSpaceAvailable.SeparatorColor4 = System.Drawing.Color.White;
            this.prgSpaceAvailable.Size = new System.Drawing.Size(842, 23);
            this.prgSpaceAvailable.TabIndex = 6;
            this.prgSpaceAvailable.ThemeName = "Office2007Black";
            // 
            // lvwAudioFiles
            // 
            this.lvwAudioFiles.ForeColor = System.Drawing.Color.White;
            this.lvwAudioFiles.GroupItemSize = new System.Drawing.Size(200, 20);
            this.lvwAudioFiles.ItemSize = new System.Drawing.Size(200, 20);
            this.lvwAudioFiles.Location = new System.Drawing.Point(6, 60);
            this.lvwAudioFiles.Name = "lvwAudioFiles";
            // 
            // 
            // 
            this.lvwAudioFiles.RootElement.ForeColor = System.Drawing.Color.White;
            this.lvwAudioFiles.Size = new System.Drawing.Size(842, 155);
            this.lvwAudioFiles.TabIndex = 5;
            this.lvwAudioFiles.Text = "radListView1";
            this.lvwAudioFiles.ThemeName = "ControlDefault";
            // 
            // radButton7
            // 
            this.radButton7.Location = new System.Drawing.Point(113, 308);
            this.radButton7.Name = "radButton7";
            this.radButton7.Size = new System.Drawing.Size(101, 24);
            this.radButton7.TabIndex = 4;
            this.radButton7.Text = "Delete File(s)";
            this.radButton7.ThemeName = "Office2007Black";
            // 
            // cmdAddFiles
            // 
            this.cmdAddFiles.Location = new System.Drawing.Point(6, 308);
            this.cmdAddFiles.Name = "cmdAddFiles";
            this.cmdAddFiles.Size = new System.Drawing.Size(101, 24);
            this.cmdAddFiles.TabIndex = 3;
            this.cmdAddFiles.Text = "Add File(s)";
            this.cmdAddFiles.ThemeName = "Office2007Black";
            this.cmdAddFiles.Click += new System.EventHandler(this.cmdAddFiles_Click);
            // 
            // lblDriveDescriptions
            // 
            this.lblDriveDescriptions.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblDriveDescriptions.Location = new System.Drawing.Point(6, 4);
            this.lblDriveDescriptions.Name = "lblDriveDescriptions";
            this.lblDriveDescriptions.Size = new System.Drawing.Size(35, 18);
            this.lblDriveDescriptions.TabIndex = 1;
            this.lblDriveDescriptions.Text = "Drive:";
            this.lblDriveDescriptions.ThemeName = "Office2007Black";
            // 
            // cboDrive
            // 
            this.cboDrive.DropDownAnimationEnabled = true;
            this.cboDrive.Location = new System.Drawing.Point(47, 4);
            this.cboDrive.Name = "cboDrive";
            this.cboDrive.ShowImageInEditorArea = true;
            this.cboDrive.Size = new System.Drawing.Size(801, 22);
            this.cboDrive.TabIndex = 0;
            this.cboDrive.ThemeName = "Office2007Black";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pgeDataCD
            // 
            this.pgeDataCD.Controls.Add(this.radLabel1);
            this.pgeDataCD.Controls.Add(this.radDropDownList1);
            this.pgeDataCD.Controls.Add(this.radLabel2);
            this.pgeDataCD.Controls.Add(this.radDropDownList2);
            this.pgeDataCD.Location = new System.Drawing.Point(10, 37);
            this.pgeDataCD.Name = "pgeDataCD";
            this.pgeDataCD.Size = new System.Drawing.Size(851, 335);
            this.pgeDataCD.Text = "Data CD";
            // 
            // radLabel1
            // 
            this.radLabel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.radLabel1.Location = new System.Drawing.Point(3, 31);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(40, 18);
            this.radLabel1.TabIndex = 14;
            this.radLabel1.Text = "Speed:";
            this.radLabel1.ThemeName = "Office2007Black";
            // 
            // radDropDownList1
            // 
            this.radDropDownList1.DropDownAnimationEnabled = true;
            this.radDropDownList1.Location = new System.Drawing.Point(44, 31);
            this.radDropDownList1.Name = "radDropDownList1";
            this.radDropDownList1.ShowImageInEditorArea = true;
            this.radDropDownList1.Size = new System.Drawing.Size(801, 22);
            this.radDropDownList1.TabIndex = 13;
            this.radDropDownList1.ThemeName = "Office2007Black";
            // 
            // radLabel2
            // 
            this.radLabel2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.radLabel2.Location = new System.Drawing.Point(3, 3);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(35, 18);
            this.radLabel2.TabIndex = 12;
            this.radLabel2.Text = "Drive:";
            this.radLabel2.ThemeName = "Office2007Black";
            // 
            // radDropDownList2
            // 
            this.radDropDownList2.DropDownAnimationEnabled = true;
            this.radDropDownList2.Location = new System.Drawing.Point(44, 3);
            this.radDropDownList2.Name = "radDropDownList2";
            this.radDropDownList2.ShowImageInEditorArea = true;
            this.radDropDownList2.Size = new System.Drawing.Size(801, 22);
            this.radDropDownList2.TabIndex = 11;
            this.radDropDownList2.ThemeName = "Office2007Black";
            // 
            // pgeDecodeMP3s
            // 
            this.pgeDecodeMP3s.Controls.Add(this.prgDecode);
            this.pgeDecodeMP3s.Controls.Add(this.cmdDecodeNow);
            this.pgeDecodeMP3s.Controls.Add(this.lblDecodeMp3Status);
            this.pgeDecodeMP3s.Controls.Add(this.tvwDecodeMp3s);
            this.pgeDecodeMP3s.Location = new System.Drawing.Point(10, 37);
            this.pgeDecodeMP3s.Name = "pgeDecodeMP3s";
            this.pgeDecodeMP3s.Size = new System.Drawing.Size(851, 335);
            this.pgeDecodeMP3s.Text = "Decode MP3s";
            this.pgeDecodeMP3s.Paint += new System.Windows.Forms.PaintEventHandler(this.pgeDecodeMP3s_Paint);
            // 
            // tvwDecodeMp3s
            // 
            this.tvwDecodeMp3s.Location = new System.Drawing.Point(3, 3);
            this.tvwDecodeMp3s.Name = "tvwDecodeMp3s";
            this.tvwDecodeMp3s.Size = new System.Drawing.Size(845, 268);
            this.tvwDecodeMp3s.SpacingBetweenNodes = -1;
            this.tvwDecodeMp3s.TabIndex = 0;
            this.tvwDecodeMp3s.Text = "radTreeView1";
            this.tvwDecodeMp3s.SelectedNodeChanged += new Telerik.WinControls.UI.RadTreeView.RadTreeViewEventHandler(this.radTreeView1_SelectedNodeChanged);
            // 
            // lblDecodeMp3Status
            // 
            this.lblDecodeMp3Status.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblDecodeMp3Status.Location = new System.Drawing.Point(3, 314);
            this.lblDecodeMp3Status.Name = "lblDecodeMp3Status";
            this.lblDecodeMp3Status.Size = new System.Drawing.Size(839, 18);
            this.lblDecodeMp3Status.TabIndex = 1;
            this.lblDecodeMp3Status.Text = resources.GetString("lblDecodeMp3Status.Text");
            this.lblDecodeMp3Status.ThemeName = "Office2007Black";
            // 
            // cmdDecodeNow
            // 
            this.cmdDecodeNow.Enabled = false;
            this.cmdDecodeNow.Location = new System.Drawing.Point(718, 308);
            this.cmdDecodeNow.Name = "cmdDecodeNow";
            this.cmdDecodeNow.Size = new System.Drawing.Size(130, 24);
            this.cmdDecodeNow.TabIndex = 0;
            this.cmdDecodeNow.Text = "Decode";
            this.cmdDecodeNow.Click += new System.EventHandler(this.cmdDecodeNow_Click);
            // 
            // prgDecode
            // 
            this.prgDecode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.prgDecode.ImageIndex = -1;
            this.prgDecode.ImageKey = "";
            this.prgDecode.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.prgDecode.Location = new System.Drawing.Point(3, 277);
            this.prgDecode.Name = "prgDecode";
            this.prgDecode.SeparatorColor1 = System.Drawing.Color.White;
            this.prgDecode.SeparatorColor2 = System.Drawing.Color.White;
            this.prgDecode.SeparatorColor3 = System.Drawing.Color.White;
            this.prgDecode.SeparatorColor4 = System.Drawing.Color.White;
            this.prgDecode.Size = new System.Drawing.Size(845, 25);
            this.prgDecode.TabIndex = 2;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(872, 383);
            this.Controls.Add(this.rpvFunctions);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "audiogen";
            this.ThemeName = "Office2007Black";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rpvFunctions)).EndInit();
            this.rpvFunctions.ResumeLayout(false);
            this.pgeAudioCD.ResumeLayout(false);
            this.pgeAudioCD.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.prgTotalProgress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prgTrackProgress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSpeedDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSpeedAudioBurn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmdBurnAudioFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmdClearAudioFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prgSpaceAvailable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lvwAudioFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmdAddFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDriveDescriptions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDrive)).EndInit();
            this.pgeDataCD.ResumeLayout(false);
            this.pgeDataCD.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList2)).EndInit();
            this.pgeDecodeMP3s.ResumeLayout(false);
            this.pgeDecodeMP3s.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tvwDecodeMp3s)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDecodeMp3Status)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmdDecodeNow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prgDecode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.Office2007BlackTheme office2007BlackTheme1;
        private Telerik.WinControls.Themes.AquaTheme aquaTheme1;
        private Telerik.WinControls.UI.RadPageView rpvFunctions;
        private Telerik.WinControls.UI.RadPageViewPage pgeAudioCD;
        private Telerik.WinControls.UI.RadDropDownList cboDrive;
        private Telerik.WinControls.UI.RadLabel lblDriveDescriptions;
        private Telerik.WinControls.UI.RadButton radButton7;
        private Telerik.WinControls.UI.RadButton cmdAddFiles;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Telerik.WinControls.UI.RadListView lvwAudioFiles;
        private Telerik.WinControls.UI.RadButton cmdClearAudioFiles;
        private Telerik.WinControls.UI.RadProgressBar prgSpaceAvailable;
        private Telerik.WinControls.UI.RadButton cmdBurnAudioFiles;
        private Telerik.WinControls.UI.RadLabel lblSpeedDescription;
        private Telerik.WinControls.UI.RadDropDownList cboSpeedAudioBurn;
        private Telerik.WinControls.UI.RadLabel lblStatus;
        private Telerik.WinControls.UI.RadProgressBar prgTotalProgress;
        private Telerik.WinControls.UI.RadProgressBar prgTrackProgress;
        private Telerik.WinControls.UI.RadPageViewPage pgeDataCD;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadDropDownList radDropDownList1;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadDropDownList radDropDownList2;
        private Telerik.WinControls.UI.RadPageViewPage pgeDecodeMP3s;
        private Telerik.WinControls.UI.RadTreeView tvwDecodeMp3s;
        private Telerik.WinControls.UI.RadLabel lblDecodeMp3Status;
        private Telerik.WinControls.UI.RadProgressBar prgDecode;
        private Telerik.WinControls.UI.RadButton cmdDecodeNow;
    }
}
