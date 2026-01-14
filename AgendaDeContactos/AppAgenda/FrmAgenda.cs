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
    public partial class FrmAgenda : Form
    {
        private ContactoNegocio cNegocio = new ContactoNegocio();

        public FrmAgenda()
        {
            InitializeComponent();
        }

        private void FrmAgenda_Load(object sender, EventArgs e)
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
                MessageBox.Show("No se pudo cargar la agenda");
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

            if(dgvContactos.CurrentRow != null)
            {
                Contacto seleccionado = (Contacto)dgvContactos.CurrentRow.DataBoundItem;
                CargarDatos(seleccionado);
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
            FrmContactos frmAgregar = new FrmContactos();
            DialogResult result = frmAgregar.ShowDialog();

            if(result != DialogResult.Cancel)
            {
                Cargar();
            }
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
                        Cargar();
                    }
                }
            }
            catch
            {
                MessageBox.Show("No se pudo eliminar el contacto, intentelo mas tarde");
            }

            
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if(dgvContactos.CurrentRow != null)
                {
                    Contacto seleccionado = (Contacto)dgvContactos.CurrentRow.DataBoundItem;
                    FrmContactos frmModificar = new FrmContactos(seleccionado);
                    DialogResult result = frmModificar.ShowDialog();

                    if(result != DialogResult.Cancel) 
                    {
                        Cargar();
                    }
                }
            }
            catch{
                MessageBox.Show("No se pudo deleccionar el elemento correctamente, intente con otro por el momento");
                CambiarVisibilidadLabels(false);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

            if (txtBuscar.Text.Length > 0)
            {
                List<Contacto> listaFiltrada = cNegocio.BuscarConFiltro(txtBuscar.Text);
                CargarLista(listaFiltrada);
            }
            else
            {
                CargarLista(cNegocio.Listar());
            }

             CambiarVisibilidadLabels(false);
        }
    }
}
