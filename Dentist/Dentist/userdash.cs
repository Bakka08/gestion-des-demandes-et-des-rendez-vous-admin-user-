using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace Dentist
{
    public partial class userdash : Form
    {
        string parametres = "SERVER=127.0.0.1; DATABASE=dentist; UID=root; PASSWORD=";
        private MySqlConnection maconnexion;
        
        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='dentist';username=root;password=");

        MySqlDataAdapter adapter,adapter2;

        DataTable table = new DataTable();
        DataTable table2 = new DataTable();
        DataTable table5 = new DataTable();
        DataTable dataTable = new DataTable();
        int currRowIndex;
        string id;
        string nom;
        string prenom;
        string email;
        string age;
        string telephone;
        string adresse;
        string mdp;

        public userdash(string idd )
        {
            this.id=idd;
            InitializeComponent();
            loadsession(id);
            loaddemande(id);
            
           
        }

        private void loadrdv(string d)
        {

            adapter = new MySqlDataAdapter("SELECT * FROM `rdv` WHERE `patient_id` = '" + d + "'", connection); ;
            adapter.Fill(table5);


            int i3;
            String[] myArray5 = new String[10];
            foreach (DataRow dataRow2 in table5.Rows)
            {
                 i3 = 0;
                foreach (var item in dataRow2.ItemArray)
                {
                    myArray5[i3] = item.ToString();
                    i3++;
                }
                string time = myArray5[6] + ":" + myArray5[7];
                dentiste.Text = myArray5[3];
                assi.Text = myArray5[4];
                date.Text= myArray5[5];
                A.Text = time;
                periode.Text= myArray5[8]+" min";


            }

        }
        private void loaddemande(string r)
        {
            adapter2 = new MySqlDataAdapter("SELECT * FROM `demande` WHERE `patient_id` = '" + r + "'", connection); ;
            adapter2.Fill(table2);


            int i2;
            String[] myArray2 = new String[8];
            foreach (DataRow dataRow in table2.Rows)
            {
                i2 = 0;
                foreach (var item in dataRow.ItemArray)
                {
                    myArray2[i2] = item.ToString();
                    i2++;
                }

                if (myArray2[1] == r)
                {
                Etatt.Text = myArray2[2];
                date11.Text = myArray2[3];
                date22.Text = myArray2[4];
                    if(Etatt.Text == "annuler" )
                    {
                        DS.Text = "Supprimer";
                        DS.FillColor = Color.Red;
                        Etatt.ForeColor = Color.Red;

                    }else if ( Etatt.Text == "dans l'attente")
                    {
                        DS.Text = "Annuler";
                        DS.ForeColor= Color.Black;
                        DS.FillColor = Color.Yellow;
                        Etatt.ForeColor = Color.Yellow;


                    }else
                    {
                        DS.Text = "Active";
                        DS.ForeColor = Color.Black;
                        DS.FillColor = Color.Green;
                        Etatt.ForeColor = Color.Green;
                        DS.Enabled = false;
                        loadrdv(id);

                    }


                    




                }
                else
                {

                    DS.Text = "Demander";
                    DS.FillColor = Color.Aqua;
                }
                
              



            }
        }





        private void loadsession(string d )
        {
            adapter = new MySqlDataAdapter("SELECT * FROM `patient` WHERE `id` = '" + d+ "'", connection);;
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
                    email = myArray[3];
                    age = myArray[4];
                    telephone = myArray[5];
                    adresse = myArray[6];
                mdp = myArray[7];
                guna2HtmlLabel3.Text =  "Bonjour "+nom + " " + prenom;
                X1.Text=nom; X2.Text=prenom;
                X3.Text=email;X4.Text=age;
                X5.Text=telephone;X6.Text=adresse;



                }

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

        private void userdash_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel13_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
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

        private void ModifierBtn_Click(object sender, EventArgs e)
        {
            updateuser a = new updateuser(id,nom,prenom,email,age,telephone,adresse);
            a.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DateTime intime = Convert.ToDateTime(date1.Text);
            DateTime outtime = Convert.ToDateTime(date2.Text);

            if (outtime >= intime)
            {

                string test = "Demander";
                string r = DS.Text;
                if (r.Equals(test) == true)
                {
                    maconnexion = new MySqlConnection(parametres);
                    maconnexion.Open();
                    MySqlCommand cmd = maconnexion.CreateCommand();
                    cmd.CommandText = "INSERT INTO demande (id, patient_id,etat ,date1, date2) VALUES (@id, @patient_id,@etat,@date1 , @date2 )";
                    cmd.Parameters.AddWithValue("@id", "null");
                    cmd.Parameters.AddWithValue("@patient_id", id);
                    cmd.Parameters.AddWithValue("@etat", "dans l'attente");
                    cmd.Parameters.AddWithValue("@date1", date1.Text);
                    cmd.Parameters.AddWithValue("@date2", date2.Text);
                    cmd.ExecuteNonQuery();
                    maconnexion.Close();

                    userdash a = new userdash(id);
                    a.Show();
                    this.Hide();

                }
                else
                {


                    DialogResult dialogDelete = MessageBox.Show("voulez-vous vraiment supprimer cette demande", "Supprimer la demande", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dialogDelete == DialogResult.OK)
                    {

                        maconnexion = new MySqlConnection(parametres);
                        maconnexion.Open();
                        MySqlCommand cmd = maconnexion.CreateCommand();
                        cmd.CommandText = "DELETE FROM demande WHERE patient_id=" + id;
                        cmd.ExecuteNonQuery();
                        maconnexion.Close();

                        userdash a = new userdash(id);
                        a.Show();
                        this.Hide();
                    }

                }

            }
            else { MessageBox.Show("error time "); }

            
        }

        private void guna2HtmlLabel15_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel12_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

            DialogResult dialogDelete = MessageBox.Show("voulez-vous vraiment Quitter", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogDelete == DialogResult.OK)
            {
;
             

                Form1 a = new Form1();
                a.Show();
                this.Hide();
            }


        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
