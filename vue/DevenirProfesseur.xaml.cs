using ebosy.controleur;
using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace ebosy.vue
{
    /// <summary>
    /// Logique d'interaction pour DevenirProfesseur.xaml
    /// </summary>
    public partial class DevenirProfesseur : Window
    {
		private byte[] _imageBytes = null;
		MySqlConnection conn = DBUtils.GetDBConnection();
		Requete requete = new Requete();
		HashageMotDePasse hashageMotDePasse = new HashageMotDePasse();

		public DevenirProfesseur()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
			string[] nation = new string[] {
				"Afghanistan",
				"Albania",
				"Algeria",
				"American Samoa",
				"Andorra",
				"Angola",
				"Anguilla",
				"Antarctica",
				"Antigua and Barbuda",
				"Argentina",
				"Armenia",
				"Aruba",
				"Australia",
				"Austria",
				"Azerbaijan",
				"Bahamas",
				"Bahrain",
				"Bangladesh",
				"Barbados",
				"Belarus",
				"Belgium",
				"Belize",
				"Benin",
				"Bermuda",
				"Bhutan",
				"Bolivia",
				"Bosnia and Herzegovina",
				"Botswana",
				"Bouvet Island",
				"Brazil",
				"British Indian Ocean Territory",
				"Brunei Darussalam",
				"Bulgaria",
				"Burkina Faso",
				"Burundi",
				"Cambodia",
				"Cameroon",
				"Canada",
				"Cape Verde",
				"Cayman Islands",
				"Central African Republic",
				"Chad",
				"Chile",
				"China",
				"Christmas Island",
				"Cocos (Keeling) Islands",
				"Colombia",
				"Comoros",
				"Congo",
				"Congo, the Democratic Republic of the",
				"Cook Islands",
				"Costa Rica",
				"Cote D'Ivoire",
				"Croatia",
				"Cuba",
				"Cyprus",
				"Czech Republic",
				"Denmark",
				"Djibouti",
				"Dominica",
				"Dominican Republic",
				"Ecuador",
				"Egypt",
				"El Salvador",
				"Equatorial Guinea",
				"Eritrea",
				"Estonia",
				"Ethiopia",
				"Falkland Islands (Malvinas)",
				"Faroe Islands",
				"Fiji",
				"Finland",
				"France",
				"French Guiana",
				"French Polynesia",
				"French Southern Territories",
				"Gabon",
				"Gambia",
				"Georgia",
				"Germany",
				"Ghana",
				"Gibraltar",
				"Greece",
				"Greenland",
				"Grenada",
				"Guadeloupe",
				"Guam",
				"Guatemala",
				"Guinea",
				"Guinea-Bissau",
				"Guyana",
				"Haiti",
				"Heard Island and Mcdonald Islands",
				"Holy See (Vatican City State)",
				"Honduras",
				"Hong Kong",
				"Hungary",
				"Iceland",
				"India",
				"Indonesia",
				"Iran, Islamic Republic of",
				"Iraq",
				"Ireland",
				"Israel",
				"Italy",
				"Jamaica",
				"Japan",
				"Jordan",
				"Kazakhstan",
				"Kenya",
				"Kiribati",
				"Korea, Democratic People's Republic of",
				"Korea, Republic of",
				"Kuwait",
				"Kyrgyzstan",
				"Lao People's Democratic Republic",
				"Latvia",
				"Lebanon",
				"Lesotho",
				"Liberia",
				"Libyan Arab Jamahiriya",
				"Liechtenstein",
				"Lithuania",
				"Luxembourg",
				"Macao",
				"Macedonia, the Former Yugoslav Republic of",
				"Madagascar",
				"Malawi",
				"Malaysia",
				"Maldives",
				"Mali",
				"Malta",
				"Marshall Islands",
				"Martinique",
				"Mauritania",
				"Mauritius",
				"Mayotte",
				"Mexico",
				"Micronesia, Federated States of",
				"Moldova, Republic of",
				"Monaco",
				"Mongolia",
				"Montserrat",
				"Morocco",
				"Mozambique",
				"Myanmar",
				"Namibia",
				"Nauru",
				"Nepal",
				"Netherlands",
				"Netherlands Antilles",
				"New Caledonia",
				"New Zealand",
				"Nicaragua",
				"Niger",
				"Nigeria",
				"Niue",
				"Norfolk Island",
				"Northern Mariana Islands",
				"Norway",
				"Oman",
				"Pakistan",
				"Palau",
				"Palestinian Territory, Occupied",
				"Panama",
				"Papua New Guinea",
				"Paraguay",
				"Peru",
				"Philippines",
				"Pitcairn",
				"Poland",
				"Portugal",
				"Puerto Rico",
				"Qatar",
				"Reunion",
				"Romania",
				"Russian Federation",
				"Rwanda",
				"Saint Helena",
				"Saint Kitts and Nevis",
				"Saint Lucia",
				"Saint Pierre and Miquelon",
				"Saint Vincent and the Grenadines",
				"Samoa",
				"San Marino",
				"Sao Tome and Principe",
				"Saudi Arabia",
				"Senegal",
				"Serbia and Montenegro",
				"Seychelles",
				"Sierra Leone",
				"Singapore",
				"Slovakia",
				"Slovenia",
				"Solomon Islands",
				"Somalia",
				"South Africa",
				"South Georgia and the South Sandwich Islands",
				"Spain",
				"Sri Lanka",
				"Sudan",
				"Suriname",
				"Svalbard and Jan Mayen",
				"Swaziland",
				"Sweden",
				"Switzerland",
				"Syrian Arab Republic",
				"Taiwan, Province of China",
				"Tajikistan",
				"Tanzania, United Republic of",
				"Thailand",
				"Timor-Leste",
				"Togo",
				"Tokelau",
				"Tonga",
				"Trinidad and Tobago",
				"Tunisia",
				"Turkey",
				"Turkmenistan",
				"Turks and Caicos Islands",
				"Tuvalu",
				"Uganda",
				"Ukraine",
				"United Arab Emirates",
				"United Kingdom",
				"United States",
				"United States Minor Outlying Islands",
				"Uruguay",
				"Uzbekistan",
				"Vanuatu",
				"Venezuela",
				"Viet Nam",
				"Virgin Islands, British",
				"Virgin Islands, US",
				"Wallis and Futuna",
				"Western Sahara",
				"Yemen",
				"Zambia",
				"Zimbabwe",
			};

			for (int i = 0; i < nation.Length; i++)
			{
				ComboBoxPays.Items.Add(nation[i]);
			}
		}

        private void ParcourirImage_Click(object sender, RoutedEventArgs e)
        {
			string filename = "";
			OpenFileDialog openFile = new OpenFileDialog();
			openFile.InitialDirectory = "c:\\";
			openFile.DefaultExt = "jpg";
			openFile.Filter = "Choix d'image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif";

			if (openFile.ShowDialog() == true)
			{
				ImageProfesseur.Source = new BitmapImage(new Uri(openFile.FileName));
			}

			filename = openFile.FileName;

			using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
			{
				_imageBytes = new byte[fs.Length];
				fs.Read(_imageBytes, 0, System.Convert.ToInt32(fs.Length));
			}
		}

        private void ButtonAccepter_Click(object sender, RoutedEventArgs e)
        {
			string passwordhash = "";
			if (PasswordBox.Password == PasswordBoxConfirmer.Password)
			{
				string verifParain = "SELECT codeparrainage FROM `parent` WHERE codeparrainage='" + TextBoxCodeParrainage.Text + "'";
				conn.Open();
				MySqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = verifParain;
				string CodeParrain;
				CodeParrain = (string)cmd.ExecuteScalar();
				conn.Close();

				if (TextBoxCodeParrainage.Text == CodeParrain)

				{

					passwordhash = hashageMotDePasse.Hashage(PasswordBox.Password);

					string insertProf = "INSERT INTO professeur (nom, prenom,dernierDiplome, niveauEnseigne, matiere, adresseEmail" +
					", numeroTelephone, pays, ville, password, codeparrainage,image) " +
					"VALUES ('" + TextBoxNom.Text + "','" + TextBoxPrenom.Text + "','" + TextBoxDiplome.Text + "','" + TextBoxNiveau.Text + "','" 
					+ TextBoxMatiere.Text + "','" + TextBoxMail.Text + "','" + TextBoxNumero.Text + "','" 
					+ ComboBoxPays.Text + "','" + TextBoxVille.Text + "','" + passwordhash + "','" + TextBoxCodeParrainage.Text + "',@img)";
					conn.Open();
					MySqlCommand cmd1 = conn.CreateCommand();
					cmd1.CommandText = insertProf;
					cmd1.Parameters.Add(new MySqlParameter("@img", _imageBytes));
					cmd1.ExecuteNonQuery();
					conn.Close();
					MessageBox.Show("Insertion réussie");


				}
				else
					MessageBox.Show("Vérifiez votre Code parrainage");
			}

			else
			{
				MessageBox.Show("Mot de passe incorrect");

				PasswordBox.Password = "";
				PasswordBoxConfirmer.Password = "";
			}
			conn.Close();
			Close();
		}

        private void ButtonAnnuler_Click(object sender, RoutedEventArgs e)
        {
			TextBoxCodeParrainage.Text = "";
			TextBoxDiplome.Text = "";
			TextBoxMail.Text = "";
			TextBoxMatiere.Text = "";
			TextBoxNiveau.Text = "";
			TextBoxNom.Text = "";
			TextBoxNumero.Text = "";
			TextBoxPrenom.Text = "";
			TextBoxVille.Text = "";
			PasswordBox.Password = "";
			PasswordBoxConfirmer.Password = "";
			ComboBoxPays.Text = "";
        }

    }
}
