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
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.Contracts;
using BalearesChallenge.Services;

namespace BalearesChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactoController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly Utilidades _utilidades;

        public ContactoController(AppDBContext context, Utilidades utilidades)
        {
            _context = context;
            _utilidades = utilidades;
        }

        [HttpPost]
        public async Task<ActionResult<ContactoModel>> PostContacto(ContactoModel contacto)
        {
            try
            {
                if (contacto.IdTransporte.HasValue && !_utilidades.TransporteModelExists(contacto.IdTransporte.Value))
                    return NotFound("Transporte no encontrado para actualizar.");

                var validacion = ValidarContacto(contacto);

                if (validacion != null)
                    return validacion;

                if (contacto.IdTransporte > 0)
                {
                    bool transporteEnUso = await _context.Contactos
                        .Where(c => c.IdTransporte == contacto.IdTransporte && c.IdContacto != contacto.IdContacto)
                        .AnyAsync();

                    if (transporteEnUso)
                        return BadRequest("El transporte ya está asignado a otro contacto. Intente con otro transporte por favor.");

                    var transporte = await _context.Transportes.FindAsync(contacto.IdTransporte);

                    if (transporte == null)
                        return NotFound("Error. Transporte no encontrado.");

                    contacto.Transporte = transporte;
                }
                else
                {
                    contacto.Transporte = null;
                }

                _context.Contactos.Add(contacto);
                
                await _context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Se añadió el contacto correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al crear el contacto: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactoModel>> GetContactoById(int id)
        {
            try
            {
                var contactoModel = await _context.Contactos.Include(c => c.Transporte)
                                                  .Where(c => c.IdContacto == id)
                                                  .Select(c => new
                                                  {
                                                      c.IdContacto,
                                                      c.Nombre,
                                                      c.Empresa,
                                                      c.Email,
                                                      c.FechaNacimiento,
                                                      c.Telefono,
                                                      c.Direccion,
                                                      c.IdTransporte,
                                                      TipoTransporte = c.Transporte != null ? c.Transporte.TipoTransporte : null
                                                  }).FirstOrDefaultAsync();

                if (contactoModel == null)
                    return NotFound("Contacto no encontrado.");


                return Ok(contactoModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener el contacto: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactoModel(int id, [FromBody] ContactoModel pContacto)
        {
            var validacion = ValidarContacto(pContacto);

            if (validacion != null)
                return validacion;

            var contactoModel = await _context.Contactos.Include(c => c.Transporte)
                                              .FirstOrDefaultAsync(c => c.IdContacto == id);

            if (contactoModel == null)
                return NotFound("Error. Contacto no encontrado.");

            if (pContacto.IdTransporte > 0)
            {
                bool transporteEnUso = await _context.Contactos
                    .Where(c => c.IdTransporte == pContacto.IdTransporte && c.IdContacto != id)
                    .AnyAsync();

                if (transporteEnUso)
                    return BadRequest("El transporte ya está asignado a otro contacto. Intente con otro transporte por favor.");

                var transporte = await _context.Transportes.FindAsync(pContacto.IdTransporte);

                if (transporte == null)
                    return NotFound("Error. Transporte no encontrado.");

                contactoModel.Transporte = transporte;
            }
            else
            {
                contactoModel.Transporte = null;
            }

            contactoModel.Nombre = pContacto.Nombre;
            contactoModel.Empresa = pContacto.Empresa;
            contactoModel.Email = pContacto.Email;
            contactoModel.FechaNacimiento = pContacto.FechaNacimiento;
            contactoModel.Telefono = pContacto.Telefono;
            contactoModel.Direccion = pContacto.Direccion;

            _context.Entry(contactoModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = $"Se ha modificado el contacto con id {id} correctamente." });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactoExists(id))
                {
                    return NotFound("Contacto no encontrado para actualizar.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error. Por favor actualice algún dato del contacto.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al actualizar el contacto: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContacto(int id)
        {
            try
            {
                var contactoModel = await _context.Contactos.FindAsync(id);
                if (contactoModel == null)
                {
                    return NotFound("Contacto no encontrado para eliminar.");
                }

                _context.Contactos.Remove(contactoModel);
                await _context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = $"Se ha eliminado el usuario con id {id} correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al eliminar el contacto: {ex.Message}");
            }
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<ContactoModel>>> BuscarContacto([FromQuery] string? email, [FromQuery] int? telefono)
        {
            try
            {
                var query = _context.Contactos.AsQueryable();

                if (!string.IsNullOrWhiteSpace(email))
                {
                    query = query.Where(c => c.Email == email);
                }

                if (telefono != null)
                {
                    query = query.Where(c => c.Telefono == telefono);
                }

                var contactos = await query.ToListAsync();

                if (!contactos.Any())
                {
                    return NotFound("No se encontraron contactos con los criterios especificados.");
                }

                return contactos;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al buscar contactos: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactoModel>>> GetContactos()
        {
            try
            {
                var contactos = await _context.Contactos.Select(c => new
                {
                    c.IdContacto,
                    c.Nombre,
                    c.Empresa,
                    c.Email,
                    c.FechaNacimiento,
                    c.Telefono,
                    c.Direccion,
                    c.Transporte.TipoTransporte
                }).ToListAsync();

                return Ok(contactos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener contactos: {ex.Message}");
            }
        }

        [HttpGet("direccion")]
        public async Task<ActionResult<IEnumerable<ContactoModel>>> GetContactosDireccion([FromQuery] string? provincia, [FromQuery] string? ciudad)
        {
            try
            {
                var query = _context.Contactos.AsQueryable();

                var contactos = await _context.Contactos.ToListAsync();

                var resultados = contactos.Where(c =>
                {
                    var dir = c.Direccion.ToLower().Split(',');

                    return dir.Length >= 3 &&
                           ((string.IsNullOrWhiteSpace(ciudad) && dir[1].Trim().Contains(ciudad.ToLower())) ||
                           (string.IsNullOrWhiteSpace(provincia) && dir[2].Trim().Contains(provincia.ToLower())));
                }).ToList();

                return resultados.Any() ? Ok(resultados) : NotFound("No se encontraron contactos en la dirección especificada.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al buscar contactos por dirección: {ex.Message}");
            }
        }

        [HttpGet("ordenados")]
        public async Task<ActionResult<IEnumerable<ContactoModel>>> GetContactosOrdenadosEmail()
        {
            try
            {
                return await _context.Contactos.OrderBy(c => c.Email).ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al ordenar contactos: {ex.Message}");
            }
        }

        private bool ContactoExists(int id)
        {
            return _context.Contactos.Any(e => e.IdContacto == id);
        }

        private ActionResult ValidarContacto(ContactoModel contacto)
        {
            string patronEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (string.IsNullOrEmpty(contacto.Nombre) || string.IsNullOrEmpty(contacto.Empresa) || string.IsNullOrEmpty(contacto.Email) ||
                contacto.FechaNacimiento == null || contacto.Telefono == null || string.IsNullOrEmpty(contacto.Direccion))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Ingrese todos los campos por favor.");
            }

            if (!Regex.IsMatch(contacto.Email, patronEmail))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Ingrese una dirección de email válida por favor.");
            }

            if (contacto.Nombre.Length < 2 || contacto.Empresa.Length < 2)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Los nombres del contacto o de la empresa deben tener al menos dos letras.");
            }

            if (!(contacto.FechaNacimiento is DateTime))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Ingrese una fecha de nacimiento válida en formato yyyy-MM-dd.");
            }

            if (!int.TryParse(contacto.Telefono.ToString(), out _))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Ingrese un campo numérico.");
            }

            return null;
        }

    }
}
