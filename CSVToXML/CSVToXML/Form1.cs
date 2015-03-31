using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace CSVToXML
{
    public partial class frmCSVToXML : Form
    {
        private List<string> headers = new List<string>();
        private int headerCounter = 0;

        //CONSTRUCTORS
        public frmCSVToXML()
        {
            InitializeComponent();

            headers.Add("Player_Name");
            headers.Add("DollarBankroll");
            headers.Add("EuroBankroll");
            headers.Add("DollarMakeup");
            headers.Add("EuroMakeup");
        }

        //METHODS
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var lines = File.ReadAllLines("Players.csv");

                var xml = new XElement("TopElement", lines.Select(line => new XElement("Item", line.Split(',').Select((column, index) => new XElement(headers[headerCounter], column)))));

                xml.Save("Players.xml");
            }
            catch (Exception b)
            {
                throw b;
            }
        }
    }
}
