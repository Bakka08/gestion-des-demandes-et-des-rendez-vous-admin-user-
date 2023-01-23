using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Dentist
{
    public partial class admindash : Form
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

        string ALT;
        int currRowIndex;
        string id;
        string nom;
        string prenom;
        string email;
        string specialite;
        string telephone;
        string mdp;
        public admindash(string id )
        {
            this.id = id;
            InitializeComponent();
            loadsession(id);
            loaddemande();
            loaddentist();
            loaddassi();

        }
        private void loaddassi()
        {
            string query = "select * from admin where specialite = 'Assistance dentaire' LIMIT 20;";
            MySqlConnection conDataBase = new MySqlConnection(parametres);
            MySqlCommand cmdDataBase = new MySqlCommand(query, conDataBase);
            MySqlDataReader myReader;
           
            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    string aid = myReader.GetString("nom");
                    string u = myReader.GetString("prenom");
                    assicombo.Items.Add(aid+" "+u);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void loaddentist()
        {
            string query = "select * from admin where specialite = 'dentiste' LIMIT 20;";
            MySqlConnection conDataBase = new MySqlConnection(parametres);
            MySqlCommand cmdDataBase = new MySqlCommand(query, conDataBase);
            MySqlDataReader myReader;
            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    string aid = myReader.GetString("nom");
                    string u = myReader.GetString("prenom");
                    dentistcombo.Items.Add(aid+" "+u);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
        private void loaddemande()
        {
            
            listedemande.Rows.Clear();

            maconnexion = new MySqlConnection(parametres);
            maconnexion.Open();
            string request = "select *  from demande";

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
                listedemande.Rows.Add(myArray[0], myArray[1], myArray2[1], myArray2[2], myArray2[3], myArray2[4], myArray2[5], myArray2[6], myArray[2], myArray[3], myArray[4]);

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

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void listedemande_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.listedemande.Rows[e.RowIndex];

            idtxt.Text = row.Cells[0].Value.ToString() ;
            patientidtxt.Text = row.Cells[1].Value.ToString() ;
            entretxt.Text = row.Cells[9].Value.ToString() ;
            ettxt.Text  = row.Cells[10].Value.ToString() ;


        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogUpdate = MessageBox.Show("voulez-vous vraiment annuler cette demande ", "annuler la demande", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogUpdate == DialogResult.OK)
            {

                if (idtxt.Text == "#" )
                {
                    DialogResult dialogClose = MessageBox.Show("Veuillez selectionner une demande", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {

                    maconnexion = new MySqlConnection(parametres);
                    maconnexion.Open();

                    MySqlCommand cmd = maconnexion.CreateCommand();
                    cmd.CommandText = "UPDATE demande SET  etat= @etat WHERE id=" + idtxt.Text;
                    cmd.Parameters.AddWithValue("@etat", "annuler");
                    

                    cmd.ExecuteNonQuery();
                    maconnexion.Close();

                    admindash a = new admindash(id);
                    a.Show();
                    this.Hide();




                }
            }
        }

        private void ModifierBtn_Click(object sender, EventArgs e)
        {
           

                if (idtxt.Text == "#" )
                {
                    DialogResult dialogClose = MessageBox.Show("Veuillez selectionner une demande", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {
                    DateTime intime = Convert.ToDateTime(entretxt.Text);
                    DateTime outtime = Convert.ToDateTime(ettxt.Text);
                    DateTime Stime = Convert.ToDateTime(date1.Text);
                    if (outtime>Stime && Stime>intime)
                    {
                    maconnexion = new MySqlConnection(parametres);
                    maconnexion.Open();
                    MySqlCommand cmd = maconnexion.CreateCommand();
                    cmd.CommandText = "INSERT INTO rdv (id, patient_id,accepte_par ,dentiste, assis, date , heur,min , periode) VALUES (@id, @patient_id,@accepte_par ,@dentiste, @assis, @date , @heur,@min , @periode)";
                    cmd.Parameters.AddWithValue("@id", "null");
                    cmd.Parameters.AddWithValue("@patient_id", patientidtxt.Text);
                    cmd.Parameters.AddWithValue("accepte_par", nom + " "+prenom);
                    cmd.Parameters.AddWithValue("dentiste", dentistcombo.Text);
                    cmd.Parameters.AddWithValue("assis", assicombo.Text);
                    cmd.Parameters.AddWithValue("@date", date1.Text);
                    cmd.Parameters.AddWithValue("@heur", time1txt.Text);
                    cmd.Parameters.AddWithValue("@min", time2txt.Text);
                    cmd.Parameters.AddWithValue("@periode", periodetxt.Text);
                  


                    cmd.ExecuteNonQuery();
                    maconnexion.Close();

                    maconnexion = new MySqlConnection(parametres);
                    maconnexion.Open();

                    MySqlCommand cmd2 = maconnexion.CreateCommand();
                    cmd2.CommandText = "UPDATE demande SET  etat= @etat WHERE id=" + idtxt.Text;
                    cmd2.Parameters.AddWithValue("@etat", "Active");


                    cmd2.ExecuteNonQuery();
                    maconnexion.Close();

                    admindash a = new admindash(id);
                    a.Show();
                    this.Hide();





                   
                    }
                    else
                    {
                    MessageBox.Show("error time ");





                        

                    }


                    

                    

                    

                }
            }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            rdv a = new rdv(id);
            a.Show();
            this.Hide();



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

        private void admindash_Load(object sender, EventArgs e)
        {

        }

    }
}
