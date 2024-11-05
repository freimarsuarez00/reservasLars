using gestionDeHotel.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Reserva
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [ForeignKey("Habitacion")]
    [Column("habitacionId")]
    public int HabitacionId { get; set; }

    [Required]
    [ForeignKey("Cliente")]
    [Column("clienteCedula")]
    public string ClienteCedula { get; set; }

    [Required]
    [Column("fechaReserva")]
    public DateTime FechaReserva { get; set; }

    [Column("rowVersion")]
    [Timestamp]
    public byte[] RowVersion { get; set; }

    // Navegación
    public virtual Habitacion Habitacion { get; set; }
    public virtual Cliente Cliente { get; set; }
}