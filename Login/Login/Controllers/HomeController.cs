using Newtonsoft.Json.Linq;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Login.Models;

namespace Login.Controllers
{
    public class HomeController : Controller
    {        
        TP_Entities ctx = new TP_Entities();

        public ActionResult Index()
        {            
            if (Request.Cookies["recordarme"] != null && Request.Cookies["recordarme"]["estado"] == "verdadero" )
            {
                int id = Convert.ToInt32(Request.Cookies["recordarme"]["idUsuario"]);                    
                Usuario UsLog = ctx.Usuarios.FirstOrDefault(o => o.IdUsuario == id);
                Session["login"] = true;
                Session["id"] = UsLog.IdUsuario;

                return View("Home", UsLog);                
            }

            return View();
        }


        public ActionResult Registracion()
        {           
            return View();
        }


        [HttpPost]
        public ActionResult Registracion(UsuarioRegistro UsReg)
        {
            var response = Request["g-recaptcha-response"];
            string secretKey = "6Ld3NWAUAAAAAJJ2Mco-UNBPCdXwIZwIeNs1r6fC";
            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");
            ViewBag.Message = status ? "Google reCaptcha validation success" : "Google reCaptcha validation failed";
            status = true;

            if (status == true)
            {
                TP_Entities ctx = new TP_Entities();
                Usuario UsEncontrado = ctx.Usuarios.FirstOrDefault(o => o.Email == UsReg.Email && o.Activo == 0);

                if (UsEncontrado == null)
                {
                    Usuario UsNuevo = new Usuario();
                    UsNuevo.Nombre = UsReg.Nombre;
                    UsNuevo.Apellido = UsReg.Apellido;
                    UsNuevo.Email = UsReg.Email;
                    UsNuevo.Contrasenia = UsReg.Contrasenia;
                    UsNuevo.FechaRegistracion = DateTime.Now;
                    UsNuevo.CodigoActivacion = result;
                    
                    ctx.Usuarios.Add(UsNuevo);
                    //ctx.SaveChanges();

                    try
                    {
                        ctx.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        // Retrieve the error messages as a list of strings.
                        var errorMessages = ex.EntityValidationErrors
                                .SelectMany(x => x.ValidationErrors)
                                .Select(x => x.ErrorMessage);

                        // Join the list to a single string.
                        var fullErrorMessage = string.Join("; ", errorMessages);

                        // Combine the original exception message with the new one.
                        var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                        // Throw a new DbEntityValidationException with the improved exception message.
                        throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                    }
                }
                else 
                {
                    UsEncontrado.Nombre = UsReg.Nombre;
                    UsEncontrado.Apellido = UsReg.Apellido;
                    UsEncontrado.Contrasenia = UsReg.Contrasenia;
                    UsEncontrado.Activo = 1;
                    UsEncontrado.FechaActivacion = DateTime.Now;
                    
                    ViewBag.Reactivado = UsReg;
                    return View("Reactivacion");
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(UsReg);
            }

        }

        
        public ActionResult Home()
        {
            if (Session["login"] is true)
            {
                return View();
            }
            else
            {
                return View("Index");
            }
        }

        
        [HttpPost]
        public ActionResult Login(UsuarioLogin Us)
        {                        
            Usuario UsLog = ctx.Usuarios.FirstOrDefault(o => o.Email == Us.Email);

            if (UsLog != null)
            {
                if (UsLog.Activo == 1)
                {
                    if ((UsLog.Email == Us.Email) && (UsLog.Contrasenia == Us.Contrasenia))
                    {
                        if (Us.Recordarme is true)
                        {                            
                            HttpCookie recordarme = new HttpCookie("recordarme");
                            recordarme["estado"] = "verdadero";
                            recordarme["idUsuario"] = UsLog.IdUsuario.ToString();
                            recordarme.Expires = DateTime.Now.AddDays(1);
                            Response.Cookies.Add(recordarme);
                        }

                        Session["login"] = true;
                        Session["id"] = UsLog.IdUsuario;
                        return View("Home", UsLog);
                    }
                    else
                    {
                        ViewData["Invalido"] = "Verifique usuario y / o contraseña.";
                    }
                }
                else if (UsLog.Activo == 0)
                {
                    ViewData["Inactivo"] = "Usuario inactivo.";
                }
            }
            else
            {
                ViewData["Invalido"] = "Verifique usuario y / o contraseña.";
            }

            return View("Index",Us);                        
        }


        public ActionResult Logout()
        {
            if (Request.Cookies["recordarme"] != null)
            {
                Response.Cookies["recordarme"].Expires = DateTime.Now.AddDays(-1);
            }
            Session.Abandon();

            return RedirectToAction("Index");
        }
    }

}