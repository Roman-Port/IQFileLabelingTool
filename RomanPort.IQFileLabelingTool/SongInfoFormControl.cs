using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RomanPort.IQFileLabelingTool
{
    public delegate void EntryTextUpdatedEventArgs();
    
    public partial class SongInfoFormControl : UserControl
    {
        public SongInfoFormControl()
        {
            InitializeComponent();
        }

        public event EntryTextUpdatedEventArgs OnTextUpdated;

        public void ClearForm()
        {
            callsign.Text = "";
            artist.Text = "";
            title.Text = "";
            prefix.Text = "";
            suffix.Text = "";
            notes.Text = "";
            switchOk.Checked = true;
            switchRds.Checked = true;
            switchHd.Checked = true;
        }

        private void EntryTextUpdated(object sender, EventArgs e)
        {
            OnTextUpdated?.Invoke();
        }

        private void SongInfoFormControl_Load(object sender, EventArgs e)
        {

        }
    }
}
