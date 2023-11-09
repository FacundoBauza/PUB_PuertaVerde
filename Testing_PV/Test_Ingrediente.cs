using BusinessLayer.Interfaces;
using Domain.DT;
using Domain.Entidades;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi_PUB_PV.Controllers;
using WebApi_PUB_PV.Models;

namespace Testing_PV
{
    public class Test_Ingrediente
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Post_ValidIngrediente_ReturnsOkResult()
        {
            // Arrange
            var mockBL = new Mock<IB_Ingrediente>();
            var controller = new IngredienteController(mockBL.Object);
            var ingrediente = new DTIngrediente { /* configurar la mesa de prueba */ };
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
            var mesa = new DTIngrediente { /* configurar una mesa inválida */ };
            var mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Error al agregar la mesa" };

            mockBL.Setup(bl => bl.Agregar_Ingrediente(It.IsAny<DTIngrediente>())).Returns(mensajeRetorno);

            // Act
            var result = controller.Post(mesa) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }
    }
}
