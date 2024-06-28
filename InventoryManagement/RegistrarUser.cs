﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using conexionesDB;
using System.Windows.Forms;
using CapaNegocio;
using CapaDatos;

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

            //validacion los campos userid y roleid
            if (!int.TryParse(txtuserid.Text, out userid) || !int.TryParse(txtroleid.Text, out roleid))
            {
                MessageBox.Show("El ID de usuario y el ID de rol deben ser números enteros válidos.");
                return;
            }

            //validacion de entrada de usuario y contraseña
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(txtpassword.Text))
                MessageBox.Show("ingrese el usuario y contrasena");
            else
                MessageBox.Show("sesion iniciada correctamente");

            try
            {
                //manejamos la conexion de la base de datos y previene injection sql
                string datosregister = NUsuarios.insertarusuarios(userid, usuario, password, roleid);
                //MessageBox.Show(datosregister);
               
                DataTable usuarios = NUsuarios.mostrarusuarios();
                dataGridViewRegister.DataSource = usuarios;

            }
            catch (Exception ex)
            {
                MessageBox.Show("error al registrar el usuario" + ex.Message);
            }
        }
    }
}
