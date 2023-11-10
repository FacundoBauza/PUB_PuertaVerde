using System.Collections.Generic;
using BusinessLayer.Interfaces;
using Domain.DT;
using Domain.Entidades;
using iText.Kernel.Pdf.Canvas.Wmf;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebApi_PUB_PV.Controllers;
using WebApi_PUB_PV.Models;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace Testing_PV
{
    [TestFixture]
    internal class Test_Caja
    {
        [Test]
        public void Post_ValidCaja_ReturnsOkResult()
        {
            // Arrange
            var mockBusinessLayer = new Mock<IB_Caja>();
            var controller = new CajaController(mockBusinessLayer.Object);
            var validCaja = new DTCaja
            {
                id = 1,
                fecha = new(),
                totalPrecios = 0,
                estado = false
            };

            // Configura el comportamiento del mock para Set_Cajas
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "Caja agregada correctamente" };
            mockBusinessLayer.Setup(bl => bl.Set_Cajas(validCaja)).Returns(mensajeRetorno);

            // Act
            var result = controller.Post(validCaja) as OkObjectResult;

            // Assert
            // Añade aserciones específicas según tu comportamiento esperado
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<StatusResponse>(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }

        [Test]
        public void Post_InvalidCaja_ReturnsBadRequestResult()
        {
            // Arrange
            var mockBusinessLayer = new Mock<IB_Caja>();
            var controller = new CajaController(mockBusinessLayer.Object);
            var invalidCaja = new DTCaja
            {
                id = -1,
                fecha = new(),
                totalPrecios = 0,
                estado = false
            };
            // Configura el comportamiento del mock para Set_Cajas con un DTO inválido
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Error al agregar la caja" };
            mockBusinessLayer.Setup(bl => bl.Set_Cajas(invalidCaja)).Returns(mensajeRetorno);

            // Act
            var result = controller.Post(invalidCaja) as BadRequestObjectResult;

            // Assert
            // Añade más aserciones específicas según tu comportamiento esperado
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<StatusResponse>(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }

        [Test]
        public void Put_ValidCaja_ReturnsOkResult()
        {
            // Arrange
            var mockBusinessLayer = new Mock<IB_Caja>();
            var controller = new CajaController(mockBusinessLayer.Object);
            var validCaja = new DTCaja
            {
                id = 1,
                fecha = new(),
                totalPrecios = 0,
                estado = false
            };

            // Configura el comportamiento del mock para Modificar_Cajas
            MensajeRetorno mensajeRetorno =new MensajeRetorno { status = true, mensaje = "Caja modificada correctamente" };
            mockBusinessLayer.Setup(bl => bl.Modificar_Cajas(validCaja)).Returns(mensajeRetorno);

            // Act
            var result = controller.Put(validCaja) as OkObjectResult;

            // Assert
            // Añade más aserciones específicas según tu comportamiento esperado
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<StatusResponse>(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }

        [Test]
        public void Put_InvalidCaja_ReturnsBadRequestResult()
        {
            // Arrange
            var mockBusinessLayer = new Mock<IB_Caja>();
            var controller = new CajaController(mockBusinessLayer.Object);
            var invalidCaja = new DTCaja
            {
                id = -1,
                fecha = new(),
                totalPrecios = 0,
                estado = false
            };

            // Configura el comportamiento del mock para Modificar_Cajas con un DTO inválido
            MensajeRetorno mensajeRetorno =new MensajeRetorno { status = false, mensaje = "Error al modificar la caja" };
            mockBusinessLayer.Setup(bl => bl.Modificar_Cajas(invalidCaja)).Returns(mensajeRetorno);

            // Act
            var result = controller.Put(invalidCaja) as BadRequestObjectResult;

            // Assert
            // Añade más aserciones específicas según tu comportamiento esperado
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<StatusResponse>(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }
        [Test]
        public void BajaCaja_ValidId_ReturnsOkResult()
        {
            // Arrange
            var mockBusinessLayer = new Mock<IB_Caja>();
            var controller = new CajaController(mockBusinessLayer.Object);
            var validId = 1;

            // Configura el comportamiento del mock para Baja_Cajas
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "Caja dada de baja correctamente" };
            mockBusinessLayer.Setup(bl => bl.Baja_Cajas(validId)).Returns(mensajeRetorno);

            // Act
            var result = controller.BajaCaja(validId) as OkObjectResult;

            // Assert
            // Añade aserciones específicas según tu comportamiento esperado
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<StatusResponse>(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }

        [Test]
        public void BajaCaja_InvalidId_ReturnsBadRequestResult()
        {
            // Arrange
            var mockBusinessLayer = new Mock<IB_Caja>();
            var controller = new CajaController(mockBusinessLayer.Object);
            var invalidId = -1;

            // Configura el comportamiento del mock para Baja_Cajas con un ID inválido
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Error al dar de baja la caja" };
            mockBusinessLayer.Setup(bl => bl.Baja_Cajas(invalidId)).Returns(mensajeRetorno);

            // Act
            var result = controller.BajaCaja(invalidId) as BadRequestObjectResult;

            // Assert
            // Añade aserciones específicas según tu comportamiento esperado
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<StatusResponse>(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }
    }
}
