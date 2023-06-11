using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Net.Http;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WindowsFormsAppGB
{
    public partial class Operation : Form
    {
        private string accountId;
        private HttpClient httpClient;
        private Form1 form1;
        public Operation(string accountId, Form1 form)
        {
            form1 = form;
            InitializeComponent();
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44396/");
            textBox2.Enabled = false;
            this.accountId = accountId;
            label7.Text = accountId;
        }
        


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void Operation_Load(object sender, EventArgs e)
        {

        }

private void checkBox1_CheckedChanged(object sender, EventArgs e)
{
    if (checkBox1.Checked)
    {
        checkBox2.Checked = false;
        checkBox3.Checked = false;
    }
}






        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                checkBox3.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged_1(object sender, EventArgs e)
        {
            textBox2.Enabled = checkBox3.Checked;
            if (checkBox3.Checked)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {

                var requestData = new
                {
                    idCompte = this.accountId,
                    montant = decimal.Parse(textBox1.Text)
                };



            

                try
                {
                    string json = JsonConvert.SerializeObject(requestData);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync("api/Comptes/debit", content);
                    response.EnsureSuccessStatusCode();

                    // Le compte a été ajouté avec succès
                    MessageBox.Show("Débit effectué avec succès.");
                    this.Close();
                    this.form1.getAllComptes2();
                    //textBox1.Text = textBox2.Text = null;
                    //getAllComptes2();
                }
                catch (Exception ex)
                {
                    // Gérer les erreurs de requête
                    MessageBox.Show("Une erreur s'est produite lors de passer le debit : " + ex.Message);
                }
            }
 

            if (checkBox2.Checked)
            {

                var requestData = new
                {
                    idCompte = this.accountId,
                    montant = decimal.Parse(textBox1.Text)
                };





                try
                {
                    string json = JsonConvert.SerializeObject(requestData);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync("api/Comptes/credit", content);
                    response.EnsureSuccessStatusCode();

                    // Le compte a été ajouté avec succès
                    MessageBox.Show("Credit effectué avec succès.");
                    this.Close();
                    this.form1.getAllComptes2();
                    //textBox1.Text = textBox2.Text = null;
                    //getAllComptes2();
                }
                catch (Exception ex)
                {
                    // Gérer les erreurs de requête
                    MessageBox.Show("Une erreur s'est produite lors de passer le credit : " + ex.Message);
                }
            }
            if (checkBox3.Checked)
            {

                var requestData = new
                {
                    idCompteSource = this.accountId,
                    idCompteDestinataire=int.Parse(textBox2.Text),
                    montant = decimal.Parse(textBox1.Text)
                };





                try
                {
                    string json = JsonConvert.SerializeObject(requestData);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync("api/Comptes/virement", content);
                    response.EnsureSuccessStatusCode();

                    // Le compte a été ajouté avec succès
                    MessageBox.Show("Virement effectué avec succès.");
                    this.Close();
                    this.form1.getAllComptes2();
                    //textBox1.Text = textBox2.Text = null;
                    //getAllComptes2();
                }
                catch (Exception ex)
                {
                    // Gérer les erreurs de requête
                    MessageBox.Show("Une erreur s'est produite lors de passer le virement : " + ex.Message);
                }
            }
        }
    }
}
