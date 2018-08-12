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
    public partial class FrmREUsuarios : Form
    {
        string REUsuario = Directory.GetCurrentDirectory() + "\\ReportedeUsuarios.html";

        public FrmREUsuarios()
        {
            InitializeComponent();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("excel.EXE", "\"" + REUsuario + "\"");
        }

        private void btnGoogle_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.EXE", "\"" + REUsuario + "\"");
        }

        private void btnEnglish_Click(object sender, EventArgs e)
        {
            if (btnEnglish.Text.CompareTo("ENGLISH") == 0)
            {
                FrmLogin.idioma = "ENGLISH";
                btnEnglish.Text = "ESPAÑOL";
                FrmREUsuarios_Load(sender, e);
            }
            else
            {
                FrmLogin.idioma = "ESPAÑOL";
                btnEnglish.Text = "ENGLISH";
                FrmREUsuarios_Load(sender, e);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmREUsuarios_Load(object sender, EventArgs e)
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
            MySqlCommand cmd = new MySqlCommand("Select Id_Usuario, usuario, clave, nivel from usuarios", cn);
            MySqlDataReader leer = cmd.ExecuteReader();
            StreamWriter objWriter = new StreamWriter(REUsuario);
            if (FrmLogin.idioma == "ENGLISH")
            {
                objWriter.WriteLine("<html><head><title>REPORT OF USERS</title></head><BODY>REPORT OF USERS<br><br>");
                objWriter.WriteLine("<TABLE border=1 cellspacing=0><TBODY>");
                objWriter.WriteLine("<TR bgcolor=#fa560a'><TD>ID OF USER</TD><TD>USER</TD><TD>KEY</TD><TD>LEVEL</TD></TR>");
            }
            else
            {
                objWriter.WriteLine("<html><head><title>REPORTE DE USUARIOS</title></head><BODY>REPORTE DE USUARIOS<br><br>");
                objWriter.WriteLine("<TABLE border=1 cellspacing=0><TBODY>");
                objWriter.WriteLine("<TR bgcolor=#fa560a'><TD>ID DE USUARIO</TD><TD>USUARIO</TD><TD>CLAVE</TD><TD>NIVEL</TD></TR>");
            }
            while (leer.Read())
            {
                objWriter.WriteLine("<TR><TD>" + leer.GetString(0) + "</TD><TD>" + leer.GetString(1) + "</TD><TD>" + leer.GetString(2) + "</TD><TD>" + leer.GetString(3) + "</TD></TR>");
            }
            cn.Close();
            objWriter.WriteLine("</TBODY></TABLE></BODY></html>");
            objWriter.Close();
            Uri dir = new Uri(REUsuario);
            webBrowser1.Url = dir;
        }
    }
}
