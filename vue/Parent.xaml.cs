using ebosy.controleur;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ebosy.vue
{
    /// <summary>
    /// Logique d'interaction pour Parent.xaml
    /// </summary>
    public partial class Parent : Window
    {
        MySqlConnection conn = DBUtils.GetDBConnection();

        public Parent()
        {
            InitializeComponent();
        }

        private void TextBoxRecherche_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void ButtonAccueil_Click(object sender, RoutedEventArgs e)
        {
            AccueilParent accueilParent = new AccueilParent();
            this.ContentControlParent.Content = accueilParent;
        }

        private void ButtonAjouterCompteEnfant_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonListeParrainer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonEnvoyerMail_Click(object sender, RoutedEventArgs e)
        {
            EnvoyerMail envoyerMail = new EnvoyerMail();
            envoyerMail.ShowDialog();
        }

        private void ButtonChat_Click(object sender, RoutedEventArgs e)
        {
            /*ChatBox chatBox = new ChatBox();
            this.ContentControlParent.Content = chatBox;*/
            ChatBox chatBox = new ChatBox();
            chatBox.ShowDialog();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AccueilParent accueilParent = new AccueilParent();
            this.ContentControlParent.Content = accueilParent;
            LoadNomPrenom(conn);
            LoadImg(conn);
        }

        public void LoadNomPrenom(MySqlConnection conn)
        {
            try
            {
                conn.Open();
                IdentifiantUtilisateur utilisateur = new IdentifiantUtilisateur();
                string mail = utilisateur.ObtenirUnIdentifiant();
                string queryIdentifiant = "SELECT nom, prenom FROM parent WHERE adresseEmail ='" + mail + "'";
                MySqlCommand cmd = new MySqlCommand(queryIdentifiant, conn);
                MySqlDataReader mySqlData = cmd.ExecuteReader();
                List<Session> sessions = new List<Session>();
                while (mySqlData.Read())
                {
                    sessions.Add(new Session
                    {
                        Nom = mySqlData.GetString(0),
                        Prenom = mySqlData.GetString(1),
                    });
                }
                identifiant.Content = sessions[0].Nom + " " + sessions[0].Prenom;
                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadImg(MySqlConnection conn)
        {
            try
            {
                conn.Open();
                IdentifiantUtilisateur utilisateur = new IdentifiantUtilisateur();
                string mail = utilisateur.ObtenirUnIdentifiant();
                string queryIdentifiant = "SELECT image FROM parent WHERE adresseEmail ='" + mail + "'";
                MySqlCommand cmd = new MySqlCommand(queryIdentifiant, conn);
                byte[] image = (byte[])cmd.ExecuteScalar();


                MemoryStream stream = new MemoryStream(image);
                stream.Seek(0, SeekOrigin.Begin);

                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                BitmapImage bm = new BitmapImage();

                bm.BeginInit();
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);

                bm.StreamSource = ms;
                bm.StreamSource.Seek(0, SeekOrigin.Begin);
                bm.EndInit();

                Picture.Source = bm;

                stream.Close();
                stream.Dispose();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
