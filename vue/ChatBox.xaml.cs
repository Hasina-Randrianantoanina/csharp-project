using ebosy.controleur;
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
using System.Net;
using System.Net.Sockets;

namespace ebosy.vue
{
    /// <summary>
    /// Logique d'interaction pour ChatBox.xaml
    /// </summary>
    public partial class ChatBox : Window
    {
        Socket socket;
        EndPoint endPointLocal, endPointRemote;
        byte[] buffer;
        public ChatBox()
        {
            InitializeComponent();
        }

        private void ButtonMessageEnvoyer_Click(object sender, RoutedEventArgs e)
        {
            /*ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
            byte[] sendingMessage = new byte[1500];
            sendingMessage = aSCIIEncoding.GetBytes(TextBoxMessageAEnvoyer.Text);
            socket.Send(sendingMessage);*/
            EnvoyerMessage envoie = new EnvoyerMessage();
            StackPanelChatBox.Children.Add(envoie);
            envoie.TextBlockMessage.Text = TextBoxMessageAEnvoyer.Text;
            envoie.TextBlockDate.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm");
            TextBoxMessageAEnvoyer.Clear();
            /*EnvoyerMessage envoie = new EnvoyerMessage();
            StackPanelChatBox.Children.Add(envoie);
            envoie.TextBlockMessage.Text = TextBoxMessageAEnvoyer.Text;
            envoie.TextBlockDate.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm");
            TextBoxMessageAEnvoyer.Clear();*/
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*ObtenirIP obtenir = new ObtenirIP();
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            ConnexionIP();*/
            //Console.WriteLine(obtenir.ObtenirAdresseIP());
            //Console.ReadLine();
        }

        private void ConnexionIP() {
            ObtenirIP obtenir = new ObtenirIP();
            endPointLocal = new IPEndPoint(IPAddress.Parse(obtenir.ObtenirAdresseIP()),Convert.ToInt32("80"));
            socket.Bind(endPointLocal);

            endPointRemote = new IPEndPoint(IPAddress.Parse("192.168.88.14"), Convert.ToInt32("80"));
            socket.Connect(endPointRemote);

            buffer = new byte[1500];
            socket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref endPointRemote, new AsyncCallback(MessageCallback), buffer);
        }

        private void MessageCallback(IAsyncResult asyncResult)
        {
            try
            {
                byte[] receivedData = new byte[1500];
                receivedData = (byte[])asyncResult.AsyncState;

                ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
                string receivedMessage = aSCIIEncoding.GetString(receivedData);

                RecevoirMessage recu = new RecevoirMessage();

                StackPanelChatBox.Children.Add(recu);
                recu.TextBlockMessageRecu.Text = receivedMessage;

                buffer = new byte[1500];
                socket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref endPointRemote, new AsyncCallback(MessageCallback), buffer);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

    }
}
