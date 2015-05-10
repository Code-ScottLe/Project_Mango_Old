using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Mango_Engine
{
    public interface M_Source
    {
        bool next_page();

        string get_url();
    }

    public abstract class  Mango_Source : M_Source
    {
        //Represent a source for Mango.

        #region Fields
        //Fields
        private string _base_url;
        private string _url;
        private Encoding _encoding_type;
        #endregion

        #region Properties
        //Properties

        public string base_url
        {
            get
            {
                return _base_url;
            }

            set
            {
                _base_url = value;
            }
        }

        public string current_url
        {
            get
            {
                return _url;
            }

            set
            {
                _url = value;
            }
        }

        public Encoding encoding_type
        {
            get
            {
                return _encoding_type;
            }

            set
            {
                _encoding_type = value;
            }
        }
        #endregion

        #region Constructor
        //Constructor
        protected Mango_Source()
        {
            //default constructor, hidden.
        }

        protected Mango_Source(string url_source)
        {
            //Create a new instance of Mango_Source, representing a source for Mango, accept a string of URL.
            _url = _base_url = url_source;
            init();
        }
        #endregion

        #region Methods
        //Methods

        protected virtual void init()
        {
            //Initialize the class.
            //Assuming that the url is not null.

            //Create a WebRequest to request information about the source.
            WebRequest my_request = WebRequest.Create(_base_url);

            //Set an Timeout-limitation. (milisecond)
            my_request.Timeout = 2000;

            //Get the respond back from the URL.
            try
            {
                WebResponse my_response = my_request.GetResponse();

                //if reached here, mean it was able to get the respond back from the service.

                //Read the header.
                WebHeaderCollection my_header = my_response.Headers;

                //Get the Data Encoding.
                //in the header: Content-Type: text/html; charset=UTF-8
                string Content_Type = my_header["Content-Type"];

                string encoding_str = Content_Type.Substring(Content_Type.IndexOf("=") + 1);

                Encoding encode = string_to_encoding(encoding_str);

                //encode was converted. set to encoding type
                _encoding_type = encode;
                
            }
           
            catch(WebException e)
            {
                //Timed out.
            }

        }

        public static Encoding string_to_encoding(string encoding_str)
        {
            if(encoding_str == "UTF-8")
            {
                return Encoding.UTF8;
            }

            else if (encoding_str == "UTF-7")
            {
                return Encoding.UTF7;
            }

            else if (encoding_str == "ASCII")
            {
                return Encoding.ASCII;
            }

            else if (encoding_str == "Unicode")
            {
                return Encoding.Unicode;
            }

            else
            {
                return null;
            }
        }
        abstract public bool next_page();

        abstract public string get_url();
    }

#endregion
}
