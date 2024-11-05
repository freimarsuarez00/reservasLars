using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestionDeHotel.Models
{
    [Table("clientes")]
        public class Cliente
        {
            [Key]
            [Column("cedula")]
            [MaxLength(20)]
            public string Cedula { get; set; }

            [Required]
            [Column("nombre1")]
            [MaxLength(50)]
            public string Nombre1 { get; set; }

            [Column("nombre2")]
            [MaxLength(50)]
            public string Nombre2 { get; set; }

            [Required]
            [Column("apellido1")]
            [MaxLength(50)]
            public string Apellido1 { get; set; }

            [Column("apellido2")]
            [MaxLength(50)]
            public string Apellido2 { get; set; }

            [Column("direccion")]
            [MaxLength(255)]
            public string Direccion { get; set; }

            [Column("telefono")]
            [MaxLength(15)]
            public string Telefono { get; set; }

            [Column("email")]
            [MaxLength(100)]
            public string Email { get; set; }
        }
    
}
