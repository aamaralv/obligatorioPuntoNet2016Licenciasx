using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using Fomularios;
using System.IO;

namespace Fomularios
{
    public partial class Configuraciones : Form
    {
        private int maxidusuario = 0;
        private string carpetacliente = "";
        private WSConfiguraciones.WebService ws = new WSConfiguraciones.WebService();

        public Configuraciones()
        {
            String conexion = LeerConfiguracion();
            InitializeComponent();
            if (String.IsNullOrEmpty(conexion))
            {
                tabControlAcciones.GetControl(0).Enabled = false;
                tabControlAcciones.GetControl(1).Enabled = false;
                tabControlAcciones.GetControl(2).Enabled = true;
                tabControlAcciones.GetControl(3).Enabled = false;
                tabControlAcciones.GetControl(4).Enabled = false;
            }
            this.tabControlAcciones.Visible = true;

        }



        private void Acciones_Load(object sender, EventArgs e)
        {

            switch (tabControlAcciones.SelectedIndex)
            {
                case 0:
                    CargarUsuarios();
                    break;
                case 1:
                    CargarRoles();
                    CargarUsuariosPorRol();
                    break;
                case 2:
                    CargarConexiones();
                    break;
                case 3:
                    CargarRutas();
                    break;
                case 4:
                    CargarClientes();
                    break;
            }

        }

