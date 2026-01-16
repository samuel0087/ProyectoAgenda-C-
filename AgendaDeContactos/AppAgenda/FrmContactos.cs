using Dominio;
using Negocio;
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
        ContactoNegocio cNegocio = new ContactoNegocio();

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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(contacto == null)
            {
                contacto = new Contacto();
            }

            try
            {
                if(contacto.IdContacto == 0)
                {
                    if (cNegocio.ExisteTelefono(txtTelefono.Text))
                    {
                        DialogResult result = MessageBox.Show("El telefono ya esta registrado, Desea guardarlo igualmente?", "Atencion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.No)
                        {
                            return;
                        }
                    }
                }

                if (!ValidarFormulario())
                {
                    MessageBox.Show("Verifique los campor por favor.");
                    return;
                }

                contacto.Nombre = txtNombre.Text;
                contacto.Apellido = txtApellido.Text;
                contacto.Telefono = txtTelefono.Text;

                DialogResult resultado = MessageBox.Show("Esta seguro de que desea realizar esta operacion?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (resultado == DialogResult.No) 
                {
                    return;
                }


                if(contacto.IdContacto != 0)
                {
                    cNegocio.Modificar(contacto);
                }
                else
                {
                    cNegocio.Agregar(contacto);
                }

                MessageBox.Show("Operacion realizada con exito");

                this.DialogResult = DialogResult.OK;
                Close();
            }
            catch
            {
                MessageBox.Show("No se pudo guardar el registro correctamente");
            }
   
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }

            if( txtNombre.Text != "")
            {
                if (txtNombre.Text[txtNombre.Text.Length - 1] == ' ' && e.KeyChar == ' ')
                {
                    e.Handled = true;
                }
            }

            if(txtNombre.Text == "" && e.KeyChar == ' ')
            {
                e.Handled= true;
            }
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }

            if (txtApellido.Text != "")
            {
                if (txtApellido.Text[txtApellido.Text.Length - 1] == ' ' && e.KeyChar == ' ')
                {
                    e.Handled = true;
                }
            }

            if (txtApellido.Text == "" && e.KeyChar == ' ')
            {
                e.Handled = true;
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '+')
            {
                e.Handled = true;
            }

            if(txtTelefono.Text != "" && e.KeyChar == '+')
            {
                e.Handled = true;
            }

        }

        private bool ValidarFormulario()
        {
            bool estado = true;
            lblNombre.Text = string.Empty;
            lblApellido.Text = string.Empty;
            lblErrorTelefono.Text = string.Empty;

            if (string.IsNullOrEmpty(txtNombre.Text)) 
            {
                lblNombre.Text = "Campo oblgatorio";
                estado = false;
            }

            if (string.IsNullOrEmpty(txtApellido.Text))
            {
                lblApellido.Text = "Campo obligatorio";
                estado = false;
            }

            if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                lblErrorTelefono.Text = "Campo obligatorio";
                estado = false;
            }
            else if (txtTelefono.Text == "+")
            {
                lblErrorTelefono.Text = "Debe contener numeros";
                estado = false;
            }


            return estado;
        }
    }
}
