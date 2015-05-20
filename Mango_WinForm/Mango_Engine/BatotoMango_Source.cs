using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Net;
using HtmlAgilityPack;
using System.Net.Http;

namespace Mango_Engine
{
    public class BatotoMango_Source : Mango_Source
    {
        #region Fields
        //Fields
        private int _total_pages;
        private List<string> _pages;
        private int _current_page_index;
        #endregion

        #region Properties
        //Properties
        public int numbers_of_pages
        {
            get
            {
                return _total_pages;
            }

            set
            {
                _total_pages = value;
            }
        }
        #endregion

        #region Constructors
        //Constructors
        BatotoMango_Source()
        {
            //default constructor, do nothing,.
        }

        public BatotoMango_Source(string url_source)
        {
            //Create a new instace of BatotoMango_Source, an object represent bato.to source for mango.
            _url = _base_url = url_source;
            _pages = new List<string>();
            _total_pages = 0;
            _current_page_index = 0;
            this.init();
        }
        #endregion

        #region Methods
        //Methods

        protected override void init()
        {
            /*Initialize the instance of the batoto source. (synchronous )*/

            //Create a HttpClient to get the data from the current URL
            HttpClient my_client = new HttpClient();

            //set the timeout of the client (5 secs)
            my_client.Timeout = new TimeSpan(0, 0, 5);

            /*Getting the Response as well as the stream to the file in the background.*/
            var get_asynced_task = my_client.GetAsync(current_url);         //Possible Exception Throw.
            var get_stream_asynced_task = my_client.GetStreamAsync(current_url);

            //Verify that the web has reponded.
            if(!get_asynced_task.IsCompleted)
            {
                get_asynced_task.Wait();    //Force to wait before further executing.
            }
            
            HttpResponseMessage source_response = get_asynced_task.Result;

            /*Get The Data Encoding from the site from the Content-Type (belong in Content Header)*/
            var source_response_header = source_response.Content.Headers;
            string content_type = source_response_header.ContentType.ToString();
            string encoding_str = content_type.Substring(content_type.IndexOf("=") + 1);

            //Set the encoding
            _encoding_type = string_to_encoding(encoding_str);

            /*Get a stream to the Source HTML file.*/
            //Verify that the get stream task has been done.
            if(!get_stream_asynced_task.IsCompleted)
            {
                get_stream_asynced_task.Wait();     //Force wait if not yet done.
            }

            /*Load up the source stream as HTML*/
            HtmlDocument my_doc = new HtmlDocument();
            my_doc.Load(get_stream_asynced_task.Result, encoding_type);

            /*Attempt to search for the page_select combo box, which contains all the files.*/
            //Get all the nodes 
            HtmlNodeCollection select_nodes_collection = my_doc.DocumentNode.SelectNodes("//select");

            HtmlNode page_select_node = null;

            //search among the select boxes for the page_select.
            foreach(HtmlNode select_node in select_nodes_collection)
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

            /*Got the page_select node, get the links witth the total pages number*/
            HtmlNodeCollection page_select_option_nodes_collection = page_select_node.SelectNodes("option");

            //set the number of pages.
            numbers_of_pages = page_select_option_nodes_collection.Count;

            //Add in the links (inside the attribute "value")
            for(int i = 0; i < numbers_of_pages;i++)
            {
                HtmlNode page_select_option_node = page_select_option_nodes_collection[i];
                string page_link = page_select_option_node.Attributes["value"].Value;
                _pages.Add(page_link);

            }

            //Done with setting. Close the client
            my_client.Dispose();

        }
        public override bool next_page()
        {
           //get the next link ready.
            _current_page_index++;

            if(_current_page_index < _total_pages)
            {
                current_url = _pages[_current_page_index];
                return true;
            }

            else
            {
                return false;
            }
        }

        public async Task<bool> next_page_Async()
        {
            //modify the URL to get to the next page.

            //Initialize a client for the Html file.
            HttpClient my_client = new HttpClient();
            Stream source_html = await my_client.GetStreamAsync(current_url);
   
            //Load up the temp html file.
            HtmlDocument my_doc = new HtmlDocument();
            my_doc.Load(source_html,encoding_type);

            //Attemp to search for the page_select combo box, which contain all the files.
            /*Example:
             * <select name="page_select" id="page_select" onchange="window.location=this.value;">
             * .... values
             * </select>
             * */
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

            if (page_select_node == null)
            {
                //if it is still null (no values was found), error.
                return false;
            }

            //if reach here, mean the correct node was found.
            HtmlNode next_page = null;

            //Now search through all the options attribute and find the one that is marked as "selected" (which is the current page)
            /*
             * Example:
             * <select name="page_select" id="page_select" onchange="window.location=this.value;">\
             *       <option value="http://bato.to/read/_/306043/d-frag_v9_ch65_by_hot-chocolate-scans/1" selected="selected">page 1</option>
             * </select>
             * 
             * */
            HtmlNodeCollection option_nodes = page_select_node.SelectNodes("option");
            foreach (HtmlNode option_node in option_nodes)
            {
                if (!option_node.Attributes.Contains("selected"))
                {
                    //Not selected. Keep moving.
                    continue;
                }

                if (option_node.Attributes["selected"].Value == "selected")
                {
                    //matched. 
                    if (option_node != option_nodes.Last())
                    {
                        //Get the next guy if only the current node is not the last node.
                        next_page = option_nodes[option_nodes.IndexOf(option_node) + 1];
                    }

                    break;
                }
            }

            if (next_page == null)
            {
                //nothing was found, something is wrong or current selectd page was the last one.
                return false;
            }

            //set the url to the next page. (BUG CHECK)
            current_url = next_page.Attributes["value"].Value;

            //everything is good. Clear out the data and return
            my_client.Dispose();
            source_html.Dispose();
            return true;
        }

        public override string get_url()
        {
            return current_url;
        }

        public override string get_image_url()
        {
            /*parse the html file and get the picture url out.*/

            /*Get a stream to the Source HTML.*/
            HttpClient my_client = new HttpClient();
            var get_stream_asynced_task = my_client.GetStreamAsync(current_url);
            get_stream_asynced_task.Wait();

            /*Load up the temp html file.*/
            HtmlDocument my_doc = new HtmlDocument();
            my_doc.Load(get_stream_asynced_task.Result, encoding_type);

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
            get_file_name(src);

            //Release all the resource of the client
            my_client.Dispose();

            return src;
        }

        public async Task<string> get_image_url_Async()
        {
            //parse the html file and get the picture url out
            //Initalize the HttpClient for the source html.
            HttpClient my_client = new HttpClient();

            //Getting a stream to the source html file.
            Stream source_html = await my_client.GetStreamAsync(current_url);

            //Load up the html file.
            HtmlDocument my_doc = new HtmlDocument();
            my_doc.Load(source_html, encoding_type);

            //Batoto use the <img id="comic_page" ... > to hold the source.
            //Search among all the img tags for the correct node.
            HtmlNode comic_node = null;

            foreach (HtmlNode img_node in my_doc.DocumentNode.SelectNodes("//img"))
            {
                //make sure the attribute id is valid.
                if (!img_node.Attributes.Contains("id"))
                {
                    continue;
                }

                if (img_node.Attributes["id"].Value == "comic_page")
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
            string src = comic_node.Attributes["src"].Value;
            get_file_name(src);

            //Release all the resource of the client
            my_client.Dispose();

            return src;
        }
        #endregion

    }
}
