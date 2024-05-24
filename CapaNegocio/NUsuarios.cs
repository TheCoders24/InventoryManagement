using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class NUsuarios
    {

        /*
      UserID INT PRIMARY KEY,
      Username VARCHAR(50) NOT NULL,
      Contraseña VARCHAR(50) NOT NULL,
      RoleID INT NOT NULL,
      FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
         */

        public NUsuarios() { }

        #region insertarusuarios
        public static string insertarusuarios(int userid,string username,string contraseña,int roleid) 
        {
            DUsuarios dUsuarios = new DUsuarios()
            {
               UserID = userid,
               UserName = username,
               Contraseña = contraseña,
               RoleID = roleid
            };

            return dUsuarios.insertarusuario(dUsuarios);
        }
        #endregion

        #region actulizarusuarios
        public static string actualizausuarios(int userid,string username,string contraseña,int roleid)
        {
            DUsuarios dUsuarios = new DUsuarios()
            {
                UserID= userid,
                UserName = username,
                Contraseña =contraseña,
                RoleID = roleid
            };
            return dUsuarios.ActualizarUsuario(dUsuarios);
        }
        #endregion

        #region eliminarusuarios

        public static string eliminarusuario(int userid)
        {
            DUsuarios dUsuarios = new DUsuarios()
            {
                UserID = userid
            };

            return dUsuarios.eliminarusuario(dUsuarios);

        }

        #endregion

        #region mostrarusuarios
        public static DataTable mostrarusuarios(DUsuarios dUsuarios)
        {
            return new DUsuarios().mostrarusuarios(dUsuarios);
        }
        #endregion

    }
}
