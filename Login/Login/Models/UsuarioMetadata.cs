using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Login.Models
{
    public class UsuarioMetadata
    {       
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }


        //Email de registro
        [Required]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "El email ingresado es invalido")]
        [CustomValidation(typeof(UsuarioMetadata), "ValidarEmailUnico")]
        public string Email { get; set; }

        public static ValidationResult ValidarEmailUnico(object value, ValidationContext context)
        {
            var usuario = context.ObjectInstance as Usuario;

            if (usuario == null || string.IsNullOrEmpty(usuario.Email))
                return new ValidationResult(string.Format("Email es requerido."));

            //para validar que no exista otro email igual, debo chequear en la base
            TP_Entities ctx = new TP_Entities();
            
            var existeEmail = ctx.Usuarios.Any(o => o.Email == usuario.Email);

            if (existeEmail)
            {
                return new ValidationResult(string.Format("El Email {0} ya está siendo usado.", usuario.Email));
            }

            return ValidationResult.Success;
        }


        //Validacion contraseña en registro
        [Required]
        [StringLength(20)]
        [CustomValidation(typeof(UsuarioMetadata), "ValidarContrasenia")]
        public string Contrasenia { get; set; }

        public static ValidationResult ValidarContrasenia(object value, ValidationContext context)
        {
            var usuario = context.ObjectInstance as Usuario;

            if (usuario == null || string.IsNullOrEmpty(usuario.Contrasenia))
                return new ValidationResult(string.Format("La contraseña es requerida."));
          
            string mayusculas = "^(?=.*[A-Z])";
            string minusculas = "^(?=.*[a-z])";
            string numeros = "^(?=.*[0-9])";

            bool tieneMayusculas = Regex.IsMatch(usuario.Contrasenia, mayusculas);
            if (!tieneMayusculas)
            {
                return new ValidationResult(string.Format("La contraseña debe contener al menos una mayuscula", usuario.Contrasenia));
            }

            bool tieneMinusculas = Regex.IsMatch(usuario.Contrasenia, minusculas);
            if (!tieneMinusculas)
            {
                return new ValidationResult(string.Format("La contraseña debe contener al menos una minuscula", usuario.Contrasenia));
            }

            bool tieneNumeros = Regex.IsMatch(usuario.Contrasenia, numeros);
            if (!tieneNumeros)
            {
                return new ValidationResult(string.Format("La contraseña debe contener al menos un numero", usuario.Contrasenia));
            }

            return ValidationResult.Success;
        }


        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20)]
        [CustomValidation(typeof(UsuarioMetadata), "ValidarContraseniasIguales")]
        public string Contrasenia2 { get; set; }

        public static ValidationResult ValidarContraseniasIguales(object value, ValidationContext context)
        {
            var usuario = context.ObjectInstance as Usuario;

            if (usuario == null || string.IsNullOrEmpty(usuario.Contrasenia))
                return new ValidationResult(string.Format("La contraseña es requerida."));      
            
            if (usuario.Contrasenia != usuario.Contrasenia2)
            {
                return new ValidationResult(string.Format("Las contraseñas no coinciden", usuario.Contrasenia));
            }

            return ValidationResult.Success;
        }


        //Email de login
        [Required(ErrorMessage = "El campo email de login es obligatorio")]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "El email ingresado es invalido")]
        [CustomValidation(typeof(UsuarioMetadata), "ValidarEmailLogin")]
        public string LoginEmail { get; set; }

        public static ValidationResult ValidarEmailLogin(object value, ValidationContext context)
        {
            var usuario = context.ObjectInstance as Usuario;

            if (usuario == null || string.IsNullOrEmpty(usuario.LoginEmail))
                return new ValidationResult(string.Format("Email de login es requerido."));                        

            return ValidationResult.Success;
        }


        //Validacion contraseña en login
        [Required(ErrorMessage = "El campo contraseña de login es obligatorio")]
        [StringLength(20)]        
        public string LoginContrasenia { get; set; }
      
    }
}