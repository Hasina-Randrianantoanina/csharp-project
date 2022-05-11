using ebosy.controleur;
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
    /// Logique d'interaction pour AccueilProfesseur.xaml
    /// </summary>
    public partial class AccueilProfesseur : UserControl
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        public AccueilProfesseur()
        {
            InitializeComponent();
        }
        private void btn_ajouter_Click(object sender, RoutedEventArgs e)
        {
            AjouterCalendrier ajouter = new AjouterCalendrier();
            ajouter.ShowDialog();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CarteCalendrier UCCardCalendrier = new CarteCalendrier();
            LoadCalendrierDateActuProf();
        }

        public void LoadCalendrierDateActuProf()
        {
            try
            {
                conn.Open();
                IdentifiantUtilisateur utilisateur = new IdentifiantUtilisateur();
                string mail = utilisateur.ObtenirUnIdentifiant();
                string queryIdentifiant = "SELECT nom, prenom FROM professeur WHERE adresseEmail ='" + mail + "'";
                MySqlCommand cmd1 = new MySqlCommand(queryIdentifiant, conn);
                MySqlDataReader mySqlData1 = cmd1.ExecuteReader();
                List<Session> sessions = new List<Session>();
                while (mySqlData1.Read())
                {
                    sessions.Add(new Session
                    {
                        Nom = mySqlData1.GetString(0),
                        Prenom = mySqlData1.GetString(1),
                    });
                }


                string nomPrenom = sessions[0].Nom + " " + sessions[0].Prenom;
                conn.Close();

                for (int j = 0; j < 13; j++)
                {
                    conn.Open();
                    string dateActu2 = DateTime.Now.AddDays(j).ToString("dd/MM/yyyy");

                    string calendrier = "SELECT  `heureD`, `heureF`, `nomEleve`, `matiereEleve` FROM `calendrier` where dateActu = '" + dateActu2 + "' AND professeur = '" + nomPrenom + "'";
                    MySqlCommand cmd = new MySqlCommand(calendrier, conn);

                    MySqlDataReader mySqlData = cmd.ExecuteReader();
                    List<ListCalendrier> listCalendriers = new List<ListCalendrier>();

                    while (mySqlData.Read())
                    {
                        listCalendriers.Add(new ListCalendrier
                        {
                            heureDebut = mySqlData.GetString(0),
                            heureFin = mySqlData.GetString(1),
                            nomEleve = mySqlData.GetString(2),
                            matiere = mySqlData.GetString(3),
                        });
                    }
                    mySqlData.Close();
                    conn.Close();

                    CarteCalendrier UCCCalendrier = new CarteCalendrier()
                    {
                        Margin = new Thickness(10)
                    };

                    StackPanelCarteCalendrier.Children.Add(UCCCalendrier);

                    UCCCalendrier.DateActu.Text = DateTime.Now.AddDays(j).ToString("dddd, dd MMMM yyyy");

                    for (int i = 0; i < listCalendriers.Count; i++)
                    {
                        UCCCalendrier.LVRdv.Items.Add(listCalendriers[i].heureDebut + " - " + listCalendriers[i].heureFin + " cours : " + listCalendriers[i].matiere + " avec " + listCalendriers[i].nomEleve);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
