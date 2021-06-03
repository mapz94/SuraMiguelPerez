using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuraMiguelPerez.UI
{
    public partial class GestionSeguros : Form
    {

        List<cliente> clientes = null;
        List<tiposeguro> tipos = null;
        public GestionSeguros()
        {
            InitializeComponent();

            clientes = GetClientes();

            comboBox1.DataSource = clientes;
            comboBox1.DisplayMember = "rut";

            tipos = GetTipoSeguro();
            comboBox2.DataSource = tipos;
            comboBox2.DisplayMember = "nombreTipo";

        }

        private List<cliente> GetClientes()
        {
            using (SuraEntities entity = new SuraEntities())
            {
                IQueryable<cliente> clientes = from d in entity.clientes
                                               select d;
                List<cliente> clientesList = clientes.ToList();

                return clientesList;

            }
        }

        private List<tiposeguro> GetTipoSeguro()
        {
            using (SuraEntities entity = new SuraEntities())
            {
                IQueryable<tiposeguro> tipos = from d in entity.tiposeguros
                                               select d;
                List<tiposeguro> clientesList = tipos.ToList();

                return clientesList;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(
                comboBox1.SelectedItem == null &&
                comboBox2.SelectedItem == null
            )
            {
                MessageBox.Show("Favor seleccionar Cliente y tipo de seguro.");
                return;
            }
            tiposeguro tipo = (tiposeguro)comboBox2.SelectedItem;
            cliente cliente = (cliente)comboBox1.SelectedItem;
            var result = cliente.saldo - tipo.valor + cliente.adicional;
            textBox1.Text = result.ToString("N0");
            label6.Text = result >= 0 ? "Usted si puede contratar el seguro." : "El saldo disponible no es suficiente para cubrir el seguro seleccionado.";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cliente cliente = (cliente)comboBox1.SelectedItem;
            textBox3.Text = cliente.adicional.ToString();
        }
    }
}
