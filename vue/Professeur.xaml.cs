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
    /// Logique d'interaction pour Professeur.xaml
    /// </summary>
    public partial class Professeur : Window
    {
        MySqlConnection conn = DBUtils.GetDBConnection();

        public Professeur()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AccueilProfesseur UCHomeProf = new AccueilProfesseur();
            this.CC_Professeur.Content = UCHomeProf;
            LoadNomPrenom();

            IdentifiantUtilisateur utilisateur = new IdentifiantUtilisateur();
            string mail = utilisateur.ObtenirUnIdentifiant();
            string queryIdentifiant = "SELECT image FROM professeur WHERE adresseEmail ='" + mail + "'";

            LoadImg(queryIdentifiant);
        }

        private void LvHomeProfesseur_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AccueilProfesseur UCHomeProf = new AccueilProfesseur();
            this.CC_Professeur.Content = UCHomeProf;
        }

        private void btn_close_parent_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void LoadNomPrenom()
        {
            try
            {
                conn.Open();
                IdentifiantUtilisateur utilisateur = new IdentifiantUtilisateur();
                string mail = utilisateur.ObtenirUnIdentifiant();
                string queryIdentifiant = "SELECT nom, prenom FROM professeur WHERE adresseEmail ='" + mail + "'";
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


                identifiantProf.Content = sessions[0].Nom + " " + sessions[0].Prenom;
                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadImg(string sql)
        {
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);
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

        private void ChatBox_Click(object sender, RoutedEventArgs e)
        {
            vue.ChatBox chat = new vue.ChatBox();
            chat.ShowDialog();
        }

        private void btn_ajouter_Click(object sender, RoutedEventArgs e)
        {
            AjouterCalendrier calendrier = new AjouterCalendrier();
            calendrier.ShowDialog();
        }

        private void btn_actualiser_Click(object sender, RoutedEventArgs e)
        {
            AccueilProfesseur UCHomeProf = new AccueilProfesseur();
            this.CC_Professeur.Content = UCHomeProf;
        }

        private void ButtonAccueilProfesseur_Click(object sender, RoutedEventArgs e)
        {
            AccueilProfesseur UCHomeProf = new AccueilProfesseur();
            this.CC_Professeur.Content = UCHomeProf;
        }

        private void ButtonNotification_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonVisio_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonDéconnexion_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
