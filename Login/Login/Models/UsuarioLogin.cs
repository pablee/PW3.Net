using System.ComponentModel.DataAnnotations;
using Login.Models;

namespace Login.Models
{
    [MetadataType(typeof(UsuarioLoginMetadata))]
    public partial class UsuarioLogin
    {
        public string Email { get; set; }
        public string Contrasenia { get; set; }
        public bool Recordarme { get; set; }
    }
}