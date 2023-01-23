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

namespace Dentist
{
    public partial class rdv : Form
    {
        string parametres = "SERVER=127.0.0.1; DATABASE=dentist; UID=root; PASSWORD=";
        private MySqlConnection maconnexion;

        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='dentist';username=root;password=");

        MySqlDataAdapter adapter, adapter2;

        DataTable table = new DataTable();
        DataTable table2 = new DataTable();
        DataTable table3 = new DataTable();
        DataTable dataTable = new DataTable();
        DataTable dataTable2 = new DataTable();
        DataTable dataTable3 = new DataTable();
        string ro;
        int currRowIndex;
        string id;
        string nom;
        string prenom;
        string email;
        string specialite;
        string telephone;
        string mdp;
        public rdv(string idd)
        {
            this.ro = idd;
            InitializeComponent();
            loadsession(idd);
            loadrdv();
        }

        private void loadrdv()
        {
            listedemande.Rows.Clear();

            maconnexion = new MySqlConnection(parametres);
            maconnexion.Open();
            string request = "select *  from rdv";

            MySqlCommand cmd = new MySqlCommand(request, maconnexion);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dataTable);

            int i;
            String[] myArray = new String[10];
            foreach (DataRow dataRow in dataTable.Rows)
            {
                i = 0;
                foreach (var item in dataRow.ItemArray)
                {
                    myArray[i] = item.ToString();
                    i++;
                }



                adapter = new MySqlDataAdapter("SELECT * FROM `patient` WHERE `id` = '" + myArray[1] + "'", connection); ;
                adapter.Fill(dataTable3);

                int i2;
                String[] myArray2 = new String[10];
                foreach (DataRow dataRow2 in dataTable3.Rows)
                {
                    i2 = 0;
                    foreach (var item in dataRow2.ItemArray)
                    {
                        myArray2[i2] = item.ToString();
                        i2++;
                    }



                }
                string R = myArray[6] + ":" + myArray[7];
                listedemande.Rows.Add(myArray[0], myArray[1], myArray2[1], myArray2[2], myArray[2], myArray[3], myArray[4], myArray[5],R , myArray[8]+" min");

            }

        }
        private void loadsession(string d)
        {
            adapter = new MySqlDataAdapter("SELECT * FROM `admin` WHERE `id` = '" + d + "'", connection); ;
            adapter.Fill(table);


            int i;
            String[] myArray = new String[8];
            foreach (DataRow dataRow in table.Rows)
            {
                i = 0;
                foreach (var item in dataRow.ItemArray)
                {
                    myArray[i] = item.ToString();
                    i++;
                }
                nom = myArray[1];
                prenom = myArray[2];
                specialite = myArray[3];
                email = myArray[4];
                telephone = myArray[5];
                mdp = myArray[6];
                session.Text = "Bonjour " + nom + " " + prenom;
                guna2HtmlLabel1.Text = specialite;




            }
        }

        private void rdv_Load(object sender, EventArgs e)
        {

        }

        private void listedemande_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
           admindash a = new admindash(ro);
            a.Show();
            this.Hide();
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
    }
}
