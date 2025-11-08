using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MiniVentas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // 1️⃣ Leer credenciales del appsettings.json
            var userConfig = _configuration.GetSection("User");
            var usernameConfig = userConfig["Username"];
            var passwordConfig = userConfig["Password"];

            // 2️⃣ Validar usuario y contraseña
            if (request.Username != usernameConfig || request.Password != passwordConfig)
            {
                return Unauthorized("Usuario o contraseña incorrectos");
            }

            // 3️⃣ Crear claims con la info del usuario
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, usernameConfig),
            new Claim("nombre", userConfig["Nombre"]),
            new Claim("rol", userConfig["Rol"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            // 4️⃣ Crear token JWT
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // 5️⃣ Devolver el token
            return Ok(new
            {
                token = tokenString,
                expiracion = token.ValidTo,
                usuario = new
                {
                    Nombre = userConfig["Nombre"],
                    Rol = userConfig["Rol"]
                }
            });
        }




    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
