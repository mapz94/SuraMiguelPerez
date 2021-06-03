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
    public partial class clientes_form : Form
    {
        private cliente cliente = null;
        public clientes_form()
        {
            InitializeComponent();
        }

        public clientes_form(cliente cliente)
        {
            InitializeComponent();

            txt_nom.Text = cliente.nombre;
            txt_ape.Text = cliente.apellido;

            nombreLabel.Text = "Cambiar Nombre:";
            apellidoLabel.Text = "Cambiar Apellido:";

            txt_rut.Enabled = false;
            txt_adicional.Enabled = false;
            txt_saldo.Enabled = false;


            runButton.Text = "Cambiar datos";
            this.cliente = cliente;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void runButton_Click(object sender, EventArgs e) // Registrar
        {
            if (
                txt_nom.Text == "" ||
                txt_ape.Text == "" ||
                txt_rut.Text == "" ||
                txt_saldo.Text == "" ||
                txt_adicional.Text == ""
                )
            {
                MessageBox.Show("Favor ingrese todos los datos.");
                return;
            }
            using (SuraEntities entity = new SuraEntities())
            {
                if (this.cliente is null) // Create
                {
                    cliente cliente = new cliente()
                    {
                        rut = txt_rut.Text,
                        nombre = txt_nom.Text,
                        apellido = txt_ape.Text,
                        saldo = int.Parse(txt_saldo.Text),
                        adicional = int.Parse(txt_adicional.Text),
                        fecha_ing = DateTime.Now


                    };
                    entity.clientes.Add(cliente);
                    int result = entity.SaveChanges();
                    if (result > 0) MessageBox.Show("Se ha registrado correctamente!");
                    else MessageBox.Show("Hubo un error al registrar");
                }
                else // Update
                {
                    cliente client = (from a in entity.clientes where a.id == this.cliente.id select a).SingleOrDefault();
                    client.nombre = txt_nom.Text;
                    client.apellido = txt_ape.Text;
                    client.rut = txt_rut.Text;
                    client.saldo = int.Parse(txt_saldo.Text);
                    client.adicional = int.Parse(txt_adicional.Text);
                    
                    entity.SaveChanges();
                    MessageBox.Show("Cambios realizados de forma exitosa.");
                }
                this.Close();
            }
        }
    }
}
