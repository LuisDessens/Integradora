using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // referencia a la libreria

namespace Integradora
{
    public partial class FrmVentas : Form
    {
        public FrmVentas()
        {
            InitializeComponent();
        }

        private void btnEnglish_Click(object sender, EventArgs e)
        {
            if (btnEnglish.Text.CompareTo("ENGLISH") == 0)
            {
                FrmLogin.idioma = "ENGLISH";
                btnEnglish.Text = "ESPAÑOL";
                FrmVentas_Load(sender, e);
            }
            else
            {
                FrmLogin.idioma = "ESPAÑOL";
                btnEnglish.Text = "ENGLISH";
                FrmVentas_Load(sender, e);
            }
        }

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            //textbox ocultos
            txtCliente.Hide();
            txtProducto.Hide();
            txtUsuario.Hide();
            //IDIOMA
            if (FrmLogin.idioma == "ENGLISH")
            {
                btnEnglish.Text = "ESPAÑOL";
                btnBuscar.Text = "SEARCH";
                btnAgregar.Text = "ADD";
                btnModificar.Text = "MODIFY";
                btnEliminar.Text = "REMOVED";
                btnSalir.Text = "EXIT";

                label1.Text = "ID_SALE";
                label2.Text = "ID_PRODUCT";
                label3.Text = "PRICE";
                label4.Text = "ID_CUSTOMER";
                label5.Text = "ID_USER";
                label6.Text = "DATE";
            }
            else
            {
                btnEnglish.Text = "ENGLISH";
                btnBuscar.Text = "BUSCAR";
                btnAgregar.Text = "AGREGAR";
                btnModificar.Text = "MODIFICAR";
                btnEliminar.Text = "ELIMINAR";
                btnSalir.Text = "SALIR";

                label1.Text = "ID_VENTA";
                label2.Text = "ID_PRODUCTO";
                label3.Text = "PRECIO";
                label4.Text = "ID_CLIENTE";
                label5.Text = "ID_USUARIO";
                label6.Text = "FECHA";
            }

