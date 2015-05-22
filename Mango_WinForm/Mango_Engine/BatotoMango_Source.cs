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
        private List<string> _pages;
        private int _current_page_index;
        #endregion

        #region Properties
        //Properties
        #endregion

        #region Constructors
        //Constructors
        BatotoMango_Source() : base()
        {
            //default constructor, call base constructor.
            _pages = new List<string>();
            _total_pages = 0;
            _current_page_index = 0;

        }

        public BatotoMango_Source(string url_source) : base()
        {
            //Create a new instace of BatotoMango_Source, an object represent bato.to source for mango.
            _url = _base_url = url_source;
            _pages = new List<string>();
            _total_pages = 0;
            _current_page_index = 0;
            
        }
        #endregion

        #region Methods
        //Methods

        public override void init()
        {
            /*Initialize the instance of the batoto source. (synchronous )*/

            //Create a HttpClient to get the data from the current URL
            HttpClient my_client = new HttpClient();

            //set the timeout of the client (5 secs)
            my_client.Timeout = new TimeSpan(0, 0, 5);

            try
            {
                /*Getting the Response as well as the stream to the file in the background.*/
                var get_asynced_task = my_client.GetAsync(current_url);         //Possible Exception Throw.
                var get_stream_asynced_task = my_client.GetStreamAsync(current_url);

                //Verify that the web has reponded.
                if(!get_asynced_task.IsCompleted)
                {
                    get_asynced_task.Wait();    //Force to wait before further executing.         
                }

                //Verify that the task is not faulted.
                if (get_asynced_task.IsFaulted)
                {
                    //Is faulted, throw the same exception Exception to be handle in the catch code.
                
                    throw get_asynced_task.Exception;
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

                if(get_stream_asynced_task.IsFaulted)
                {
                    //Exeption was thrown inside the task.
                    throw get_stream_asynced_task.Exception;
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

            catch (AggregateException ae)
            {
                throw new MangoException("Initalize failed", ae);
            }
            

        }

        public override async Task initAsync()
        {
            /*Initialize the instance of the batoto source. (asynchronous )*/

            //Create a HttpClient to get the data from the current URL
            HttpClient my_client = new HttpClient();

            //set the timeout of the client (5 secs)
            my_client.Timeout = new TimeSpan(0, 0, 5);

            /*Getting the Response as well as the stream to the file in the background.*/

            try
            {
                HttpResponseMessage source_response = await my_client.GetAsync(current_url);         //Possible Exception Throw.

                /*Get The Data Encoding from the site from the Content-Type (belong in Content Header)*/
                var source_response_header = source_response.Content.Headers;
                string content_type = source_response_header.ContentType.ToString();
                string encoding_str = content_type.Substring(content_type.IndexOf("=") + 1);

                //Set the encoding
                _encoding_type = string_to_encoding(encoding_str);

                /*Get a stream to the Source HTML file.*/
                Stream source_html = await my_client.GetStreamAsync(current_url);

                /*Load up the source stream as HTML*/
                HtmlDocument my_doc = new HtmlDocument();
                my_doc.Load(source_html, encoding_type);

                /*Attempt to search for the page_select combo box, which contains all the files.*/
                //Get all the nodes 
                HtmlNodeCollection select_nodes_collection = my_doc.DocumentNode.SelectNodes("//select");

                HtmlNode page_select_node = null;

                //search among the select boxes for the page_select.
                foreach (HtmlNode select_node in select_nodes_collection)
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
                for (int i = 0; i < numbers_of_pages; i++)
                {
                    HtmlNode page_select_option_node = page_select_option_nodes_collection[i];
                    string page_link = page_select_option_node.Attributes["value"].Value;
                    _pages.Add(page_link);

                }

                //Done with setting. Close the client
                my_client.Dispose();
            }

            catch (Exception ae)
            {
                //Exception was thrown.
                throw new MangoException("Initialize Failed", ae);
            }
                    
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
