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
    public partial class FrmVEUsuario : Form
    {
        string VEUsuario = Directory.GetCurrentDirectory() + "\\VentasDeUnUsuario.html";

        public FrmVEUsuario()
        {
            InitializeComponent();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("excel.EXE", "\"" + VEUsuario + "\"");
        }

        private void btnGoogle_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.EXE", "\"" + VEUsuario + "\"");
        }

        private void btnEnglish_Click(object sender, EventArgs e)
        {
            if (btnEnglish.Text.CompareTo("ENGLISH") == 0)
            {
                FrmLogin.idioma = "ENGLISH";
                btnEnglish.Text = "ESPAÑOL";
                FrmVEUsuario_Load(sender, e);
            }
            else
            {
                FrmLogin.idioma = "ESPAÑOL";
                btnEnglish.Text = "ENGLISH";
                FrmVEUsuario_Load(sender, e);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmVEUsuario_Load(object sender, EventArgs e)
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
            MySqlCommand cmd = new MySqlCommand("Select fecha, producto, productos.precio, usuario from ventas, productos, usuarios where ventas.Id_producto=productos.Id_producto and ventas.Id_usuario=usuarios.Id_usuario order by fecha", cn);
            MySqlDataReader leer = cmd.ExecuteReader();
            StreamWriter objWriter = new StreamWriter(VEUsuario);
            if (FrmLogin.idioma == "ENGLISH")
            {
                objWriter.WriteLine("<html><head><title>SALE OF A PRODUCT</title></head><BODY>SALE OF A PRODUCT<br><br>");
                objWriter.WriteLine("<TABLE border=1 cellspacing=0><TBODY>");
                objWriter.WriteLine("<TR bgcolor=#fa560a'><TD>DATE OF VENTA</TD><TD>PRODUCT</TD><TD>PRICE OF SALE</TD><TD>USER</TD></TR>");
            }
            else
            {
                objWriter.WriteLine("<html><head><title>VENTA DE UN PRODUCTO</title></head><BODY>VENTA DE UN PRODUCTO<br><br>");
                objWriter.WriteLine("<TABLE border=1 cellspacing=0><TBODY>");
                objWriter.WriteLine("<TR bgcolor=#fa560a'><TD>FECHA DE VENTA</TD><TD>PRODUCTO</TD><TD>PRECIO DE VENTA</TD><TD>USUARIO</TD></TR>");
            }
            while (leer.Read())
            {
                objWriter.WriteLine("<TR><TD>" + leer.GetString(0) + "</TD><TD>" + leer.GetString(1) + "</TD><TD>" + leer.GetString(2) + "</TD><TD>" + leer.GetString(3) + "</TD></TR>");
            }
            cn.Close();
            objWriter.WriteLine("</TBODY></TABLE></BODY></html>");
            objWriter.Close();
            Uri dir = new Uri(VEUsuario);
            webBrowser1.Url = dir;
        }
    }
}
