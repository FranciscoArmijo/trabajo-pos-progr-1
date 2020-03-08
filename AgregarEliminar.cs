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

namespace Tienda_de_Abarrotes___Francisco_Armijo
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection("server=LAPTOP-0CGV4VHK ; database=Abarrotes ; integrated security = true");
            conexion.Open();
            string cadena = "select Id, Descripcion, Precio, Cantidad from Productos ";
            SqlCommand comando = new SqlCommand(cadena, conexion);
            SqlDataAdapter dat = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            dat.Fill(tabla);
            dataGridView1.DataSource = tabla;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            
            {
                MessageBox.Show("La casilla ID no debe estar vacia");
            }
            else
            {
                SqlConnection conexion = new SqlConnection("server=LAPTOP-0CGV4VHK ; database=Abarrotes ; integrated security = true");
                conexion.Open();
                string cadena = "select Id, Descripcion, Precio, Cantidad from Productos ";
                SqlCommand comando = new SqlCommand(cadena, conexion);
                SqlDataAdapter dat = new SqlDataAdapter(comando);
                DataTable tabla = new DataTable();
                //agregar elemento de la tabla
                if (radioButton1.Checked)
                {
                    //se cargan lso valores que hay en los textbox
                    string id = textBox1.Text;
                    string desc = textBox2.Text;
                    string prec = textBox3.Text;
                    string cant = textBox4.Text;

                    string agregar = "insert into Productos(Id,Descripcion,Precio, Cantidad) values (" + id + ",'" + desc + "'," + prec + ", " + cant + ")";
                    SqlCommand cargar = new SqlCommand(agregar, conexion);
                    cargar.ExecuteNonQuery();
                    MessageBox.Show("Los datos se guardaron correctamente");
                }
                //Eliminar registro segun el Id
                if (radioButton2.Checked)
                {
                    string id = textBox1.Text;
                    string eliminar = "delete from Productos where Id =" + id;
                    SqlCommand cargar = new SqlCommand(eliminar, conexion);
                    cargar.ExecuteNonQuery();
                    MessageBox.Show("Los datos se eliminaron correctamente");
                }
                //al final de ejecutar se muestra la tabla cuando se preciona le boton
                dat.Fill(tabla);
                dataGridView1.DataSource = tabla;

            }
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button1.Text = "Eliminar";
            if (radioButton2.Checked)
            {
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button1.Text = "Agregar";
            if (radioButton1.Checked)
            {
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
