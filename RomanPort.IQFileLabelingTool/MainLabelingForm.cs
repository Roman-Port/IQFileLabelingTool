using RomanPort.IQFileLabelingTool.Util;
using RomanPort.LibSDR.Framework.Components.FFT;
using RomanPort.LibSDR.Framework.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RomanPort.IQFileLabelingTool
{
    public partial class MainLabelingForm : Form
    {
        public MainLabelingForm(ConfigFile config)
        {
            InitializeComponent();
            fftDisplay.FftMinDb = -100;
            this.config = config;
            files = new List<RadioFile>();
            songInfo.OnTextUpdated += SongInfo_OnTextUpdated;
        }

        public static float volume = 1;

        private ConfigFile config;
        private FileStream indexFile;
        private List<RadioFile> files;
        private RadioBuffers buffer;
        private Timer fftTimer;
        private UnsafeBuffer fftBuffer;
        private unsafe float* fftBufferPtr;
        private bool lockPlayBar;
        private FFTPrettifier fftSmoothenerMainSpectrum;
        private FFTPrettifier fftSmoothenerMainWaterfall;
        private FFTPrettifier fftSmoothenerMpx;

        private RadioFile activeFile;
        private RadioPlayer activePlayer;

        public const int FFT_SIZE = 16384;

        private unsafe void MainLabelingForm_Load(object sender, EventArgs e)
        {
            //Open buffers
            buffer = new RadioBuffers(32768);
            fftBuffer = UnsafeBuffer.Create(FFT_SIZE, sizeof(float));
            fftBufferPtr = (float*)fftBuffer;

            //Open index file and go to end
            indexFile = new FileStream(config.iq_output_index_file, FileMode.OpenOrCreate);
            indexFile.Position = indexFile.Length;

            //Seek
            SeekUsableFiles();

            //Generate menu
            fileList.BeginUpdate();
            foreach(var f in files)
            {
                var entry = new ListViewItem(new string[]
                {
                    f.info.Name,
                    Math.Round(f.info.Length / 1000f / 1000f, 1) + " MB"
                });
                entry.Tag = f;
                fileList.Items.Add(entry);
            }
            fileList.EndUpdate();

            //Configure FFT timer
            fftTimer = new Timer();
            fftTimer.Interval = 50;
            fftTimer.Tick += FftTimer_Tick;
        }

        private unsafe void FftTimer_Tick(object sender, EventArgs e)
        {
            //Abort if needed
            if (activePlayer == null || !activePlayer.fftReady)
                return;

            //Update main FFT spectrum
            fftDisplay.RefreshFFT();
            fftMpx.RefreshFFT();

            //Update time
            if(!lockPlayBar)
                playbackBar.Value = (int)activePlayer.positionSeconds;
            playbackTimer.Text = SecondsToString(activePlayer.positionSeconds) + " / " + SecondsToString(activePlayer.lengthSeconds);
        }

        private void SeekUsableFiles()
        {
            string[] files = Directory.GetFiles(config.iq_input_dir);
            foreach (var f in files)
            {
                //Check extension
                if (!f.EndsWith(".wav"))
                    continue;

                //Load
                RadioFile file = new RadioFile(f);
                if(file.metadataGenerated)
                    this.files.Add(file);
            }
        }

        public void UnloadFile()
        {
            //If there is an active player, stop it
            activePlayer?.Stop();

            //Stop timer
            fftTimer?.Stop();

            //Clear form and UI
            playbackBar.Value = 0;
            songInfo.ClearForm();
            btnDelete.Enabled = false;
            btnMoveMisc.Enabled = false;
            btnSave.Enabled = false;
        }

        public void DelistFile()
        {
            //Remove it
            for(int i = 0; i<fileList.Items.Count; i++)
            {
                if(fileList.Items[i].Tag == activeFile)
                {
                    fileList.Items.RemoveAt(i);
                    break;
                }
            }
        }

        public void SwitchFile(RadioFile f)
        {
            //Set state
            UnloadFile();
            activeFile = f;

            //Generate waveform
            waveformView.Image = FileWaveformGenerator.GenerateWaveformImage(activeFile.GetMetaWaveform(), waveformView.Width, waveformView.Height);

            //Create player
            activePlayer = new RadioPlayer(f, buffer);
            activePlayer.Start();

            //Create FFTs
            fftSmoothenerMainSpectrum = new FFTPrettifier(activePlayer.mainFft);
            fftSmoothenerMainWaterfall = new FFTPrettifier(activePlayer.mainFft, 0.9f, 0.7f);
            fftSmoothenerMpx = new FFTPrettifier(activePlayer.mpxFft);

            //Apply FFTs
            fftDisplay.ConfigureFFT(fftSmoothenerMainSpectrum, fftSmoothenerMainWaterfall);
            fftMpx.SetFFT(fftSmoothenerMpx);

            //Set up UI
            playbackBar.Value = 0;
            playbackBar.Maximum = (int)activePlayer.lengthSeconds;
            btnDelete.Enabled = true;
            btnMoveMisc.Enabled = true;
            btnSave.Enabled = false;

            //Configure RDS
            activePlayer.rds.OnPsBufferUpdated += Rds_OnPsBufferUpdated;
            activePlayer.rds.OnRtBufferUpdated += Rds_OnRtBufferUpdated;

            //Configure metadata
            if (f.TryGetMetaCallsign(out string callLetters))
                songInfo.callsign.Text = callLetters + "-FM";
            if (f.TryGetMetaTrackInfo(out string title, out string artist, out string stationName))
            {
                songInfo.artist.Text = artist;
                songInfo.title.Text = title;
            }

            //Start timer
            fftTimer.Start();
        }

        private void Rds_OnRtBufferUpdated(LibSDR.Extras.RDS.RDSClient client, char[] buffer)
        {
            SetRdsTextBox(rdsRt, buffer);
        }

        private void Rds_OnPsBufferUpdated(LibSDR.Extras.RDS.RDSClient client, char[] buffer)
        {
            SetRdsTextBox(rdsPs, buffer);
        }

        private void SetRdsTextBox(TextBox box, char[] buffer)
        {
            char[] b = new char[buffer.Length];
            for(int i = 0; i<buffer.Length; i++)
            {
                if (buffer[i] == 0x00)
                    b[i] = ' ';
                else
                    b[i] = buffer[i];
            }
            BeginInvoke((MethodInvoker)delegate
            {
                box.Text = new string(b);
            });
        }

        private void volumeAdjust_Scroll(object sender, EventArgs e)
        {
            volume = volumeAdjust.Value / 100f;
        }

        private static string SecondsToString(float time)
        {
            int timeInt = (int)time;
            int mins = timeInt / 60;
            int secs = timeInt % 60;
            return mins.ToString().PadLeft(2, '0') + ":" + secs.ToString().PadLeft(2, '0');
        }

        private void playbackBar_Scroll(object sender, EventArgs e)
        {
            activePlayer.Seek(playbackBar.Value);
        }

        private void playbackBar_MouseUp(object sender, MouseEventArgs e)
        {
            lockPlayBar = false;
        }

        private void playbackBar_MouseDown(object sender, MouseEventArgs e)
        {
            lockPlayBar = true;
        }

        private void fileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fileList.SelectedItems.Count == 0)
                UnloadFile();
            else
                SwitchFile((RadioFile)fileList.SelectedItems[0].Tag);
        }

        private void SongInfo_OnTextUpdated()
        {
            btnSave.Enabled = songInfo.callsign.Text.Length > 0 &&
                songInfo.artist.Text.Length > 0 &&
                songInfo.title.Text.Length > 0 &&
                songInfo.prefix.SelectedIndex != -1 &&
                songInfo.suffix.SelectedIndex != -1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Generate the random ID for this file
            char[] idBuffer = new char[8];
            Random idRand = new Random();
            for (int i = 0; i < idBuffer.Length; i++)
                idBuffer[i] = (char)idRand.Next(65, 90); //A-Z ASCII
            string id = new string(idBuffer);

            //Get various details
            string radio = activeFile.GetMetaSampleRate() == 750000 ? "AirSpy" : "RTL-SDR";
            string sha256 = BitConverter.ToString(activeFile.GetMetaHash()).Replace("-", "");
            string size = Math.Round(activeFile.info.Length / 1000f / 1000f, 1) + " MB";
            string time = activeFile.info.LastWriteTime.ToShortDateString();

            //Create entry and filename
            string entry = $"{songInfo.callsign.Text}\t{songInfo.artist.Text}\t{songInfo.title.Text}\t{time}\tUnzipped (ID2)\t{songInfo.prefix.Text}\t{songInfo.suffix.Text}\t{radio}\t{size}\t{GetEntryCheck(songInfo.switchRds.Checked)}\t{GetEntryCheck(songInfo.switchHd.Checked)}\t{GetEntryCheck(songInfo.switchOk.Checked)}\t{songInfo.notes.Text}\t{id}\t{sha256}\n";
            string filename = $"{GetPathSafeString(songInfo.callsign.Text)} - {GetPathSafeString(songInfo.artist.Text)} - {GetPathSafeString(songInfo.title.Text)} - {id}.wav";

            //Close radio
            UnloadFile();

            //Delist
            DelistFile();

            //Move file to new destination
            File.Move(activeFile.info.FullName, config.iq_output_dir + filename);

            //Delete metadata
            File.Delete(activeFile.metaPath);

            //Write entry
            byte[] entryPayload = Encoding.UTF8.GetBytes(entry);
            indexFile.Write(entryPayload, 0, entryPayload.Length);
            indexFile.Flush();
        }

        private string GetPathSafeString(string s)
        {
            char[] bad = Path.GetInvalidFileNameChars();
            foreach (char c in bad)
                s = s.Replace(c, '_');
            return s;
        }

        private string GetEntryCheck(bool ok)
        {
            return ok ? "✓" : "✗";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Prompt
            if (MessageBox.Show($"Are you sure you wish to delete {activeFile.info.Name}?", "Delete File", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            //Close radio
            UnloadFile();

            //Delist
            DelistFile();

            //Delete
            File.Delete(activeFile.info.FullName);
            File.Delete(activeFile.metaPath);
        }

        private void btnMoveMisc_Click(object sender, EventArgs e)
        {
            //Close radio
            UnloadFile();

            //Delist
            DelistFile();

            //Move
            string output = config.iq_misc_dir + activeFile.info.Name;
            int index = 0;
            while(File.Exists(output))
                output = config.iq_misc_dir + index++ + "_" + activeFile.info.Name;
            File.Move(activeFile.info.FullName, output);

            //Remove meta
            File.Delete(activeFile.metaPath);
        }
    }
}
