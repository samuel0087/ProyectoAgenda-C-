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
    public partial class FormularioContactos : Form
    {
        private Contacto contacto = null;

        public FormularioContactos()
        {
            InitializeComponent();
        }

        public FormularioContactos(Contacto contacto)
        {
            this.contacto = new Contacto();
            this.contacto = contacto;
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormularioContactos_Load(object sender, EventArgs e)
        {
            if(contacto != null)
            {
                Text = "Modifiicar";
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
