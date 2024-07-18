using System;
using System.Collections.Generic;
using System.Data;
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
        #region variables_globales_conexiondb
        public static string Servidor {  get; set; }
        public static string user { get; set; }
        public static string password { get; set; }
        public static string database { get; set; }
        #endregion

        #region constructor
        public Darticulos() { }
        public Darticulos(int ProductID,string NombreProducto,string Descripcion,int precio,string textobuscar)
        {
            ProductoID = ProductID;
            NombreProductos = NombreProducto;
            Descripciones = Descripcion;
            precios = precio;
            TextoBuscar = textobuscar;

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
        public string TextoBuscar { get; set; }
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

                //Establecemos el comando sql 
                var comandosql = new SqlCommand("[spInsertarProducto]", conexionsql);
                comandosql.CommandType = System.Data.CommandType.StoredProcedure;

                //parametros para el comando de sql sel pocedimiento almacenado
                var productID = new SqlParameter("@ProductID", SqlDbType.Int);
                productID.Direction = ParameterDirection.Output;
                comandosql.Parameters.Add(productID);


                var Nombre_producto = new SqlParameter("@NombreProducto", SqlDbType.VarChar, 50);
                Nombre_producto.Value = darticulos.NombreProductos;
                comandosql.Parameters.Add(Nombre_producto);

                var Descripcion = new SqlParameter("@Descripcion", SqlDbType.VarChar, 200);
                Descripcion.Value = darticulos.Descripciones;
                comandosql.Parameters.Add(Descripcion);

                var precio = new SqlParameter("@Precio",SqlDbType.Int);
                precio.Direction = ParameterDirection.Output;
                comandosql.Parameters.Add(precio);

                respuesta = comandosql.ExecuteNonQuery() ==1 ? "Ok" :"no se puede insertar el registro";

            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                if (conexionsql.State == ConnectionState.Open)
                {
                    conexionsql.Close();
                }
            }
            return respuesta;
        }
        #endregion

        #region actualizar_productos
        public string ActualizarProductos(Darticulos darticulos)
        {
            string respuesta = "";
            conexionDB conexionDB = new conexionDB();
            var conexionsql = new SqlConnection(conexionDB.ConexionStatic(Servidor, user, password, database));

            try
            {

                conexionsql.Open();
                var comandosql = new SqlCommand("[spEditarProducto]", conexionsql);
                comandosql.CommandType = CommandType.StoredProcedure;

                //parametros para el comando de sql sel pocedimiento almacenado
                var productID = new SqlParameter("@ProductID", SqlDbType.Int);
                productID.Direction = ParameterDirection.Output;
                comandosql.Parameters.Add(productID);

                var Nombre_producto = new SqlParameter("@NombreProducto", SqlDbType.VarChar, 50);
                Nombre_producto.Value = darticulos.NombreProductos;
                comandosql.Parameters.Add(Nombre_producto);

                var Descripcion = new SqlParameter("@Descripcion", SqlDbType.VarChar, 200);
                Descripcion.Value = darticulos.Descripciones;
                comandosql.Parameters.Add(Descripcion);

                var precio = new SqlParameter("@Precio", SqlDbType.Int);
                precio.Direction = ParameterDirection.Output;
                comandosql.Parameters.Add(precio);

                respuesta = comandosql.ExecuteNonQuery() == 1 ? "Ok" : "no se puede insertar el registro";


            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                if (conexionsql.State == ConnectionState.Open)
                    conexionsql.Close();
            }
            return respuesta;   
        }
        #endregion

        #region EliminarProductos
        public string EliminarProductos(Darticulos darticulos)
        {
            string respuesta = "";
            conexionDB conexionDB = new conexionDB();
            var conexionsql = new SqlConnection(conexionDB.ConexionStatic(Servidor,user,password,database));
            try
            {

                conexionsql.Open();
                //establecemos el comando sql para el procedimientos almacenados
                var comandosql = new SqlCommand("[spEliminarProducto]", conexionsql);
                comandosql.CommandType = CommandType.StoredProcedure;

                //paramentros para el command sql del procedimientos almacenados
                var productid = new SqlParameter("@ProductID", SqlDbType.Int);
                productid.Value = darticulos.ProductoID;
                comandosql.Parameters.Add(productid);

                //ejecutamos del comando
                respuesta = comandosql.ExecuteNonQuery() == 1 ? "ok" : "no se puede eliminar el registro";

            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }finally
            {
                if(conexionsql.State == ConnectionState.Open)
                    conexionsql.Close();
            }
            return respuesta;
        }
        #endregion

        #region mostrar_productos
        public DataTable mostrar_productos()
        {
            conexionDB conexionDB = new conexionDB();
            var resultadotablas = new DataTable("Productos");
            var conexionsql = new SqlConnection(conexionDB.ConexionStatic(Servidor,user,password,database));
            try
            {
                var comandosql = new SqlCommand("[sp_MostrarProductos]", conexionsql);
                comandosql.CommandType = CommandType.StoredProcedure;    

                var sqlData = new SqlDataAdapter(comandosql);
                sqlData.Fill(resultadotablas);

            }catch(Exception)
            {
                resultadotablas = null;
            }
            return resultadotablas;
        }
        #endregion

        #region MetodoStockArticulos
        //Metodo Mostrar
        public DataTable StockArticulos()
        {
            //Cadena de conexion y DataTable (tabla)
            var resultadoTabla = new DataTable("Productos");
            var conexionSql = new SqlConnection(conexionDB.ConexionStatic(Servidor,user,password,database));
            try
            {
                var comandoSql = new SqlCommand("[MostrarProductos]", conexionSql);
                comandoSql.CommandType = CommandType.StoredProcedure;

                var SqlDat = new SqlDataAdapter(comandoSql);
                SqlDat.Fill(resultadoTabla);

            }
            catch (Exception)
            {
                resultadoTabla = null;
            }

            return resultadoTabla;


        }
        #endregion

        #region MetodoBuscarNombre
        //Metodo BuscarNombre
        public DataTable BuscarNombre(Darticulos Articulo)
        {
            //Cadena de conexion y DataTable (tabla)
            var resultadoTabla = new DataTable("articulo");
            var conexionSql = new SqlConnection(conexionDB.ConexionStatic(conexionDB.ServidorNo,conexionDB.Users, conexionDB.Users, conexionDB.Password));


            try
            {

                var comandoSql = new SqlCommand("[BuscarProductoPorNombre]", conexionSql);
                comandoSql.CommandType = CommandType.StoredProcedure;

                //Parametros
                var ParTextoBuscar = new SqlParameter("@textobuscar", SqlDbType.VarChar, 50);
                ParTextoBuscar.Value = Articulo.TextoBuscar;
                comandoSql.Parameters.Add(ParTextoBuscar);


                var SqlDat = new SqlDataAdapter(comandoSql);
                SqlDat.Fill(resultadoTabla);

            }
            catch (Exception)
            {
                resultadoTabla = null;
            }

            return resultadoTabla;
        }
        #endregion

    }
}
