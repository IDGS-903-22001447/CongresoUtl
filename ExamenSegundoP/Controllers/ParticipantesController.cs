using ExamenSegundoP.Data;
using ExamenSegundoP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamenSegundoP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantesController : ControllerBase
    {
        private readonly CongresoDbContext _context;

        public ParticipantesController(CongresoDbContext context)
        {
            _context = context;
        }

        // GET: api/participantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participante>>> GetParticipantes()
        {
            return await _context.Participantes.ToListAsync();
        }

        // GET: api/participantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Participante>> GetParticipante(int id)
        {
            var participante = await _context.Participantes.FindAsync(id);

            if (participante == null)
                return NotFound(new { mensaje = "Participante no encontrado" });

            return participante;
        }

        // POST: api/participantes
        [HttpPost]
        public async Task<ActionResult<Participante>> CrearParticipante([FromBody] Participante participante)
        {
            _context.Participantes.Add(participante);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetParticipante), new { id = participante.Id }, participante);
        }

        // PUT: api/participantes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarParticipante(int id, [FromBody] Participante participante)
        {
            if (id != participante.Id)
                return BadRequest(new { mensaje = "El ID no coincide" });

            _context.Entry(participante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Participantes.Any(e => e.Id == id))
                    return NotFound(new { mensaje = "Participante no encontrado" });

                throw;
            }

            return Ok(new { mensaje = "Participante actualizado correctamente" });
        }

        // DELETE: api/participantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarParticipante(int id)
        {
            var participante = await _context.Participantes.FindAsync(id);
            if (participante == null)
                return NotFound(new { mensaje = "Participante no encontrado" });

            _context.Participantes.Remove(participante);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Participante eliminado correctamente" });
        }
    }
}
