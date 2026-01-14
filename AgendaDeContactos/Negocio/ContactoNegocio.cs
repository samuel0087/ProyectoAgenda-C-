using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Conexion;

namespace Negocio
{
    public class ContactoNegocio
    {

        public List<Contacto> Listar()
        {
            List<Contacto> listaContactos = new List<Contacto>();
            AccesoDatos datos = new AccesoDatos();
            string query = "SELECT Id_Contacto, Apellido, Nombre, Telefono FROM Contactos ORDER BY Apellido ASC";

            try
            {
                datos.setearConsulta(query);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Contacto aux = new Contacto();
                    aux.IdContacto = datos.Lector["Id_Contacto"] is DBNull ? 0 : (int)datos.Lector["Id_Contacto"];
                    aux.Apellido = datos.Lector["Apellido"] is DBNull ? "" : (string)datos.Lector["Apellido"];
                    aux.Nombre = datos.Lector["Nombre"] is DBNull ? "" : (string)datos.Lector["Nombre"];
                    aux.Telefono = datos.Lector["Telefono"] is DBNull ? "" : (string)datos.Lector["Telefono"];

                    listaContactos.Add(aux);
                }

                return listaContactos;

            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            string query = "DELETE FROM Contactos WHERE Id_Contacto = @id";

            try
            {
                datos.setearConsulta(query);
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Agregar(Contacto contacto)
        {
            AccesoDatos datos = new AccesoDatos();
            string query = "INSERT INTO Contactos (Apellido, Nombre, Telefono) VALUES (@apellido, @nombre, @telefono)";

            try
            {
                datos.setearConsulta(query);
                datos.setearParametro("@apellido", contacto.Apellido);
                datos.setearParametro("@nombre", contacto.Nombre);
                datos.setearParametro("@telefono", contacto.Telefono);

                datos.ejecutarAccion();

            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(Contacto contacto)
        {
            AccesoDatos datos = new AccesoDatos();
            string query = "UPDATE Contactos SET Apellido = @apellido, Nombre = @nombre, Telefono = @telefono WHERE Id_Contacto = @id";

            try
            {
                datos.setearConsulta(query);
                datos.setearParametro("@apellido", contacto.Apellido);
                datos.setearParametro("@nombre", contacto.Nombre);
                datos.setearParametro("@telefono", contacto.Telefono);
                datos.setearParametro("@id", contacto.IdContacto);

                datos.ejecutarAccion();

            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool ExisteTelefono(string telefono)
        {
            AccesoDatos datos = new AccesoDatos();
            string query = "SELECT Id_Contacto FROM Contactos WHERE Telefono = @telefono";

            try
            {
                datos.setearConsulta(query);
                datos.setearParametro("@telefono", telefono);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return true;
                }

                return false;
            }
            finally{
                datos.cerrarConexion();
            }
        }

        public List<Contacto> BuscarConFiltro(string filtro)
        {
            List<Contacto> listaFiltrada = new List<Contacto>();
           
            listaFiltrada = this.Listar().FindAll(contacto => contacto.Nombre.ToUpper().Contains(filtro.ToUpper()) || contacto.Telefono.Contains(filtro));
            return listaFiltrada;
        }
    }
}
