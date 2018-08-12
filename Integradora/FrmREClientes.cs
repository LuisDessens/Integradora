using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Integradora
{
    public partial class FrmREClientes : Form
    {
        string REClientes = Directory.GetCurrentDirectory() + "\\ReportedeClientes.html";
        public FrmREClientes()
        {
            InitializeComponent();
        }


        private void btnExcel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("excel.EXE", "\"" + REClientes + "\"");
        }

        private void btnGoogle_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.EXE", "\"" + REClientes + "\"");
        }

        private void btnEnglish_Click(object sender, EventArgs e)
        {
            if (btnEnglish.Text.CompareTo("ENGLISH") == 0)
            {
                FrmLogin.idioma = "ENGLISH";
                btnEnglish.Text = "ESPAÑOL";
                FrmREClientes_Load(sender, e);
            }
            else
            {
                FrmLogin.idioma = "ESPAÑOL";
                btnEnglish.Text = "ENGLISH";
                FrmREClientes_Load(sender, e);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmREClientes_Load(object sender, EventArgs e)
        {
            //IDIOMA
            if (FrmLogin.idioma == "ENGLISH")
            {
                this.Text = "REPORT CUSTOMERS";
                btnEnglish.Text = "ESPAÑOL";
                btnSalir.Text = "EXIT";
            }
            else
            {
                this.Text = "REPORTE DE CLIENTES";
                btnEnglish.Text = "ENGLISH";
                btnSalir.Text = "SALIR";
            }
            //REPORTE
            MySqlConnection cn = new MySqlConnection("host=localhost; uid=root; pwd=secret; database=base_integ;");
            cn.Open();
            MySqlCommand cmd = new MySqlCommand("Select Id_Cliente, cliente, domicilio, telefono from clientes", cn);
            MySqlDataReader leer = cmd.ExecuteReader();
            StreamWriter objWriter = new StreamWriter(REClientes);
            if (FrmLogin.idioma == "ENGLISH")
            {
                objWriter.WriteLine("<html><head><title>REPORT OF CUSTOMERS</title></head><BODY>REPORT OF CUSTOMERS<br><br>");
                objWriter.WriteLine("<TABLE border=1 cellspacing=0><TBODY>");
                objWriter.WriteLine("<TR bgcolor=#fa560a'><TD>ID OF CUSTOMER</TD><TD>CUSTOMER</TD><TD>DIRECTION</TD><TD>PHONE</TD></TR>");
            }
            else
            {
                objWriter.WriteLine("<html><head><title>REPORTE DE CLIENTES</title></head><BODY>REPORTE DE CLIENTES<br><br>");
                objWriter.WriteLine("<TABLE border=1 cellspacing=0><TBODY>");
                objWriter.WriteLine("<TR bgcolor=#fa560a'><TD>ID DE CLIENTE</TD><TD>CLIENTE</TD><TD>DIRECCION</TD><TD>TELEFONO</TD></TR>");
            }
            while (leer.Read())
            {
                objWriter.WriteLine("<TR><TD>" + leer.GetString(0) + "</TD><TD>" + leer.GetString(1) + "</TD><TD>" + leer.GetString(2) + "</TD><TD>" + leer.GetString(3) + "</TD></TR>");
            }
            cn.Close();
            objWriter.WriteLine("</TBODY></TABLE></BODY></html>");
            objWriter.Close();
            Uri dir = new Uri(REClientes);
            webBrowser1.Url = dir;
        }
    }
}
