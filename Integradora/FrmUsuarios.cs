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
    public partial class FrmUsuarios : Form
    {
        public FrmUsuarios()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ////select * from (nombre de la tabla) where nombre like'(las letras del nombre a buscar)%';
            MySqlConnection cn = new MySqlConnection("host=localhost; uid=root; pwd=secret; database=base_integ;");
            MySqlCommand cmd = new MySqlCommand("Select * from usuarios where usuario like '%" + txtBuscar.Text.Trim() + "%' limit 50", cn);
            cn.Open();
            MySqlDataReader leer = cmd.ExecuteReader();
            listBox1.Items.Clear();
            while (leer.Read())
            {
                listBox1.Items.Add(string.Format("{0,-11} {1,-40} {2,-12} {3,-15}.", leer.GetString(0), leer.GetString(1), leer.GetString(2), leer.GetString(3)));
            }
            cn.Close();
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                txtIdUsua.Text = listBox1.Items[listBox1.SelectedIndex].ToString().Substring(0, 11).Trim();
                txtUsuario.Text = listBox1.Items[listBox1.SelectedIndex].ToString().Substring(12, 40).Trim();
                txtClave.Text = listBox1.Items[listBox1.SelectedIndex].ToString().Substring(53, 12).Trim();
                cmbNivel.Text = listBox1.Items[listBox1.SelectedIndex].ToString().Substring(66, 15).Trim();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            MySqlConnection cn = new MySqlConnection("host=localhost; uid=root; pwd=secret; database=base_integ;");
            MySqlCommand cmd = new MySqlCommand("insert into usuarios values(null,'" + txtUsuario.Text + "'," + "'" + txtClave.Text + "'" + ",'" + cmbNivel.Text + "')", cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            if (btnEnglish.Text.CompareTo("ENGLISH") == 0)
            {
                MessageBox.Show("USUARIO GUARDADO");
            }
            else
            {
                MessageBox.Show("USER SAVED");
            }
            btnBuscar_Click(sender, e);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            //update (nombre de la tabla) set (nombre columna) = "mariaFuentes", where id_cliente = 1;
            MySqlConnection cn = new MySqlConnection("host=localhost; uid=root; pwd=secret; database=base_integ;");
            cn.Open();
            MySqlCommand cmd = new MySqlCommand("update usuarios set usuario='" + txtUsuario.Text.Trim() + "', clave='" + txtClave.Text.Trim() + "', nivel='" + cmbNivel.Text.Trim() + "' where id_usuario=" + txtIdUsua.Text.Trim(), cn);
            cmd.ExecuteNonQuery();
            cn.Close();
            if (btnEnglish.Text.CompareTo("ENGLISH") == 0)
            {
                MessageBox.Show("USUARIO MODIFICADO");
            }
            else
            {
                MessageBox.Show("USER MODIFIED");
            }
            btnBuscar_Click(sender, e);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //delete from (nombre de la table) where id_clave='202';
            MySqlConnection cn = new MySqlConnection("host=localhost; uid=root; pwd=secret; database=base_integ;");
            cn.Open();
            MySqlCommand cmd = new MySqlCommand("delete from usuarios where id_usuario=" + txtIdUsua.Text, cn);
            MySqlDataReader leer = cmd.ExecuteReader();
            cn.Close();
            if (btnEnglish.Text.CompareTo("ENGLISH") == 0)
            {
                MessageBox.Show("USUARIO ELIMINADO");
            }
            else
            {
                MessageBox.Show("USER REMOVED", "Excellent");
            }
            btnBuscar_Click(sender, e);
        }

        private void btnEnglish_Click(object sender, EventArgs e)
        {
            if (btnEnglish.Text.CompareTo("ENGLISH") == 0)
            {
                FrmLogin.idioma = "ENGLISH";
                btnEnglish.Text = "ESPAÑOL";
                FrmUsuarios_Load(sender, e);
            }
            else
            {
                FrmLogin.idioma = "ESPAÑOL";
                btnEnglish.Text = "ENGLISH";
                FrmUsuarios_Load(sender, e);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            if (FrmLogin.idioma == "ENGLISH")
            {
                btnEnglish.Text = "ESPAÑOL";
                btnBuscar.Text = "SEARCH";
                btnAgregar.Text = "ADD";
                btnModificar.Text = "MODIFY";
                btnEliminar.Text = "REMOVED";
                btnSalir.Text = "EXIT";

                label1.Text = "ID_USER";
                label2.Text = "USER";
                label3.Text = "KEY";
                label4.Text = "LEVEL";
            }
            else
            {
                btnEnglish.Text = "ENGLISH";
                btnBuscar.Text = "BUSCAR";
                btnAgregar.Text = "AGREGAR";
                btnModificar.Text = "MODIFICAR";
                btnEliminar.Text = "ELIMINAR";
                btnSalir.Text = "SALIR";

                label1.Text = "ID_USUARIO";
                label2.Text = "USUARIO";
                label3.Text = "CLAVE";
                label4.Text = "NIVEL";
            }
        }
    }
}
