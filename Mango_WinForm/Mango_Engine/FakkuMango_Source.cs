using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;

namespace Mango_Engine
{
    class FakkuMango_Source : Mango_Source
    {
        /* Represent a Mango Source from Fakku*/

        #region Fields
        /*Fields*/
        #endregion

        #region Properties
        /*Properties*/
        #endregion

        #region Constructor
        /*Constructor*/
        FakkuMango_Source() : base()
        {
            //Default constructor, call base constructor
            _source_name = "Fakku";
        }

        public FakkuMango_Source(string url_source) : base()
        {
            //Create new instance of the FakkuMango_Source with a valid URL link.
            _source_name = "Fakku";
            _url = _base_url = url_source;
        }
        #endregion

        #region Methods
        /*Methods*/

        public override void init()
        {
            /*Initialzie the current instance of the FakkuMango_Source (sync)*/
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

                /*Attemp to find the <div> node which contain the page number*/

                //Find the unique div node that hold 9 columns of content.
                HtmlNode div_node_9col_content = my_doc.DocumentNode.SelectSingleNode("//div[@class=\"nine columns content-right \"]");

                //Find the div node that hold the page numbers data inside the 9 columns of content
                HtmlNode div_node_row_page_number = div_node_9col_content.SelectSingleNode("//div[@class=\"left\" and text()= \"Pages\"]").ParentNode;

                //Get the nodes that contain the numbers of pages
                HtmlNode div_node_page_number = div_node_row_page_number.SelectSingleNode("div[@class=\"right\"]");

                /*The div node will be in this format: <div class = "right"> 22 pages </div>
                 * We will have to clean the string before converting it to number*/

                //WARNING: This will break if the inner text is mistyped.
                string page_number_string = div_node_row_page_number.InnerText.Substring(0,
                    div_node_row_page_number.InnerText.IndexOf(" "));

                Int32.TryParse(page_number_string, out _total_pages);

                //Done reading the number of pages, set the URL to the reading page.
                current_url += "/read#page=1";

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

        }

        public override bool next_page()
        {
            throw new NotImplementedException();
        }

        public override async Task<bool> next_page_Async()
        {

        }

        public override string get_url()
        {
            throw new NotImplementedException();
        }

        public override string get_image_url()
        {
            throw new NotImplementedException();
        }

        public override async Task<string> get_image_url_Async()
        {

        }

        protected override string get_file_name(string src_url)
        {
            return base.get_file_name(src_url);
        }

        #endregion 

    }
}
