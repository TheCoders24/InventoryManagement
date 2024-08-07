﻿using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using conexionesDB;
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
            try
            {
                string usuario = textBox1.Text;
                string password = textBox2.Text;

                //validar entradas de usuarios
                if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("porfavor ingrese el usuario y el password");
                    return;
                }
                //manejamos la conexion de la base de datos y previene injection sql 
                DataTable datos = NUsuarios.login(usuario.ToString(), password.ToString());
                if (datos == null || datos.Rows.Count == 0)
                {
                    FrmArticulos frmArticulos = new FrmArticulos();
                    frmArticulos.ShowDialog();
                    this.Close();
                }
                else
                    MessageBox.Show("Ocurrio un Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al intentar iniciar sesion"+ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegistrarUser registrarUser = new RegistrarUser();
            registrarUser.Show();
        }
    }
}
