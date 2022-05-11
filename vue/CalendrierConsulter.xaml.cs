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
using System.Windows.Shapes;

namespace ebosy.vue
{
    /// <summary>
    /// Logique d'interaction pour CalendrierConsulter.xaml
    /// </summary>
    public partial class CalendrierConsulter : Window
    {
        public string _nomProf = null;
        MySqlConnection conn = DBUtils.GetDBConnection();

        public CalendrierConsulter(string nomProf)
        {
            InitializeComponent();
            _nomProf = nomProf;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int j = 0; j < 5; j++)
                {
                    conn.Open();
                    string dateActu2 = DateTime.Now.AddDays(j).ToString("dd/MM/yyyy");

                    string calendrier = "SELECT  `heureD`, `heureF`, `nomEleve`, `matiereEleve` FROM `calendrier` where dateActu = '" + dateActu2 + "' AND professeur = '" + _nomProf + "'";
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

                    CarteCalendrier UCCCalendrier2 = new CarteCalendrier()
                    {
                        Margin = new Thickness(10)
                    };

                    SpCardCalendrier.Children.Add(UCCCalendrier2);

                    UCCCalendrier2.DateActu.Text = DateTime.Now.AddDays(j).ToString("dddd, dd MMMM yyyy");

                    for (int i = 0; i < listCalendriers.Count; i++)
                    {
                        UCCCalendrier2.LVRdv.Items.Add(listCalendriers[i].heureDebut + " " + listCalendriers[i].heureFin + " " + listCalendriers[i].nomEleve + " " + listCalendriers[i].matiere);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
