using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conexionesDB
{
    public class conexionDB
    {
        #region Variables_Globales
        public static String ServidorNo { get; set; }   
        public static String Users { get; set;}
        public static String Password {  get; set; }
        public static String DataBase { get; set; }
        #endregion

        #region constructor
        public conexionDB()
        {
           
        }
        #endregion

        #region conexionstatica
        public static String ConexionStatic(string Servidor,string User,string Passwordd,string Base_Datos)
        {
            #region Variablesconcadenaconexion
            ServidorNo = "DESKTOP-7FVF0T5";
            Users = "sa";
            Password = "12345";
            DataBase = "InventoryManagement";
            #endregion
 
            return $"Data Source={ServidorNo};Initial Catalog={DataBase};User ID={Users};Password={Password};TrustServerCertificate=true;";
        }
        #endregion
    }
}
