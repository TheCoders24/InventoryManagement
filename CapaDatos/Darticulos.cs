using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//dll de la conexion
using conexionesDB;

namespace CapaDatos
{
    public class Darticulos
    {
        public static string Servidor {  get; set; }
        public static string user { get; set; }
        public static string password { get; set; }
        public static string database { get; set; }
       
        #region constructor
        public Darticulos() { }
        public Darticulos(int ProductID,string NombreProducto,string Descripcion,int precio)
        {
            ProductoID = ProductID;
            NombreProductos = NombreProducto;
            Descripciones = Descripcion;
            precios = precio;
            
            conexionDB conexionDB = new conexionDB();
            conexionDB.ServidorNo = Servidor;
            conexionDB.Users = user;
            conexionDB.Password = password; 
            conexionDB.DataBase = database;
        }
        #endregion

        #region propiedades de la clase
        public int ProductoID { get; set; } 
        public string NombreProductos { get; set; }
        public string Descripciones { get; set; }
        public int precios { get; set; }
        #endregion

        #region insertarproductos
        public string insertarproductos(Darticulos darticulos)
        {
            conexionDB conexionDB = new conexionDB();
            string respuesta = "";
            var conexionsql = new SqlConnection(conexionDB.ConexionStatic(Servidor,user,password,database));
            try
            {
                conexionsql.Open(); //abrimos la conexion del sql server

                var comandosql = new SqlCommand("[sp_InsertarProducto]");
                comandosql.CommandType = System.Data.CommandType.StoredProcedure;


            }catch (Exception ex)
            {

            }
            finally
            {

            }
            return respuesta;
        }
        #endregion
    }
}
