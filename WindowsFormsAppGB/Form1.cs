using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net.Http;

namespace WindowsFormsAppGB
{
    public partial class Form1 : Form
    {
        private HttpClient httpClient;
        private int rowIndex = -1;
        public Form1()
        {
            InitializeComponent();
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44396/");
            getAllComptes();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public async void getAllComptes()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("api/Comptes");
                response.EnsureSuccessStatusCode();

                using (HttpContent content = response.Content)
                {

                    string data = await content.ReadAsStringAsync();
                    List<Models.Compte> comptes = JsonConvert.DeserializeObject<List<Models.Compte>>(data);
                    DataTable table = new DataTable();
                    table.Columns.Add("Id");
                    table.Columns.Add("Nom");
                    table.Columns.Add("Solde");
                    foreach (Models.Compte compte in comptes)
                    {
                        table.Rows.Add(compte.ID_compte, compte.Nom, compte.Solde);
                    }
                    dataGridView1.DataSource = null;
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();

                    // Assign the DataTable as the data source for the DataGridView
                    dataGridView1.DataSource = table;
                    DataGridViewButtonColumn buttonColumn1 = new DataGridViewButtonColumn();
                    buttonColumn1.Name = "Operation";
                    buttonColumn1.Text = "ajouter";
                    buttonColumn1.UseColumnTextForButtonValue = true;

                    // Ajout de la première colonne de boutons à la fin des colonnes existantes
                    dataGridView1.Columns.Add(buttonColumn1);

                    // Création de la deuxième colonne de boutons
                    DataGridViewButtonColumn buttonColumn2 = new DataGridViewButtonColumn();
                    buttonColumn2.Name = "Releve de compte";
                    buttonColumn2.Text = "afficher";
                    buttonColumn2.UseColumnTextForButtonValue = true;

                    // Ajout de la deuxième colonne de boutons à la fin des colonnes existantes
                    dataGridView1.Columns.Add(buttonColumn2);
                    dataGridView1.CellContentClick += (sender, e) =>
                    {
                        // Vérification que le clic est sur une cellule de bouton
                        if (e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                        {
                            // Récupération de l'ID du compte correspondant à la ligne
                            string accountId = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();

                            // Récupération du nom du compte correspondant à la ligne
                            string accountName = dataGridView1.Rows[e.RowIndex].Cells["Nom"].Value.ToString();

                            // Vérification de la colonne du bouton
                            if (dataGridView1.Columns[e.ColumnIndex].Name == "Releve de compte")
                            {
                                // Affichage de l'ID du compte
                                
                                ReleveCompte releveCompte = new ReleveCompte(accountId);
                                releveCompte.Show();
                            }
                            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Operation")
                            {
                             
                                
                                Operation operation = new Operation(accountId, this);
                                operation.Show();
                            }
                        }
                    };


                }
            }

            catch (Exception ex)
            {
                // Gérer les erreurs de requête
                MessageBox.Show("Une erreur s'est produite lors de la récupération des comptes : " + ex.Message);
            }
        }
        public async void getAllComptes2()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("api/Comptes");
                response.EnsureSuccessStatusCode();

                using (HttpContent content = response.Content)
                {

                    string data = await content.ReadAsStringAsync();
                    List<Models.Compte> comptes = JsonConvert.DeserializeObject<List<Models.Compte>>(data);
                    DataTable table = new DataTable();
                    table.Columns.Add("Id");
                    table.Columns.Add("Nom");
                    table.Columns.Add("Solde");
                    foreach (Models.Compte compte in comptes)
                    {
                        table.Rows.Add(compte.ID_compte, compte.Nom, compte.Solde);
                    }
                    dataGridView1.DataSource = null;
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();

                    // Assign the DataTable as the data source for the DataGridView
                    dataGridView1.DataSource = table;
                    DataGridViewButtonColumn buttonColumn1 = new DataGridViewButtonColumn();
                    buttonColumn1.Name = "Operation";
                    buttonColumn1.Text = "ajouter";
                    buttonColumn1.UseColumnTextForButtonValue = true;

                    // Ajout de la première colonne de boutons à la fin des colonnes existantes
                    dataGridView1.Columns.Add(buttonColumn1);

                    // Création de la deuxième colonne de boutons
                    DataGridViewButtonColumn buttonColumn2 = new DataGridViewButtonColumn();
                    buttonColumn2.Name = "Releve de compte";
                    buttonColumn2.Text = "afficher";
                    buttonColumn2.UseColumnTextForButtonValue = true;

                    // Ajout de la deuxième colonne de boutons à la fin des colonnes existantes
                    dataGridView1.Columns.Add(buttonColumn2);
                    
                    


                }
            }

            catch (Exception ex)
            {
                // Gérer les erreurs de requête
                MessageBox.Show("Une erreur s'est produite lors de la récupération des comptes : " + ex.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
   
        }


        private async void button2_Click(object sender, EventArgs e)
        {
            int idCompte = int.Parse(dataGridView1.Rows[rowIndex].Cells["id"].Value.ToString());
            if (rowIndex < 0)
            {
                MessageBox.Show("please choose article to delete");
                return;
            }
            try
            {
                HttpResponseMessage response = await httpClient.DeleteAsync($"api/Comptes/{idCompte}");
                response.EnsureSuccessStatusCode();

                // Le compte a été supprimé avec succès
                MessageBox.Show("Le compte a été supprimé avec succès.");
                textBox1.Text = textBox2.Text = null;
                getAllComptes2();

            }
            catch (Exception ex)
            {
                // Gérer les erreurs de requête
                MessageBox.Show("Une erreur s'est produite lors de la suppression du compte : " + ex.Message);
            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                rowIndex = e.RowIndex;
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["nom"].Value.ToString();
                string valeurCellule = dataGridView1.Rows[e.RowIndex].Cells["nom"].Value.ToString();
                textBox2.Text = (dataGridView1.Rows[e.RowIndex].Cells["solde"].Value.ToString()) ;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Models.Compte nouveauCompte = new Models.Compte
            {
                Nom = textBox1.Text,
                Solde =decimal.Parse(textBox2.Text)
            };
            try
            {
                string json = JsonConvert.SerializeObject(nouveauCompte);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync("api/Comptes", content);
                response.EnsureSuccessStatusCode();

                // Le compte a été ajouté avec succès
                MessageBox.Show("Le compte a été ajouté avec succès.");

                textBox1.Text = textBox2.Text = null;
                getAllComptes2();
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de requête
                MessageBox.Show("Une erreur s'est produite lors de l'ajout du compte : " + ex.Message);
            }
        }
    }
}
