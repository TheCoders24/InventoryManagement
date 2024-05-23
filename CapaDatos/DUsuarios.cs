using conexionesDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Http.Headers;
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

                var username = new SqlParameter("@Username", SqlDbType.VarChar,50);
                username.Value = dUsuarios.UserName;
                comandosql.Parameters.Add(username);

                var contrasena = new SqlParameter("@Contraseña", SqlDbType.VarChar, 50);
                contrasena.Value =dUsuarios.Contraseña;
                comandosql.Parameters.Add(contrasena);

                var roleID = new SqlParameter("@RoleID",SqlDbType.Int);
                roleID.Direction = ParameterDirection.Output;
                comandosql.Parameters.Add(roleID);


                //Ejecucion del comando
                respuesta = comandosql.ExecuteNonQuery() == 1 ? "Ok" : "No se pudo insertar el registro";

            }
            catch (Exception ex)
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

        #region actualizar_usuarios
        public string ActualizarUsuario(DUsuarios dUsuarios)
        {
            string respuesta = "";
            var conexionsql = new SqlConnection(conexionDB.ConexionStatic(Servidor,user,password,database));

            try
            {

                conexionsql.Open();

                var comandosql = new SqlCommand("[sp_ActualizarUsuario]", conexionsql);
                comandosql.CommandType = CommandType.StoredProcedure;

                var userid = new SqlParameter("@UserID", SqlDbType.Int);
                userid.Value = dUsuarios.UserID;
                comandosql.Parameters.Add(userid);

                var username = new SqlParameter("@Username", SqlDbType.VarChar,50);
                username.Value = dUsuarios.UserName;
                comandosql.Parameters.Add(username);

                var contrasena = new SqlParameter("@Contraseña", SqlDbType.VarChar, 50);
                contrasena.Value = dUsuarios.Contraseña;
                comandosql.Parameters.Add(contrasena);

                var roleid = new SqlParameter("RoleID", SqlDbType.Int);
                roleid.Value = dUsuarios.RoleID;
                comandosql.Parameters.Add (roleid);

                //Ejecucion del comando
                respuesta = comandosql.ExecuteNonQuery() == 1 ? "Ok" : "No se pudo editar el registro";

            }
            catch (Exception ex)
            {
                respuesta += ex.Message;
            }
            finally
            {
                if(conexionsql.State == ConnectionState.Open)
                    conexionsql.Close();
            }

            return respuesta;
        }
        #endregion

        #region eliminar_usuarios
        public string eliminarusuario(DUsuarios dUsuarios)
        {
            var respuesta = "";
            var conexionsql = new SqlConnection(conexionDB.ConexionStatic(Servidor,user,password,database));
            try
            {

                conexionsql.Open();

                var comandosql = new SqlCommand("[sp_EliminarUsuario]", conexionsql);
                comandosql.CommandType = CommandType.StoredProcedure;

                var userID = new SqlParameter("@UserID",SqlDbType.Int);
                userID.Value = dUsuarios.UserID;
                comandosql.Parameters.Add(userID);

            }catch (Exception ex)
            {
                respuesta+= ex.Message;
            }
            finally
            {
                if(conexionsql.State == ConnectionState.Open)
                    conexionsql.Close();
            }

            return respuesta;
        }
        #endregion

        #region mostrarusuarios
        public DataTable mostrarusuarios(DUsuarios dUsuarios)
        {
            var resultadotablausuario = new DataTable("Usuarios");
            var conexionsql = new SqlConnection(conexionDB.ConexionStatic(Servidor,user,password,database));
            try
            {

                var comandosql = new SqlCommand("[sp_MostrarUsuariosConRoles]", conexionsql);
                comandosql.CommandType= CommandType.StoredProcedure;

                SqlDataAdapter sqlData = new SqlDataAdapter(comandosql);
                sqlData.Fill(resultadotablausuario);

            }catch (Exception ex)
            {
                resultadotablausuario = null;
            }
            finally
            {
                if(conexionsql.State == ConnectionState.Open)
                    conexionsql.Close();
            }
            return resultadotablausuario;
        }
        #endregion

        #region login
        public DataTable login(DUsuarios dUsuarios)
        {
            var resultadotabla = new DataTable("Usuarios");
            var conexionsql = new SqlConnection(conexionDB.ConexionStatic(Servidor, user, password, database));
            try
            {

                var comandosql = new SqlCommand("[sp_login]", conexionsql);
                comandosql.CommandType = CommandType.StoredProcedure;

                //Parametros
                var parUsuario = new SqlParameter("@UserName", SqlDbType.VarChar, 20);
                parUsuario.Value = dUsuarios.UserName;
                comandosql.Parameters.Add(parUsuario);


                SqlDataAdapter sqldat = new SqlDataAdapter(comandosql);
                sqldat.Fill(resultadotabla);

            }
            catch (Exception ex)
            {
              
                resultadotabla = null;
            }
            return resultadotabla;
        }
        #endregion

    }
}
