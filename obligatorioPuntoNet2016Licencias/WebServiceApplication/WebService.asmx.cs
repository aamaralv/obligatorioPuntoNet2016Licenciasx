using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;


namespace WebServiceApplication{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]

    public class WebService : System.Web.Services.WebService{

        /*-------------------------------------------------USUARIOS----------------------------------------------------*/

        //Insertar Usuarios
        [WebMethod]
        public void AddUsuarios(int idUsuario, String nombre, String usuario1, String contrasena, String correo){
            try{
                using (LicenciasEntities DBF = new LicenciasEntities())
                {
                    Usuario usuario = new Usuario();
                    {
                        usuario.IdUsuario  = idUsuario;
                        usuario.Nombre     = nombre;
                        usuario.Usuario1   = usuario1;
                        usuario.Contraseña = contrasena;
                        usuario.Correo     = correo;
                        DBF.Usuario.Add(usuario);
                        DBF.SaveChanges();
                    }
                }
            }catch (Exception ex){
                System.Console.Write(ex.StackTrace);
            }
        }

        //Actualizar usuario
        [WebMethod]
        public void UpdateUsuario(int idUsuario, String nombre, String usuario1, String contrasena, String correo)
        {
            try
            {
                using (LicenciasEntities DBF = new LicenciasEntities())
                {
                    var original = DBF.Usuario.Find(idUsuario);
                    if (original != null)
                    {
                        original.Nombre = nombre;
                        original.Usuario1 = usuario1;
                        original.Contraseña = contrasena;
                        original.Correo = correo;
                        DBF.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.StackTrace);
            }
        }

        //Listar Usuarios
        [WebMethod]
        [System.Xml.Serialization.XmlInclude(typeof(Usuario))]
        public List<Usuario> ListUsuarios(){
            List<Usuario> list = null;
            try{
                using (LicenciasEntities DBF = new LicenciasEntities())
                {
                    DBF.Configuration.ProxyCreationEnabled = false;
                    list = DBF.Usuario.ToList();
                }
            }catch (Exception ex){
                System.Console.Write(ex.StackTrace);
            }
            return list;
        }


        //Listar usuario
        [WebMethod]
        [System.Xml.Serialization.XmlInclude(typeof(Usuario))]
        public Usuario ListUsuario(int idUsu)
        {
            Usuario user = null;
            try
            {
                using (LicenciasEntities DBF = new LicenciasEntities())
                {
                    DBF.Configuration.ProxyCreationEnabled = false;
                    user = DBF.Usuario.Find(idUsu);
                }
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.StackTrace);
            }
            return user;
        }


        /*-------------------------------------------------ROLES----------------------------------------------------*/

        //Insertar Roles
        [WebMethod]
        public void AddRol(int idRol, int idUsu){
            try{
                using (LicenciasEntities DBF = new LicenciasEntities()){
                    Usuario user = DBF.Usuario.Find(idUsu);
                    Roles roles = DBF.Roles.Find(idRol);
                    {
                        roles.Usuario.Add(user);
                        user.Roles.Add(roles);
                        DBF.SaveChanges();
                    }
                    
                }
            }catch (Exception ex){
                System.Console.Write(ex.StackTrace);
            }
        }


        ///Listar rol
        [WebMethod]
        [System.Xml.Serialization.XmlInclude(typeof(Roles))]
        [System.Xml.Serialization.XmlInclude(typeof(Usuario))]
        public List<Roles> ListRol(int idUsu){
            List<Roles> roles = null;
            try{
                using (LicenciasEntities DBF = new LicenciasEntities())
                {
                    DBF.Configuration.ProxyCreationEnabled = false;
                    DBF.Configuration.LazyLoadingEnabled = true;
                    Usuario user = DBF.Usuario.Find(idUsu);
                    //Obtengo la lista de roles para ese id
                    roles = DBF.Roles.SqlQuery("SELECT	r.IdRol, r.Descripcion FROM dbo.Roles r, dbo.UsuarioRol u WHERE   r.IdRol = u.IdRol AND u.IdUsuario = @param1", new SqlParameter("param1", idUsu)).ToList();
                }
            }catch (Exception ex){
                System.Console.Write(ex.StackTrace);
            }
            return roles;
        }


        ///Listar rol
        [WebMethod]
        [System.Xml.Serialization.XmlInclude(typeof(Roles))]
        [System.Xml.Serialization.XmlInclude(typeof(Usuario))]
        public List<Roles> ListRoles(){
            List<Roles> roles = null;
            try{
                using (LicenciasEntities DBF = new LicenciasEntities())
                {
                    DBF.Configuration.ProxyCreationEnabled = false;
                    roles = DBF.Roles.ToList();
                }
            }catch (Exception ex){
                System.Console.Write(ex.StackTrace);
            }
            return roles;
        }


        /*-------------------------------------------------CLIENTES----------------------------------------------------*/

        //Insertar Clientes
        [WebMethod]
        public void AddClientes(String nombre, String carpeta){
            try{
                using (LicenciasEntities DBF = new LicenciasEntities())
                {
                    Clientes clientes = new Clientes();
                    {
                        clientes.Nombre = nombre;
                        clientes.Carpeta = carpeta;
                        DBF.Clientes.Add(clientes);
                        DBF.SaveChanges();
                    }
                }
            }catch (Exception ex){
                System.Console.Write(ex.StackTrace);
            }
        }

        //Listar Clientes
        [WebMethod]
        [System.Xml.Serialization.XmlInclude(typeof(Clientes))]
        public List<Clientes> ListClientes(){
            List<Clientes> list = null;
            try{
                using (LicenciasEntities DBF = new LicenciasEntities())
                {
                    DBF.Configuration.ProxyCreationEnabled = false;
                    list = DBF.Clientes.ToList();
                }
            }catch (Exception ex){
                System.Console.Write(ex.StackTrace);
            }
            return list;
        }

        /*-------------------------------------------------CONFIGURACIONES----------------------------------------------------*/

        //Insertar Configuraciones
        [WebMethod]
        public void AddConfig(String clave, String valor){
            try{
                using (LicenciasEntities DBF = new LicenciasEntities())
                {
                    Configuraciones conf = new Configuraciones();
                    {
                        conf.Clave = clave;
                        conf.Valor = valor;
                        DBF.Configuraciones.Add(conf);
                        DBF.SaveChanges();
                    }
                }
            }catch (Exception ex){
                System.Console.Write(ex.StackTrace);
            }
        }

        //Listar Configuraciones
        [WebMethod]
        [System.Xml.Serialization.XmlInclude(typeof(Configuraciones))]
        public List<Configuraciones> ListConfig(){
            List<Configuraciones> list = null;
            try{
                using (LicenciasEntities DBF = new LicenciasEntities())
                {
                    DBF.Configuration.ProxyCreationEnabled = false;
                    list = DBF.Configuraciones.ToList();
                }
            }catch (Exception ex){
                System.Console.Write(ex.StackTrace);
            }
            return list;
        }

        //Actualizar configuracion
        [WebMethod]
        public void UpdateConfig(int clave, String valor){
            try{
                using (LicenciasEntities DBF = new LicenciasEntities())
                {
                    var original = DBF.Configuraciones.Find(clave);
                    if (original != null){
                        original.Valor = valor;
                        DBF.SaveChanges();
                    }

                }
            }catch (Exception ex){
                System.Console.Write(ex.StackTrace);
            }
        }

    }
}
