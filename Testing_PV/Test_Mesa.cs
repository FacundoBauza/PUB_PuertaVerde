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

namespace Testing_PV
{
    [TestFixture]
    public class Test_Mesa
    {
        // Define una implementación Mock o Falsa de IB_Mesa para pruebas
        public IB_Mesa mesaService;

        public MesaController mesaController;
        public Mock<IB_Mesa> mockBL;

        [SetUp]
        public void Configuracion()
        {
            // Inicializa la implementación Mock o Falsa

            mockBL = new Mock<IB_Mesa>();
            // Puedes utilizar una biblioteca de simulación como Moq para crear un mock
            // Crea una instancia de MesaController con el servicio falso
            mesaController = new MesaController(mockBL.Object);
        }

        [Test]
        public void Post_ValidMesa_RetornaOkResult()
        {
            // Arrange
            var mesa = new DTMesa { id_Mesa = 0, enUso = false, precioTotal = 0, nombre = "" };
            var mensajeRetorno = new MensajeRetorno { status = true, mensaje = "Mesa agregada correctamente" };

            mockBL.Setup(bl => bl.Agregar_Mesa(It.IsAny<DTMesa>())).Returns(mensajeRetorno);

            // Act
            var result = mesaController.Post(mesa) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }

        [Test]
        public void Post_InvalidMesa_RetornaBadRequest()
        {
            // Arrange
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
            var result = mesaController.Post(mesa) as ObjectResult;

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
            int id = 6; // Proporciona un ID existente
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
        public void BajaMesa_IdInexistente_RetornaBadRequest()
        {
            // Arrange
            int id = 0; // Proporciona un ID inexistente
            var mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Exepción no controlada" };
            mockBL.Setup(bl => bl.Baja_Mesa(It.IsAny<int>())).Returns(mensajeRetorno);
            // Act
            var resultado = mesaController.BajaMesa(id) as ObjectResult;
            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(400, resultado.StatusCode);
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
            mockBL.Setup(bl => bl.Modificar_Mesa(It.IsAny<DTMesa>())).Returns(mensajeRetorno);
            var resultado = mesaController.Put(mesa) as ObjectResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)resultado.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)resultado.Value).StatusMessage);
        }
        [Test]
        public void Put_DatosValidos_RetornaBadRequest()
        {
            // Arrange
            var mesa = new DTMesa
            {
                id_Mesa = 0,
                enUso = false,
                precioTotal = 0,
                nombre = "1",
            };

            // Act
            var mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Exepción no controlada" };
            
            mockBL.Setup(bl => bl.Modificar_Mesa(It.IsAny<DTMesa>())).Returns(mensajeRetorno);
            var resultado = mesaController.Put(mesa) as ObjectResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(400, resultado.StatusCode);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)resultado.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)resultado.Value).StatusMessage);
        }
        [Test]
        public void PutPrecio_DatosValidos_RetornaOk()
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
            mockBL.Setup(bl => bl.Modificar_Precio_Mesa(It.IsAny<DTMesa>())).Returns(mensajeRetorno);
            var resultado = mesaController.PutPrecio(mesa) as ObjectResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)resultado.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)resultado.Value).StatusMessage);
        }
        [Test]
        public void PutPrecio_DatosValidos_RetornaBadRequest()
        {
            // Arrange
            var mesa = new DTMesa
            {
                id_Mesa = 0,
                enUso = false,
                precioTotal = 0,
                nombre = "1",
            };

            // Act
            var mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Exepción no controlada" };

            mockBL.Setup(bl => bl.Modificar_Precio_Mesa(It.IsAny<DTMesa>())).Returns(mensajeRetorno);
            var resultado = mesaController.PutPrecio(mesa) as ObjectResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(400, resultado.StatusCode);
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
