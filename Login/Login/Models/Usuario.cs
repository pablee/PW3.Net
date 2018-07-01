using System.ComponentModel.DataAnnotations;
using Login.Models;

namespace Login
{
    [MetadataType(typeof(UsuarioMetadata))]
    public partial class Usuario
    {
        public string Contrasenia2 { get; set; }
        public bool Recordarme { get; set; }

        public string LoginEmail { get; set; }
        public string LoginContrasenia { get; set; }
    }
}