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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("La casilla ID no debe estar vacia");
            }
            else
            {
                string ID = textBox2.Text;
                SqlConnection conexion = new SqlConnection("server=LAPTOP-0CGV4VHK ; database=Abarrotes ; integrated security = true");
                conexion.Open();
                string cadena = "select Id, Descripcion, Precio, Cantidad from Productos where Id = " + ID;
                SqlCommand comando = new SqlCommand(cadena, conexion);
                SqlDataReader read = comando.ExecuteReader();
                if (read.Read())
                {
                    label12.Text = read["Descripcion"].ToString();
                    label13.Text = read["Precio"].ToString();
                    label14.Text = read["Cantidad"].ToString();
                }
                else
                    MessageBox.Show("No existe el producto");
                conexion.Close();
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("La casilla ID no debe estar vacia");
            }
            else {
                string desc = textBox7.Text;
                int prec = Convert.ToInt32(textBox6.Text);
                int cant = Convert.ToInt32(textBox5.Text);
                SqlConnection conexion = new SqlConnection("server=LAPTOP-0CGV4VHK ; database=Abarrotes ; integrated security = true");
                conexion.Open();
                int ID = Convert.ToInt32(textBox8.Text);
                string cadena = "select Id, Descripcion, Precio, Cantidad from Productos ";
                string cambiar = "update Productos set Descripcion = '" + desc + "', Precio = " + prec + ", Cantidad = " + cant + " where Id = " + ID;
                SqlCommand comando = new SqlCommand(cadena, conexion);
                SqlCommand comando2 = new SqlCommand(cambiar, conexion);
                comando.ExecuteNonQuery();
                comando2.ExecuteNonQuery();
                SqlDataAdapter dat = new SqlDataAdapter(comando);
                DataTable tabla = new DataTable();
                dat.Fill(tabla);
                dataGridView1.DataSource = tabla;
                MessageBox.Show("Producto Modificado");

            }

            

        }
    }
}
