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
    /// Logique d'interaction pour ListParent.xaml
    /// </summary>
    public partial class ListParent : UserControl
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        public ListParent()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CarteParent carteParent = new CarteParent();
            string queryNom = "SELECT nom,prenom,nbrEnfant,adresseEmail,pays,ville,numeroTelephone FROM parent";
            AfficheListeProffeseur(conn, queryNom);
        }

        public void AfficheListeProffeseur(MySqlConnection conn, string sql)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader mySqlData = cmd.ExecuteReader();
                List<ListeDesParents> listParent = new List<ListeDesParents>();
                while (mySqlData.Read())
                {
                    listParent.Add(new ListeDesParents
                    {
                        Nom = mySqlData.GetString(0),
                        Prenom = mySqlData.GetString(1),
                        NombreEnfant = mySqlData.GetInt32(2),
                        AdresseMail = mySqlData.GetString(3),
                        Pays = mySqlData.GetString(4),
                        Ville = mySqlData.GetString(5),
                        NumeroTelephone = mySqlData.GetString(6),
                    });
                }
                mySqlData.Close();
                conn.Close();

                for (int i = 0; i < listParent.Count; i++)
                {
                    CarteParent CarteParent = new CarteParent()
                    {
                        Margin = new Thickness(10)
                    };
                    StackPanelListParent.Children.Add(CarteParent);
                    CarteParent.TextBlockNomParent.Text = listParent[i].Nom + " " + listParent[i].Prenom;
                    CarteParent.textBlocNombreEnfant.Text = "Nombre d'enfant : "+listParent[i].NombreEnfant.ToString();
                    CarteParent.TextBlockMail.Text = listParent[i].AdresseMail;
                    CarteParent.TextBlockLocalisationParent.Text = listParent[i].Ville + ", " + listParent[i].Pays;
                    CarteParent.TextBlockNumeroTelephone.Text = listParent[i].NumeroTelephone;

                    /*string queryIdentifiant = "SELECT image FROM professeur WHERE nom ='" + listParent[i].Nom + "' AND prenom ='" + listParent[i].Prenom + "'" +
                        "AND matiere='" + listParent[i].Matiere + "'AND niveauEnseigne='" + listParent[i].Niveau + "'AND pays='" + listParent[i].Pays + "'" +
                        "AND ville='" + listProfesseurs[i].Ville + "'";*/

                    string queryIdentifiant = "SELECT `image` FROM `parent` WHERE `nom`='"+ listParent[i].Nom + "' AND `prenom`='"+ listParent[i].Prenom + "' AND `nbrEnfant`="+ listParent[i].NombreEnfant 
                        + " AND `adresseEmail`='"+ listParent[i].AdresseMail + "' AND `numeroTelephone`='"+ listParent[i].NumeroTelephone + "' AND `pays` ='"+ listParent[i].Pays 
                        + "' AND `ville`='"+ listParent[i].Ville + "'";

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

                        CarteParent.ImageProfilParent.Source = bm;

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
    }
}

