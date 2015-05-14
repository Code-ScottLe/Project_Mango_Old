using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;

namespace Mango_Engine
{
    public class Mango_Downloader
    {
        #region Fields
        //Fields
        private Mango_Source _html;
        private string _save_to;
        #endregion

        #region Properties
        //Properties
        public Mango_Source source_html
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
        #endregion

        #region Constructor
        //Contructors
        Mango_Downloader()
        {
            //default constructor
            //Hidden by default.

        }

        public Mango_Downloader(Mango_Source html, string local_uri)
        {
            //Create a downloader for Mango, accept in a string of HTML (source) and the local path (where to save)
            source_html = html;
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


        #endregion
    }
}
