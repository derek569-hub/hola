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
using System.Xml.Serialization;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private List<Producto> productos = new List<Producto>();
        private const string RutaXml = "productos.xml";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out int id) &&
         double.TryParse(txtPrecio.Text, out double precio) &&
         !string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                productos.Add(new Producto { Id = id, Nombre = txtNombre.Text, Precio = precio });
                CargarProductosEnGrid();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Por favor, ingrese datos válidos.");
            }
        }

        private void CargarProductosEnGrid()
        {
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = productos;
        }

        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtPrecio.Clear();
        }

        private void btnGuardarXml_Click(object sender, EventArgs e)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(List<Producto>));
            using (StreamWriter writer = new StreamWriter(RutaXml))
            {
                serializer.Serialize(writer, productos);
            }
            MessageBox.Show("Datos guardados en XML.");
        }

        private void btnLeerXml_Click(object sender, EventArgs e)
        {
            if (File.Exists(RutaXml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Producto>));
                using (StreamReader reader = new StreamReader(RutaXml))
                {
                    productos = (List<Producto>)serializer.Deserialize(reader);
                }
                CargarProductosEnGrid();
                MessageBox.Show("Datos cargados desde XML.");
            }
            else
            {
                MessageBox.Show("El archivo XML no existe.");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
