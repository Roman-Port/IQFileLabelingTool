using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RomanPort.IQFileLabelingTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ConfigFile cfg = LoadConfigFile();
            if (cfg == null)
                return;
            Application.Run(new InitialProcessingForm(cfg));
        }

        public const string CONF_PATH = "config.json";

        private static ConfigFile LoadConfigFile()
        {
            //Make file if needed
            if (!File.Exists(CONF_PATH))
                File.WriteAllText(CONF_PATH, JsonConvert.SerializeObject(new ConfigFile(), Formatting.Indented));

            //Loop it
            ConfigFile file;
            while (true)
            {
                //Load
                file = JsonConvert.DeserializeObject<ConfigFile>(File.ReadAllText(CONF_PATH));

                //Validate
                if (file.iq_input_dir != null && file.iq_misc_dir != null && file.iq_output_dir != null && file.iq_output_index_file != null)
                    return file;

                //Warn
                if (MessageBox.Show($"No valid config file! \"{CONF_PATH}\" has been generated, please edit it and try again.", "No Config File", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) != DialogResult.Retry)
                    return null;
            } 
        }
    }
}
