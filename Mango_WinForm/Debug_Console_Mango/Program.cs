using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Debug_Console_Mango
{
    class Program
    {
        static void Main(string[] args)
        {
            //sample URL:http://bato.to/read/_/306043/d-frag_v9_ch65_by_hot-chocolate-scans
            WebClient my_client = new WebClient();

            //try to get a response.
            var response = my_client.DownloadString("http://bato.to/read/_/306043/d-frag_v9_ch65_by_hot-chocolate-scans");

            WebHeaderCollection response_encode = my_client.ResponseHeaders;

            /*
            for (int i = 0; i < response_encode.Count;i++)
            {
                Console.Write(response_encode.GetKey(i));
                Console.Write(" :  ");
                Console.WriteLine(response_encode[i]);
            }
             */

            string Content_Type = response_encode["Content-Type"];
            Console.WriteLine(Content_Type);
            Console.WriteLine(Content_Type.IndexOf("charset="));

            string trim = Content_Type.Substring(Content_Type.IndexOf("=") + 1);

            Console.WriteLine(trim);

            

        }
    }
}
