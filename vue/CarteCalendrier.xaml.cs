using ebosy.controleur;
using ebosy.Modele;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour CarteCalendrier.xaml
    /// </summary>
    public partial class CarteCalendrier : UserControl
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        public CarteCalendrier()
        {
            InitializeComponent();
        }
        private void BtnTerminer_Click(object sender, RoutedEventArgs e)
        {
            IdentifiantUtilisateur utilisateur = new IdentifiantUtilisateur();
            string mail = utilisateur.ObtenirUnIdentifiant();
            conn.Open();
            string queryId = "SELECT `nom`, `prenom` FROM `professeur` WHERE adresseEmail = '" + mail + "'";
            MySqlCommand cmd = new MySqlCommand(queryId, conn);
            MySqlDataReader mySqlData = cmd.ExecuteReader();
            List<ListProfesseur> listProfesseurs = new List<ListProfesseur>();
            while (mySqlData.Read())
            {
                listProfesseurs.Add(new ListProfesseur
                {
                    Nom = mySqlData.GetString(0),
                    Prenom = mySqlData.GetString(1),
                });
            }
            mySqlData.Close();
            conn.Close();

            string NomPrenom = listProfesseurs[0].Nom + " " + listProfesseurs[0].Prenom;

            conn.Open();
            String item = LVRdv.SelectedItems[0].ToString();
            string[] subs = item.Split(' ');
            string debut = subs[0] + " " + subs[1];
            string fin = subs[3] + " " + subs[4];
            string eleve = subs[9];
            string matiere = subs[7];

            string delete = "DELETE FROM `calendrier` WHERE heureD='" + debut + "' AND heureF = '" + fin + "' AND nomEleve='" + eleve + "' AND matiereEleve='" + matiere + "' AND professeur='" + NomPrenom + "'";
            /*String item = LVRdv.SelectedItems[0].ToString();
            string[] subs = item.Split(' ');*/
            Console.WriteLine(delete);
            Console.ReadLine();
            MySqlCommand cmd1 = conn.CreateCommand();
            cmd1.CommandText = delete;
            cmd1.ExecuteNonQuery();

            conn.Close();
        }

        public void ActualiseCalendrier()
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


                string nomPrenom = sessions[0].Nom + " " + sessions[0].Prenom;
                conn.Close();

                for (int j = 0; j < 13; j++)
                {
                    conn.Open();
                    string dateActu2 = DateTime.Now.AddDays(j).ToString("dd/MM/yyyy");

                    string calendrier = "SELECT  `heureD`, `heureF`, `nomEleve`, `matiereEleve` FROM `calendrier` where dateActu = '" + dateActu2 + "' AND professeur = '" + nomPrenom + "'";
                    MySqlCommand cmd3 = new MySqlCommand(calendrier, conn);

                    MySqlDataReader mySqlData2 = cmd3.ExecuteReader();
                    List<ListCalendrier> listCalendriers = new List<ListCalendrier>();

                    while (mySqlData2.Read())
                    {
                        listCalendriers.Add(new ListCalendrier
                        {
                            heureDebut = mySqlData2.GetString(0),
                            heureFin = mySqlData2.GetString(1),
                            nomEleve = mySqlData2.GetString(2),
                            matiere = mySqlData2.GetString(3),
                        });
                    }
                    mySqlData2.Close();
                    conn.Close();

                    CarteCalendrier UCCCalendrier = new CarteCalendrier()
                    {
                        Margin = new Thickness(10)
                    };
                    AccueilProfesseur UCHProf = new AccueilProfesseur();
                    UCHProf.StackPanelCarteCalendrier.Children.Add(UCCCalendrier);

                    UCCCalendrier.DateActu.Text = DateTime.Now.AddDays(j).ToString("dddd, dd MMMM yyyy");

                    for (int i = 0; i < listCalendriers.Count; i++)
                    {
                        UCCCalendrier.LVRdv.Items.Add(listCalendriers[i].heureDebut + " - " + listCalendriers[i].heureFin + " Cours de " + listCalendriers[i].matiere +" avec " +listCalendriers[i].nomEleve);
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
