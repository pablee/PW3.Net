using System.ComponentModel.DataAnnotations;
using System.Linq;

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

        [Required]
        [StringLength(200)]
        [EmailAddress]        
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Contrasenia { get; set; }
        
    }
}