using ebosy.controleur;
using ebosy.Modele;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ebosy.vue
{
    /// <summary>
    /// Logique d'interaction pour AccueilParent.xaml
    /// </summary>
    public partial class AccueilParent : UserControl
    {
        //Requete requete = new Requete();
        MySqlConnection conn = DBUtils.GetDBConnection();

        public AccueilParent()
        {
            InitializeComponent();
        }

        

        public void AfficheListeProffeseur(MySqlConnection conn, string sql)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader mySqlData = cmd.ExecuteReader();
                List<ListProfesseur> listProfesseurs = new List<ListProfesseur>();
                while (mySqlData.Read())
                {
                    listProfesseurs.Add(new ListProfesseur
                    {
                        Nom = mySqlData.GetString(0),
                        Prenom = mySqlData.GetString(1),
                        Matiere = mySqlData.GetString(2),
                        Niveau = mySqlData.GetString(3),
                        Pays = mySqlData.GetString(4),
                        Ville = mySqlData.GetString(5),
                    });
                }
                mySqlData.Close();
                conn.Close();

                for (int i = 0; i < listProfesseurs.Count; i++)
                {
                    CarteProfesseur CarteProfesseur = new CarteProfesseur()
                    {
                        Margin = new Thickness(10)
                    };
                    StackPanelCarteParent.Children.Add(CarteProfesseur);
                    CarteProfesseur.TextBlockNom.Text = listProfesseurs[i].Nom + " " + listProfesseurs[i].Prenom;
                    CarteProfesseur.TextBlockMatiere.Text = listProfesseurs[i].Matiere;
                    CarteProfesseur.TextBlockNiveau.Text = listProfesseurs[i].Niveau;
                    CarteProfesseur.TextBlockLocalisation.Text = listProfesseurs[i].Ville + ", " + listProfesseurs[i].Pays;

                    string queryIdentifiant = "SELECT image FROM professeur WHERE nom ='" + listProfesseurs[i].Nom + "' AND prenom ='" + listProfesseurs[i].Prenom + "'" +
                        "AND matiere='" + listProfesseurs[i].Matiere + "'AND niveauEnseigne='" + listProfesseurs[i].Niveau + "'AND pays='" + listProfesseurs[i].Pays + "'" +
                        "AND ville='" + listProfesseurs[i].Ville + "'";

                    try
                    {
                        conn.Open();

                        MySqlCommand cmd2 = new MySqlCommand(queryIdentifiant, conn);
                        byte[] image = (byte[])cmd2.ExecuteScalar();


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

                        CarteProfesseur.ImageProfilProfesseur.Source = bm;

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string queryNom = "SELECT nom,prenom,matiere,niveauEnseigne,pays,ville FROM professeur";
            AfficheListeProffeseur(conn, queryNom);
        }

    }
}
