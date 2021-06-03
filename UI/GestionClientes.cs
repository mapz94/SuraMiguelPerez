using SuraMiguelPerez;
using Sura.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuraMiguelPerez.UI;

namespace Sura.UI
{
    public partial class GestionClientes : Form
    {
        private List<cliente> clientes = null;
        public GestionClientes()
        {
            InitializeComponent();
            refreshButton_Click(null, null);
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

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in table.SelectedRows)
            {
                int id = int.Parse(item.Cells[0].Value.ToString());

                using (SuraEntities entity = new SuraEntities())
                {
                    var delete = from d in entity.clientes
                                 where d.id == id
                                 select d;
                    foreach (var d in delete)
                    {
                        entity.clientes.Remove(d);
                    }

                    entity.SaveChanges();
                    MessageBox.Show("Se ha eliminado correctamente.");
                }
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Hide();
            Form menu = new MenuForm();
            menu.Show();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Form clientForm = new clientes_form();
            clientForm.Show();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            clientes = GetClientes();

            table.DataSource = clientes;
            table.Columns[7].Visible = false;

        }

        private void editButton_Click(object sender, EventArgs e)
        {
            cliente cliente = null;
            foreach (DataGridViewRow item in table.SelectedRows)
            {
                int id = int.Parse(item.Cells[0].Value.ToString());
                cliente = clientes.Find(a => a.id == id);
            }
            Form form = new clientes_form(cliente);
            form.Show();
        }


    }
}
