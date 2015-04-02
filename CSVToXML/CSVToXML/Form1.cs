using System;
using System.CodeDom;
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
    public partial class FrmCsvToXml : Form
    {
        //FIELDS
        private FileStream csvFileStream;
        private FileStream xmlFileStream;

        private StreamReader csvStreamReader;

        private List<string> xmlData = new List<string>();
        private string directory;

        //CONSTRUCTORS
        public FrmCsvToXml()
        {
            InitializeComponent();

            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Players|*.csv";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    directory = openFileDialog1.FileName;
                }

                csvFileStream = new FileStream(directory, FileMode.Open, FileAccess.Read);
                xmlFileStream = new FileStream(directory.Replace(".csv", ".xml"), FileMode.Create, FileAccess.Write);

                csvStreamReader = new StreamReader(csvFileStream);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        //METHODS
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string regel = csvStreamReader.ReadLine();

                xmlData.Add("<Players>");

                while (regel != null)
                {
                    int columnCounter = 0;
                    string[] regelSplit = regel.Split(',');

                    xmlData.Add("\t<Player>");
                    foreach (string column in regelSplit)
                    {
                        string columnName;
                        if (columnCounter == 0)
                        {
                            columnName = "Player_Name";
                        }
                        else if (columnCounter == 1)
                        {
                            columnName = "DollarBankroll";
                        }
                        else if (columnCounter == 2)
                        {
                            columnName = "EuroBankroll";
                        }
                        else if (columnCounter == 3)
                        {
                            columnName = "DollarMakeup";
                        }
                        else if (columnCounter == 4)
                        {
                            columnName = "EuroMakeup";
                        }
                        else
                        {
                            columnName = "ERROR";
                        }
                        xmlData.Add("\t\t<" + columnName + ">" + column + "</" + columnName + ">");

                        if (columnCounter < 4)
                        {
                            columnCounter++;
                        }
                        else
                        {
                            columnCounter = 0;
                        }
                    }

                    xmlData.Add("\t</Player>");

                    regel = csvStreamReader.ReadLine();
                }

                xmlData.Add("</Players>");
                File.WriteAllLines(directory.Replace(".csv", ".xml"), xmlData.ToArray());
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                try
                {
                    csvStreamReader.Close();
                }
                catch (Exception exception)
                {

                    throw exception;
                }
                finally
                {
                    try
                    {
                        xmlFileStream.Close();
                        csvFileStream.Close();
                    }
                    catch (Exception exception)
                    {
                        throw exception;
                    }
                }
            }
        }
    }
}
