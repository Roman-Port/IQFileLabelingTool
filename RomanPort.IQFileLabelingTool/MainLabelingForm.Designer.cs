
namespace RomanPort.IQFileLabelingTool
{
    partial class MainLabelingForm
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
            this.fileList = new System.Windows.Forms.ListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.playbackBar = new System.Windows.Forms.TrackBar();
            this.waveformView = new System.Windows.Forms.PictureBox();
            this.playbackTimer = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.volumeAdjust = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.rdsPs = new System.Windows.Forms.TextBox();
            this.rdsRt = new System.Windows.Forms.TextBox();
            this.fftDisplay = new RomanPort.LibSDR.UI.FFTDisplay();
            this.fftMpx = new RomanPort.LibSDR.UI.FFTSpectrumView();
            this.btnMoveMisc = new System.Windows.Forms.Button();
            this.songInfo = new RomanPort.IQFileLabelingTool.SongInfoFormControl();
            ((System.ComponentModel.ISupportInitialize)(this.playbackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.waveformView)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volumeAdjust)).BeginInit();
            this.SuspendLayout();
            // 
            // fileList
            // 
            this.fileList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.fileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.size});
            this.fileList.FullRowSelect = true;
            this.fileList.HideSelection = false;
            this.fileList.Location = new System.Drawing.Point(12, 12);
            this.fileList.Name = "fileList";
            this.fileList.Size = new System.Drawing.Size(248, 402);
            this.fileList.TabIndex = 0;
            this.fileList.UseCompatibleStateImageBehavior = false;
            this.fileList.View = System.Windows.Forms.View.Details;
            this.fileList.SelectedIndexChanged += new System.EventHandler(this.fileList_SelectedIndexChanged);
            // 
            // name
            // 
            this.name.Text = "Name";
            this.name.Width = 142;
            // 
            // size
            // 
            this.size.Text = "Size";
            this.size.Width = 80;
            // 
            // playbackBar
            // 
            this.playbackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playbackBar.LargeChange = 10;
            this.playbackBar.Location = new System.Drawing.Point(8, 528);
            this.playbackBar.Name = "playbackBar";
            this.playbackBar.Size = new System.Drawing.Size(907, 45);
            this.playbackBar.TabIndex = 1;
            this.playbackBar.Value = 5;
            this.playbackBar.Scroll += new System.EventHandler(this.playbackBar_Scroll);
            this.playbackBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.playbackBar_MouseDown);
            this.playbackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.playbackBar_MouseUp);
            // 
            // waveformView
            // 
            this.waveformView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.waveformView.Location = new System.Drawing.Point(12, 470);
            this.waveformView.Name = "waveformView";
            this.waveformView.Size = new System.Drawing.Size(897, 56);
            this.waveformView.TabIndex = 2;
            this.waveformView.TabStop = false;
            // 
            // playbackTimer
            // 
            this.playbackTimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.playbackTimer.BackColor = System.Drawing.SystemColors.ControlText;
            this.playbackTimer.ForeColor = System.Drawing.SystemColors.Control;
            this.playbackTimer.Location = new System.Drawing.Point(519, 11);
            this.playbackTimer.Name = "playbackTimer";
            this.playbackTimer.Size = new System.Drawing.Size(100, 23);
            this.playbackTimer.TabIndex = 7;
            this.playbackTimer.Text = "00:00 / 00:00";
            this.playbackTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(780, 572);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(129, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(625, 11);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.btnMoveMisc);
            this.panel1.Controls.Add(this.volumeAdjust);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.playbackTimer);
            this.panel1.Location = new System.Drawing.Point(-7, 561);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(935, 51);
            this.panel1.TabIndex = 10;
            // 
            // volumeAdjust
            // 
            this.volumeAdjust.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.volumeAdjust.LargeChange = 20;
            this.volumeAdjust.Location = new System.Drawing.Point(15, 14);
            this.volumeAdjust.Maximum = 100;
            this.volumeAdjust.Name = "volumeAdjust";
            this.volumeAdjust.Size = new System.Drawing.Size(104, 45);
            this.volumeAdjust.TabIndex = 0;
            this.volumeAdjust.TickFrequency = 20;
            this.volumeAdjust.Value = 100;
            this.volumeAdjust.Scroll += new System.EventHandler(this.volumeAdjust_Scroll);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Location = new System.Drawing.Point(15, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Volume";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rdsPs
            // 
            this.rdsPs.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdsPs.Location = new System.Drawing.Point(266, 12);
            this.rdsPs.MaxLength = 8;
            this.rdsPs.Name = "rdsPs";
            this.rdsPs.ReadOnly = true;
            this.rdsPs.Size = new System.Drawing.Size(71, 20);
            this.rdsPs.TabIndex = 11;
            this.rdsPs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // rdsRt
            // 
            this.rdsRt.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdsRt.Location = new System.Drawing.Point(343, 12);
            this.rdsRt.MaxLength = 64;
            this.rdsRt.Name = "rdsRt";
            this.rdsRt.ReadOnly = true;
            this.rdsRt.Size = new System.Drawing.Size(652, 20);
            this.rdsRt.TabIndex = 12;
            // 
            // fftDisplay
            // 
            this.fftDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fftDisplay.FftMaxDb = 0F;
            this.fftDisplay.FftMinDb = -80F;
            this.fftDisplay.Location = new System.Drawing.Point(266, 38);
            this.fftDisplay.Name = "fftDisplay";
            this.fftDisplay.Size = new System.Drawing.Size(643, 287);
            this.fftDisplay.TabIndex = 5;
            // 
            // fftMpx
            // 
            this.fftMpx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fftMpx.FftMaxDb = 0F;
            this.fftMpx.FftMinDb = -80F;
            this.fftMpx.Location = new System.Drawing.Point(266, 331);
            this.fftMpx.Name = "fftMpx";
            this.fftMpx.Size = new System.Drawing.Size(643, 83);
            this.fftMpx.TabIndex = 4;
            // 
            // btnMoveMisc
            // 
            this.btnMoveMisc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveMisc.Enabled = false;
            this.btnMoveMisc.Location = new System.Drawing.Point(706, 11);
            this.btnMoveMisc.Name = "btnMoveMisc";
            this.btnMoveMisc.Size = new System.Drawing.Size(75, 23);
            this.btnMoveMisc.TabIndex = 10;
            this.btnMoveMisc.Text = "Move Misc";
            this.btnMoveMisc.UseVisualStyleBackColor = true;
            this.btnMoveMisc.Click += new System.EventHandler(this.btnMoveMisc_Click);
            // 
            // songInfo
            // 
            this.songInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.songInfo.Location = new System.Drawing.Point(12, 421);
            this.songInfo.Name = "songInfo";
            this.songInfo.Size = new System.Drawing.Size(897, 40);
            this.songInfo.TabIndex = 6;
            // 
            // MainLabelingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 604);
            this.Controls.Add(this.rdsRt);
            this.Controls.Add(this.rdsPs);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.songInfo);
            this.Controls.Add(this.fftDisplay);
            this.Controls.Add(this.fftMpx);
            this.Controls.Add(this.waveformView);
            this.Controls.Add(this.fileList);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.playbackBar);
            this.Name = "MainLabelingForm";
            this.Text = "MainLabelingForm";
            this.Load += new System.EventHandler(this.MainLabelingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.playbackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.waveformView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volumeAdjust)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView fileList;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader size;
        private System.Windows.Forms.TrackBar playbackBar;
        private System.Windows.Forms.PictureBox waveformView;
        private LibSDR.UI.FFTSpectrumView fftMpx;
        private LibSDR.UI.FFTDisplay fftDisplay;
        private SongInfoFormControl songInfo;
        private System.Windows.Forms.Label playbackTimer;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar volumeAdjust;
        private System.Windows.Forms.TextBox rdsPs;
        private System.Windows.Forms.TextBox rdsRt;
        private System.Windows.Forms.Button btnMoveMisc;
    }
}