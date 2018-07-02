using System.ComponentModel.DataAnnotations;
using Login.Models;

namespace Login.Models
{
    [MetadataType(typeof(UsuarioRegistroMetadata))]
    public partial class UsuarioRegistro
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }
        public string Contrasenia2 { get; set; }              
    }    
}