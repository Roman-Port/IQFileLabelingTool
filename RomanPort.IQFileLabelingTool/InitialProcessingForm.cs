using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RomanPort.IQFileLabelingTool
{
    public partial class InitialProcessingForm : Form
    {
        public InitialProcessingForm(ConfigFile config)
        {
            InitializeComponent();
            this.config = config;
        }

        private ConfigFile config;
        private List<RadioFile> files;
        private Thread[] workers = new Thread[0];
        private int completedWorkers = 0;

        private void InitialProcessingForm_Load(object sender, EventArgs e)
        {
            //Search for .WAV files in the output
            files = SeekFiles(out List<RadioFile> unindexed);
            int unindexedCount = unindexed.Count;

            //Configure UI
            totalProgress.Value = 0;
            totalProgress.Maximum = unindexedCount;

            //Either finish or spin off a worker
            if(unindexedCount == 0)
            {
                OnFinished();
            } else
            {
                CreateWorkers(unindexed);
            }
        }

        private void OnFinished()
        {
            Hide();
            new MainLabelingForm(config).ShowDialog();
            Close();
        }

        private void CreateWorkers(List<RadioFile> unindexed)
        {
            //Determine the number of workers to produce
            int workerCount = Math.Max(1, Environment.ProcessorCount);
            int chunkSize = (unindexed.Count / workerCount) + 1;

            //Produce chunks
            List<RadioFile>[] chunks = new List<RadioFile>[workerCount];
            for (int i = 0; i < chunks.Length; i++)
                chunks[i] = new List<RadioFile>();
            for(int i = 0; i < unindexed.Count; i++)
                chunks[i / chunkSize].Add(unindexed[i]);

            //Spin off workers
            workers = new Thread[workerCount];
            for (int i = 0; i < workerCount; i++) {
                workers[i] = new Thread(Worker);
                workers[i].IsBackground = true;
                workers[i].Start(new Tuple<List<RadioFile>, int>(chunks[i], i));
            }
        }

        private void Worker(object obj)
        {
            //Cast our parameters
            Tuple<List<RadioFile>, int> parameters = (Tuple<List<RadioFile>, int>)obj;
            List<RadioFile> files = parameters.Item1;
            int workerIndex = parameters.Item2;
            
            //Open a buffer
            RadioBuffers buffer = new RadioBuffers(32768);

            //Begin processing
            foreach(var f in files)
            {
                //Compute the metadata
                f.GenerateMetadata(buffer, workerIndex);

                //Update UI
                Invoke((MethodInvoker)delegate
                {
                    totalProgress.Value++;
                });
            }

            //Clean up
            buffer.Dispose();

            //Update state
            completedWorkers++;

            //Send finished if this were the last one to complete
            if (completedWorkers == workers.Length)
            {
                Invoke((MethodInvoker)delegate
                {
                    OnFinished();
                });
            }
        }

        private List<RadioFile> SeekFiles(out List<RadioFile> unindexed)
        {
            string[] files = Directory.GetFiles(config.iq_input_dir);
            List<RadioFile> output = new List<RadioFile>();
            unindexed = new List<RadioFile>();
            foreach (var f in files)
            {
                //Check extension
                if (!f.EndsWith(".wav"))
                    continue;

                //Load
                RadioFile file = new RadioFile(f);
                output.Add(file);
                if (!file.metadataGenerated)
                    unindexed.Add(file);
            }
            return output;
        }
    }
}
