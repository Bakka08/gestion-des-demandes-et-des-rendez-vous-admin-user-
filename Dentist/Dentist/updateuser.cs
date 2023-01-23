using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
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
    public partial class updateuser : Form
    {
        string parametres = "SERVER=127.0.0.1; DATABASE=dentist; UID=root; PASSWORD=";
        private MySqlConnection maconnexion;
        DataTable dataTable = new DataTable();
        int currRowIndex;
        String nom; string prenom; string email; string age; string telephone; string adresse; 
        int id;
        string d;
        public updateuser(String id,String nom, string prenom, string email, string age , string telephone ,string adresse  )
        {
            this.d= id;
            this.id= Convert.ToInt32(id); ;
            this.nom= nom;
            this.prenom= prenom;
            this.email= email;
            this.age= age;
            this.telephone= telephone;
            this.adresse= adresse;
            
            InitializeComponent();
            guna2HtmlLabel3.Text = "Bonjour " + nom + " " + prenom;
            nomtxt.Text= nom;
            prenomtxt.Text= prenom;
            emailtxt.Text= email;
            agetxt.Text= age;
            telephonetxt.Text= telephone;
            adressetxt.Text= adresse;


        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void updateuser_Load(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            DialogResult dialogUpdate = MessageBox.Show("voulez-vous vraiment modifier les informations  ", "Modifier une appartement", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogUpdate == DialogResult.OK)
            {

                if (emailtxt.Text == "" || nomtxt.Text == "" || prenomtxt.Text == "" || telephonetxt.Text == "" || adressetxt.Text == "" || agetxt.Text == "" )
                {
                    DialogResult dialogClose = MessageBox.Show("Veuillez renseigner tous les champs", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {

                    maconnexion = new MySqlConnection(parametres);
                    maconnexion.Open();

                    MySqlCommand cmd = maconnexion.CreateCommand();
                    cmd.CommandText = "UPDATE patient SET nom = @nom,prenom = @prenom , email = @email ,age = @age ,telephone = @telephone, adresse= @adresse WHERE id=" + id;
                    cmd.Parameters.AddWithValue("@nom", nomtxt.Text);
                    cmd.Parameters.AddWithValue("@prenom", prenomtxt.Text);
                    cmd.Parameters.AddWithValue("@email", emailtxt.Text);
                    cmd.Parameters.AddWithValue("@age", agetxt.Text);
                    cmd.Parameters.AddWithValue("@telephone", telephonetxt.Text);
                    cmd.Parameters.AddWithValue("@adresse", adressetxt.Text);

                    cmd.ExecuteNonQuery();
                    maconnexion.Close();

                    userdash a = new userdash(d);
                    a.Show();
                    this.Hide();
                    
                }
            }
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
           

                userdash a = new userdash(d);
                a.Show();
                this.Hide();
            }

        }
    }

