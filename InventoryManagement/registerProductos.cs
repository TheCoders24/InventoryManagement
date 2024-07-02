using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaDatos;
using CapaNegocio;

namespace InventoryManagement
{
    public partial class registerProductos : Form
    {
        public registerProductos()
        {
            InitializeComponent();
        }

        private void btnRegisterProduct_Click(object sender, EventArgs e)
        {
            try
            {
                string Respuesta = "";
                if (txtNombreProducto.Text == string.Empty)
                {
                    MessageBox.Show("Falta ingresar algunos datos" + "Ingresa el Nombre" + txtNombreProducto);
                }
                else if(txtDescripcion.Text == string.Empty){
                    MessageBox.Show("Falta ingresar algunos datos" + "seleccione la Descripcion" + txtDescripcion);
                }else if (txtProductID.Text == string.Empty)
                {
                    MessageBox.Show("Falta ingresar algunos datos" + "seleccione el Productos ID" + txtProductID);
                }else if (txtPrecio.Text == string.Empty)
                {
                    MessageBox.Show("Falta ingresar algunos datos" + "selecione el Precio " + txtPrecio);
                }
                try
                {
                   
                }
                catch (Exception ex) {
                    MessageBox.Show("Sucedio un Error al Registrar el Producto" + ex.Message);
                }
            }catch(Exception ex) 
            {
                MessageBox.Show("sucedio un error al registrar el producto"+ ex.Message + ex.StackTrace);
            }
        }
    }
}
