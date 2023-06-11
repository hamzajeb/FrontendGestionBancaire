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
    public partial class ReleveCompte : Form
    {
        private HttpClient httpClient;
        private string accountId;
        public ReleveCompte(string accountId)
        {
            InitializeComponent();
            this.accountId = accountId;
            label2.Text = accountId;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44396/");
            getAllMvtOfCompte();
        }

        public async void getAllMvtOfCompte()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"api/Comptes/mouvements/{this.accountId}");
                response.EnsureSuccessStatusCode();

                using (HttpContent content = response.Content)
                {

                    string data = await content.ReadAsStringAsync();
                    List<Models.Mouvement> mouvements = JsonConvert.DeserializeObject<List<Models.Mouvement>>(data);
                    DataTable table = new DataTable();
                    table.Columns.Add("Id");
                    table.Columns.Add("Id_Compte");
                    table.Columns.Add("Id_Compte_destinataire");
                    table.Columns.Add("type_mouvement");
                    table.Columns.Add("montant");
                    table.Columns.Add("date");
                    foreach (Models.Mouvement mvt in mouvements)
                    {
                        table.Rows.Add(mvt.ID_mouvement, mvt.ID_compte, mvt.ID_compte_destinataire,mvt.Type_mouvement,mvt.Montant,mvt.Date);
                    }
                    dataGridView1.DataSource = null;
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();

                    // Assign the DataTable as the data source for the DataGridView
                    dataGridView1.DataSource = table;




                }
            }

            catch (Exception ex)
            {
                // Gérer les erreurs de requête
                MessageBox.Show("Une erreur s'est produite lors de la récupération des comptes : " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ReleveCompte_Load(object sender, EventArgs e)
        {

        }
    }
}