        private void btnSeleccionarNuevaRuta_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdb = new FolderBrowserDialog();
            if (fdb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!String.IsNullOrEmpty(fdb.SelectedPath))
                    lblRutaRepositorioNueva.Text = fdb.SelectedPath;
            }

        }

        private void btnSeleccionarLicencia_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!String.IsNullOrEmpty(ofd.FileName.ToString()))
                    lblRutaNuevaLicencia.Text = ofd.FileName.ToString();
            }
        }

        private void btnSeleccionarOI_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!String.IsNullOrEmpty(ofd.FileName.ToString()))
                    lblRutaNuevaOI.Text = ofd.FileName.ToString();
            }
        }



        private void btnSeleccionarCarpetaCliente_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdb = new FolderBrowserDialog();
            if (fdb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show(fdb.SelectedPath);
                this.carpetacliente = fdb.SelectedPath;


            }

        }
        static String LeerConfiguracion()
        {
            string result = "";
            try
            {
                var config = ConfigurationManager.ConnectionStrings[0].ConnectionString;
            }
            catch (ConfigurationErrorsException)
            {
                MessageBox.Show("Error Leyendo App.config");
            }
            return result;
        }

        private void CargarRoles()
        {
            /*Carga de combos para seleccion de roles*/
            WSConfiguraciones.Roles[] roles = ws.ListRoles();


            this.cmbroles.DataSource = roles;
            this.cmbroles.DisplayMember = "Descripcion";
            this.cmbroles.ValueMember = "IdRol";
            this.cmbroles.SelectedIndex = 0;


            this.cmbrolusuario.DataSource = roles;
            this.cmbrolusuario.DisplayMember = "Descripcion";
            this.cmbrolusuario.ValueMember = "IdRol";
            this.cmbrolusuario.SelectedIndex = 0;

        }

        private void CargarConexiones()
        {
            /*Carga de datos para la pestaña de conexiones*/
            WSConfiguraciones.Configuraciones[] configuraciones = ws.ListConfig();
            //String clave = @"Ruta\|Repositorio";  Estaria bueno poder buscar por clave (tendria que devolver clave, valor el procedimiento del ws)
            txbServidorCliente.Text = configuraciones[0].Valor;
            lblRutaRepositorio.Text = configuraciones[9].Valor;
            lblRutaOI.Text = configuraciones[8].Valor;
            lblRutaLicencia.Text = configuraciones[7].Valor;

        }

        private void CargarRutas()
        {
            /*Carga de datos para la pestaña de rutas*/

        }

        private void RegistrarUsuario()
        {


            ws.AddUsuarios(maxidusuario + 1, this.txtnombreusuario.Text, txtloginusuario.Text, txtcontraseñausuario.Text, txtcorreousuario.Text);

            DialogResult res = MessageBox.Show("Usuario se registro con exito!", "Configuracion", MessageBoxButtons.OK);


        }

        private void CargarUsuarios()
        {
            this.dataGridViewUsuarios.Enabled = true;

            WSConfiguraciones.Usuario[] usuarios = ws.ListUsuarios();
            this.dataGridViewUsuarios.Rows.Clear();

            foreach (WSConfiguraciones.Usuario usuario in usuarios)
            {
                if (maxidusuario < usuario.IdUsuario)
                {
                    maxidusuario = usuario.IdUsuario;
                }
                //Console.WriteLine("numero " + usuario.IdUsuario);
                WSConfiguraciones.Roles[] roles = ws.ListRol(usuario.IdUsuario);

                foreach (WSConfiguraciones.Roles rol in roles)
                {
                    this.dataGridViewUsuarios.Rows.Insert(this.dataGridViewUsuarios.NewRowIndex, usuario.IdUsuario, usuario.Nombre, usuario.Usuario1, rol.Descripcion, usuario.Correo);

                }


            }

        }


        private void CargarClientes()
        {
            this.dataGridViewUsuarios.Enabled = true;

            WSConfiguraciones.Clientes[] clientes = ws.ListClientes();
            this.dataGridViewregistrados.Rows.Clear();

            foreach (WSConfiguraciones.Clientes cliente in clientes)
            {

                this.dataGridViewregistrados.Rows.Insert(this.dataGridViewregistrados.NewRowIndex, cliente.Nombre, cliente.Carpeta);


            }

        }

        private void CargarUsuariosPorRol()
        {
            this.dataGridViewUsuarios.Enabled = true;

            WSConfiguraciones.Usuario[] usuarios = ws.ListUsuarios();
            this.lbxusuariosasignados.Items.Clear();
            this.lbxusuariosinrol.Items.Clear();

            foreach (WSConfiguraciones.Usuario usuario in usuarios)
            {
                if (maxidusuario < usuario.IdUsuario)
                {
                    maxidusuario = usuario.IdUsuario;
                }
                Console.WriteLine("numero " + usuario.IdUsuario);
                WSConfiguraciones.Roles[] roles = ws.ListRol(usuario.IdUsuario);


                foreach (WSConfiguraciones.Roles rol in roles)
                {

                    Console.WriteLine("this.cmbroles.SelectedValue.ToString() " + this.cmbroles.SelectedValue.ToString() + "rol " + rol.IdRol);
                    if (this.cmbroles.SelectedValue.ToString().Equals(rol.IdRol.ToString()))
                    {
                        Console.WriteLine("numeroa " + usuario.IdUsuario);
                        this.lbxusuariosasignados.Items.Add(usuario);
                        this.lbxusuariosasignados.DisplayMember = "Nombre";
                        this.lbxusuariosasignados.ValueMember = "IdUsuario";
                    }

                }


                if (roles.Length == 0)
                {
                    Console.WriteLine("numerob " + usuario.IdUsuario);
                    this.lbxusuariosinrol.Items.Add(usuario);
                    this.lbxusuariosinrol.DisplayMember = "Nombre";
                    this.lbxusuariosinrol.ValueMember = "IdUsuario";
                }

            }


        }


        private void AsignarRolUsuario()
        {


            int idrol = int.Parse(this.cmbroles.SelectedValue.ToString());

            if (this.lbxusuariosinrol.SelectedItem != null)
            {

                WSConfiguraciones.Usuario usuario = (WSConfiguraciones.Usuario)this.lbxusuariosinrol.SelectedItem;
                int idusuario = usuario.IdUsuario;
                Console.WriteLine("id usuario " + usuario.IdUsuario + " idrol " + idrol);

                ws.AddRol(idrol, idusuario);

                this.lbxusuariosinrol.Items.Remove(usuario);
                this.lbxusuariosasignados.Items.Add(usuario);
                this.lbxusuariosasignados.DisplayMember = "Nombre";
                this.lbxusuariosasignados.ValueMember = "IdUsuario";


                DialogResult res = MessageBox.Show("Se asigno rol al usuario exito!", "Configuracion", MessageBoxButtons.OK);
            }
            else {
                DialogResult res = MessageBox.Show("Seleccione un usuario!", "Configuracion", MessageBoxButtons.OK);
            }


        }

        private void RemoverRolUsuario()
        {


            int idrol = int.Parse(this.cmbroles.SelectedValue.ToString());

            if (this.lbxusuariosasignados.SelectedItem!= null)
            {
                WSConfiguraciones.Usuario usuario = (WSConfiguraciones.Usuario)this.lbxusuariosasignados.SelectedItem;
                int idusuario = usuario.IdUsuario;
                Console.WriteLine("id usuario " + usuario.IdUsuario + " idrol " + idrol);

                ws.RemoveRol(idrol, idusuario);

                this.lbxusuariosasignados.Items.Remove(usuario);
                this.lbxusuariosinrol.Items.Add(usuario);
                this.lbxusuariosinrol.DisplayMember = "Nombre";
                this.lbxusuariosinrol.ValueMember = "IdUsuario";



                DialogResult res = MessageBox.Show("Seleccione un usuario!", "Configuracion", MessageBoxButtons.OK);
            }
            else {
                DialogResult res = MessageBox.Show("Seleccione un usuario!", "Configuracion", MessageBoxButtons.OK);
            }


        }

        private void RegistrarCliente()
        {


            if (this.txtnombrecliente.Text.Equals(String.Empty) || this.carpetacliente.Equals(String.Empty))
            {
                DialogResult res = MessageBox.Show("Los campos no pueden estar vacios,verifique por por favor!", "Configuracion", MessageBoxButtons.OK);
            }
            else
            {
                ws.AddClientes(this.txtnombrecliente.Text, this.carpetacliente);


                DialogResult res = MessageBox.Show("Se registro el cliente con exito!", "Configuracion", MessageBoxButtons.OK);
            }

        }


        private void chkEsLocal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEsLocal.CheckState == CheckState.Checked)
            {
                txbUbicacion.Text = ".";
                txbUbicacion.Enabled = false;
            }
            else
            {
                txbUbicacion.Text = "";
                txbUbicacion.Enabled = true;
            }

        }

        private void btnProbarBD_Click(object sender, EventArgs e)
        {
            String connectionString = armarString();
            SqlConnection myConnection = null;
            String mensaje = null;
            try
            {
                myConnection = new SqlConnection(connectionString);
                myConnection.Open();
                mensaje = "OK";
                tabControlAcciones.GetControl(0).Enabled = true;
                tabControlAcciones.GetControl(1).Enabled = true;
                tabControlAcciones.GetControl(3).Enabled = true;
                tabControlAcciones.GetControl(4).Enabled = true;

                CargarConexiones();


            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 4060:
                        mensaje = "Base de datos inválida.";
                        break;
                    case 18456:
                        mensaje = "Fallo al loguearse.";
                        break;
                    default:
                        mensaje = "Error al intentar abrir la BD.";
                        break;
                }
            }

            finally
            {
                txbMensajeBD.Text = mensaje;
                myConnection.Close();
            }

        }
        private String armarString()
        {
            StringBuilder str = new StringBuilder();
            str.Append("Data Source =");
            str.Append(txbUbicacion.Text);
            str.Append("; Initial Catalog =");
            str.Append(txbBase.Text);
            if (String.IsNullOrEmpty(txbUsuario.Text) && String.IsNullOrEmpty(txbContraseña.Text))
            {
                str.Append("; Integrated Security = True");
            }
            else
            {
                str.Append("User Id=");
                str.Append(txbUsuario.Text);
                str.Append("; Password = ");
                str.Append(txbContraseña.Text);
            }
            str.Append(";Connection Timeout = 5");
            return str.ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            RegistrarUsuario();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Desea cerrar el programa ?", "Configuracion", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
                this.Close();
        }

        private void cmbroles_SelectedIndexChanged(object sender, EventArgs e)
        {

            CargarUsuariosPorRol();


        }

        private void btnasignarrol_Click(object sender, EventArgs e)
        {
            AsignarRolUsuario();
        }


        private void btnconfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                RegistrarCliente();
                CargarClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnConfirmarNuevaRuta_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(lblRutaRepositorioNueva.Text))
                    MessageBox.Show("Seleccione una ruta antes de continuar.");
                else
                {

                    String clave = @"Ruta\|Repositorio";
                    ws.UpdateConfig(clave, lblRutaRepositorioNueva.Text);
                    CargarConexiones(); //CargarRutas();   Hay que mejorarlo porque esta chancho
                    MessageBox.Show("Actualización exitosa.");
                    lblRutaRepositorioNueva.Text = "";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnConfirmarLicencia_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(lblRutaNuevaLicencia.Text))
                    MessageBox.Show("Seleccione un archivo antes de continuar.");
                else
                {
                    if (String.IsNullOrEmpty(lblRutaRepositorio.Text))
                    {
                        MessageBox.Show("Por favor, indicar la ruta del repositorio.");
                    }
                    else
                    {
                        String clave = @"Ruta\|Licencia";
                        //CargarRutas();   Hay que mejorarlo porque esta chancho
                        string direccionOrigen = lblRutaNuevaLicencia.Text;
                        string direccionDestino = String.Format("{0}{1}{2}", lblRutaRepositorio.Text, @"\", lblRutaNuevaLicencia.Text.Substring(lblRutaNuevaLicencia.Text.LastIndexOf(@"\")).Replace(@"\", ""));
                        ws.UpdateConfig(clave, direccionDestino);
                        File.Copy(direccionOrigen, direccionDestino, true);
                        MessageBox.Show("La licencia por defecto fue actualizada.");
                        lblRutaNuevaLicencia.Text = "";
                        CargarConexiones();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnConfirmarOI_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(lblRutaNuevaOI.Text))
                    MessageBox.Show("Seleccione una archivo antes de continuar.");
                else
                {
                    if (String.IsNullOrEmpty(lblRutaRepositorio.Text))
                    {
                        MessageBox.Show("Por favor, indicar la ruta del repositorio.");
                    }
                    else
                    {
                        String clave = @"Ruta\|OI";


                        string direccionOrigen = lblRutaNuevaOI.Text;
                        string direccionDestino = String.Format("{0}{1}{2}", lblRutaRepositorio.Text, @"\", lblRutaNuevaOI.Text.Substring(lblRutaNuevaOI.Text.LastIndexOf(@"\")).Replace(@"\", ""));
                        ws.UpdateConfig(clave, direccionDestino);
                        File.Copy(direccionOrigen, direccionDestino, true);
                        MessageBox.Show("La licencia por defecto fue actualizada.");
                        lblRutaNuevaOI.Text = "";
                        CargarConexiones();


                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGuardarMail_Click(object sender, EventArgs e)
        {

        }

        private void btnquitarrol_Click(object sender, EventArgs e)
        {
            RemoverRolUsuario();
        }
    }
}
