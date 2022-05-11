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
    /// Logique d'interaction pour CarteProfesseur.xaml
    /// </summary>
    public partial class CarteProfesseur : UserControl
    {
        public CarteProfesseur()
        {
            InitializeComponent();
        }

        private void ButtonConsulterClendrier_Click(object sender, RoutedEventArgs e)
        {
            string nomProf = TextBlockNom.Text;
            CalendrierConsulter calendrier = new CalendrierConsulter(nomProf);
            calendrier.ShowDialog();
        }

        private void ButtonContacter_Click(object sender, RoutedEventArgs e)
        {
            ChatBox chatBox = new ChatBox();

            
            //ImageProfilProfesseur.Source = "";

            chatBox.TextBlockADiscuter.Text = TextBlockNom.Text;
            chatBox.TextBlockLocalisation.Text = TextBlockLocalisation.Text;
            chatBox.TextBlockDiplome.Text = TextBlockNiveau.Text;
            chatBox.TextBlockNomADiscuter.Text = TextBlockNom.Text;

            chatBox.ShowDialog();
        }
    }
}
