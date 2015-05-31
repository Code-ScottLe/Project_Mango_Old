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
            SourcesList_ComboBox.Items.Add("Fakku");
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
            progressBar1.Value = 0;
            try
            {
                //Create a new instance of the mango source.
                DetailedProgress_Box.AppendText( "Checking Source... \n");
                Mango_Source my_source = new FakkuMango_Source(SourceUrl_Box.Text);
                await my_source.initAsync();

                //Source is OK, create downloader
                DetailedProgress_Box.AppendText("Initalize Downloader... \n");
                Mango_Downloader my_downloader = new Mango_Downloader(my_source, SaveTo_TextBox.Text);

                //Everything is good. Start downloading.
                DetailedProgress_Box.AppendText( "Downloading...\n.\n.\n.\n");
                do
                {
                    await my_downloader.DownloadCurrentPageAsync();
                    DetailedProgress_Box.AppendText(my_downloader.current_filename + " downloaded\n");
                    int completed = my_downloader.completed_percentage();
                    progressBar1.Value = completed;
               
                } while ( await my_downloader.get_next_page_Async() == true);

                //everything is good. 
                progressBar1.Value = 100;
                DetailedProgress_Box.AppendText("Completed!\n");
            }
            
            catch (Exception ex)
            {
                StringBuilder str = new StringBuilder();
                str.AppendFormat("Something is wrong! \n {0} \n Detailed Inner Exception: {1}\n", ex.Message, ex.InnerException);
                DetailedProgress_Box.AppendText(str.ToString());
            }
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //About the program.
            (new Form2()).Show();
        }
    }
}
