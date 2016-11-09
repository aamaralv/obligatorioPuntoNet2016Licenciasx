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

namespace Fomularios
{
    public partial class Configuraciones : Form
    {
        public Configuraciones()
        {
            String conexion = LeerConfiguracion();
            InitializeComponent();
            if (String.IsNullOrEmpty(conexion)) {
                tabControlAcciones.GetControl(0).Enabled = false;
                tabControlAcciones.GetControl(1).Enabled = false;
                tabControlAcciones.GetControl(2).Enabled = true;
                tabControlAcciones.GetControl(3).Enabled = false;
                tabControlAcciones.GetControl(4).Enabled = false;
            }
            

        }



        private void Acciones_Load(object sender, EventArgs e)
        {
            this.tabControlAcciones.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdb = new FolderBrowserDialog();
            if (fdb.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                MessageBox.Show(fdb.SelectedPath);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
           OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "BAK|*.bak";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show("File Name:"+ ofd.FileName+ " Safe File Name:"+ ofd.SafeFileName);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "BAK|*.bak";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show("File Name:" + ofd.FileName + " Safe File Name:" + ofd.SafeFileName);
            }
        }

        private void lblpassusuario_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdb = new FolderBrowserDialog();
            if (fdb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show(fdb.SelectedPath);
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

        private void chkEsLocal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEsLocal.CheckState == CheckState.Checked)
            {
                txbUbicacion.Text = ".";
                txbUbicacion.Enabled = false;
            }
            else {
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
        
            finally {
                txbMensajeBD.Text = mensaje;
                myConnection.Close();
            }

        }
        private String armarString() {
            StringBuilder str = new StringBuilder();
            str.Append("Data Source =");
            str.Append(txbUbicacion.Text);
            str.Append("; Initial Catalog =");
            str.Append(txbBase.Text);
            if (String.IsNullOrEmpty(txbUsuario.Text) && String.IsNullOrEmpty(txbContraseña.Text)) {
                str.Append("; Integrated Security = True");
            }
            else {
                str.Append("User Id=");
                str.Append(txbUsuario.Text);
                str.Append("; Password = ");
                str.Append(txbContraseña.Text);
            }
            Console.WriteLine(str.ToString());
            return str.ToString(); 
        }
    }
}
