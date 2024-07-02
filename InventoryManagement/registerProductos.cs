using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                }

            }catch(Exception ex) 
            {
                MessageBox.Show("sucedio un error al registrar el producto"+ ex.Message + ex.StackTrace);
            }
        }
    }
}
