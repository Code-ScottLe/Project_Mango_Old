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

        string get_image_url();
    }

    public abstract class  Mango_Source : M_Source
    {
        //Represent a source for Mango.

        #region Fields
        //Fields
        protected int _total_pages;
        protected string _base_url;
        protected string _url;
        protected string _file_name;
        protected Encoding _encoding_type;
        #endregion

        #region Properties
        //Properties

        public string base_url
        {
            get
            {
                return _base_url;
            }

            protected set
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

            protected set
            {
                _url = value;
            }
        }

        public string current_file_name
        {
            get
            {
                return _file_name;
            }

            protected set
            {
                _file_name = value;
            }
        }

        public int numbers_of_pages
        {
            get
            {
                return _total_pages;
            }

            protected set
            {
                _total_pages = value;
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
            _url = string.Empty;
            _base_url = string.Empty;
            _file_name = string.Empty;
            _encoding_type = Encoding.UTF8;     //default encoding.
        }

        protected Mango_Source(string url_source) : this()
        {
            //Create a new instance of Mango_Source, representing a source for Mango, accept a string of URL.
            _url = _base_url = url_source;
            
        }
        #endregion

        #region Methods
        //Methods

        public virtual void init()
        {
            //Initialize the class.
            //Assuming that the url is not null.

            //Create a WebRequest to request information about the source.
            HttpWebRequest my_request = (HttpWebRequest)WebRequest.Create(_base_url);

            //Set an Timeout-limitation. (milisecond)
            my_request.Timeout = 5000;

            //Get the respond back from the URL.
  
            HttpWebResponse my_response = (HttpWebResponse)my_request.GetResponse();    

            //if reached here, mean it was able to get the respond back from the service.    


            //Get the Data Encoding.    
            //in the header: Content-Type: text/html; charset=UTF-8    
            string Content_Type = my_response.ContentType;    

            string encoding_str = Content_Type.Substring(Content_Type.IndexOf("=") + 1);    

            Encoding encode = string_to_encoding(encoding_str);    

            //encode was converted. set to encoding type    
            _encoding_type = encode;    

            //Done with the response.Release the connection.
            my_response.Close();

        }

        public virtual async Task initAsync()
        {
            //Initialize the class.
            //Assuming that the url is not null.

            //Create a WebRequest to request information about the source.
            HttpWebRequest my_request = (HttpWebRequest)WebRequest.Create(_base_url);

            //Set an Timeout-limitation. (milisecond)
            my_request.Timeout = 5000;

            //Get the respond back from the URL.
            HttpWebResponse my_response =  (HttpWebResponse)(await my_request.GetResponseAsync());

            //if reached here, mean it was able to get the respond back from the service.    


            //Get the Data Encoding.    
            //in the header: Content-Type: text/html; charset=UTF-8    
            string Content_Type = my_response.ContentType;

            string encoding_str = Content_Type.Substring(Content_Type.IndexOf("=") + 1);

            Encoding encode = string_to_encoding(encoding_str);

            //encode was converted. set to encoding type    
            _encoding_type = encode;

            //Done with the response.Release the connection.
            my_response.Close();

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

        abstract public string get_image_url();

        protected virtual string get_file_name(string src_url)
        {
           //Parse the URl and give back the original file name.
            //Strat: Scan from the bottom up for the last /.
            int last_slash_index = src_url.LastIndexOf('/');

            //create a substr without that last slash
            string filename = src_url.Substring(last_slash_index + 1);

            //set that to filename
            _file_name = filename;

            //return a copy of that.
            return filename;
        }
    }

#endregion
}
