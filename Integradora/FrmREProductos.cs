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
    public partial class FrmREProductos : Form
    {
        string REProductos = Directory.GetCurrentDirectory() + "\\ReportedeProductos.html";

        public FrmREProductos()
        {
            InitializeComponent();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("excel.EXE", "\"" + REProductos + "\"");
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void btnGoogle_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.EXE", "\"" + REProductos + "\"");
        }

        private void btnEnglish_Click(object sender, EventArgs e)
        {
            if (btnEnglish.Text.CompareTo("ENGLISH") == 0)
            {
                FrmLogin.idioma = "ENGLISH";
                btnEnglish.Text = "ESPAÑOL";
                FrmREProductos_Load(sender, e);
            }
            else
            {
                FrmLogin.idioma = "ESPAÑOL";
                btnEnglish.Text = "ENGLISH";
                FrmREProductos_Load(sender, e);
            }
        }

        private void FrmREProductos_Load(object sender, EventArgs e)
        {
            //IDIOMA
            if (FrmLogin.idioma == "ENGLISH")
            {

                this.Text = "REPORT PRODUCTS";
                btnEnglish.Text = "ESPAÑOL";
                btnSalir.Text = "EXIT";
            }
            else
            {
                this.Text = "REPORTE DE PRODUCTOS";
                btnEnglish.Text = "ENGLISH";
                btnSalir.Text = "SALIR";
            }
            //REPORTE
            MySqlConnection cn = new MySqlConnection("host=localhost; uid=root; pwd=secret; database=base_integ;");
            cn.Open();
            MySqlCommand cmd = new MySqlCommand("Select Id_producto, producto, precio, cantidad, estilo, talla, color from productos", cn);
            MySqlDataReader leer = cmd.ExecuteReader();
            StreamWriter objWriter = new StreamWriter(REProductos);
            if (FrmLogin.idioma == "ENGLISH")
            {
                objWriter.WriteLine("<html><head><title>REPORT OF PRODUCTS</title></head><BODY>REPORT OF PRODUCTS<br><br>");
                objWriter.WriteLine("<TABLE border=1 cellspacing=0><TBODY>");
                objWriter.WriteLine("<TR bgcolor=#fa560a'><TD>ID OF PRODUCT</TD><TD>PRODUCT</TD><TD>PRICE</TD><TD>AMOUNT</TD><TD>STYLE</TD><TD>SIZE</TD><TD>COLOR</TD></TR>");
            }
            else
            {
                objWriter.WriteLine("<html><head><title>REPORTE DE PRODUCTOS</title></head><BODY>REPORTE DE PRODUCTOS<br><br>");
                objWriter.WriteLine("<TABLE border=1 cellspacing=0><TBODY>");
                objWriter.WriteLine("<TR bgcolor=#fa560a'><TD>ID DE PRODUCTO</TD><TD>PPRODUCTO</TD><TD>PRECIO</TD><TD>CANTIDAD</TD><TD>ESTILO</TD><TD>TALLA</TD><TD>COLOR</TD></TR>");
            }
            while (leer.Read())
            {
                objWriter.WriteLine("<TR><TD>" + leer.GetString(0) + "</TD><TD>" + leer.GetString(1) + "</TD><TD>" + leer.GetString(2) + "</TD><TD>" + leer.GetString(3) + "</TD><TD>" + leer.GetString(4) + "</TD><TD>" + leer.GetString(5) + "</TD><TD>" + leer.GetString(6) + "</TD></TR>");
            }
            cn.Close();
            objWriter.WriteLine("</TBODY></TABLE></BODY></html>");
            objWriter.Close();
            Uri dir = new Uri(REProductos);
            webBrowser1.Url = dir;
        }
    }
}
