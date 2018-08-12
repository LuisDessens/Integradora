using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace Integradora
{
    public partial class FrmVEProducto : Form
    {
        string VEProducto = Directory.GetCurrentDirectory() + "\\VentasDeUnProducto.html";

        public FrmVEProducto()
        {
            InitializeComponent();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("excel.EXE", "\"" + VEProducto + "\"");
        }

        private void btnGoogle_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.EXE", "\"" + VEProducto + "\"");
        }

        private void btnEnglish_Click(object sender, EventArgs e)
        {
            if (btnEnglish.Text.CompareTo("ENGLISH") == 0)
            {
                FrmLogin.idioma = "ENGLISH";
                btnEnglish.Text = "ESPAÑOL";
                comboBox1_SelectedIndexChanged(sender, e);
            }
            else
            {
                FrmLogin.idioma = "ESPAÑOL";
                btnEnglish.Text = "ENGLISH";
                comboBox1_SelectedIndexChanged(sender, e);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmVEProducto_Load(object sender, EventArgs e)
        {
            //REPORTE
            MySqlConnection cn = new MySqlConnection("host=localhost; uid=root; pwd=secret; database=base_integ;");
            cn.Open();
            MySqlCommand cmd = new MySqlCommand("Select producto, Id_producto from productos", cn);
            MySqlDataReader lector = cmd.ExecuteReader();
            comboBox1.Items.Clear();
            while (lector.Read())
            {
                comboBox1.Items.Add(string.Format("{0,-40} {1,-11} ", lector.GetString(0), lector.GetString(1)));
            }
            cn.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //REPORTE COMBOBOX
            MySqlConnection cn = new MySqlConnection("host=localhost; uid=root; pwd=secret; database=base_integ;");
            cn.Open();
            MySqlCommand cmd = new MySqlCommand("select fecha, producto, productos.precio from productos, ventas where ventas.Id_producto=productos.Id_producto and ventas.Id_producto=" + comboBox1.SelectedItem.ToString().Substring(41, 11), cn);
            MySqlDataReader lector = cmd.ExecuteReader();
            StreamWriter objWriter = new StreamWriter(VEProducto);
            if (FrmLogin.idioma == "ENGLISH")
            {
                objWriter.WriteLine("<html><head><title>SALE OF A PRODUCT</title></head><BODY>SALE OF A PRODUCT<br><br>");
                objWriter.WriteLine("<TABLE border=1 cellspacing=0><TBODY>");
                objWriter.WriteLine("<TR bgcolor=#fa560a><TD>DATE OF SALE</TD><TD>PRODUCT</TD><TD>PRICE</TD></TR>");
            }
            else
            {
                objWriter.WriteLine("<html><head><title>VENTA DE UN PRODUCTO</title></head><BODY>VENTA DE UN PRODUCTO<br><br>");
                objWriter.WriteLine("<TABLE border=1 cellspacing=0><TBODY>");
                objWriter.WriteLine("<TR bgcolor=#fa560a><TD>FECHA DE VENTA</TD><TD>PRODUCTO</TD><TD>PRECIO</TD></TR>");
            }
            while (lector.Read())
            {
                objWriter.WriteLine("<TR><TD>" + lector.GetString(0) + "</TD><TD align=center>" + lector.GetString(1) + "</TD><TD align=right>" + lector.GetString(2) + "</TD></TR>");
            }
            cn.Close();
            objWriter.WriteLine("</TBODY></TABLE></BODY></html>");
            objWriter.Close();
            Uri dir = new Uri(VEProducto);
            webBrowser1.Url = dir;
            //IDIOMA
            if (FrmLogin.idioma == "ENGLISH")
            {
                this.Text = "SALE OF A PRODUCT";
                btnEnglish.Text = "ESPAÑOL";
                btnSalir.Text = "EXIT";
                label1.Text = "SEARCH PRODUCT";
            }
            else
            {
                this.Text = "VENTA DE UN PRODUCTO";
                btnEnglish.Text = "ENGLISH";
                btnSalir.Text = "SALIR";
                label1.Text = "BUSCAR PRODUCTO";
            }
        }
    }
}
