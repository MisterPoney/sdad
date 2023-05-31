using Microsoft.VisualBasic.ApplicationServices;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeviantGameLauncher
{
    public partial class BaseLauncher : Form
    {
        public static IMongoClient client = new MongoClient();

        public static IMongoDatabase db = client.GetDatabase("mydatabase");

        public static IMongoCollection<Player> collection = db.GetCollection<Player>("mycol");
        private string nomeUsuario;
        private string id;
        private string user; 
        private string password;

        public BaseLauncher()
        {
            InitializeComponent();
            btnCarregar.Enabled = false;
            btnCarregar.Hide();
            btnJogar.Hide();
            pictureBox2.Hide();
            label2.Hide();
            btnComprar.Hide();
            picGta.Hide();
            label4.Hide();
            label5.Show();
        }

        public BaseLauncher(string name) : this() 
        {
            nomeUsuario = name;
            MessageBox.Show("name: " + name);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            btnJogar.Show();
            pictureBox2.Show();
            label2.Show();
            btnComprar.Hide();
            picGta.Hide();
            label4.Hide();
            label5.Hide();
        }

        private void PicBoxSecondGame_Click(object sender, EventArgs e)
        {
            btnComprar.Show();
            picGta.Show();
            label4.Show();
            btnJogar.Hide();
            pictureBox2.Hide();
            label2.Hide();
            label5.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "https://localhost:7029",
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private void btnCarregar_Click(object sender, EventArgs e)
        {
            CarregarDados();
           
             
        }

        private void btnSubstituir_Click(object sender, EventArgs e)
        {
            

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtDados_TextChanged(object sender, EventArgs e)
        {
            
        }

        public void CarregarDados()
        {
            //user = nomeUsuario;

            //var filter = Builders<Player>.Filter.Eq(u => u.Name, user);
            //var usuarioEncontrado = collection.Find(filter).FirstOrDefault();

            //string jsonDocumento = usuarioEncontrado.ToJson();
            //@"C:\Users\Marcos Andreo\AppData\LocalLow\DefaultCompany\Pursuit\1\data.json";

            //if (usuarioEncontrado != null)
            //{
            //    File.WriteAllText(caminhoArquivo, jsonDocumento);
            //    SubstituirValoresBDToJSOn();

            //}
            //else
            //{
            //    MessageBox.Show("usuario n encontrado");
            //}
        }

        public void SubstituirValoresBDToJSOn()
        {
            string caminhoArquivo = @"C:\Users\Marcos Andreo\AppData\LocalLow\DefaultCompany\Pursuit\1\data.json"; 
            string conteudoJson = File.ReadAllText(caminhoArquivo);

            BsonDocument bsonDocument = BsonDocument.Parse(conteudoJson);

            // Obter os valores a serem substituídos do MongoDB
            string id = bsonDocument["_id"].ToString();
            string nome = bsonDocument["Name"].ToString();
            string senha = bsonDocument["Password"].ToString();

            // Conectar ao MongoDB
            string connectionString = "mongodb://localhost:27017";
            string databaseName = "nome_do_banco_de_dados";
            string collectionName = "nome_da_colecao";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            var collection = database.GetCollection<BsonDocument>(collectionName);

            // Filtrar documento no MongoDB pelo ID
            var filtro = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            var documentoMongo = collection.Find(filtro).FirstOrDefault();

            if (documentoMongo != null)
            {
                // Atualizar os valores no objeto BsonDocument com os valores do MongoDB
                bsonDocument["_id"] = documentoMongo["_id"];
                bsonDocument["Name"] = documentoMongo["Name"];
                bsonDocument["Password"] = documentoMongo["Password"];

                // Converter o objeto BsonDocument de volta para uma string JSON atualizada
                string jsonAtualizado = bsonDocument.ToJson();

                // Salvar o JSON atualizado no arquivo local
                File.WriteAllText(caminhoArquivo, jsonAtualizado);

                Console.WriteLine("Valores substituídos com sucesso!");
            }
            else
            {
                Console.WriteLine("Documento não encontrado no MongoDB!");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            string caminhoArquivo = @"C:\Users\Marcos Andreo\AppData\LocalLow\DefaultCompany\Pursuit\1\data.json";
            string jsonLocal = File.ReadAllText(caminhoArquivo);
            JObject jsonObject = JObject.Parse(jsonLocal);

            int pontos = jsonObject["pontos"].ToObject<int>();
            int level = jsonObject["level"].ToObject<int>();
            int kills = jsonObject["kills"].ToObject<int>();


            var filtro = Builders<Player>.Filter.Eq(p => p.Name, nomeUsuario);
            var update = Builders<Player>.Update.Set(p => p.Pontos, pontos)
                                                .Set(p => p.Level, level)
                                                .Set(p => p.Kills, kills);

            collection.UpdateOne(filtro, update);

            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void btnJogar_Click(object sender, EventArgs e)
        {
            Process.Start(@"D:\PURSUIT\Pursuit\Build\Pursuit.exe");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnComprar_Click(object sender, EventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "https://store.steampowered.com/app/271590/Grand_Theft_Auto_V/",
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
