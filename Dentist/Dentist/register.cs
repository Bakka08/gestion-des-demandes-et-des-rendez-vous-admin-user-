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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Dentist
{
    public partial class register : Form
    {
        string parametres = "SERVER=127.0.0.1; DATABASE=dentist; UID=root; PASSWORD=";
        private MySqlConnection maconnexion;
        DataTable dataTable = new DataTable();
        int currRowIndex;
        public register()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
          
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
          
        }

        private void guna2CircleButton2_Click_1(object sender, EventArgs e)
        {

            string test = "";
            if (nomtxt.Text == "" || prenomtxt.Text == "" || emailtxt.Text == "" || agetxt.Text == "" || telephonetxt.Text == "" || adressetxt.Text == "")
            {
                MessageBox.Show("error");
                register a = new register();
                a.Show();
                this.Hide();


            }
            else
            {
                maconnexion = new MySqlConnection(parametres);
                maconnexion.Open();
                MySqlCommand cmd = maconnexion.CreateCommand();
                cmd.CommandText = "INSERT INTO patient (id, nom,prenom ,email, age, telephone , adresse , mdp) VALUES (@id, @nom,@prenom,@email , @age, @telephone , @adresse , @mdp )";
                cmd.Parameters.AddWithValue("@id", "null");
                cmd.Parameters.AddWithValue("@nom", nomtxt.Text);
                cmd.Parameters.AddWithValue("prenom", prenomtxt.Text);
                cmd.Parameters.AddWithValue("email", emailtxt.Text);
                cmd.Parameters.AddWithValue("@age", agetxt.Text);
                cmd.Parameters.AddWithValue("@telephone", telephonetxt.Text);
                cmd.Parameters.AddWithValue("@adresse", adressetxt.Text);
                cmd.Parameters.AddWithValue("@mdp", mdptxt.Text);


                cmd.ExecuteNonQuery();
                maconnexion.Close();

                Form1 a = new Form1();
                this.Hide();
                a.Show();

            }
           
        }

        private void label8_Click(object sender, EventArgs e)
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

        private void guna2CircleButton1_Click_1(object sender, EventArgs e)
        {
            Form1 a = new Form1();
            this.Hide();
            a.Show();
        }
    }
}
