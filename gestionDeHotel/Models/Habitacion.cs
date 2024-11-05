using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestionDeHotel.Models
{
    [Table("habitaciones")]
    public class Habitacion
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nombre")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [Column("precio")]
        [DataType(DataType.Currency)]
        public decimal Precio { get; set; }

        [Column("disponible")]
        public bool Disponible { get; set; } = true; // Valor por defecto
    }
}
