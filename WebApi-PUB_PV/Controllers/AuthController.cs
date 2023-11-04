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
            if (userExists != null && userExists.registro_Activo == true)
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponse { StatusOk = false, StatusMessage = "El usuario ya existe!" });

            Usuarios user = new()
            {
                registro_Activo = true,
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
        //[Authorize(Roles = "USER")]
        public async Task<ActionResult<DTUsuario>> GetInformacionUsuario()
        {
            string username = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Usuarios usuario = await _userManager.Users.Where(x => x.UserName == username).FirstOrDefaultAsync();

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            DTUsuario response = new DTUsuario()
            {
                apellido = usuario.apellido,
                email = usuario.Email,
                id = usuario.Id,
                nombre = usuario.nombre,
                username = usuario.UserName,
                registro_Activo = usuario.registro_Activo
            };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            // TODO Agregar roles.
            return response;
        }

        [HttpGet]
        [Route("Usuarios")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<IEnumerable<DTUsuario>>> GetUsuarios()
        {
            return await _userManager.Users.Select(x => new DTUsuario()
            {
                apellido = x.apellido,
                nombre = x.nombre,
                email = x.Email,
                username = x.UserName,
                registro_Activo = x.registro_Activo
            }).ToListAsync();
        }

        [HttpGet]
        [Route("listarUsuarios")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<IEnumerable<DTUsuarioInfo>>> listarUsuarios()
        {
            // Filtra los usuarios cuyo campo registroActivo sea true
            var usuariosActivos = await _userManager.Users
                .Where(user => user.registro_Activo)
                .Select(x => new DTUsuarioInfo()
                {
                    apellido = x.apellido,
                    nombre = x.nombre,
                    email = x.Email,
                    password = x.PasswordHash
                })
                .ToListAsync();

            return usuariosActivos;
        }


        [HttpPost]
        //[Authorize(Roles = "ADMIN")]
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
        public async Task<List<DTRol>> GetRol(string username)
        {
            List<DTRol> list = new List<DTRol>();
            foreach(string s in (List<string>)await _userManager.GetRolesAsync(await _userManager.FindByNameAsync(username)))
            {
                if (s.Equals("USER")) 
                    list.Add(new DTRol(1, s));
                else if (s.Equals("ADMIN"))
                    list.Add(new DTRol(2, s));
                else if (s.Equals("COCINA"))
                    list.Add(new DTRol(3, s));
                else if (s.Equals("CAJA"))
                    list.Add(new DTRol(4, s));
                else if (s.Equals("MOZO"))
                    list.Add(new DTRol(5, s));
            }

            return list;
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

        [HttpDelete]
        //[Authorize(Roles = "ADMIN")]
        [Route("BajaUsuario/{username}")]
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

        [HttpPut]
        //[Authorize(Roles = "ADMIN")]
        [Route("modificarUsuario")]
        [ProducesResponseType(typeof(StatusResponse), 200)]
        public async Task<IActionResult> modificarUser([FromBody] DTUsuarioInfo dtuser)
        {
            var userExists = await _userManager.FindByNameAsync(dtuser.email);
            if (userExists == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponse { StatusOk = false, StatusMessage = "No existe un usuario con username " + dtuser.email });

            userExists.nombre = dtuser.nombre;
            userExists.apellido = dtuser.apellido;

            //Actulizar User
            await _userManager.UpdateAsync(userExists);

            return Ok(new StatusResponse { StatusOk = true, StatusMessage = "Usuario modificado correctamente!" });
        }

        [HttpDelete]
        //[Authorize(Roles = "ADMIN")]
        [Route("BajaRole")]
        [ProducesResponseType(typeof(StatusResponse), 200)]
        public async Task<IActionResult> BajaRole([FromBody] AddRoleModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponse { StatusOk = false, StatusMessage = "No existe un usuario con username " + model.UserName });

            var roleExists = await _roleManager.FindByNameAsync(model.RoleName);
            if (roleExists.Name == "USER")
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponse { StatusOk = false, StatusMessage = "No se puede eliminar el Rol User" + model.RoleName });
            if (roleExists == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponse { StatusOk = false, StatusMessage = "No existe un role con roleName " + model.RoleName });

            // Quitar Rol al User
            await _userManager.RemoveFromRoleAsync(userExists, roleExists.Name);

            return Ok(new StatusResponse { StatusOk = true, StatusMessage = "Rol quitado del usuario correctamente!" });
        }
    }
}
