using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema
{
    internal class Conexion
    {
        public static MySqlConnection GetConnection()
        {
            String servidor = "localhost";
            String puerto = "3306";
            String usuario = "root";
            string password = "123456";
            string bd = "sistema_usuarios";

            string cadenaConexion = "server=" + servidor + "; port=" + puerto + "; user id=" + usuario +"; password=" + password + "; database=" + bd;

            try
            {
                MySqlConnection conexion = new MySqlConnection(cadenaConexion);

                return conexion;
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error: "+ e.Message);
                return null;
            }    
            
            
        }
    }
}
