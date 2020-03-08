using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Printing;


namespace Tienda_de_Abarrotes___Francisco_Armijo
{
    public partial class venta : Form
    {
        public venta()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection("server=LAPTOP-0CGV4VHK ; database=Abarrotes ; integrated security = true");
            conexion.Open();
            string cadena = "select Id, Descripcion, Precio, Cantidad from Venta ";
            string limpiar = "delete from Venta";
            SqlCommand eliminar = new SqlCommand(limpiar, conexion);
            eliminar.ExecuteNonQuery();
            SqlCommand comando = new SqlCommand(cadena, conexion);
            SqlDataAdapter dat = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            dat.Fill(tabla);
            dataGridView1.DataSource = tabla;
            string cadena2 = "select Id, Descripcion, Precio, Cantidad, Total from Venta ";
            SqlCommand comando2 = new SqlCommand(cadena2, conexion);
            SqlDataAdapter dat2 = new SqlDataAdapter(comando2);
            DataTable tabla2 = new DataTable();
            dat2.Fill(tabla2);
            dataGridView2.DataSource = tabla2;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || textBox1.Text=="")
            {
                MessageBox.Show("Debe ingresar un criterio y un identificador de busqueda");
            }
            else
            {
                SqlConnection conexion = new SqlConnection("server=LAPTOP-0CGV4VHK ; database=Abarrotes ; integrated security = true");
                conexion.Open();
                //conectar a sql
                string cadena = "select Id, Descripcion, Precio, Cantidad from Temp ";
                SqlCommand comando = new SqlCommand(cadena, conexion);
                SqlDataAdapter dat = new SqlDataAdapter(comando);
                DataTable tabla = new DataTable();
                //declarar el buscador y la condicion
                string buscar = comboBox1.Text;
                string condicion = textBox1.Text;
                //limpiar la tabla temporal
                string limpiar = "delete from Temp";
                SqlCommand eliminar = new SqlCommand(limpiar, conexion);
                eliminar.ExecuteNonQuery();
                if (buscar == "Id" || buscar == "Precio")
                {
                    //linea comandos sql para copiar de la tabla productos a la temporal para numero
                    string agregar = "insert into Temp (Id,Descripcion,Precio, Cantidad) select Id, Descripcion, Precio, Cantidad from Productos where " + buscar + " = " + condicion;
                    SqlCommand cargar = new SqlCommand(agregar, conexion);
                    cargar.ExecuteNonQuery();
                }
                else
                {
                    //agregar si el selector es string
                    string agregar = "insert into Temp (Id,Descripcion,Precio, Cantidad) select Id, Descripcion, Precio, Cantidad from Productos where " + buscar + " like '%" + condicion + "%'";
                    SqlCommand cargar = new SqlCommand(agregar, conexion);
                    cargar.ExecuteNonQuery();
                }


                dat.Fill(tabla);
                dataGridView1.DataSource = tabla;
            }
            
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = "Ingrese " + comboBox1.Text + " del producto:";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" && textBox3.Text=="")
            {
                MessageBox.Show("Debe llenar los datos ID y Cantidad ");
            } else 
            {
                SqlConnection conexion = new SqlConnection("server=LAPTOP-0CGV4VHK ; database=Abarrotes ; integrated security = true");
                conexion.Open();
                string ID = textBox2.Text;
                string cant = textBox3.Text;
                //listado de comandos para msotrar en la tabla
                string cadena = "select Id, Descripcion, Precio, Cantidad, Total from Venta ";
                SqlCommand comando = new SqlCommand(cadena, conexion);
                SqlDataAdapter dat = new SqlDataAdapter(comando);
                DataTable tabla = new DataTable();
                //comando para copiar del 
                string copiar = "insert into Venta (Id,Descripcion,Precio ) select Id, Descripcion, Precio from Productos where Id = " + ID;
                string cantidad = "update Venta set Cantidad = " + cant + " where Id = " + ID;
                string multi = "update Venta set Total = Precio*Cantidad where Id = " + ID;
                SqlCommand cargar = new SqlCommand(copiar, conexion);
                SqlCommand cargar2 = new SqlCommand(cantidad, conexion);
                SqlCommand cargar3 = new SqlCommand(multi, conexion);

                cargar.ExecuteNonQuery();
                cargar2.ExecuteNonQuery();
                cargar3.ExecuteNonQuery();
                dat.Fill(tabla);
                dataGridView2.DataSource = tabla;
            }
            
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection("server=LAPTOP-0CGV4VHK ; database=Abarrotes ; integrated security = true");
            conexion.Open();
            //limpiar venta
            string cadena = "select Id, Descripcion, Precio, Cantidad, Total from Venta ";
            SqlCommand comando = new SqlCommand(cadena, conexion);
            SqlDataAdapter dat = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            string limpiar = "delete from Venta";
            SqlCommand eliminar = new SqlCommand(limpiar, conexion);
            eliminar.ExecuteNonQuery();
            dat.Fill(tabla);
            dataGridView2.DataSource = tabla;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text=="")
            {
                MessageBox.Show("Ingrese el ID en el recuadro correspondiente ");
            }
            else
            {
                SqlConnection conexion = new SqlConnection("server=LAPTOP-0CGV4VHK ; database=Abarrotes ; integrated security = true");
                conexion.Open();
                string cadena = "select Id, Descripcion, Precio, Cantidad, Total from Venta ";
                SqlCommand comando = new SqlCommand(cadena, conexion);
                SqlDataAdapter dat = new SqlDataAdapter(comando);
                DataTable tabla = new DataTable();
                string id = textBox2.Text;
                string eliminar = "delete from Venta where Id = " + id;
                SqlCommand borrar = new SqlCommand(eliminar, conexion);
                borrar.ExecuteNonQuery();
                MessageBox.Show("Se eliminó este producto de la lista");
                dat.Fill(tabla);
                dataGridView2.DataSource = tabla;
            }
            
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //for busca linea por linea en la columna total y la va sumando a subtotal
            int subTotal = 0;
            int total = 0;
            double iva = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                subTotal += Convert.ToInt32(row.Cells["Total"].Value);
            }
            iva = subTotal * 0.18;
            total = (int)Math.Ceiling(subTotal + iva);
            label11.Text = "$ " + subTotal;
            label12.Text = "$ " + iva;
            label13.Text = "$ " + total;
        }

        private void button6_Click(object sender, EventArgs e)
        {
           

        }
    }
}
