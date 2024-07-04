using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    public class Narticulo
    {
        #region Insertar_Articulo
        public static string Insertar_articulo(int productid,string nombreproduct, string descripction, int precios)
        {
            Darticulos articulo = new Darticulos()
            {
                ProductoID = productid,
                NombreProductos = nombreproduct,
                Descripciones = descripction,
                precios = precios,
            };
            return articulo.insertarproductos(articulo);
        }
        #endregion

        #region editar_Articulo
        public static string Editar_Productos(int productid, string nombreproduct, string descripction, int precios)
        {
            Darticulos articulos = new Darticulos()
            {
                ProductoID= productid,
                NombreProductos= nombreproduct,
                Descripciones = descripction,
                precios = precios
            };
            return articulos.ActualizarProductos(articulos);
        }
        #endregion

        #region Eliminar_Articulo
        public static string Eliminar_Articulo(int productid, string nombreproduct, string descripction, int precios)
        {
            Darticulos articulos = new Darticulos()
            {
                ProductoID = productid,
                NombreProductos = nombreproduct,
                Descripciones = descripction,
                precios = precios,
            };
            return articulos.EliminarProductos(articulos);
        }
        #endregion

        #region Mostrar_articulo
        public static DataTable Mostrar_Articulo()
        {
            return new Darticulos().mostrar_productos();
        }
        #endregion

        #region StockArticulos_articulos
        public static DataTable StockArticulos()
        {
            return new Darticulos().StockArticulos();
        }
        #endregion
    }
}
