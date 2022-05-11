using ebosy.Modele;
using ebosy.vue;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ebosy.controleur
{
    class Requete
    {
        //ObtenirIP ip = new ObtenirIP();
        //AccueilParent accueilParent = new AccueilParent();
        Parent parent = new Parent();

        /*public void InsererUneAdresseIP(MySqlConnection conn,string mail)
        {
            conn.Open();
            string sql = "INSERT INTO `ip`(`host`, `ip`, `mail`) VALUES ('" + ip.ObtenirHote() + "','" + ip.ObtenirIp() + "','" + mail + "')";
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            conn.Close();
        }*/

       /* public void EffacerIP(MySqlConnection conn,string mail)
        {
            conn.Open();
            string sql = "DELETE FROM `ip` WHERE `mail`= '"+mail+"'";
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            conn.Close();
        }*/

        public string SelectData(MySqlConnection conn,string query)
        {
            try {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                string mailPasse;
                mailPasse = (string)cmd.ExecuteScalar();
                conn.Close();
                return mailPasse;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        /*public void AfficheListeProffeseur(MySqlConnection conn,string sql)
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
                    accueilParent.StackPanelCarteProfesseur.Children.Add(CarteProfesseur);
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
        }*/

       /* public void LoadNomPrenom(MySqlConnection conn)
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
                parent.identifiant.Content = sessions[0].Nom + " " + sessions[0].Prenom;
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

                parent.Picture.Source = bm;

                stream.Close();
                stream.Dispose();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/
    }
}
