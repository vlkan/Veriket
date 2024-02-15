using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Veriket.WinForms.Entity;

namespace Veriket.WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text files | *.txt"; // file types, that will be allowed to upload
            dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                String path = dialog.FileName; // get name of file
                using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open)))
                {
                    List<string> strContent = new List<string>();
                    var list = new List<Log>();

                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        strContent.Add(line);
                        var temp = line.Split("|".ToCharArray()[0]);

                        list.Add(new Log() { Date = temp[0], ComputerName = temp[1], Username = temp[2] });
                    }
                    dataGridView1.DataSource = list;

                    Console.WriteLine(strContent);
                }
            }
        }
    }
}
