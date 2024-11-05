using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using gestionDeHotel.Data;
using gestionDeHotel.Models;
using gestionDeHotel.Services;

namespace gestionDeHotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservaController : ControllerBase
    {
        private readonly ApplicationDbContext _context; // Para interactuar con la base de datos
        private readonly IEmailSender _emailSender; // Para enviar correos electrónicos

        public ReservaController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        // POST: api/reserva/reserve-room
        [HttpPost("reserve-room")]
        public async Task<IActionResult> ReserveRoom([FromBody] Reserva reserva)
        {
            // Verifica que el modelo sea válido
            if (reserva == null || !ModelState.IsValid)
            {
                return BadRequest("Datos de reserva inválidos.");
            }

            // Verifica que el cliente exista
            var cliente = await _context.Clientes.FindAsync(reserva.ClienteCedula);
            if (cliente == null)
            {
                return BadRequest("Cliente no encontrado.");
            }

            // Verifica que la habitación exista y esté disponible
            var habitacion = await _context.Habitaciones.FindAsync(reserva.HabitacionId);
            if (habitacion == null || !habitacion.Disponible)
            {
                return NotFound("La habitación solicitada no está disponible.");
            }

            // Establece la fecha de reserva actual
            reserva.FechaReserva = DateTime.UtcNow;

            // Agrega la reserva a la base de datos
            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();

            // Actualiza el estado de la habitación a no disponible
            habitacion.Disponible = false; // Cambiar según tu lógica
            _context.Habitaciones.Update(habitacion);
            await _context.SaveChangesAsync();

            // Envía un correo de confirmación
            await _emailSender.SendEmailAsync(cliente.Email, "Confirmación de Reserva",
                $"Su reserva ha sido confirmada para la habitación {habitacion.Nombre}.");

            return Ok("Reserva creada con éxito.");
        }

        // GET: api/reserva/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReserva(int id)
        {
            var reserva = await _context.Reservas.Include(r => r.Habitacion)
                                                 .FirstOrDefaultAsync(r => r.Id == id);
            if (reserva == null)
            {
                return NotFound("Reserva no encontrada.");
            }

            return Ok(reserva);
        }

        // DELETE: api/reserva/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelarReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound("Reserva no encontrada.");
            }

            _context.Reservas.Remove(reserva);

            // Actualiza el estado de la habitación a disponible
            var habitacion = await _context.Habitaciones.FindAsync(reserva.HabitacionId);
            if (habitacion != null)
            {
                habitacion.Disponible = true; // Cambiar según tu lógica
                _context.Habitaciones.Update(habitacion);
            }

            await _context.SaveChangesAsync();

            return Ok("Reserva cancelada con éxito.");
        }
    }
}