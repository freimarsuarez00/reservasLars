using gestionDeHotel.Data;
using gestionDeHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class HabitacionController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public HabitacionController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/habitacion
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Habitacion>>> ObtenerHabitaciones()
    {
        return await _context.Habitaciones.ToListAsync();
    }

    // GET: api/habitacion/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Habitacion>> ObtenerHabitacion(int id)
    {
        var habitacion = await _context.Habitaciones.FindAsync(id);

        if (habitacion == null)
        {
            return NotFound();
        }

        return habitacion;
    }

    // POST: api/habitacion
    [HttpPost]
    public async Task<ActionResult<Habitacion>> CrearHabitacion([FromBody] Habitacion habitacion)
    {
        if (habitacion == null)
        {
            return BadRequest("Se requieren datos de la habitación.");
        }

        _context.Habitaciones.Add(habitacion);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(ObtenerHabitacion), new { id = habitacion.Id }, habitacion);
    }

    // PUT: api/habitacion/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarHabitacion(int id, [FromBody] Habitacion habitacion)
    {
        if (id != habitacion.Id)
        {
            return BadRequest("Los ID de la habitación no coinciden.");
        }

        _context.Entry(habitacion).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!HabitacionExiste(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/habitacion/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarHabitacion(int id)
    {
        var habitacion = await _context.Habitaciones.FindAsync(id);
        if (habitacion == null)
        {
            return NotFound();
        }

        _context.Habitaciones.Remove(habitacion);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool HabitacionExiste(int id)
    {
        return _context.Habitaciones.Any(e => e.Id == id);
    }
}