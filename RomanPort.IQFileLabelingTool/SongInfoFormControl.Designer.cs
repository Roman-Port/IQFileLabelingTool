
namespace RomanPort.IQFileLabelingTool
{
    partial class SongInfoFormControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.callsign = new System.Windows.Forms.TextBox();
            this.artist = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.prefix = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.suffix = new System.Windows.Forms.ComboBox();
            this.notes = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.switchOk = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.switchHd = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.switchRds = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(-3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Callsign";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // callsign
            // 
            this.callsign.Location = new System.Drawing.Point(0, 18);
            this.callsign.Name = "callsign";
            this.callsign.Size = new System.Drawing.Size(79, 20);
            this.callsign.TabIndex = 1;
            this.callsign.TextChanged += new System.EventHandler(this.EntryTextUpdated);
            // 
            // artist
            // 
            this.artist.Location = new System.Drawing.Point(85, 18);
            this.artist.Name = "artist";
            this.artist.Size = new System.Drawing.Size(150, 20);
            this.artist.TabIndex = 3;
            this.artist.TextChanged += new System.EventHandler(this.EntryTextUpdated);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(82, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Artist";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // title
            // 
            this.title.Location = new System.Drawing.Point(241, 18);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(150, 20);
            this.title.TabIndex = 5;
            this.title.TextChanged += new System.EventHandler(this.EntryTextUpdated);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(238, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Title";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // prefix
            // 
            this.prefix.FormattingEnabled = true;
            this.prefix.Items.AddRange(new object[] {
            "Station slogan",
            "Special station slogan",
            "Station promo",
            "DJ, no clip",
            "DJ, clip",
            "Call Letters",
            "Song"});
            this.prefix.Location = new System.Drawing.Point(397, 17);
            this.prefix.Name = "prefix";
            this.prefix.Size = new System.Drawing.Size(121, 21);
            this.prefix.TabIndex = 6;
            this.prefix.SelectedIndexChanged += new System.EventHandler(this.EntryTextUpdated);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(394, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Prefix";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(521, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Suffix";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // suffix
            // 
            this.suffix.FormattingEnabled = true;
            this.suffix.Items.AddRange(new object[] {
            "Station slogan",
            "Special station slogan",
            "Station promo",
            "DJ, no clip",
            "DJ, clip",
            "Call Letters",
            "Song"});
            this.suffix.Location = new System.Drawing.Point(524, 17);
            this.suffix.Name = "suffix";
            this.suffix.Size = new System.Drawing.Size(121, 21);
            this.suffix.TabIndex = 8;
            this.suffix.SelectedIndexChanged += new System.EventHandler(this.EntryTextUpdated);
            // 
            // notes
            // 
            this.notes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.notes.Location = new System.Drawing.Point(651, 18);
            this.notes.Name = "notes";
            this.notes.Size = new System.Drawing.Size(270, 20);
            this.notes.TabIndex = 11;
            this.notes.TextChanged += new System.EventHandler(this.EntryTextUpdated);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(648, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(153, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "Notes";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // switchOk
            // 
            this.switchOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.switchOk.Location = new System.Drawing.Point(927, 18);
            this.switchOk.Name = "switchOk";
            this.switchOk.Size = new System.Drawing.Size(29, 20);
            this.switchOk.TabIndex = 12;
            this.switchOk.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Location = new System.Drawing.Point(924, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 15);
            this.label7.TabIndex = 13;
            this.label7.Text = "OK";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.Location = new System.Drawing.Point(951, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 15);
            this.label8.TabIndex = 15;
            this.label8.Text = "HD";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // switchHd
            // 
            this.switchHd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.switchHd.Location = new System.Drawing.Point(954, 18);
            this.switchHd.Name = "switchHd";
            this.switchHd.Size = new System.Drawing.Size(29, 20);
            this.switchHd.TabIndex = 14;
            this.switchHd.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.Location = new System.Drawing.Point(978, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 15);
            this.label9.TabIndex = 17;
            this.label9.Text = "RDS";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // switchRds
            // 
            this.switchRds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.switchRds.Location = new System.Drawing.Point(981, 18);
            this.switchRds.Name = "switchRds";
            this.switchRds.Size = new System.Drawing.Size(29, 20);
            this.switchRds.TabIndex = 16;
            this.switchRds.UseVisualStyleBackColor = true;
            // 
            // SongInfoFormControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label9);
            this.Controls.Add(this.switchRds);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.switchHd);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.switchOk);
            this.Controls.Add(this.notes);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.suffix);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.prefix);
            this.Controls.Add(this.title);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.artist);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.callsign);
            this.Controls.Add(this.label1);
            this.Name = "SongInfoFormControl";
            this.Size = new System.Drawing.Size(1006, 40);
            this.Load += new System.EventHandler(this.SongInfoFormControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox callsign;
        public System.Windows.Forms.TextBox artist;
        public System.Windows.Forms.TextBox title;
        public System.Windows.Forms.ComboBox prefix;
        public System.Windows.Forms.ComboBox suffix;
        public System.Windows.Forms.TextBox notes;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.CheckBox switchOk;
        public System.Windows.Forms.CheckBox switchHd;
        public System.Windows.Forms.CheckBox switchRds;
    }
}
