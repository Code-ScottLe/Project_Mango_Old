using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }

        public override string get_url()
        {
            throw new NotImplementedException();
        }
    }
}
