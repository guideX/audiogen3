using System;
using System.Windows.Forms;
using FlamedLib;
namespace Audiogen3 {
    public partial class frmInit : Telerik.WinControls.UI.RadForm {
        public void EndAbout() {
            lblStatus.Text = "Loading Complete";
            pgbProgress.Value1 = 100;
            pgbProgress.Value2 = 100;
            Application.DoEvents();
            System.Threading.Thread.Sleep(500);
        }
        public void BeginAbout() {
            lblStatus.Text = "Loading";
            pgbProgress.Value1 = 0;
            pgbProgress.Value2 = 0;
            Application.DoEvents();
            System.Threading.Thread.Sleep(500);
        }
        public void SetProgress(int progress) {
            pgbProgress.Value1 = progress;
            pgbProgress.Value2 = progress;
            this.Refresh();
            pgbProgress.Refresh();
            Application.DoEvents();
        }
        public frmInit() {
            InitializeComponent();
        }
        public void ShowText(string text) {
            lblStatus.Text = text;
            lblStatus.Refresh();
            this.Refresh();
            Application.DoEvents();
            System.Threading.Thread.Sleep(500);
        }
        private void frmInit_Load(object sender, EventArgs e) {
            /*
            this.Visible = true;
            this.Refresh();
            //var driveInfo = new FL_DriveInfo();
            //var manager = new FlamedLib.FL_Manager();
            lblStatus.Text = "Searching for interfaces ...";
            lblStatus.Refresh();
            this.Refresh();
            if (!manager.Init(false)) {
                MessageBox.Show("No interfaces found.");
                Application.Exit();
            }
            System.Threading.Thread.Sleep(500);
            lblStatus.Text = "Used interface:" + manager.CurrentInterface;
            lblStatus.Refresh();
            this.Refresh();
            System.Threading.Thread.Sleep(500);
            lblStatus.Text = "Searching for Drives ...";
            lblStatus.Refresh();
            this.Refresh();
            foreach (var drive in manager.GetCDVDROMs()) {
                if (drive != null) {
                    var driveLetter = drive.ToString();
                    var info = driveInfo.GetInfo(driveLetter);
                    lblStatus.Text = driveLetter + ": " + driveInfo.Vendor + " " + driveInfo.Product + " " + driveInfo.Revision;
                    System.Threading.Thread.Sleep(500);
                    lblStatus.Refresh();
                    this.Refresh();
                }
            }
            lblStatus.Text = "Ready.";
            System.Threading.Thread.Sleep(500);
            lblStatus.Refresh();
            this.Refresh();
            this.Close();
             */
        }
    }
}