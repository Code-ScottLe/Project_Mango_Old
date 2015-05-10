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
        //Fields

        //Properties

        //Constructors
        BatotoMango_Source()
        {
            //default constructor, do nothing,.
        }

        public BatotoMango_Source(string url_source) : base (url_source)
        {
            //Create a new instace of BatotoMango_Source, an object represent bato.to source for mango.
        }

        //Methods
        public override bool next_page()
        {
            //modify the URL to get to the next page.

            //Get the current path of the software (for a temp html file)
            var current_path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string temp_html = current_path + "temp_html.html";

            //download the webpage and save as HTML.
            WebClient my_client = new WebClient();
            my_client.Encoding = encoding_type;
            my_client.DownloadFile(url, temp_html);

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
                    next_page = option_node.NextSibling;
                    break;
                }
            }

            if(next_page == null)
            {
                //nothing was found, something is wrong or current selectd page was the last one.
                return false;
            }

            //set the url to the next page.
            url = next_page.Attributes["value"].Value;

            //everything is good.
            return true;
        }

        public override string get_url()
        {
            return url;
        }
    }
}
