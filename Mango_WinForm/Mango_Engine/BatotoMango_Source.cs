using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Net;
using HtmlAgilityPack;

namespace Mango_Engine
{
    public class BatotoMango_Source : Mango_Source
    {
        #region Fields
        //Fields
        private int _pages;
        #endregion

        #region Properties
        //Properties
        public int pages
        {
            get
            {
                return _pages;
            }

            set
            {
                _pages = value;
            }
        }
        #endregion

        #region Constructors
        //Constructors
        BatotoMango_Source()
        {
            //default constructor, do nothing,.
        }

        public BatotoMango_Source(string url_source) : base (url_source)
        {
            //Create a new instace of BatotoMango_Source, an object represent bato.to source for mango.
        }
        #endregion

        #region Methods
        //Methods

        protected override void init()
        {
            //Base init.
            base.init();

            /*
            //Get the data from the web about the number of pages for the current eposide.
            //Get the current path of the software (for a temp html file)
            var current_path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string temp_html = current_path + "temp_html.html";

            //download the webpage and save as HTML.
            WebClient my_client = new WebClient();
            my_client.Encoding = encoding_type;
            my_client.DownloadFile(current_url, temp_html);

            //Done with downloading, dispose the WebClient
            my_client.Dispose();

            //Load up the temp html file.
            HtmlDocument my_doc = new HtmlDocument();
            my_doc.Load(temp_html);

            //Attemp to search for the page_select combo box, which contain all the files.
            HtmlNodeCollection select_nodes = my_doc.DocumentNode.SelectNodes("//select");
            HtmlNode page_select_node = null;

            //Search among the select boxes
            foreach (HtmlNode select_node in select_nodes)
            {
                //if exists the field ID
                if (!select_node.Attributes.Contains("id"))
                {
                    continue;
                }

                //Where ID = "page_select"
                if (select_node.Attributes["id"].Value == "page_select")
                {
                    //this is the one.
                    page_select_node = select_node;
                    break;
                }
            }

            if(page_select_node == null)
            {
                //something is off.
                pages = -1;
            }

            else
            {
                //all okay.
                //Get the last text node inside this select node. 
                int last_node_index = page_select_node.ChildNodes.IndexOf(page_select_node.LastChild) - 1;
                HtmlNode last_node = page_select_node.ChildNodes[last_node_index];

                //get the text value.
                string last_page = last_node.InnerText;

                //convert to from str to number for the number of pages.
                int number_of_pages = -1;
                Int32.TryParse(last_page.Substring(5), out number_of_pages);

                //assign to the page number.
                pages = number_of_pages;
            }
            */
        }
        public override bool next_page()
        {
            //modify the URL to get to the next page.

            //Get the current path of the software (for a temp html file)
            var current_path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string temp_html = current_path + "temp_html.html";

            //download the webpage and save as HTML.
            WebClient my_client = new WebClient();
            my_client.Encoding = encoding_type;
            my_client.DownloadFile(current_url, temp_html);

            //Done with downloading, dispose the WebClient
            my_client.Dispose();

            //Load up the temp html file.
            HtmlDocument my_doc = new HtmlDocument();
            my_doc.Load(temp_html);

            //Attemp to search for the page_select combo box, which contain all the files.
            HtmlNodeCollection select_nodes = my_doc.DocumentNode.SelectNodes("//select");
            HtmlNode page_select_node = null;

            //Search among the select boxes
            foreach (HtmlNode select_node in select_nodes)
            {
                //if exists the field ID
                if (!select_node.Attributes.Contains("id"))
                {
                    continue;
                }
                
                //Where ID = "page_select"
                if (select_node.Attributes["id"].Value == "page_select")
                {
                    //this is the one.
                    page_select_node = select_node;
                    break;
                }
            }

            if(page_select_node == null)
            {
                //if it is still null (no values was found), error.
                return false;
            }

            //if reach here, mean the correct node was found.
            HtmlNode next_page = null;

            //Now search through all the options attribute and find the one that is marked as "selected"
            foreach(HtmlNode option_node in page_select_node.ChildNodes)
            {
                if(!option_node.Attributes.Contains("selected"))
                {
                    //Not selected. Keep moving.
                    continue;
                }

                if(option_node.Attributes["selected"].Value == "selected")
                {
                    //matched. Get the next guy 
                    next_page = page_select_node.ChildNodes[page_select_node.ChildNodes.IndexOf(option_node) + 2];
                    break;
                }
            }

            if(next_page == null)
            {
                //nothing was found, something is wrong or current selectd page was the last one.
                return false;
            }

            //set the url to the next page. (BUG CHECK)
            current_url = next_page.Attributes["value"].Value;

            //Done parsing the temp file, delete it.
            File.Delete(temp_html);

            //everything is good.
            return true;
        }

        public override string get_url()
        {
            return current_url;
        }

        public override string get_image_url()
        {
            //parse the html file and get the picture url out.

            //Get the current path of the software (for a temp html file)
            var current_path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string temp_html = current_path + "temp_html.html";

            //download the webpage and save as HTML.
            WebClient my_client = new WebClient();
            my_client.Encoding = encoding_type;
            my_client.DownloadFile(current_url, temp_html);

            //Done with downloading, dispose the WebClient
            my_client.Dispose();

            //Load up the temp html file.
            HtmlDocument my_doc = new HtmlDocument();
            my_doc.Load(temp_html);

            //Batoto use the <img id="comic_page" ... > to hold the source.
            //Search among all the img tags for the correct node.
            HtmlNode comic_node = null;

            foreach (HtmlNode img_node in my_doc.DocumentNode.SelectNodes("//img"))
            {
                //make sure the attribute id is valid.
                if(!img_node.Attributes.Contains("id"))
                {
                    continue;
                }

                if(img_node.Attributes["id"].Value == "comic_page")
                {
                    //found it.
                    comic_node = img_node;
                    break;
                }
            }

            if (comic_node == null)
            {
                //null still, probably error.
                return string.Empty;
            }

            //if reach here, mean the file source node was found.
            //pulling out the src file url and return it.
            string src =  comic_node.Attributes["src"].Value;

            File.Delete(temp_html);

            get_file_name(src);

            return src;
        }

        #endregion

    }
}
