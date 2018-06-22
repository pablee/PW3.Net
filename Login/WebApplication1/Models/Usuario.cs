using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Usuario
    {
        public string Correo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }
        public List<Usuario> usuariosRegistrados = new List<Usuario>();        

        public Usuario()
        {
            
        }

        public Boolean IniciarSesion(Usuario User)
        {
            if (User.Correo=="Pablo")
            {
                if(User.Password=="12345")
                {
                    return true;
                }
            }
            return false;
        }

        public Boolean Registrarse(Usuario User)
        {
            usuariosRegistrados.Add(User);
            return true;
        }
    }
}