using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Login.Controllers
{
    public class HomeController : Controller
    {        
        TP_Entities ctx = new TP_Entities();

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Registracion()
        {           
            return View();
        }


        [HttpPost]
        public ActionResult Registracion(Usuario Us)
        {
            var response = Request["g-recaptcha-response"];
            string secretKey = "6Ld3NWAUAAAAAJJ2Mco-UNBPCdXwIZwIeNs1r6fC";
            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");
            ViewBag.Message = status ? "Google reCaptcha validation success" : "Google reCaptcha validation failed";

            if ((ModelState.IsValid)&&(status == true))
            {
                /*
                DateTime myDateTime = DateTime.Now;
                string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                */        
                Us.FechaRegistracion = DateTime.Now;
                Us.CodigoActivacion = result;
                ctx.Usuarios.Add(Us);
                ctx.SaveChanges();

                /*
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
                */
                return RedirectToAction("Index");
            }
            else
            {                
                return View(Us);
            }
            
        }
     
        
        [HttpPost]
        public ActionResult Login(Usuario Us)
        {                        
            Usuario UsLog = ctx.Usuarios.FirstOrDefault(o => o.Email == Us.Email);

            if ((UsLog != null) && (UsLog.Contrasenia == Us.Contrasenia))
            {
                return View("Login", UsLog);
            }
            else
            {
                ViewBag.Invalido = "Verifique usuario y / o contraseña.";
            }
            
            return View("Index",Us);                        
        }
        
    }
}