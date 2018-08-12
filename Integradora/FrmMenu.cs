using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Integradora
{
    public partial class FrmMenu : Form
    {
        public static string nivel;
        int contador;
        Timer timer = new Timer();
        public FrmMenu()
        {
            InitializeComponent();
            contador = 0;
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            if (contador == 0)
            {
                FrmLogin login = new FrmLogin();
                login.ShowDialog();
                contador = 1;
            }

            if (FrmLogin.idioma == "ENGLISH")
            {
                //File
                archivoToolStripMenuItem.Text = "FILE";
                clientesToolStripMenuItem.Text = "CUSTOMERS";
                productosToolStripMenuItem.Text = "PRODUCTS";
                usuariosToolStripMenuItem.Text = "USERS";
                ventasToolStripMenuItem.Text = "SALES";
                salirToolStripMenuItem.Text = "EXIT";

                //Reports
                reportesToolStripMenuItem.Text = "REPORTS";
                reporteDeClientesToolStripMenuItem.Text = "REPORT OF CUSTOMERS";
                reporteDeProductosToolStripMenuItem.Text = "REPORT OF PRODUCTS";
                reporteDeUsuariosToolStripMenuItem.Text = "REPORT OF USERS";
                reporteDeVentasToolStripMenuItem.Text = "REPORT OF SALES";
                ventasdeunproductotoolStripMenuItem.Text = "SALES OF A PRODUCT";
                totaldeventasporcadaproductoToolStripMenuItem.Text = "TOTAL SALES OF EACH PRODUCT";
                ventasdeunusuarioToolStripMenuItem.Text = "SALES OF A USER";

                //Configuration

                //Idiom
                idiomaToolStripMenuItem.Text = "LENGUAGE";
                englishToolStripMenuItem.Text = "ESPAÑOL";
                this.Text = "PRINCIPAL MENU";
            }
            else
            {
                archivoToolStripMenuItem.Text = "ARCHIVO";
                clientesToolStripMenuItem.Text = "CLIENTES";
                productosToolStripMenuItem.Text = "PRODUCTOS";
                usuariosToolStripMenuItem.Text = "USUARIOS";
                ventasToolStripMenuItem.Text = "VENTAS";
                salirToolStripMenuItem.Text = "SALIR";

                //Reportes
                reportesToolStripMenuItem.Text = "REPORTES";
                reporteDeClientesToolStripMenuItem.Text = "REPORTE DE CLIENTES";
                reporteDeProductosToolStripMenuItem.Text = "REPORTE DE PRODUCTOS";
                reporteDeUsuariosToolStripMenuItem.Text = "REPORTE DE USUARIOS";
                reporteDeVentasToolStripMenuItem.Text = "REPORTE DE VENTAS";
                ventasdeunproductotoolStripMenuItem.Text = "VENTA DE UN PRODUCTO";
                totaldeventasporcadaproductoToolStripMenuItem.Text = "TOTAL DE VENTAS DE CADA PRODUCTO";
                ventasdeunusuarioToolStripMenuItem.Text = "VENTAS DE UN USUARIO";

                //Configuracion

                //Idioma
                idiomaToolStripMenuItem.Text = "IDIOMA";
                englishToolStripMenuItem.Text = "ENGLISH";
                this.Text = "MENU PRINCIPAL";
            }
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmClientes clientes = new FrmClientes();
            clientes.Show();
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProductos productos = new FrmProductos();
            productos.Show();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nivel == "1")
            {
                FrmUsuarios usuario = new FrmUsuarios();
                usuario.Show();
            }
            else
            {
                if (englishToolStripMenuItem.Text.CompareTo("ENGLISH") == 0)
                {
                    MessageBox.Show("YOU DO NOT HAVE SUFFICIENT PRIVILEGES TO ACCESS THIS WINDOW");
                }
                else
                {
                    MessageBox.Show("USTED NO CUENTA CON SUFICIENTES PERMISOS PARA ENTRAR A ESTA VENTANA");
                }
            }
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVentas ventas = new FrmVentas();
            ventas.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (englishToolStripMenuItem.Text.CompareTo("ENGLISH") == 0)
            {
                FrmLogin.idioma = "ENGLISH";
                englishToolStripMenuItem.Text = "ESPAÑOL";
                FrmMenu_Load(sender, e);
            }
            else
            {
                FrmLogin.idioma = "ESPAÑOL";
                englishToolStripMenuItem.Text = "ENGLISH";
                FrmMenu_Load(sender, e);
            }
        }

        private void reporteDeClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmREClientes clientes = new FrmREClientes();
            clientes.Show();
        }

        private void reporteDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nivel == "1")
            {
                FrmREUsuarios REUsuarios = new FrmREUsuarios();
                REUsuarios.Show();
            }
            else
            {
                if (englishToolStripMenuItem.Text.CompareTo("ENGLISH") == 0)
                {
                    MessageBox.Show("YOU DO NOT HAVE SUFFICIENT PRIVILEGES TO ACCESS THIS WINDOW");
                }
                else
                {
                    MessageBox.Show("USTED NO CUENTA CON SUFICIENTES PERMISOS PARA ENTRAR A ESTA VENTANA");
                }
            }
        }

        private void reporteDeProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmREProductos productos = new FrmREProductos();
            productos.Show();
        }

        private void reporteDeVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmREVentas ventas = new FrmREVentas();
            ventas.Show();
        }

        private void ventasdeunproductotoolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVEProducto VEProducto = new FrmVEProducto();
            VEProducto.Show();
        }

        private void totaldeventasporcadaproductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTotalDVDCP TotalDVDCP = new FrmTotalDVDCP();
            TotalDVDCP.Show();
        }

        private void ventasdeunusuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVEUsuario VEUsuario = new FrmVEUsuario();
            VEUsuario.Show();
        }

        private void btnConexionBD_Click(object sender, EventArgs e)
        {
            FrmConexionBD cn = new FrmConexionBD();
            cn.ShowDialog();
        }

        private void FrmMenu_Activated(object sender, EventArgs e)
        {

            //if (frmlogin.flag == true)
            //{
            //        frmconexionbd bd = new frmconexionbd();
            //        bd.showdialog();
            //        frmlogin.flag = false;
            //        timer.stop();
            //}
        }
    }
}
