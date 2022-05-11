using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebosy.controleur
{
    class IdentifiantUtilisateur
    {
        public string ObtenirUnIdentifiant()
        {
            StreamReader reader = new StreamReader("C:/Users/Public/file.txt");
            string content = reader.ReadToEnd();
            return content;
        }
    }
}
