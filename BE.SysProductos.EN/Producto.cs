using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.SysProductos.EN
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El precio del producto es obligatorio")]
        public decimal Precio { get; set; }
        [Required(ErrorMessage = "La cantidad del producto es obligatoria")]
        public int CantidadDisponible { get; set; }
        [Required(ErrorMessage = "La Fecha del producto es obligatoria")]
        public DateTime FechaCreacion { get; set; }
    }
}
