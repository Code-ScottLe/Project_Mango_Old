﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;
using System.IO;

namespace Mango_Engine
{
    public class MangaHereMango_Source : Mango_Source
    {
        #region Fields
        /*Fields*/
        private List<string> _pages;
        private int _current_page_index;
        #endregion

        #region Propeties
        /*Properties*/
        #endregion

        #region Constructor
        /*Constructors*/

        MangaHereMango_Source() : base()
        {
            //default constructor. Call base.
            _source_name = "MangaHere";
            _pages = new List<string>();
            _current_page_index = 0;
        }

        public MangaHereMango_Source(string url_source) : base(url_source)
        {
            //Create a new instace of MangaHereMango_Source, an object represent mangahere.co source for mango.
            _source_name = "MangaHere";
            _pages = new List<string>();
            _current_page_index = 0;
        }
        #endregion

        #region Methods
        /*Methods*/
        public override void init()
        {
            /*Initialize the instance of the batoto source. (synchronous )*/

            //Create a HttpClient to get the data from the current URL
            HttpClient my_client = new HttpClient();

            //set the timeout of the client (30 secs)
            my_client.Timeout = new TimeSpan(0, 0, 30);

            try
            {
                /*Getting the Response as well as the stream to the file in the background.*/
                var get_asynced_task = my_client.GetAsync(current_url);         //Possible Exception Throw.
                var get_stream_asynced_task = my_client.GetStreamAsync(current_url);

                //Verify that the web has reponded.
                if (!get_asynced_task.IsCompleted)
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
                if (!get_stream_asynced_task.IsCompleted)
                {
                    get_stream_asynced_task.Wait();     //Force wait if not yet done.                               
                }

                if (get_stream_asynced_task.IsFaulted)
                {
                    //Exeption was thrown inside the task.
                    throw get_stream_asynced_task.Exception;
                }

                /*Load up the source stream as HTML*/
                HtmlDocument my_doc = new HtmlDocument();
                my_doc.Load(get_stream_asynced_task.Result, encoding_type);

                //Search for the SelectBox for the number of pages and all the pages links.
                HtmlNode select_node = my_doc.DocumentNode.SelectSingleNode("//select[@class = \"wid60\"]");

                //Get all the Options nodes inside the select node and extract the links between them.
                foreach (HtmlNode option_node in select_node.SelectNodes("option"))
                {
                    _pages.Add(option_node.Attributes["value"].Value);
                }

                //Set the total of pages.
                total_pages = _pages.Count;

                //Done with setting. Close the client
                my_client.Dispose();
            }

            catch (Exception ae)
            {
                throw new MangoException("Initalize failed", ae);
            }
            
        }

        public override async Task initAsync()
        {
            /*Initialize the instance of the batoto source. (asynchronous )*/

            //Create a HttpClient to get the data from the current URL
            HttpClient my_client = new HttpClient();

            //set the timeout of the client (30 secs)
            my_client.Timeout = new TimeSpan(0, 0, 30);

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

                //Search for the SelectBox for the number of pages and all the pages links.
                HtmlNode select_node = my_doc.DocumentNode.SelectSingleNode("//select[@class = \"wid60\"]");

                //Get all the Options nodes inside the select node and extract the links between them.
                foreach (HtmlNode option_node in select_node.SelectNodes("option"))
                {
                    _pages.Add(option_node.Attributes["value"].Value);
                }

                //Set the total of pages.
                total_pages = _pages.Count;

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

            if (_current_page_index < _total_pages)
            {
                current_url = _pages[_current_page_index];
                return true;
            }

            else
            {
                return false;
            }
        }

        public override async Task<bool> next_page_Async()
        {
            return await Task.Factory.StartNew<bool>(() => next_page());
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
            HtmlNode comic_node = my_doc.DocumentNode.SelectSingleNode("//img[@id = \"image\"]");         

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

        public override async Task<string> get_image_url_Async()
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
            HtmlNode comic_node = my_doc.DocumentNode.SelectSingleNode("//img[@id = \"image\"]");      

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

        protected override string get_file_name(string src_url)
        {
            //Parse the URl and give back the original file name.
            //Strat: Scan from the bottom up for the last /.
            int last_slash_index = src_url.LastIndexOf('/');
            int last_question_mark = src_url.LastIndexOf('?');

            //create a substr without that last slash
            string filename = src_url.Substring(last_slash_index + 1, last_question_mark - last_slash_index - 1);

            //set that to filename
            _file_name = filename;

            //return a copy of that.
            return filename;
        }

        #endregion
    }
}
