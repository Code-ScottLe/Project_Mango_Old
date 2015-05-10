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

            Mango_Downloader my_downloader = new Mango_Downloader(my_source, "D:\\Test\\");

            my_downloader.start();
        }
    }
}
