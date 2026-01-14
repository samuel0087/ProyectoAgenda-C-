using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace AppAgenda
{
    public partial class AgendaApp : Form
    {
        private ContactoNegocio cNegocio = new ContactoNegocio();

        public AgendaApp()
        {
            InitializeComponent();
        }

        private void AgendaApp_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void Cargar()
        {
            try
            {
                CargarLista(cNegocio.Listar());
                CambiarVisibilidadLabels(false);
            }
            catch
            { 
            }

        }

        private void CargarLista(List<Contacto> lista)
        {
            try
            {
                dgvContactos.DataSource = null;
                dgvContactos.DataSource = lista;
                dgvContactos.Columns["IdContacto"].Visible = false;
            }
            catch 
            {
                MessageBox.Show("No se pudo cargar la lista, intentelo nuevamente");
            }

        }

        private void CargarDatos(Contacto contacto)
        {
            CambiarVisibilidadLabels(true);
            lblNombre.Text = "Nombre: " + contacto.Nombre;
            lblApellido.Text = "Apellido: " + contacto.Apellido;
            lblTelefono.Text = "Telefono: " + contacto.Telefono;
        }

        private void dgvContactos_SelectionChanged(object sender, EventArgs e)
        {

            try
            {
                if(dgvContactos.CurrentRow != null)
                {
                    Contacto seleccionado = (Contacto)dgvContactos.CurrentRow.DataBoundItem;
                    CargarDatos(seleccionado);
                }
            }
            catch 
            {
                MessageBox.Show("No se pudo deleccionar el elemento correctamente, intente con otro por el momento");
                CambiarVisibilidadLabels(false);
            }
            
        }

        private void CambiarVisibilidadLabels(bool estado)
        {
            lblNombre.Visible = estado;
            lblApellido.Visible = estado;
            lblTelefono.Visible = estado;
            btnModificar.Enabled = estado;
            btnEliminar.Enabled = estado;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FormularioContactos fmrAgregar = new FormularioContactos();
            fmrAgregar.ShowDialog();
            Cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvContactos.CurrentRow != null)
                {
                    Contacto seleccionado = (Contacto)dgvContactos.CurrentRow.DataBoundItem;

                    DialogResult result = MessageBox.Show("Esta seguro que quiere eliminar este registro permanentemente?", "Eliminando...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        cNegocio.Eliminar(seleccionado.IdContacto);
                        MessageBox.Show("Eliminado exitosamente", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch
            {
                MessageBox.Show("No se pudo eliminar el contacto, intentelo mas tarde");
            }
            finally
            {
                Cargar();
            }
            
        }
    }
}
