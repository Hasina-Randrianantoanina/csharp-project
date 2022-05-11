using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebosy.controleur
{
    class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            /*string host = "mysql-rova021.alwaysdata.net";
            int port = 3306;
            string database = "rova021_ebosy";
            string username = "rova021_ebosy01";
            string password = "#123456Na";*/

            string host = "localhost";
            int port = 3306;
            string database = "ebosydata";
            string username = "root";
            string password = "";

            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }
    }
}
