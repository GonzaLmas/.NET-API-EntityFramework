using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BalearesChallenge.Data;
using BalearesChallenge.Models;
using Microsoft.AspNetCore.Authorization;
using BalearesChallenge.Services;

namespace BalearesChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransporteController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly Utilidades _utilidades;

        public TransporteController(AppDBContext context, Utilidades utilidades)
        {
            _context = context;
            _utilidades = utilidades;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransporteModel>>> GetTransportes()
        {
            return await _context.Transportes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransporteModel>> GetTransporteModel(int id)
        {
            var transporteModel = await _context.Transportes.FindAsync(id);

            if (transporteModel == null)
            {
                return NotFound();
            }

            return transporteModel;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransporteModel(int id, [FromBody] TransporteModel transporteModel)
        {
            if (_utilidades.TransporteModelExists(id))
            {
                var transporte = await _context.Transportes.FindAsync(id);

                if (transporte == null)
                {
                    return NotFound("Error. Contacto no encontrado.");
                }

                transporte.TipoTransporte = transporteModel.TipoTransporte;

                _context.Entry(transporte).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = $"Se ha modificado el transporte con id {id} correctamente." });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_utilidades.TransporteModelExists(id))
                    {
                        return NotFound("Transporte no encontrado para actualizar.");
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, "Error. Por favor actualice el tipo de transporte.");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Error al actualizar el transporte: {ex.Message}");
                }
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<TransporteModel>> PostTransporteModel([FromBody] TransporteModel transporteModel)
        {
            _context.Transportes.Add(transporteModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransporteModel", new { id = transporteModel.IdTransporte }, transporteModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransporteModel(int id)
        {
            var transporteModel = await _context.Transportes.FindAsync(id);
            if (transporteModel == null)
            {
                return NotFound();
            }

            _context.Transportes.Remove(transporteModel);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = $"Se ha eliminado el transporte con id {id} correctamente." });
        }


    }
}
