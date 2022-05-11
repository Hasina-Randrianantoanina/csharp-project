using ebosy.controleur;
using ebosy.vue;
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

namespace ebosy
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObtenirFichier fichier = new ObtenirFichier();
        Requete requete = new Requete();
        IdentifiantUtilisateur utilisateur = new IdentifiantUtilisateur();
        HashageMotDePasse HashageMotDePasse = new HashageMotDePasse();
        MySqlConnection conn = DBUtils.GetDBConnection();

        vue.Professeur professeur = new vue.Professeur();
        vue.Parent parent = new vue.Parent();
        vue.Admin administrateur = new vue.Admin();
        vue.Eleve eleve = new vue.Eleve();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void ButtonFermer_Click(object sender, RoutedEventArgs e)
        {
            //requete.EffacerIP(conn,utilisateur.ObtenirUnIdentifiant());
            Close();
        }

        private void ButtonConnexion_Click(object sender, RoutedEventArgs e)
        {
            string fileName = @"C:\Users\Public\file.txt";
            fichier.FichierCourrier(fileName, TextBoxMail.Text);

            ConnexionCompte();
            //requete.InsererUneAdresseIP(conn,TextBoxMail.Text);
        }

        public void ConnexionCompte()
        {
            string password = HashageMotDePasse.Hashage(PasswordBox.Password);

             string queryMailParent = "SELECT adresseEmail FROM parent WHERE adresseEmail = '" + TextBoxMail.Text + "'";
             string queryPassParent = "SELECT password FROM parent WHERE password ='" + password + "'";

             string queryMailProf = "SELECT `adresseEmail` FROM `professeur` WHERE `adresseEmail` LIKE '%" + TextBoxMail.Text + "'";
             string queryPassProf = "SELECT `password` FROM `professeur` WHERE `password`LIKE '%" + password + "'";

             string queryMailAdmin = "SELECT `adresseEmail` FROM `administrateur` WHERE `adresseEmail` LIKE '%" + TextBoxMail.Text + "'";
             string queryPassAdmin = "SELECT `password` FROM `administrateur` WHERE `password`LIKE '%" + password + "'";

             string queryMailEleve = "SELECT `adresseEmail` FROM `eleve` WHERE `adresseEmail` LIKE '%" + TextBoxMail.Text + "'";
             string queryPassEleve = "SELECT `password` FROM `eleve` WHERE `password`LIKE '%" + password + "'";


             if (TextBoxMail.Text == requete.SelectData(conn, queryMailParent) && password == requete.SelectData(conn, queryPassParent))
                 parent.ShowDialog();
             else if (TextBoxMail.Text == requete.SelectData(conn, queryMailProf) && password == requete.SelectData(conn, queryPassProf))
                 professeur.ShowDialog();
             else if (TextBoxMail.Text == requete.SelectData(conn, queryMailAdmin) && password == requete.SelectData(conn, queryPassAdmin))
                 administrateur.ShowDialog();
             else if (TextBoxMail.Text == requete.SelectData(conn, queryMailEleve) && password == requete.SelectData(conn, queryPassEleve))
                 eleve.ShowDialog();
             else
                 MessageBox.Show("Introuvable");
        }

        private void ButtonDevenirProfesseur_Click(object sender, RoutedEventArgs e)
        {
            vue.DevenirProfesseur devenirProfesseur = new vue.DevenirProfesseur();
            devenirProfesseur.ShowDialog();
        }

        private void ButtonCreerUneFamille_Click(object sender, RoutedEventArgs e)
        {
            vue.CreerFamille creerFamille = new vue.CreerFamille();
            creerFamille.ShowDialog();
        }
    }
}
