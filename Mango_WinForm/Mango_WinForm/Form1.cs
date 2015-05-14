using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mango_Engine;

namespace Mango_WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //initialize the window
            InitializeComponent();

            //Set up combo boxes with sources
            //Add the officially supported Mango source here.
            SourcesList_ComboBox.Items.Add("Batoto");
            SourcesList_ComboBox.Items.Add("Generic Source");
        }

        private void SaveTo_Button_Click(object sender, EventArgs e)
        {
            //Click to choose new folder to save to.

            FolderBrowserDialog folder_browser = new FolderBrowserDialog();

            //show the user the folder browser diaglog
            if (folder_browser.ShowDialog() == DialogResult.OK)
            {
                //If the user successfully choose a folder, set that path to the saveto_textbox
                SaveTo_TextBox.Text = folder_browser.SelectedPath + "\\";
            }
        }

        private async void Download_Button_Click(object sender, EventArgs e)
        {
            //Start the download.
            BatotoMango_Source my_source = new BatotoMango_Source(SourceUrl_Box.Text);
            Mango_Downloader my_downloader = new Mango_Downloader(my_source, SaveTo_TextBox.Text);
            progressBar1.Value = 50;
            await my_downloader.startAsync();
            progressBar1.Value = 100;
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //About the program.
            (new Form2()).Show();
        }
    }
}
