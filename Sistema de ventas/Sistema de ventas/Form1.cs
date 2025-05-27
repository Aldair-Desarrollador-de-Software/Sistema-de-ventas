using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Sistema_de_ventas
{
    public partial class Form1 : Form
    {
        Dictionary<string, decimal> productos = new Dictionary<string, decimal>();
        decimal total = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            string nombre = txtProducto.Text.Trim();
            decimal precio;

            if (nombre == "" || !decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("Ingrese producto y precio válidos.");
                return;
            }

            productos[nombre] = precio;

            cmbProductos.Items.Clear();
            foreach (var p in productos.Keys)
                cmbProductos.Items.Add(p);

            txtProducto.Clear();
            txtPrecio.Clear();
        }

        private void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            if (cmbProductos.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un producto.");
                return;
            }

            string nombre = cmbProductos.SelectedItem.ToString();
            int cantidad = (int)numCantidad.Value;
            decimal precio = productos[nombre];
            decimal subtotal = precio * cantidad;

            lstCarrito.Items.Add(nombre + " x" + cantidad + " = $" + subtotal.ToString("0.00"));
            total += subtotal;
            lblTotal.Text = "Total: $" + total.ToString("0.00");
        }

        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            if (lstCarrito.SelectedIndex != -1)
            {
                string linea = lstCarrito.SelectedItem.ToString();
                int idx1 = linea.LastIndexOf('$');
                decimal valor = decimal.Parse(linea.Substring(idx1 + 1));
                total -= valor;
                lblTotal.Text = "Total: $" + total.ToString("0.00");

                lstCarrito.Items.RemoveAt(lstCarrito.SelectedIndex);
            }
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            decimal dinero;

            if (!decimal.TryParse(txtDinero.Text, out dinero))
            {
                MessageBox.Show("Ingrese una cantidad válida.");
                return;
            }

            if (dinero < total)
            {
                MessageBox.Show("Dinero insuficiente.");
                return;
            }

            decimal cambio = dinero - total;
            lblCambio.Text = "Cambio: $" + cambio.ToString("0.00");
            MessageBox.Show("Gracias por su compra.", "Venta finalizada");

            // Reiniciar
            lstCarrito.Items.Clear();
            total = 0;
            lblTotal.Text = "Total: $0.00";
            txtDinero.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }
    }
}
