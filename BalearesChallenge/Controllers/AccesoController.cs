using BalearesChallenge.Data;
using BalearesChallenge.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BalearesChallenge.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using BalearesChallenge.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace BalearesChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly AppDBContext _appDbContext;
        private readonly IConfiguration _configuration;

        public AccesoController(AppDBContext appDBContext, IConfiguration configuration)
        {
            _appDbContext = appDBContext;
            _configuration = configuration;
        }

        [HttpPost("Registrarse")]
        public async Task<IActionResult> Registrarse([FromBody] UsuarioModel pUsuario)
        {
            string salt;
            string hash = Utilidades.EncriptarClaveRegistro(pUsuario.Password, out salt); // Genera el hash y salt

            UsuarioModel user = new UsuarioModel()
            {
                Correo = pUsuario.Correo,
                Password = hash,
                Nombre = pUsuario.Nombre,
                Apellido = pUsuario.Apellido,
                Salt = salt
            };

            try
            {
                var result = await _appDbContext.Usuarios.AddAsync(user);
                await _appDbContext.SaveChangesAsync();

                if (user.IdUsuario == 0)
                    return BadRequest("Error. No se pudo crear el usuario.");

                await _appDbContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Se a creado el usuario exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Ingresar")]
        public async Task<IActionResult> Login([FromBody] UsuarioDTO pUsuario)
        {
            UsuarioModel? usuarioEncontrado = await _appDbContext.Usuarios
                                                     .Where(u => u.Correo == pUsuario.Correo)
                                                     .FirstOrDefaultAsync();

            if (usuarioEncontrado == null)
                return BadRequest("Error. Usuario no encontrado.");

            // Recupera el salt del usuario que intenta loggearse
            string saltAlmacenado = usuarioEncontrado.Salt;

            // Recalcula el hash con el salt almacenado
            string hashIngresado = Utilidades.EncriptarClaveLogin(pUsuario.Password, saltAlmacenado); 

            if (hashIngresado != usuarioEncontrado.Password)
                return BadRequest("Error. Ingrese correctamente el mail y la contraseña.");

            string userToken = CreacionTokenAuth(usuarioEncontrado);

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ha ingresado al sistema correctamente.", token = userToken });
        }

        [Authorize]
        [HttpPost("CerrarSesion")]
        public async Task<IActionResult> Logout()
        {
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ha cerrado sesión." });
        }

        private string CreacionTokenAuth(UsuarioModel usuario)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Surname, usuario.Apellido),
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(20),
                        signingCredentials: cred
                );

            string jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
