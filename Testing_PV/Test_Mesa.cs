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
    public class Test_Mesa
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Post_ValidMesa_ReturnsOkResult()
        {
            // Arrange
            var mockBL = new Mock<IB_Mesa>();
            var controller = new MesaController(mockBL.Object);
            var mesa = new DTMesa { /* configurar la mesa de prueba */ };
            var mensajeRetorno = new MensajeRetorno { status = true, mensaje = "Mesa agregada correctamente" };

            mockBL.Setup(bl => bl.Agregar_Mesa(It.IsAny<DTMesa>())).Returns(mensajeRetorno);

            // Act
            var result = controller.Post(mesa) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }

        [Test]
        public void Post_InvalidMesa_ReturnsBadRequest()
        {
            // Arrange
            var mockBL = new Mock<IB_Mesa>();
            var controller = new MesaController(mockBL.Object);
            var mesa = new DTMesa { /* configurar una mesa inválida */ };
            var mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Error al agregar la mesa" };

            mockBL.Setup(bl => bl.Agregar_Mesa(It.IsAny<DTMesa>())).Returns(mensajeRetorno);

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
