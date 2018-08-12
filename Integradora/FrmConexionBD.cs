using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Integradora
{
    public partial class FrmConexionBD : Form
    {
        public static string host = "localhost";
        public static string uid = "root";
        public static string pwd = "1234";
        public static string database = "base_integ";
        public FrmConexionBD()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            host = txtHost.Text;
            uid = txtUsuario.Text;
            pwd = txtClave.Text;
            database = txtBD.Text;
            MessageBox.Show("Base de datos establecida");
            Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
