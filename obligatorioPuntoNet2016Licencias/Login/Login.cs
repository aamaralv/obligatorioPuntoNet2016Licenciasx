using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fomularios;
using System.Configuration;

namespace Login
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (validarUsuario(sender)) {
                if (validarContraseña(sender)) {
                    String usuario = LeerConfiguracion("usuario");
                    String contraseña = LeerConfiguracion("contraseña");
                    if (this.txbUsuario.Text.ToString() == usuario && this.txbContraseña.Text.ToString() == contraseña)
                    {
                        Fomularios.Configuraciones ventana = new Fomularios.Configuraciones();
                        this.Visible = false;
                        ventana.Show();
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña inválida.");
                    }
                }
            }

        }
        private bool validarUsuario(Object sender) {
            bool retorno = false;
            if (String.IsNullOrEmpty(txbUsuario.Text))
            {
                errorProvider1.SetError(txbUsuario, "El usuario no puede ser vacío.");
            }
            else
            {
                errorProvider1.SetError(txbUsuario, String.Empty);
                retorno = true;
            }
            return retorno;
        }
        private bool validarContraseña(Object sender)
        {
            bool retorno = false;
            if (String.IsNullOrEmpty(txbContraseña.Text))
                errorProvider1.SetError(txbContraseña, "La contraseña no puede ser vacía.");
            else {
                errorProvider1.SetError(txbUsuario, String.Empty);
                retorno = true;
            }
            return retorno;
        }

        static String LeerConfiguracion(string key)
        {
            string result = "";
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                result = appSettings[key] ?? "No se encontro";
                Console.WriteLine(result);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error Leyendo App.config");
            }
            return result;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
