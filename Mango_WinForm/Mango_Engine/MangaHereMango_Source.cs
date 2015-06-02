using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango_Engine
{
    public class MangaHereMango_Source : Mango_Source
    {
        #region Fields
        /*Fields*/
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
        }

        public MangaHereMango_Source(string url_source) : base(url_source)
        {
            //Create a new instace of MangaHereMango_Source, an object represent mangahere.co source for mango.
            _source_name = "MangaHere";
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
            throw new NotImplementedException();
        }

        public override string get_image_url()
        {
            throw new NotImplementedException();
        }

        public override Task<string> get_image_url_Async()
        {
            throw new NotImplementedException();
        }

        public override string get_url()
        {
            return current_url;
        }

        #endregion
    }
}
