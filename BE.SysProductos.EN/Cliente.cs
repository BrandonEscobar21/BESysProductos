using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.SysProductos.EN
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del Cliente es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre del cliente no puede tener más de 50 caracteres")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El apellido del Cliente es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido del cliente no puede tener más de 50 caracteres")]
        public string? Apellido { get; set; }

        [Required(ErrorMessage = "El teléfono del Cliente es obligatorio.")]
        [StringLength(8, ErrorMessage = "El teléfono del cliente no puede tener más de 8 caracteres")]
        public string? Telefono { get; set; }

        [StringLength(250, ErrorMessage = "El Correo Electrónico no puede tener más de 250 caracteres.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "El DUI del Cliente es obligatorio.")]
        [StringLength(9, ErrorMessage = "El DUI del cliente no puede tener más de 9 caracteres")]
        public string? DUI { get; set; }
    }
}
