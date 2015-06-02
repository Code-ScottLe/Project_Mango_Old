using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango_Engine
{
    public class MangoSourceFactory
    {
        #region Fields
        /*Fields*/
        #endregion

        #region Property
        /*Property*/
        #endregion

        #region Constructor
        /*Constructors*/
        #endregion

        #region Methods
        /*Methods*/
        public MangoSource get_new(string source_name, string source_url)
        {
            /*Return back the correct instance of the corresponding source type (sync)*/
            MangoSource source = null;

            switch(source_name)
            {
                case "Batoto":
                    source = new BatotoMangoSource(source_url);
                    break;
                case "Fakku":
                    source = new FakkuMangoSource(source_url);
                    break;
                case "MangaHere":
                    source = new MangaHereMangoSource(source_url);
                    break;                 
            }

            if(source == null)
            {
                throw new MangoException("Can't create an instance of  Mango_Source!");
            }

            //Initalize the source (synced)
            source.init();

            //Done, return the source
            return source;
        }

        public async Task<MangoSource> get_new_Async(string source_name, string source_url)
        {
            /*Return back the correct instance of the corresponding source type (async)*/
            MangoSource source = null;

            switch (source_name)
            {
                case "Batoto":
                    source = new BatotoMangoSource(source_url);
                    break;
                case "Fakku":
                    source = new FakkuMangoSource(source_url);
                    break;

                case "MangaHere":
                    source = new MangaHereMangoSource(source_url);
                    break;            
            }

            if (source == null)
            {
                throw new MangoException("Can't create an instance of  Mango_Source!");
            }

            //Initalize the source (synced)
            await source.initAsync();

            //Done, return the source
            return source;
        }
        #endregion
    }
}
