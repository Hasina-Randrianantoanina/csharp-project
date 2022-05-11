using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebosy.controleur
{
    class ObtenirFichier
    {
        public void FichierCourrier(string FileName, string MonPseudo)
        {
            try
            {   
                if (File.Exists(FileName))
                {
                    File.Delete(FileName);
                }

                using (FileStream fileStr = File.Create(FileName))
                {  
                    Byte[] text = new UTF8Encoding(true).GetBytes(MonPseudo);
                    fileStr.Write(text, 0, text.Length);
                }

                using (StreamReader sr = File.OpenText(FileName))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
