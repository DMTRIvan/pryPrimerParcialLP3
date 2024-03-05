using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SUELDOS
{
    public partial class Form1 : Form
    {
        public DataTable tablaEmpleados;
        public DataTable tablaAdelantos;
        public string SexoM;
        public string SexoF;
        public string DNI;
        public string mail;
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
            try
            {
                Empleados emp = new Empleados();
                Adelantos ade = new Adelantos();
                tablaEmpleados = emp.getAll();
                tablaAdelantos = ade.getAll();
                armarArbolito();
            }
            catch (Exception)
            {

                MessageBox.Show("Error en la base de datos");
            }
            


        }


        private void armarArbolito()
        {
            
            TreeNode abuelo;
            TreeNode padre;
            TreeNode hijo;
            SexoF = "F";
            SexoM = "M";
            abuelo = tv.Nodes.Add("Empleados");
            padre = abuelo.Nodes.Add("Femenino");

            foreach (DataRow empleado in tablaEmpleados.Rows)
            {
                if (empleado["sexo"].ToString() == SexoF)
                {
                    hijo = padre.Nodes.Add(empleado["nombre"].ToString());
                    hijo.Tag = empleado["dni"].ToString();
                }
            }

            padre = abuelo.Nodes.Add("Masculino");
            foreach (DataRow empleadoM in tablaEmpleados.Rows)
            {

                if (empleadoM["sexo"].ToString() == SexoM)
                {
                    hijo = padre.Nodes.Add(empleadoM["nombre"].ToString());
                    hijo.Tag = empleadoM["dni"].ToString();
                    
                }
            }

        }

        public void BuscarMail(string DNI)
        {
            DataRow fila = tablaEmpleados.Rows.Find(DNI);
            mail = fila["email"].ToString();
            txtEmail.Text = mail;
        }

        public void AgregarItems(string DNI)
        {
            dgv.Rows.Clear();
            foreach (DataRow items in tablaAdelantos.Rows)
            {
                if (DNI == items["dni"].ToString())
                {
                    dgv.Rows.Add(items["adelanto"], items["fecha"], items["importe"]);
                    
                }
            }
            SumarImportes();
            //ListViewItem lvi = dgv.Items.Add("101");
            //lvi.SubItems.Add("ANITA");
            //lvi.SubItems.Add("BOBY");
        }

        public void SumarImportes()
        {
            //Esto lo suma directamente desde la grilla
            int Adelanto = 0;
            int Importe = 0;
            int Diferencia = 0;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    Adelanto += Convert.ToInt32(row.Cells[0].Value);
                    Importe += Convert.ToInt32(row.Cells[2].Value);
                }
            }
            ss.Items.Clear();
            ss.Items.Add("La suma de los Adelantos es: " + Adelanto);
            ss.Items.Add("La suma de los Importes es: " + Importe);
            Diferencia = Importe - Adelanto;
            ss.Items.Add("Lo que hay que pagar es: " + Diferencia);
            //MessageBox.Show("La suma de los Adelantos es: " + Adelanto);
            //MessageBox.Show("La suma de los Importes es: " + Importe);


        }
        private void tv_MouseClick(object sender, MouseEventArgs e)
        {

        }
        private void tv_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
        }

        private void tv_NodeMouseClick_1(object sender, TreeNodeMouseClickEventArgs e)
        {
            //Empleados empl = new Empleados();
            if (e.Node.Level == 2)
            {
                DNI = e.Node.Tag.ToString();
                BuscarMail(DNI);
                AgregarItems(DNI);
                //ss.Items.Clear();
                //ss.Items.Add(DNI);
                
            }
            if(e.Node.Level == 1)
            {
                txtEmail.Text = "";
                dgv.Rows.Clear();
                ss.Items.Clear();

            }
            if (e.Node.Level == 0)
            {
                txtEmail.Text = "";
                dgv.Rows.Clear();
                ss.Items.Clear();
            }


        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
