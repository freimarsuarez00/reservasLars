using System;
namespace gestionDeHotel.Models
{

    public class ReservaDto
    {
        public int HabitacionId { get; set; } // ID de la habitación a reservar
        public DateTime FechaReserva { get; set; } // Fecha de la reserva
        public string ClienteId { get; set; } // Cedula o ID del cliente
    }
}
