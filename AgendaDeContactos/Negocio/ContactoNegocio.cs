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

        public List<Contacto> listar()
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
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            string query = "DELETE FROM Contactos WHERE Id_Contactos = @id";

            try
            {
                datos.setearConsulta(query);
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}
