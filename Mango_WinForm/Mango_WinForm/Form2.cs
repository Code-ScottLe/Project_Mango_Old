using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

namespace Mango_WinForm
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            textBox3.Text = "Version " + Assembly.GetExecutingAssembly().GetName().Version.ToString();

            //Initalize the link to Twitter
            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = "https://twitter.com/CodeScottLe";
            Twitter_linkLabel.Links.Add(link);
        }

        private void Twitter_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Send the URL to the OS
            Process.Start(e.Link.LinkData.ToString());
        }
    }
}
