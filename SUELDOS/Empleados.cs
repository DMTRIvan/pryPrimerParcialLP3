using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.OleDb;
using System.Data;

namespace SUELDOS
{
    class Empleados
    {
        private OleDbConnection conector;
        private OleDbCommand comando;
        private OleDbDataAdapter adaptador;
        private DataTable tabla;
        public string email;

        public Empleados()
        {
            conector = new OleDbConnection(Properties.Settings.Default.CADENA);
            comando = new OleDbCommand();

            comando.Connection = conector;
            comando.CommandType = CommandType.TableDirect;
            comando.CommandText = "Empleados";

            adaptador = new OleDbDataAdapter(comando);
            tabla = new DataTable();
            adaptador.Fill(tabla);

            DataColumn[] dc = new DataColumn[1];
            dc[0] = tabla.Columns["dni"];
            tabla.PrimaryKey = dc;

        }
        public DataTable getAll()
        {
            return tabla;
        }

        public void BuscarMail(string DNI)
        {
            DataRow fila = tabla.Rows.Find(DNI);
            email = fila[email].ToString();
            
        }
    }
}
