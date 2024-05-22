using conexionesDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DUsuarios
    {
        /*
     UserID INT PRIMARY KEY,
     Username VARCHAR(50) NOT NULL,
     Contraseña VARCHAR(50) NOT NULL,
     RoleID INT NOT NULL,
     FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
        */

        #region variables_globales_conexiondb
        public static string Servidor { get; set; }
        public static string user { get; set; }
        public static string password { get; set; }
        public static string database { get; set; }
        #endregion

        #region propiedades
        public int UserID { get; set; }
        public  string UserName { get; set; }
        public  string Contraseña { get; set; }
        public  int RoleID { get; set; }
        #endregion

        #region constructor
        public DUsuarios() { }
        public DUsuarios(int userid,string username,string contrasena,int roleid) { 
            
            UserID = userid;
            UserName = username;
            Contraseña = contrasena;
            RoleID = roleid;

            conexionDB conexionDB = new conexionDB();
            conexionDB.ServidorNo = Servidor;
            conexionDB.Users = user;
            conexionDB.Password = password;
            conexionDB.DataBase = database;
        }
        #endregion

        #region insertar_usuario
        public string insertarusuario(DUsuarios dUsuarios)
        {

            string respuesta = "";
            var conexionsql = new SqlConnection(conexionDB.ConexionStatic(Servidor,user,password,database));

            try
            {

                conexionsql.Open();

                var comandosql = new SqlCommand("[sp_InsertarUsuario]",conexionsql);
                comandosql.CommandType = CommandType.StoredProcedure;

                var userid = new SqlParameter("@UserID",SqlDbType.Int);
                userid.Direction = ParameterDirection.Output;
                comandosql.Parameters.Add(userid);

            }catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                if(conexionsql.State == ConnectionState.Open)
                    conexionsql.Close();
            }
            return respuesta;

        }
        #endregion

    }
}
