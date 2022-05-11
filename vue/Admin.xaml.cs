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
using System.Windows.Shapes;

namespace ebosy.vue
{
    /// <summary>
    /// Logique d'interaction pour Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        public Admin()
        {
            InitializeComponent();
        }

        private void ButtonListeParent_Click(object sender, RoutedEventArgs e)
        {
            ListParent listParent = new ListParent();
            this.ContentControlAccueilAdmin.Content = listParent;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonListeProfesseur_Click(object sender, RoutedEventArgs e)
        {
            AccueilParent accueilParent = new AccueilParent();
            this.ContentControlAccueilAdmin.Content = accueilParent;
        }

        private void ButtonCalendrierProfesseur_Click(object sender, RoutedEventArgs e)
        {

            ActualiseCalendrier();
        }

        public void ActualiseCalendrier()
        {
            try
            {
                AccueilProfesseur UCHProf = new AccueilProfesseur();
                this.ContentControlAccueilAdmin.Content = UCHProf;
                for (int j = 0; j < 13; j++)
                {
                    conn.Open();
                    string dateActu2 = DateTime.Now.AddDays(j).ToString("dd/MM/yyyy");

                    string calendrier = "SELECT  `heureD`, `heureF`, `nomEleve`, `matiereEleve`,  `professeur` FROM `calendrier` where dateActu = '" + dateActu2 +"'";
                    MySqlCommand cmd3 = new MySqlCommand(calendrier, conn);

                    MySqlDataReader mySqlData2 = cmd3.ExecuteReader();
                    List<ListCalendrierAdmin> listCalendriersadmin = new List<ListCalendrierAdmin>();

                    while (mySqlData2.Read())
                    {
                        listCalendriersadmin.Add(new ListCalendrierAdmin
                        {
                            heureDebut = mySqlData2.GetString(0),
                            heureFin = mySqlData2.GetString(1),
                            nomEleve = mySqlData2.GetString(2),
                            matiere = mySqlData2.GetString(3),
                            professeur = mySqlData2.GetString(4),
                        });
                    }
                    mySqlData2.Close();
                    conn.Close();

                    CarteCalendrier UCCCalendrier = new CarteCalendrier()
                    {
                        Margin = new Thickness(10)
                    };
                    UCHProf.StackPanelCarteCalendrier.Children.Add(UCCCalendrier);

                    UCCCalendrier.DateActu.Text = DateTime.Now.AddDays(j).ToString("dddd, dd MMMM yyyy");

                    for (int i = 0; i < listCalendriersadmin.Count; i++)
                    {
                        UCCCalendrier.LVRdv.Items.Add(listCalendriersadmin[i].heureDebut +" - " + listCalendriersadmin[i].heureFin + " cours :  " +
                            listCalendriersadmin[i].matiere + " avec " + listCalendriersadmin[i].nomEleve + " enseigné par " + listCalendriersadmin[i].professeur.ToUpper());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonChat_Click(object sender, RoutedEventArgs e)
        {
            ChatBox chatBox = new ChatBox();
            chatBox.ShowDialog();
        }
    }
}
