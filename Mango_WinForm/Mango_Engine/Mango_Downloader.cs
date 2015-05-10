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
        //Fields
        private string _html;
        private string _save_to;

        //Properties
        public string source_html
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

        //Contructors
        Mango_Downloader()
        {
            //default constructor
            //Hidden by default.

        }

        public Mango_Downloader(string html, string local_uri)
        {
            //Create a downloader for Mango, accept in a string of HTML (source) and the local path (where to save)
            source_html = html;
            local_saving_path = local_uri;
        }


        // Methods
        public bool start()
        {
            //Start downloading with the given html and the saving path.

            //Initialize the WebClient
            WebClient my_client = new WebClient();

            //



            //all good, return true
            return true;
        }
    }
}
