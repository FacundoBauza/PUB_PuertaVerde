using BusinessLayer.Interfaces;
using Castle.Core.Configuration;
using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi_PUB_PV.Controllers;
using WebApi_PUB_PV.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Testing_PV
{
    public class Test_Ingrediente
    {
        [SetUp]
        public void Setup()
        {
            public UserManager<Usuarios> _userManager;
            public RoleManager<IdentityRole> _roleManager;
            public IConfiguration _configuration;
        }

        //-----------------------------Agregar------------------------------
        [Test]
        public void Post_ValidIngrediente_ReturnsOkResult()
        {
            // Arrange
            var mockBL = new Mock<IB_Ingrediente>();
            var controller = new IngredienteController(mockBL.Object);
            var ingrediente = new DTIngrediente(0, "Peregil", 40, 3);
            var mensajeRetorno = new MensajeRetorno { status = true, mensaje = "Ingrediente agregado correctamente" };

            mockBL.Setup(bl => bl.Agregar_Ingrediente(It.IsAny<DTIngrediente>())).Returns(mensajeRetorno);

            // Act
            var result = controller.Post(ingrediente) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }

        [Test]
        public void Post_InvalidIngrediente_ReturnsBadRequest()
        {
            // Arrange
            var mockBL = new Mock<IB_Ingrediente>();
            var controller = new IngredienteController(mockBL.Object);
            var mesa = new DTIngrediente(0, "Tomate", 40, 3);
            var mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Ya existe el ingrediente" };

            mockBL.Setup(bl => bl.Agregar_Ingrediente(It.IsAny<DTIngrediente>())).Returns(mensajeRetorno);

            // Act
            var result = controller.Post(mesa) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }

        //-----------------------------Modificar------------------------------
        [Test]
        public void ModificarIngrediente_IngredienteModificado_RetornaOk()
        {
            // Arrange
            var mockBL = new Mock<IB_Ingrediente>();
            var controller = new IngredienteController(mockBL.Object);
            DTIngrediente modificar = new DTIngrediente(13, "Tomate Rojo", 40, 3);
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "Ingrediente modificado correctamente" };
            mockBL.Setup(bl => bl.Modificar_Ingrediente(It.IsAny<DTIngrediente>())).Returns(mensajeRetorno);

            // Act
            var resultado = controller.Put(modificar) as ObjectResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)resultado.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)resultado.Value).StatusMessage);
        }

        [Test]
        public void ModificarIngrediente_IngredienteInvalido_RetornaBadRequest()
        {
            // Arrange
            var mockBL = new Mock<IB_Ingrediente>();
            var controller = new IngredienteController(mockBL.Object);
            DTIngrediente modificar = new DTIngrediente(0, "Peregil", 40, 3);
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "El ingrediente no existe" };
            mockBL.Setup(bl => bl.Modificar_Ingrediente(It.IsAny<DTIngrediente>())).Returns(mensajeRetorno);

            // Act
            var resultado = controller.Put(modificar) as ObjectResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(400, resultado.StatusCode);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)resultado.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)resultado.Value).StatusMessage);
        }

        [Test]
        public void ModificarIngrediente_UsuarioNoAutorizado_RetornaUnauthorized()
        {
            // Arrange
            var mockBL = new Mock<IB_Ingrediente>();
            var mockBLUser = new Mock<IB_Usuario>();
            var controller = new IngredienteController(mockBL.Object);
            var controllerUser = new AuthController(mockBLUser.Object);

            // Configura un contexto de usuario simulado con el rol "USER"
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, "NombreUsuario"),
                new Claim(ClaimTypes.Role, "USER")
            }));

            // Establece el contexto de usuario en el controlador
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var username = User.Identity.Name;



            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(401, resultado.StatusCode);
            Assert.AreEqual(false, ((StatusResponse)resultado.Value).StatusOk);
            Assert.AreEqual("Usuario no autorizado", ((StatusResponse)resultado.Value).StatusMessage);
        }
    }
}
