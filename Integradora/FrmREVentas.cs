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
    public partial class FrmREVentas : Form
    {
        string REVentas = Directory.GetCurrentDirectory() + "\\ReportedeVentas.html";

        public FrmREVentas()
        {
            InitializeComponent();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("excel.EXE", "\"" + REVentas + "\"");
        }

        private void btnGoogle_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.EXE", "\"" + REVentas + "\"");
        }

        private void btnEnglish_Click(object sender, EventArgs e)
        {
            if (btnEnglish.Text.CompareTo("ENGLISH") == 0)
            {
                //btnEnglish.Text = "Español";
                //btnSalir.Text = "Exit";
                FrmLogin.idioma = "ENGLISH";
                btnEnglish.Text = "ESPAÑOL";
                FrmREVentas_Load(sender, e);
            }
            else
            {
                //btnEnglish.Text = "English";
                //btnSalir.Text = "Salir";
                FrmLogin.idioma = "ESPAÑOL";
                btnEnglish.Text = "ENGLISH";
                FrmREVentas_Load(sender, e);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmREVentas_Load(object sender, EventArgs e)
        {
            //IDIOMA
            if (FrmLogin.idioma == "ENGLISH")
            {
                btnEnglish.Text = "ESPAÑOL";
                btnSalir.Text = "EXIT";
            }
            else
            {
                btnEnglish.Text = "ENGLISH";
                btnSalir.Text = "SALIR";
            }
            //REPORTE
            MySqlConnection cn = new MySqlConnection("host=" + FrmConexionBD.host + "; uid=" + FrmConexionBD.uid + "; pwd=" + FrmConexionBD.pwd + "; database=" + FrmConexionBD.database + ";");
            cn.Open();
            MySqlCommand cmd = new MySqlCommand("Select Id_venta, Id_producto, precio, Id_cliente, Id_usuario, fecha from ventas", cn);
            MySqlDataReader leer = cmd.ExecuteReader();
            StreamWriter objWriter = new StreamWriter(REVentas);
            if (FrmLogin.idioma == "ENGLISH")
            {
                objWriter.WriteLine("<html><head><title>REPORT OF SALES</title></head><BODY>REPORT OF SALES<br><br>");
                objWriter.WriteLine("<TABLE border=1 cellspacing=0><TBODY>");
                objWriter.WriteLine("<TR bgcolor=#fa560a'><TD>ID OF SALE</TD><TD>ID OF PRODUCT</TD><TD>PRICE OF SALE</TD><TD>ID OF CUSTOMER</TD><TD>ID OF USER</TD><TD>DATE OF SALE</TD></TR>");
            }
            else
            {
                objWriter.WriteLine("<html><head><title>REPORTE DE USUARIOS</title></head><BODY>REPORTE DE USUARIOS<br><br>");
                objWriter.WriteLine("<TABLE border=1 cellspacing=0><TBODY>");
                objWriter.WriteLine("<TR bgcolor=#fa560a'><TD>ID DE VENTA</TD><TD>ID DE PRODUCTO</TD><TD>PRECIO DE VENTA</TD><TD>ID DE CLIENTE</TD><TD>ID DE USUARIO</TD><TD>FECHA DE VENTA</TD></TR>");
            }
            while (leer.Read())
            {
                objWriter.WriteLine("<TR><TD>" + leer.GetString(0) + "</TD><TD>" + leer.GetString(1) + "</TD><TD>" + leer.GetString(2) + "</TD><TD>" + leer.GetString(3) + "</TD><TD>" + leer.GetString(4) + "</TD><TD>" + leer.GetString(5) + "</TD></TR>");
            }
            cn.Close();
            objWriter.WriteLine("</TBODY></TABLE></BODY></html>");
            objWriter.Close();
            Uri dir = new Uri(REVentas);
            webBrowser1.Url = dir;
        }
    }
}