            //productos
            try
            {
                MySqlConnection cn = new MySqlConnection("host=" + FrmConexionBD.host + "; uid=" + FrmConexionBD.uid + "; pwd=" + FrmConexionBD.pwd + "; database=" + FrmConexionBD.database + ";");
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("select Id_producto, producto from productos", cn);
                MySqlDataReader lector = cmd.ExecuteReader();
                while (lector.Read())
                {
                    cmdIdProd.Items.Add(string.Format("{0,-12} {1,-40}", lector.GetString(0), lector.GetString(1)));

                }
                cn.Close();
                cmdIdProd.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //usuarios
            try
            {
                MySqlConnection cn = new MySqlConnection("host=" + FrmConexionBD.host + "; uid=" + FrmConexionBD.uid + "; pwd=" + FrmConexionBD.pwd + "; database=" + FrmConexionBD.database + ";");
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("select Id_usuario, usuario from usuarios", cn);
                MySqlDataReader lector = cmd.ExecuteReader();
                while (lector.Read())
                {
                    cmdIdUsuario.Items.Add(string.Format("{0,-12} {1,-40}", lector.GetString(0), lector.GetString(1)));
                }
                cn.Close();
                cmdIdUsuario.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //clientes
            try
            {
                MySqlConnection cn = new MySqlConnection("host=" + FrmConexionBD.host + "; uid=" + FrmConexionBD.uid + "; pwd=" + FrmConexionBD.pwd + "; database=" + FrmConexionBD.database + ";");
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("select Id_cliente, cliente  from clientes", cn);
                MySqlDataReader lector = cmd.ExecuteReader();
                while (lector.Read())
                {
                    cmdIdCliente.Items.Add(string.Format("{0,-12} {1,-60}", lector.GetString(0), lector.GetString(1)));
                }
                cn.Close();
                cmdIdCliente.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            MySqlConnection cn = new MySqlConnection("host=" + FrmConexionBD.host + "; uid=" + FrmConexionBD.uid + "; pwd=" + FrmConexionBD.pwd + "; database=" + FrmConexionBD.database + ";");
            MySqlCommand cmd = new MySqlCommand("Select * from ventas where Id_producto like '%" + txtBuscar.Text.Trim() + "%' limit 50", cn);
            cn.Open();
            MySqlDataReader leer = cmd.ExecuteReader();
            listBox1.Items.Clear();
            while (leer.Read())
            {
                listBox1.Items.Add(string.Format("{0,-20} {1,-20} {2,-15} {3,-20} {4,-20} {5,-30}", leer.GetString(0), leer.GetString(1), leer.GetString(2), leer.GetString(3), leer.GetString(4), leer.GetString(5)));
            }
            cn.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            MySqlConnection cn = new MySqlConnection("host=" + FrmConexionBD.host + "; uid=" + FrmConexionBD.uid + "; pwd=" + FrmConexionBD.pwd + "; database=" + FrmConexionBD.database + ";");
            MySqlCommand cmd = new MySqlCommand("insert into ventas values(null,'" + cmdIdProd.Text.Substring(0, 1) + "','" + txtPrecio.Text + "','" + cmdIdCliente.Text.Substring(0, 1) + "','" + cmdIdUsuario.Text.Substring(0, 1) + "','" + dtpFecha.Value.ToString("yyyy-MM-dd HH:mm:ss") + "')", cn); 
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            if (btnEnglish.Text.CompareTo("ENGLISH") == 0)
            {
                MessageBox.Show("VENTA GUARDADA");
            }
            else
            {
                MessageBox.Show("SALE SAVED");
            }

            btnBuscar_Click(sender, e);
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                txtIdVenta.Text = listBox1.Items[listBox1.SelectedIndex].ToString().Substring(0, 20).Trim();
                cmdIdProd.Text = listBox1.Items[listBox1.SelectedIndex].ToString().Substring(21, 20).Trim();
                txtPrecio.Text = listBox1.Items[listBox1.SelectedIndex].ToString().Substring(42, 15).Trim();
                cmdIdCliente.Text = listBox1.Items[listBox1.SelectedIndex].ToString().Substring(58, 20).Trim();
                cmdIdUsuario.Text = listBox1.Items[listBox1.SelectedIndex].ToString().Substring(79, 20).Trim();
                dtpFecha.Value = DateTime.Parse(listBox1.Items[listBox1.SelectedIndex].ToString().Substring(100, 30).Trim());
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            MySqlConnection cn = new MySqlConnection("host=" + FrmConexionBD.host + "; uid=" + FrmConexionBD.uid + "; pwd=" + FrmConexionBD.pwd + "; database=" + FrmConexionBD.database + ";");
            cn.Open();
            MySqlCommand cmd = new MySqlCommand("update ventas set Id_producto='" + cmdIdProd.Text.Trim() + "', precio='" + cmdIdProd.Text.Substring(0, 1) + "', Id_cliente='" + cmdIdCliente.Text.Substring(0, 1) + "', Id_usuario='" + cmdIdUsuario.Text.Substring(0, 1) + "', fecha='" + dtpFecha.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' where Id_venta=" + txtIdVenta.Text.Trim(), cn);
            cmd.ExecuteNonQuery();
            cn.Close();
            if (btnEnglish.Text.CompareTo("ENGLISH") == 0)
            {
                MessageBox.Show("VENTA MODIFICADA");
            }
            else
            {
                MessageBox.Show("SALE MODIFIED");
            }
            btnBuscar_Click(sender, e);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            MySqlConnection cn = new MySqlConnection("host=" + FrmConexionBD.host + "; uid=" + FrmConexionBD.uid + "; pwd=" + FrmConexionBD.pwd + "; database=" + FrmConexionBD.database + ";");
            cn.Open();
            MySqlCommand cmd = new MySqlCommand("delete from ventas where Id_venta=" + txtIdVenta.Text, cn);
            MySqlDataReader leer = cmd.ExecuteReader();
            cn.Close();
            if (btnEnglish.Text.CompareTo("ENGLISH") == 0)
            {
                MessageBox.Show("VENTA ELIMINADA");
            }
            else
            {
                MessageBox.Show("SALE REMOVED");
            }
            btnBuscar_Click(sender, e);
        }
    }
}
