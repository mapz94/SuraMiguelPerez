using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SuraMiguelPerez.UI
{
    public partial class GestionKM : Form
    {
        private List<cliente> clientes = null;
        //private List<tiposeguro> tipos = null;
        private List<tipopago> pagos = null;
        public GestionKM()
        {
            InitializeComponent();
            clientes = GetClientes();
            comboBox1.DataSource = clientes;
            comboBox1.DisplayMember = "rut";
            /*
            tipos = GetTipoSeguro();
            comboBox2.DataSource = tipos;
            comboBox2.DisplayMember = "nombreTipo";
            */

            Dictionary<string, int> cuotas = new Dictionary<string, int>()
            {
                {"2 Cuotas", 2 },
                {"4 Cuotas", 4 },
                {"8 Cuotas", 8 }
            };
            comboBox2.DataSource = new BindingSource(cuotas, null);
            comboBox2.DisplayMember = "Key";
            comboBox2.ValueMember = "Value";

            pagos = GetTipoPago();
            comboBox3.DataSource = pagos;
            comboBox3.DisplayMember = "nombreTipo";
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

        private List<tipopago> GetTipoPago()
        {
            using (SuraEntities entity = new SuraEntities())
            {
                IQueryable<tipopago> pagos = from d in entity.tipopagoes
                                               select d;
                List<tipopago> pagosList = pagos.ToList();

                return pagosList;
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (
                comboBox1.SelectedItem == null &&
                comboBox2.SelectedItem == null
                )
            {
                MessageBox.Show("Favor seleccionar Cliente y tipo de seguro.");
                return;
            }
            //tiposeguro tipo = (tiposeguro)comboBox2.SelectedItem;
            tipopago pago = (tipopago)comboBox3.SelectedItem;
            cliente cliente = (cliente)comboBox1.SelectedItem;
            KeyValuePair<string, int> cuota = (KeyValuePair<string, int>)comboBox2.SelectedItem;

            var result = cliente.saldo / cuota.Value + pago.valor;
            
            textBox1.Text = result.ToString();
            label6.Text = result >= 0 ? "Usted si puede contratar el seguro." : "El saldo disponible no es suficiente para cubrir el seguro seleccionado.";
        }
    }
}
