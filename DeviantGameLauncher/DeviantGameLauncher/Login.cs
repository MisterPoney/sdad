
using DeviantGameLauncher;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace DeviantLaucher
{
    public partial class Login : Form
    {
        public static IMongoClient client = new MongoClient();

        public static IMongoDatabase db = client.GetDatabase("mydatabase");

        public static IMongoCollection <Player> collection = db.GetCollection<Player>("mycol");

        public static string? nomeUsuario { get; set; }

        public Login()
        {
            InitializeComponent();
            labelNotFound.Hide();
            labelCreateAccount.Hide();
            LinkCreateAccount.Hide();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {

            nomeUsuario = txtUser.Text;
            string senha = txtPass.Text;

            var filter = Builders<Player>.Filter.Eq(u => u.Name, nomeUsuario) & Builders<Player>.Filter.Eq(u => u.Password, senha);
            var usuarioEncontrado = collection.Find(filter).FirstOrDefault();


            if (usuarioEncontrado != null)
            {
                var form = new BaseLauncher(txtUser.Text);
                form.Show();
                this.Hide();
            }
            else
            {
                labelNotFound.Show();
                labelCreateAccount.Show();
                LinkCreateAccount.Show();
            }


            //Player player = new Player(txtUser.Text, txtPass.Text);

            //collection.InsertOne(player);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            
            Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkCreateAccount.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;

            var psi = new ProcessStartInfo
            {
                FileName ="https://localhost:7029/Login/Register",
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}