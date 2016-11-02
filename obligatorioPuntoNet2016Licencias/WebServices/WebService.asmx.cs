using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Collections;
using Entidades;

namespace WebServices
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod]
        public bool RegistrarUsuario(Usuario usuario)
        {
            return true;
        }

        [WebMethod]
        public bool ActualizarUsuario(Usuario usuario)
        {
            return true;
        }


        [WebMethod]
        public List<Rol> ListarRoles()
        {
            
            return null;
        }

        [WebMethod]
        public bool AsignarRolUsuario(List<Usuario>  listaUsuarios,Rol rol)
        {
            return true;
        }

        [WebMethod]
        public bool IngresarCliente(Cliente cliente)
        {
            return true;
        }

        [WebMethod]
        public List<Cliente> ListarClientes()
        {
            return null;
        }


        [WebMethod]
        public bool RegistrarConfiguracion(Configuracion configuracion)
        {
            return true;
        }

        [WebMethod]
        public bool ActualizarConfiguracion(Configuracion configuracion)
        {
            return true;
        }

        [WebMethod]
        public List<Configuracion> ListarConfiguraciones()
        {
            return null;
        }




    }
}
