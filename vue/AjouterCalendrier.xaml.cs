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
    /// Logique d'interaction pour AjouterCalendrier.xaml
    /// </summary>
    public partial class AjouterCalendrier : Window
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        public AjouterCalendrier()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonAjouter_Click(object sender, RoutedEventArgs e)
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

            string insertCalendrier = "INSERT INTO `calendrier`" +
                "(`heureD`, `heureF`, `dateactu`,`nomEleve`, `matiereEleve`,`professeur`) " +
                "VALUES ('" + TimePickerHeureDebut.Text + "','" + TimePickerHeureFin.Text + "', '"
                + DatePickerDate.Text + "','" + TextBoxEleve.Text + "','" + TextBoxMatiere.Text + "','" + NomPrenom + "')";

            conn.Open();
            MySqlCommand cmd1 = conn.CreateCommand();
            cmd1.CommandText = insertCalendrier;
            cmd1.ExecuteNonQuery();
            MessageBox.Show("Insertion réussie");

            conn.Close();

            AccueilProfesseur UCHomeProf = new AccueilProfesseur();
            Professeur professeur = new Professeur();
            professeur.CC_Professeur.Content = UCHomeProf;

            Close();
        }
    }
}
