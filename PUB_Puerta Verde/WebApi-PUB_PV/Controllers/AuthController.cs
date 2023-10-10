using BusinessLayer.Implementations;
using BusinessLayer.Interfaces;
using DataAccesLayer.Models;
using Domain.DT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi_PUB_PV.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApi_PUB_PV.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<Usuarios> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private IB_Usuario bl;

        public AuthController(IB_Usuario _bl,
                UserManager<Usuarios> userManager,
                RoleManager<IdentityRole> roleManager,
                IConfiguration configuration)
        {
            bl = _bl;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(LoginResponse), 200)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
#pragma warning disable CS8604 // Posible argumento de referencia nulo
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
            try
            {
                Usuarios user = await _userManager.FindByNameAsync(model.Username);

                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    if (user.registro_Activo)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);

                        var authClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.NameIdentifier, user.Id),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        };

                        foreach (var userRole in userRoles)
                        {
                            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                        }

                        var token = GetToken(authClaims);

                        LoginResponse lr = new LoginResponse();
                        lr.StatusOk = true;
                        lr.StatusMessage = "Usuario logueado correctamente!";
                        lr.IdUsuario = user.Id;
                        lr.Token = new JwtSecurityTokenHandler().WriteToken(token);
                        lr.Expiration = token.ValidTo;
                        lr.Email = user.Email;
                        lr.ExpirationMinutes = Convert.ToInt32((token.ValidTo - DateTime.UtcNow).TotalMinutes);
                        lr.Nombre = user.apellido + ", " + user.nombre;

                        return Ok(lr);
                    }
                    else
                    {
                        return Ok(new StatusResponse { StatusOk = true, StatusMessage = "Este Usuario esta desactivado" });
                    }       
                }
            }
            catch (Exception ex)
            {
                Unauthorized(new LoginResponse
                {
                    StatusOk = false,
                    StatusMessage = "Error: " + ex.Message,
                    Token = "",
                    Expiration = null
                });
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(typeof(StatusResponse), 200)]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponse { StatusOk = false, StatusMessage = "El usuario ya existe!" });

            Usuarios user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email,
                nombre = model.Nombre,
                apellido = model.Apellido
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                string errors = "";
                result.Errors.ToList().ForEach(x => errors += x.Description + " | ");
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponse { StatusOk = false, StatusMessage = "Error al crear usuario! Revisar los datos ingresados y probar nuevamente. Errores: " + errors });
            }

            // Asignar Rol User
            await _userManager.AddToRoleAsync(user, "USER");

            return Ok(new StatusResponse { StatusOk = true, StatusMessage = "Usuario creado correctamente!" });
        }

        [HttpGet]
        [Route("Usuario")]
        [Authorize(Roles = "USER")]
        public async Task<ActionResult<DTUsuario>> GetInformacionUsuario()
        {
            string username = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Usuarios usuario = await _userManager.Users.Where(x => x.UserName == username).FirstOrDefaultAsync();

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            DTUsuario response = new DTUsuario()
            {
                Apellido = usuario.apellido,
                Email = usuario.Email,
                Id = usuario.Id,
                Nombre = usuario.nombre,
                Username = usuario.UserName,
                registro_Activo = usuario.registro_Activo
            };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            // TODO Agregar roles.
            return response;
        }

        [HttpGet]
        [Route("Usuarios")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<IEnumerable<DTUsuario>>> GetUsuarios()
        {
            return await _userManager.Users.Select(x => new DTUsuario()
            {
                Apellido = x.apellido,
                Nombre = x.nombre,
                Email = x.Email,
                Username = x.UserName,
                registro_Activo = x.registro_Activo
            }).ToListAsync();
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [Route("AddRole")]
        [ProducesResponseType(typeof(StatusResponse), 200)]
        public async Task<IActionResult> AddRole([FromBody] AddRoleModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponse { StatusOk = false, StatusMessage = "No existe un usuario con username " + model.UserName });

            var roleExists = await _roleManager.FindByNameAsync(model.RoleName);
            if (roleExists == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponse { StatusOk = false, StatusMessage = "No existe un role con roleName " + model.RoleName });

            // Asignar Rol User
            await _userManager.AddToRoleAsync(userExists, roleExists.Name);

            return Ok(new StatusResponse { StatusOk = true, StatusMessage = "Rol agregado al usuario correctamente!" });
        }

        [HttpGet]
        [Route("ObtenerRoles")]
        public async Task<List<string>> GetRol(string username)
        {
            return (List<string>)await _userManager.GetRolesAsync(await _userManager.FindByNameAsync(username));
        }

        private void foreache(string v, object x, Task<IList<string>> task)
        {
            throw new NotImplementedException();
        }


        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            string? JWT_SECRET = Environment.GetEnvironmentVariable("JWT_SECRET");
            if (string.IsNullOrEmpty(JWT_SECRET))
                JWT_SECRET = _configuration["JWT:Secret"];

            SymmetricSecurityKey? authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_SECRET));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [Route("BajaUsuario")]
        [ProducesResponseType(typeof(StatusResponse), 200)]
        public async Task<IActionResult> UpdateUser(string username)
        {
            var userExists = await _userManager.FindByNameAsync(username);
            if (userExists == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponse { StatusOk = false, StatusMessage = "No existe un usuario con username " + username });

            userExists.registro_Activo = false;

            //Actulizar User
            await _userManager.UpdateAsync(userExists);

            return Ok(new StatusResponse { StatusOk = true, StatusMessage = "Usuario dado de Baja correctamente!" });
        }
    }
}
