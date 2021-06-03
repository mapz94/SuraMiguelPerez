using SuraMiguelPerez;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Sura.UI
{
    public partial class GestionAdmin : Form
    {
        private List<administradore> admins = null;
        public GestionAdmin()
        {
            InitializeComponent();
            refreshButton_Click(null, null);
        }
        private void editButton_Click(object sender, EventArgs e)
        {
            administradore admin = null;
            foreach (DataGridViewRow item in table.SelectedRows)
            {
                int id = int.Parse(item.Cells[0].Value.ToString());
                admin = admins.Find(a => a.id == id);
            }
            Form form = new admin_form(admin);
            form.Show();
        }

        private List<administradore> GetAdmins()
        {
            using (SuraEntities entity = new SuraEntities())
            {
                IQueryable<administradore> admins = from d in entity.administradores
                                                    select d;
                List<administradore> adminList = admins.ToList();

                return adminList;
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.table.SelectedRows)
            {
                int id = int.Parse(item.Cells[0].Value.ToString());

                using (SuraEntities entity = new SuraEntities())
                {
                    var delete = from d in entity.administradores
                                 where d.id == id
                                 select d;
                    foreach(var d in delete)
                    {
                        entity.administradores.Remove(d);
                    }

                    entity.SaveChanges();
                    MessageBox.Show("Ha eliminado correctamente.");
                }
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form menu = new MenuForm();
            menu.Show();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Form admin = new admin_form();
            admin.Show();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            admins = GetAdmins();
            List<administradore> toShow = new List<administradore>();
            foreach(administradore a in admins)
            {
                a.contrasena = new string('*', a.contrasena.Length);
                toShow.Add(a);
            }
            table.DataSource = toShow;
        }

        private void table_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            editButton.Enabled = true;
        }
    }
}
