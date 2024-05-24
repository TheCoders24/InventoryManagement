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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usuario = textBox1.Text;
            string password = textBox2.Text;

            DataTable datos = NUsuarios.login(usuario.ToString(),password.ToString());
            if (datos == null)
                MessageBox.Show("usuarios" + usuario.ToString() + "password" + password.ToString());
                
        }
    }
}
