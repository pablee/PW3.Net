using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult IniciarSesion()
        {           
            string correo = Request.Form["correo"];
            string password = Request.Form["password"];

            Usuario User = new Usuario();
            User.Correo = correo;
            User.Password = password;

            ViewBag.User = User;

            if(User.IniciarSesion(User)==true)
            {
                return View("Usuario");
            }
            else
            {
                return View("Index");
            }

            /*
            ViewData["Username"]= username;
            ViewData["Password"] = password;
            return View("Usuario");
            */
        }


        public ActionResult Registrarse()
        {
            string nombre = Request.Form["nombre"];
            string apellido = Request.Form["apellido"];
            string correo = Request.Form["correo"];
            string password = Request.Form["password"];

            Usuario User = new Usuario();
            User.Nombre = nombre;
            User.Apellido = apellido;
            User.Correo = correo;
            User.Password = password;
       
            ViewBag.User = User;

            if (User.Registrarse(User) == true)
            {
                return View("Usuario");
            }
            else
            {
                return View("Index");
            }

            /*
            ViewData["Username"]= username;
            ViewData["Password"] = password;
            return View("Usuario");
            */
        }
    }
}