using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using conexionesDB;

namespace CapaDatos
{
    public class DPresentacion
    {

        #region propiedades_variables
        public int IdPresentacion {  get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string TextoBuscar { get; set; }
        #endregion

        #region constructor
        public DPresentacion() { }
        #endregion

        #region Metodo_Insertar_Presentacion

        public string insertar_presentacion(DPresentacion presentacion)
        {
            string Respuesta = "";
            var conexionsql = new SqlConnection(conexionDB.ConexionStatic(conexionDB.ServidorNo, conexionDB.Users, conexionDB.Password, conexionDB.DataBase));

            try
            {
                //abrir la conexion
                conexionsql.Open();

                //establecer el comando sql server
                var comandoSql = new SqlCommand("[spinsertar_presentacion]", conexionsql);
                comandoSql.CommandType = CommandType.StoredProcedure;

                //parametrisacion de consultas sqlserver para evitar sql injection
                var paridpresentacion = new SqlParameter("@idpresentacion", SqlDbType.Int);
                paridpresentacion.Direction = ParameterDirection.Output;
                comandoSql.Parameters.Add(paridpresentacion);

                var parnombre = new SqlParameter("@nombre", SqlDbType.VarChar, 50);
                parnombre.Value = presentacion.Nombre;
                comandoSql.Parameters.Add(parnombre);

                var pardescripcion = new SqlParameter("@descripcion", SqlDbType.VarChar, 256);
                pardescripcion.Value = presentacion.Descripcion;
                comandoSql.Parameters.Add(pardescripcion);


                //ejecucion del comando
                Respuesta = comandoSql.ExecuteNonQuery() == 1 ? "OK" : "No se puede Insertar el Registro";

            }
            catch (Exception ex) 
            {
                Respuesta = ex.Message;
            }
            finally
            {
                if(conexionsql.State == ConnectionState.Open)
                    conexionsql.Close();
            }
            return Respuesta;
        }
        #endregion
    }
}
