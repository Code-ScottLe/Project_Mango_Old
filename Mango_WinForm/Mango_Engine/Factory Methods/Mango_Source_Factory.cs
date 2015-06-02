using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango_Engine
{
    public class Mango_Source_Factory
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
        public Mango_Source get_new(string source_name, string source_url)
        {
            /*Return back the correct instance of the corresponding source type (sync)*/
            Mango_Source source = null;

            switch(source_name)
            {
                case "Batoto":
                    source = new BatotoMango_Source(source_url);
                    break;
                case "Fakku":
                    source = new FakkuMango_Source(source_url);
                    break;
                case "MangaHere":
                    source = new MangaHereMango_Source(source_url);
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

        public async Task<Mango_Source> get_new_Async(string source_name, string source_url)
        {
            /*Return back the correct instance of the corresponding source type (async)*/
            Mango_Source source = null;

            switch (source_name)
            {
                case "Batoto":
                    source = new BatotoMango_Source(source_url);
                    break;
                case "Fakku":
                    source = new FakkuMango_Source(source_url);
                    break;

                case "MangaHere":
                    source = new MangaHereMango_Source(source_url);
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
