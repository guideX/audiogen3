﻿namespace Audiogen3
{
    partial class frmInit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInit));
            this.lblStatus = new Telerik.WinControls.UI.RadLabel();
            this.pgbProgress = new Telerik.WinControls.UI.RadProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pgbProgress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(12, 281);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(2, 2);
            this.lblStatus.TabIndex = 0;
            // 
            // pgbProgress
            // 
            this.pgbProgress.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pgbProgress.ImageIndex = -1;
            this.pgbProgress.ImageKey = "";
            this.pgbProgress.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pgbProgress.Location = new System.Drawing.Point(208, 28);
            this.pgbProgress.Name = "pgbProgress";
            this.pgbProgress.SeparatorColor1 = System.Drawing.Color.White;
            this.pgbProgress.SeparatorColor2 = System.Drawing.Color.White;
            this.pgbProgress.SeparatorColor3 = System.Drawing.Color.White;
            this.pgbProgress.SeparatorColor4 = System.Drawing.Color.White;
            this.pgbProgress.Size = new System.Drawing.Size(177, 23);
            this.pgbProgress.TabIndex = 1;
            this.pgbProgress.ThemeName = "Office2007Black";
            // 
            // frmInit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Audiogen3.Properties.Resources.ag_splash_1;
            this.ClientSize = new System.Drawing.Size(400, 311);
            this.Controls.Add(this.pgbProgress);
            this.Controls.Add(this.lblStatus);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInit";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "audiogen 3";
            this.ThemeName = "Office2007Black";
            this.Load += new System.EventHandler(this.frmInit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lblStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pgbProgress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel lblStatus;
        private Telerik.WinControls.UI.RadProgressBar pgbProgress;
    }
}
