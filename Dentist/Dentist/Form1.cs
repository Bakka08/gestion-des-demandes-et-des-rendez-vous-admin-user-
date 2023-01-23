using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters;

namespace Dentist
{
    public partial class Form1 : Form
    {
        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='dentist';username=root;password=");

        MySqlDataAdapter adapter;

        DataTable table = new DataTable();
        DataTable dataTable = new DataTable();
        int currRowIndex;

        public Form1()
        {
            InitializeComponent();
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            DialogResult dialogClose = MessageBox.Show("Voulez vous vraiment fermer l'application ?", "Quitter le programme", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogClose == DialogResult.OK)
            {
                Application.Exit();
            }
            else if (dialogClose == DialogResult.Cancel)
            {
                //do something else
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            adapter = new MySqlDataAdapter("SELECT id FROM `patient` WHERE `email` = '" + emailtxt.Text + "' AND `mdp` = '" + mdptxt.Text + "'", connection);
            adapter.Fill(table);

            if (table.Rows.Count <= 0)
            {
                MessageBox.Show("Email Or Password Are Invalid");
                emailtxt.Clear();
                mdptxt.Clear();
            }
            else
            {
                String id;
                int i;
                String[] myArray = new String[1];
                foreach (DataRow dataRow in table.Rows)
                {
                    i = 0;
                    foreach (var item in dataRow.ItemArray)
                    {
                        myArray[i] = item.ToString();
                        i++;
                    }
                    id = myArray[0];
                    
                    userdash a = new userdash(id);
                    this.Hide();
                    a.Show();



                }

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            adminlogin a = new adminlogin();
            this.Hide();
            a.Show();  
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            register a = new register();
            this.Hide();
            a.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
