using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using Mango_Engine;

namespace Debug_Console_Mango
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //sample URL:http://bato.to/read/_/306043/d-frag_v9_ch65_by_hot-chocolate-scans

            BatotoMango_Source my_source = new BatotoMango_Source("http://bato.to/read/_/306043/d-frag_v9_ch65_by_hot-chocolate-scans");

            Console.WriteLine("Current Source: {0} ", my_source.current_url);

            my_source.next_page();

            Console.Write("Next Source: {0}", my_source.current_url);

        }
    }
}
