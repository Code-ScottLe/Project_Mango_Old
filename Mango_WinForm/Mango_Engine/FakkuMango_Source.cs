using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        public FakkuMango_Source(string url_source) : base()
        {
            //Create new instance of the FakkuMango_Source with a valid URL link.
            _url = url_source;
        }
        #endregion

        #region Methods
        /*Methods*/

        public override void init()
        {
            base.init();
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
