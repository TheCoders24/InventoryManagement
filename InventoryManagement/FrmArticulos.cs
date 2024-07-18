using CapaNegocio;
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
    public partial class FrmArticulos : Form
    {

        //variables para verificar si es un articulo que se va agregar o se editara 
        private bool  is_Nuevo = false;// -> Identificar si el Articulo de Agregara
        private bool is_Editar = false;// -> Identifica si se Editara un Articulo

        // Funcion para Limpiar Los Controles
        private void Limpiar_Textobox()
        {
            txtIdArticulo.Clear();
            txtIdCategoria.Clear();
            txtCodigos.Clear();
           
            txtNombre.Clear();
            txtBuscar.Clear();
            txtDescripcion.Clear();

            txtNombre.Clear();
            txtCodigos.Focus();
        }
        public FrmArticulos()
        {
            InitializeComponent();
        }
        private void FrmArticulos_Load(object sender, EventArgs e)
        {
            Mostrar();
            Habilitar_Botones();
            Habilitar_Controles(false);
        }

        //Habilitar Botones si se quiere editar o agregar un nuevo producto
        public void Habilitar_Botones()
        {
            //if (is_Editar ||  is_Nuevo)
            //{

            //    Habilitar_Botones();
            //    btnNuevo.Enabled = false;
            //    btnGuardar.Enabled = true;
            //    btnEditar.Enabled = false;
            //    btnCancelar.Enabled = true;

            //}
            //else
            //{
            //    Habilitar_Botones();

            //    btnNuevo.Enabled = true;
            //    btnGuardar.Enabled = false;
            //    btnEditar.Enabled = true;
            //    btnCancelar.Enabled = false;

            //}
        }

        // habilitar los controles del formulario para registrar o editar articulo
        private void Habilitar_Controles(bool valor)
        {

            txtCodigos.ReadOnly = !valor;
            txtNombre.ReadOnly = !valor;
            txtIdArticulo.ReadOnly = !valor;
            txtDescripcion.ReadOnly = !valor;

           
            btnCargar.Enabled = valor;
            btnLimpiar.Enabled = valor;

        }

        //Habilitamos los buttoon del formulario
        private void Habilitar_Button()
        {
            if (is_Nuevo || is_Editar)
            {
                Habilitar_Controles(true);
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = true;
                btnEditar.Enabled = false;
                btnCancelar.Enabled = true;
            }
            else
            {
                Habilitar_Controles(false);
                btnNuevo.Enabled = true;
                btnGuardar.Enabled = false;
                btnEditar.Enabled = true;
                btnCancelar.Enabled = false;
            }
        }

        //Ocultar Columnas
        private void OcultarColumnas()
        {
            //dataListado.Columns[0].Visible = false;
            //dataListado.Columns[1].Visible = false;
            //dataListado.Columns[6].Visible = false;
            //dataListado.Columns[8].Visible = false;
        }
        //Metodo Mostrar
        private void Mostrar()
        {
            this.dataListado.DataSource = Narticulo.Mostrar_Articulo();
            this.OcultarColumnas();
            lblTotal.Text = "Total Registros: " + dataListado.Rows.Count;
        }

        //Metodo BuscarNombre
        private void BuscarNombre()
        {
            this.dataListado.DataSource = Narticulo.BuscarNombre(txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total Registros: " + dataListado.Rows.Count;
        }

        #region FUNCION_PENDIENTE_REVISAR
        //FUNCION QUEDA PENDIENTE
        //Llenar los datos del combo box (presentaciones de los articulos) 
        //private void LlenarComboPresentacion()
        //{
        //    cbPresentacion.DataSource = NPresentacion.Mostrar();
        //    cbPresentacion.ValueMember = "idpresentacion";
        //    cbPresentacion.DisplayMember = "nombre";
        //}
        #endregion

        //Para llamar al desde el formulario FrmVistaCategoria
        private static FrmArticulos _Instancia;
        public static FrmArticulos GetInstancia()
        {
            if (_Instancia == null)
            {
                _Instancia = new FrmArticulos();
            }
            return _Instancia;
        }

        //Recibirá la categoria desde el formulario FrmVistaCategoria
        public void SetCategoria(string idcategoria, string nombre)
        {
            txtIdCategoria.Text = idcategoria;
           
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            is_Nuevo = true;
            is_Editar = false;
            Limpiar_Textobox();
            Habilitar_Botones();
            Habilitar_Controles(true);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta = "";

                if (txtNombre.Text == string.Empty)
                {
                    MessageBox.Show("Falta ingresar algunos datos.");
                    MessageBox.Show(txtNombre, "Ingrese un nombre");
                }
                //else if (txtIdCategoria.Text == string.Empty)
                //{
                //    MessageBox.Show("Falta ingresar algunos datos.");
                //    MessageBox.Show(txtCategoria, "Seleccione una Categoria");
                //}
                else if (txtCodigos.Text == string.Empty)
                {
                    MessageBox.Show("Falta ingresar algunos datos.");
                    MessageBox.Show(txtCodigos, "Ingrese un valor");
                }
                else
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();

                    //pxImagen.Image = Utilidades.CambiarTamanoImagen(pxImagen.Image, 50, 50);
                    //pxImagen.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png); //Formato de la imagen.

                    byte[] imagen = ms.GetBuffer();
                    int precio = 10;
                    if (is_Nuevo)
                    {
                        //int productid,string nombreproduct, string descripction, int precios

                        respuesta = Narticulo.Insertar_articulo(Convert.ToInt16(txtCodigos.Text), txtNombre.Text,txtDescripcion.Text,precio);
                    }
                    else
                    {
                        respuesta = Narticulo.Editar_Productos(Convert.ToInt16(txtCodigos.Text), txtNombre.Text, txtDescripcion.Text,precio);
                    }

                    if (respuesta.Equals("Ok"))
                    {
                        if (is_Nuevo)
                        {
                            MessageBox.Show("El artículo se agregó correctamente");
                        }
                        else
                        {
                            MessageBox.Show("La artículo se editó correctamente");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sucedio un Error");
                    }
                    is_Nuevo = false;
                    is_Editar = false;
                    Habilitar_Botones();
                    Limpiar_Textobox();
                    Mostrar();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!txtIdArticulo.Text.Equals(""))
            {
                is_Editar = true;
                Habilitar_Botones();
                Habilitar_Controles(true);
            }
            else
            {
                MessageBox.Show("Debe seleccionar primero un registro a editar desde la pestaña Listado");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            is_Nuevo = false;
            is_Editar = false;
            Habilitar_Controles(false);
            Habilitar_Botones();
            Limpiar_Textobox();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarNombre();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion =
                    MessageBox.Show("¿Realmente desea eliminar el/los artículos seleccionados?",
                    "Sistema de Ventas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (opcion == DialogResult.Yes)
                {
                    int IdCategoria = 0;
                    string respuesta = "";

                    foreach (DataGridViewRow fila in dataListado.Rows)
                    {
                        if (Convert.ToBoolean(fila.Cells[0].Value))
                        {
                            IdCategoria = Convert.ToInt32(fila.Cells[1].Value);
                            respuesta = Narticulo.Eliminar_Articulo(IdCategoria);

                            if (respuesta.Equals("Ok"))
                            {
                                MessageBox.Show("El artículo se elimino correctamente.");
                            }
                            else
                            {
                                MessageBox.Show(respuesta);
                            }
                        }
                    }

                    Mostrar();
                    chkEliminar.Checked = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            is_Nuevo = false;
            is_Editar = false;
            Habilitar_Controles(false);
            Habilitar_Botones();
            Limpiar_Textobox();
        }

        //desabilitado hazta la actualizacion final
        private void btnCargar_Click(object sender, EventArgs e)
        {
            //desabilitado hazta la actualizacion final
            MessageBox.Show("No Esta en Funcionamiento la Funcion Que quiere");
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarNombre();
        }


        //pendiente de resolver 
        private void btnBuscarCategoria_Click(object sender, EventArgs e)
        {
            //var formulario = new FrmVistaCategoriaArticulo();
            //formulario.ShowDialog();
        }





    }
}
