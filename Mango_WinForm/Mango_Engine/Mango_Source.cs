using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango_Engine
{
    interface Mango_Source
    {
        bool next_page();

        string get_url();
    }
}
