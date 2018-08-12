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
    public partial class FrmTotalDVDCP : Form
    {
        string TotalDVDCP = Directory.GetCurrentDirectory() + "\\Toraldeventasdecadaproducto.html";

        public FrmTotalDVDCP()
        {
            InitializeComponent();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("excel.EXE", "\"" + TotalDVDCP + "\"");
        }

        private void btnGoogle_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.EXE", "\"" + TotalDVDCP + "\"");
        }

        private void btnEnglish_Click(object sender, EventArgs e)
        {
            if (btnEnglish.Text.CompareTo("ENGLISH") == 0)
            {
                FrmLogin.idioma = "ENGLISH";
                btnEnglish.Text = "ESPAÑOL";
                FrmTotalDVDCP_Load(sender, e);
            }
            else
            {
                FrmLogin.idioma = "ESPAÑOL";
                btnEnglish.Text = "ENGLISH";
                FrmTotalDVDCP_Load(sender, e);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmTotalDVDCP_Load(object sender, EventArgs e)
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
            MySqlCommand cmd = new MySqlCommand("select producto, count(Id_venta) as piezas, sum(productos.precio) as suma from productos, ventas where ventas.Id_producto=productos.Id_producto group by producto;", cn);
            MySqlDataReader lector = cmd.ExecuteReader();
            StreamWriter objWriter = new StreamWriter(TotalDVDCP);
            if (FrmLogin.idioma == "ENGLISH")
            {
                objWriter.WriteLine("<html><head><title>TOTAL OF SALES OF A PRODUCT</title></head><BODY>TOTAL OF SALES OF A PRODUCT<br><br>");
                objWriter.WriteLine("<TABLE border=1 cellspacing=0><TBODY>");
                objWriter.WriteLine("<TR bgcolor=#fa560a><TD>PRODUCT</TD><TD>PIECES</TD><TD>SUM</TD></TR>");
            }
            else
            {
                objWriter.WriteLine("<html><head><title>TOTAL DE VENTAS DE UN PRODUCTO</title></head><BODY>TOTAL DE VENTA DE UN PRODUCTO<br><br>");
                objWriter.WriteLine("<TABLE border=1 cellspacing=0><TBODY>");
                objWriter.WriteLine("<TR bgcolor=#fa560a><TD>PRODUCTO</TD><TD>PIESAS</TD><TD>SUMA</TD></TR>");
            }
            while (lector.Read())
            {
                objWriter.WriteLine("<TR><TD>" + lector.GetString(0) + "</TD><TD align=center>" + lector.GetString(1) + "</TD><TD align=right>" + lector.GetString(2) + "</TD></TR>");
            }
            cn.Close();
            objWriter.WriteLine("</TBODY></TABLE></BODY></html>");
            objWriter.Close();
            Uri dir = new Uri(TotalDVDCP);
            webBrowser1.Url = dir;
        }
    }
}
