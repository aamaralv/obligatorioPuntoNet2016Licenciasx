using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fomularios
{
    public partial class Acciones : Form
    {
        public Acciones()
        {
            InitializeComponent();
        }

        private void btningresar_Click(object sender, EventArgs e)
        {
            this.panelLogin.Visible = false;
            this.tabControlAcciones.Visible = true;
            this.Text = "Acciones";
            this.tabControlAcciones.Height = 500;
            this.tabControlAcciones.Width = 865;
            this.Width = 915;
            this.Height = 565;
            this.CenterToScreen();

        }

        private void Acciones_Load(object sender, EventArgs e)
        {
            this.tabControlAcciones.Visible = false;
            this.Text = "Login";
            this.Width =375;
            this.Height = 200;
            this.CenterToScreen();


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
    }
}
