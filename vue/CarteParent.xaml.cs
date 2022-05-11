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
    /// Logique d'interaction pour CarteParent.xaml
    /// </summary>
    public partial class CarteParent : UserControl
    {
        public CarteParent()
        {
            InitializeComponent();
        }

        private void ButtonContacter_Click(object sender, RoutedEventArgs e)
        {
            ChatBox chatBox = new ChatBox();
            chatBox.ShowDialog();
        }
    }
}
