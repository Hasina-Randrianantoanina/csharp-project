using ebosy.controleur;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
    /// Logique d'interaction pour EnvoyerMail.xaml
    /// </summary>
    public partial class EnvoyerMail : Window
    {
        Requete requete = new Requete();
        MySqlConnection conn = DBUtils.GetDBConnection();

        public EnvoyerMail()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonEnvoyer_Click(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT codeparrainage FROM parent WHERE adresseEmail = '" + TextBoxFromMail.Text + "'";
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            string body;
            body = (string)cmd.ExecuteScalar();

            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com");

                client.Port = 587;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(TextBoxFromMail.Text, PasswordBox.Password);

                MailMessage msg = new MailMessage();

                msg.From = new MailAddress(TextBoxFromMail.Text);
                msg.To.Add(TextBoxToMail.Text);
                msg.Subject = "CODE PARRAINAGE";
                msg.Body = "Voici le Code  de parrainge avec lequel vous pourriez accéder à la plateform Professeur e-Bosy (CODE: " + body + ")";

                client.Send(msg);
                MessageBox.Show("Votre Mail a été envoyé");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conn.Close();
            Close();
        }
    }
}
