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
    public partial class RegistrarUser : Form
    {
        public RegistrarUser()
        {
            InitializeComponent();
        }

        private void RegistrarUser_Load(object sender, EventArgs e)
        {

        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            int userid = Convert.ToInt32(txtuserid.Text);
            string usuario = txtusuario.Text;
            string password = txtpassword.Text;
            int roleid = Convert.ToInt32(txtroleid.Text);

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(txtpassword.Text))
                MessageBox.Show("ingrese el usuario y contrasena");
            else
                MessageBox.Show("sesion iniciada correctamente");

        }
    }
}
