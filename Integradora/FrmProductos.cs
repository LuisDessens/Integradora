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
    public partial class FrmProductos : Form
    {
        public FrmProductos()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            MySqlConnection cn = new MySqlConnection("host=localhost; uid=root; pwd=secret; database=base_integ;");
            MySqlCommand cmd = new MySqlCommand("Select * from productos where producto like '%" + txtBuscar.Text.Trim() + "%' limit 100", cn);
            cn.Open();
            MySqlDataReader leer = cmd.ExecuteReader();
            listBox1.Items.Clear();
            while (leer.Read())
            {
                listBox1.Items.Add(string.Format("{0,-11} {1,-40} {2,-15} {3,-11} {4,-40} {5,-40} {6,-20}.", leer.GetString(0), leer.GetString(1), leer.GetString(2), leer.GetString(3), leer.GetString(4), leer.GetString(5), leer.GetString(6)));
            }
            cn.Close();
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                txtIdProd.Text = listBox1.Items[listBox1.SelectedIndex].ToString().Substring(0, 11).Trim();
                txtProducto.Text = listBox1.Items[listBox1.SelectedIndex].ToString().Substring(12, 40).Trim();
                txtPrecio.Text = listBox1.Items[listBox1.SelectedIndex].ToString().Substring(53, 15).Trim();
                txtCan.Text = listBox1.Items[listBox1.SelectedIndex].ToString().Substring(69, 11).Trim();
                cmbEstilo.Text = listBox1.Items[listBox1.SelectedIndex].ToString().Substring(81, 40).Trim();
                cmbTalla.Text = listBox1.Items[listBox1.SelectedIndex].ToString().Substring(122, 40).Trim();
                cmbColor.Text = listBox1.Items[listBox1.SelectedIndex].ToString().Substring(163, 20).Trim();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            MySqlConnection cn = new MySqlConnection("host=localhost; uid=root; pwd=secret; database=base_integ;");
            MySqlCommand cmd = new MySqlCommand("insert into productos values(null,'" + txtProducto.Text + "','" + txtPrecio.Text + "','" + txtCan.Text + "','" + cmbEstilo.Text + "','" + cmbTalla.Text + "','" + cmbColor.Text + "')", cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            if (btnEnglish.Text.CompareTo("ENGLISH") == 0)
            {
                MessageBox.Show("PRODUCTO GUARDADO");
            }
            else
            {
                MessageBox.Show("PRODUCT SAVED");
            }
            btnBuscar_Click(sender, e);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            MySqlConnection cn = new MySqlConnection("host=localhost; uid=root; pwd=secret; database=base_integ;");
            cn.Open();
            MySqlCommand cmd = new MySqlCommand("update productos set producto='" + txtProducto.Text.Trim() + "', precio='" + txtPrecio.Text.Trim() + "', cantidad='" + txtCan.Text.Trim() + "', estilo='" + cmbEstilo.Text.Trim() + "', talla='" + cmbTalla.Text.Trim() + "', color='" + cmbColor.Text.Trim() + "'where id_producto=" + txtIdProd.Text.Trim(), cn);
            cmd.ExecuteNonQuery();
            cn.Close();
            if (btnEnglish.Text.CompareTo("ENGLISH") == 0)
            {
                MessageBox.Show("PRODUCTO MODIFICADO");
            }
            else
            {
                MessageBox.Show("PRODUCT MODIFIED");
            }
            btnBuscar_Click(sender, e);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            MySqlConnection cn = new MySqlConnection("host=localhost; uid=root; pwd=secret; database=base_integ;");
            cn.Open();
            MySqlCommand cmd = new MySqlCommand("delete from productos where id_producto=" + txtIdProd.Text, cn);
            MySqlDataReader leer = cmd.ExecuteReader();
            cn.Close();
            if (btnEnglish.Text.CompareTo("ENGLISH") == 0)
            {
                MessageBox.Show("PRODUCTO ELIMINADO");
            }
            else
            {
                MessageBox.Show("PRODUCT REMOVED");
            }
            btnBuscar_Click(sender, e);
        }

        private void btnEnglish_Click(object sender, EventArgs e)
        {
            if (btnEnglish.Text.CompareTo("ENGLISH") == 0)
            {
                FrmLogin.idioma = "ENGLISH";
                btnEnglish.Text = "ESPAÑOL";
                FrmProductos_Load(sender, e);
            }
            else
            {
                FrmLogin.idioma = "ESPAÑOL";
                btnEnglish.Text = "ENGLISH";
                FrmProductos_Load(sender, e);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
             Close();
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            if (FrmLogin.idioma == "ENGLISH")
            {
                btnEnglish.Text = "ESPAÑOL";
                btnBuscar.Text = "SEARCH";
                btnAgregar.Text = "ADD";
                btnModificar.Text = "MODIFY";
                btnEliminar.Text = "REMOVED";
                btnSalir.Text = "EXIT";

                label1.Text = "ID_PRODUCT";
                label2.Text = "PRODUCT";
                label3.Text = "PRICE";
                label4.Text = "AMOUNT";
                label5.Text = "STYLE";
                label6.Text = "SIZE";
                label7.Text = "COLOR";
                this.Text = "PRODUCTS";
            }
            else
            {
                btnEnglish.Text = "ENGLISH";
                btnBuscar.Text = "BUSCAR";
                btnAgregar.Text = "AGREGAR";
                btnModificar.Text = "MODIFICAR";
                btnEliminar.Text = "ELIMINAR";
                btnSalir.Text = "SALIR";

                label1.Text = "ID_PRODUCTO";
                label2.Text = "PRODUCTO";
                label3.Text = "PRECIO";
                label4.Text = "CANTIDAD";
                label5.Text = "ESTILO";
                label6.Text = "TALLA";
                label7.Text = "COLOR";
                this.Text = "PRODUCTOS";
            }
        }
    }
}
