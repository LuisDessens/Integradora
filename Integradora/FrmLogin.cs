using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // referencia a la libreria

namespace Integradora
{
    public partial class FrmLogin : Form
    {
        int intentos;
        int contador;
        bool salir;
        public static string idioma;
        public static Boolean flag = false;
        public FrmLogin()
        {
            InitializeComponent();
            intentos = 3;
            contador = 0;
            salir = true;
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            MySqlConnection cn = new MySqlConnection("host=localhost; uid=root; pwd=secret; database=base_integ;");
            cn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT nivel FROM usuarios WHERE usuario='" + txtUsuario.Text + "'AND clave='" + txtClave.Text + "' ", cn);
            MySqlDataReader leer = cmd.ExecuteReader();
            if (leer.Read())
            {
                FrmMenu.nivel = Convert.ToString(leer.GetString(0));
                salir = false;
                Close();
                if (rdbEspañol.Checked)
                {
                    FrmLogin.idioma = "ESPAÑOL";
                }
                else
                {
                    FrmLogin.idioma = "ENGLISH";
                }
                flag = true;
            }
            else
            {
                txtClave.Clear();
                MessageBox.Show("EL USUARIO Y/O LA CONTRASEÑA ES INCORRECTA, LE QUEDAN " + intentos + " INTENTOS", "ERROR DE AUTENTIFICACION");
                txtClave.Focus();
                cn.Close();
                contador++;
                intentos = intentos - 1;
            }
            if (contador == 4)
            {
                Application.Exit();
            }
        }

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (salir == true)
            {
                Application.Exit();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void rdbEspañol_CheckedChanged(object sender, EventArgs e)
        {
            this.Text = "INICIAR SESION";
            label1.Text = "USUARIO:";
            label2.Text = "CLAVE:";
            gpbIdioma.Text = "IDIOMA";
            btnEntrar.Text = "ENTRAR";
            btnSalir.Text = "SALIR";
        }

        private void rdbEnglish_CheckedChanged(object sender, EventArgs e)
        {
            this.Text = "LOG IN";
            label1.Text = "USER:";
            label2.Text = "KEY:";
            gpbIdioma.Text = "LENGUAGE";
            btnEntrar.Text = "ENTER";
            btnSalir.Text = "EXIT";
        }
    }
}
