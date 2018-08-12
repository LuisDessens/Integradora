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
    public partial class FrmClientes : Form
    {
        public FrmClientes()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            MySqlConnection cn = new MySqlConnection("host=" + FrmConexionBD.host + "; uid=" + FrmConexionBD.uid + "; pwd=" + FrmConexionBD.pwd + "; database=" + FrmConexionBD.database + ";");
            cn.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from clientes where cliente like '%" + txtBuscar.Text.Trim() + "%' limit 50", cn);
            MySqlDataReader leer = cmd.ExecuteReader();
            listBox1.Items.Clear();
            while (leer.Read())
            {
                listBox1.Items.Add(string.Format("{0,-11} {1,-60} {2,-60} {3,-20}.", leer.GetString(0), leer.GetString(1), leer.GetString(2), leer.GetString(3)));
            }
            cn.Close();
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                txtIdClien.Text = listBox1.Items[listBox1.SelectedIndex].ToString().Substring(0, 11).Trim();
                txtCliente.Text = listBox1.Items[listBox1.SelectedIndex].ToString().Substring(12, 60).Trim();
                txtDomi.Text = listBox1.Items[listBox1.SelectedIndex].ToString().Substring(73, 60).Trim();
                txtTel.Text = listBox1.Items[listBox1.SelectedIndex].ToString().Substring(134, 20).Trim();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            MySqlConnection cn = new MySqlConnection("host=" + FrmConexionBD.host + "; uid=" + FrmConexionBD.uid + "; pwd=" + FrmConexionBD.pwd + "; database=" + FrmConexionBD.database + ";");
            MySqlCommand cmd = new MySqlCommand("insert into clientes values(null,'" + txtCliente.Text + "'," + "'" + txtDomi.Text + "'" + ",'" + txtTel.Text + "')", cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            if (btnEnglish.Text.CompareTo("ENGLISH") == 0)
            {
                MessageBox.Show("CLIENTE GUARDADO");
            }
            else
            {
                MessageBox.Show("CUSTOMER SAVED");
            }

            btnBuscar_Click(sender, e);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            MySqlConnection cn = new MySqlConnection("host=" + FrmConexionBD.host + "; uid=" + FrmConexionBD.uid + "; pwd=" + FrmConexionBD.pwd + "; database=" + FrmConexionBD.database + ";");
            cn.Open();
            MySqlCommand cmd = new MySqlCommand("update clientes set cliente='" + txtCliente.Text.Trim() + "', domicilio='" + txtDomi.Text.Trim() + "', telefono='" + txtTel.Text.Trim() + "' where id_cliente=" + txtIdClien.Text.Trim(), cn);
            cmd.ExecuteNonQuery();
            cn.Close();
            if (btnEnglish.Text.CompareTo("ENGLISH") == 0)
            {
                MessageBox.Show("CLIENTE MODIFICADO");
            }
            else
            {
                MessageBox.Show("CUSTOMER MODIFIED");
            }
            btnBuscar_Click(sender, e);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            MySqlConnection cn = new MySqlConnection("host=" + FrmConexionBD.host + "; uid=" + FrmConexionBD.uid + "; pwd=" + FrmConexionBD.pwd + "; database=" + FrmConexionBD.database + ";");
            cn.Open();
            MySqlCommand cmd = new MySqlCommand("delete from clientes where id_Cliente=" + txtIdClien.Text, cn);
            MySqlDataReader leer = cmd.ExecuteReader();
            cn.Close();
            if (btnEnglish.Text.CompareTo("ENGLISH") == 0)
            {
                MessageBox.Show("CLIENTE ELIMINADO");
            }
            else
            {
                MessageBox.Show("CUSTOMER REMOVED");
            }
            btnBuscar_Click(sender, e);
        }

        private void btnEnglish_Click(object sender, EventArgs e)
        {
            if (btnEnglish.Text.CompareTo("ENGLISH") == 0)
            {
                FrmLogin.idioma = "ENGLISH";
                btnEnglish.Text = "ESPAÑOL";
                FrmClientes_Load(sender, e);

            }
            else
            {
                FrmLogin.idioma = "ESPAÑOL";
                btnEnglish.Text = "ENGLISH";
                FrmClientes_Load(sender, e);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            if (FrmLogin.idioma == "ENGLISH")
            {
                btnEnglish.Text = "ESPAÑOL";
                btnBuscar.Text = "SEARCH";
                btnAgregar.Text = "ADD";
                btnModificar.Text = "MODIFY";
                btnEliminar.Text = "REMOVED";
                btnSalir.Text = "EXIT";

                label1.Text = "ID_CUSTOMER";
                label2.Text = "CUSTOMER";
                label3.Text = "DIRECTION";
                label4.Text = "PHONE";
                this.Text = "CUSTOMERS";

            }
            else
            {
                btnEnglish.Text = "ENGLISH";
                btnBuscar.Text = "BUSCAR";
                btnAgregar.Text = "AGREGAR";
                btnModificar.Text = "MODIFICAR";
                btnEliminar.Text = "ELIMINAR";
                btnSalir.Text = "SALIR";

                label1.Text = "ID_CLIENTE";
                label2.Text = "CLIENTE";
                label3.Text = "DIRECCION";
                label4.Text = "TELEFONO";
                this.Text = "CLIENTES";
            }
        }
    }
}
