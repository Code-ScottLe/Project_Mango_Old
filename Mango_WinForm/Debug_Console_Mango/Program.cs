using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using HtmlAgilityPack;

namespace Debug_Console_Mango
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            //sample URL:http://bato.to/read/_/306043/d-frag_v9_ch65_by_hot-chocolate-scans
            WebClient my_client = new WebClient();

           //download the sample string.
            var myfile = File.Open("D:\\Test.txt", FileMode.OpenOrCreate);

            my_client.Encoding = Encoding.UTF8;

            var str = my_client.DownloadString("http://bato.to/read/_/306043/d-frag_v9_ch65_by_hot-chocolate-scans");

            StreamWriter my_writer = new StreamWriter(myfile);

            my_writer.Write(str);

            my_writer.Flush();
            my_writer.Close();
           */

            HtmlDocument my_doc = new HtmlDocument();
            my_doc.Load("D:\\Test.txt");

            HtmlNodeCollection select_nodes = my_doc.DocumentNode.SelectNodes("select");

            foreach(HtmlNode select_node in select_nodes)
            {

            }
            

        }
    }
}
