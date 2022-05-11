using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ebosy.controleur
{
    class ObtenirIP
    {
        string Host = "";
        string IP = "";

        public string ObtenirHote()
        {
            Host = Dns.GetHostName();
            return Host;
        }

        public string ObtenirAdresseIP()
        {
            IP = Dns.GetHostByName(ObtenirHote()).AddressList[0].ToString();
            return IP;
        }
    }
}
