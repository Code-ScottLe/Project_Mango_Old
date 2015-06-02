using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Mango_Engine;

namespace Mango_WinForm
{
    public class Mango_Downloader
    {
        #region Fields
        //Fields
        private MangoSource _html;
        private string _save_to;
        private int _downloaded_count;
        #endregion

        #region Properties
        //Properties
        public MangoSource source_html
        {
            get
            {
                return _html;
            }

            set
            {
                _html = value;
            }
        }

        public string local_saving_path
        {
            get
            {
                return _save_to;
            }

            set
            {
                _save_to = value;
            }
        }

        public string current_filename
        {
            get { return source_html.current_file_name; }
        }

        public int downloaded_count
        {
            get
            {
                return _downloaded_count;
            }
        }
        #endregion

        #region Constructor
        //Contructors
        Mango_Downloader()
        {
            //default constructor
            //Hidden by default.

        }

        public Mango_Downloader(MangoSource html, string local_uri)
        {
            //Create a downloader for Mango, accept in a string of HTML (source) and the local path (where to save)
            source_html = html;
            _downloaded_count = 0;
            local_saving_path = local_uri;
        }

        #endregion

        #region Methods
        // Methods
        public bool start()
        {
            //Start downloading with the given html and the saving path.

            //Initialize the WebClient
            WebClient my_client = new WebClient();

            //Set the encoding
            my_client.Encoding = source_html.encoding_type;

            //status of downloading
            bool continuing = true;

            do
            {
                //Download the current page
                my_client.DownloadFile(source_html.get_image_url(), _save_to + source_html.current_file_name);

                //Increse the download count
                _downloaded_count++;

                //try to get the next page
                continuing = source_html.next_page();

            } while (continuing == true);

            //all good, return true
            return true;
        }

        public async Task startAsync()
        {
            Task t = Task.Run(() => this.start());
            await t;
        }

        public async Task DownloadCurrentPageAsync()
        {
            //Start downloading with the given html and the saving path.

            //Initialize the WebClient
            WebClient my_client = new WebClient();

            //Set the encoding
            my_client.Encoding = source_html.encoding_type;

            //Download the current page
            await Task.Factory.StartNew(() => { my_client.DownloadFile(new Uri(source_html.get_image_url()), _save_to + source_html.current_file_name); });

            _downloaded_count++;
        }

        public bool get_next_page()
        {
            return source_html.next_page();
        }

        public async Task<bool> get_next_page_Async()
        {
            return await source_html.next_page_Async();
        }

        public int completed_percentage()
        {
            float percentaged = (float)_downloaded_count / (float)source_html.total_pages;

            int percentage = (int)(percentaged * 100);

            return percentage;
        }

        #endregion
    }
}
