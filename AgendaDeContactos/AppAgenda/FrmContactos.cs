using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppAgenda
{
    public partial class FrmContactos : Form
    {
        private Contacto contacto = null;

        public FrmContactos()
        {
            InitializeComponent();
        }

        public FrmContactos(Contacto contacto)
        {
            InitializeComponent();
            this.contacto = contacto;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmContactos_Load(object sender, EventArgs e)
        {
            if(contacto != null)
            {
                Text = "Modificar";
                lblTitulo.Text = "Modificar Contacto";
                CargarFormulario();
            }

        }

        private  void CargarFormulario()
        {
            txtNombre.Text = contacto.Nombre;
            txtApellido.Text = contacto.Apellido;
            txtTelefono.Text = contacto.Telefono;
        }
    }
}
