using BusinessLayer.Interfaces;
using Domain.DT;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WebApi_PUB_PV.Controllers;
using Domain.Entidades;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi_PUB_PV.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using DataAccesLayer.Models;

namespace WebApi_PUB_PV.Tests.Controllers
{
    [TestFixture]
    public class Test_Mesa
    {
        // Define una implementación Mock o Falsa de IB_Mesa para pruebas
        private IB_Mesa mesaService;

        private MesaController mesaController;

        [SetUp]
        public void Configuracion()
        {
            // Inicializa la implementación Mock o Falsa

            var mockBL = new Mock<IB_Mesa>();
            // Puedes utilizar una biblioteca de simulación como Moq para crear un mock
            // Crea una instancia de MesaController con el servicio falso
            mesaController = new MesaController(mesaService);
        }

        [Test]
        public void Post_ValidMesa_ReturnsOkResult()
        {
            // Arrange
            var mockBL = new Mock<IB_Mesa>();
            var controller = new MesaController(mockBL.Object);
            var mesa = new DTMesa { id_Mesa = 0, enUso = false, precioTotal = 0, nombre = "" };
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
            var mesa = new DTMesa
            {
                id_Mesa = 1,
                enUso = false,
                precioTotal = 0,
                nombre = "1",
            };
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

        [Test]
        public void BajaMesa_IdExistente_RetornaOk()
        {
            // Arrange
            var id = 1; // Proporciona un ID existente
            var mockBL = new Mock<IB_Mesa>();
            var controller = new MesaController(mockBL.Object);
            var mensajeRetorno = new MensajeRetorno { status = true, mensaje = "La mesa se dio de baja correctamente" };
            mockBL.Setup(bl => bl.Baja_Mesa(It.IsAny<int>())).Returns(mensajeRetorno);
            // Act
            var resultado = mesaController.BajaMesa(id) as ObjectResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)resultado.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)resultado.Value).StatusMessage);
        }

        [Test]
        public void Put_DatosValidos_RetornaOk()
        {
            // Arrange
            var mesa = new DTMesa
            {
                id_Mesa = 1,
                enUso = false,
                precioTotal = 0,
                nombre = "1",
            };

            // Act
            var mensajeRetorno = new MensajeRetorno { status = true, mensaje = "La mesa se guardo correctamente" };
            var resultado = mesaController.Put(mesa) as ObjectResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)resultado.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)resultado.Value).StatusMessage);
        }

        [Test]
        public void CerarMesa_DatosValidos_RetornaByteArray()
        {
            // Arrange
            var mesa = new DTMesa
            {
                id_Mesa = 1,
                enUso = false,
                precioTotal = 0,
                nombre = "1",
            };

            // Act
            var resultado = mesaController.CerarMesa(mesa);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<byte[]>(resultado.Value);
        }

    }
}
