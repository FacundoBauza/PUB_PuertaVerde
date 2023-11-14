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
        public MesaController mesaController;
        public Mock<IB_Mesa> mockBL;
        public DTMesa validMesa;
        public DTMesa invalidMesa;

        [SetUp]
        public void Configuracion()
        {
            // Inicializa la implementación Mock o Falsa
            mockBL = new Mock<IB_Mesa>();
            // Puedes utilizar una biblioteca de simulación como Moq para crear un mock
            // Crea una instancia de MesaController con el servicio falso
            mesaController = new MesaController(mockBL.Object);
            validMesa = new DTMesa
            {
                id_Mesa = 0,
                enUso = false,
                precioTotal = 0,
                nombre = ""
            };
            invalidMesa = new DTMesa
            {
                id_Mesa = -1,
                enUso = false,
                precioTotal = 0,
                nombre = "1",
            };
        }

        [Test]
        public void Post_ValidMesa_RetornaOkResult()
        {
            // Arrange
            var mensajeRetorno = new MensajeRetorno { status = true, mensaje = "Mesa agregada correctamente" };

            // Configura el comportamiento del mock
            mockBL.Setup(bl => bl.Agregar_Mesa(It.IsAny<DTMesa>())).Returns(mensajeRetorno);

            // Act
            var result = mesaController.Post(validMesa) as ObjectResult;

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
            var mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Error al agregar la mesa" };

            // Configura el comportamiento del mock
            mockBL.Setup(bl => bl.Agregar_Mesa(It.IsAny<DTMesa>())).Returns(mensajeRetorno);

            // Act
            var result = mesaController.Post(invalidMesa) as ObjectResult;

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
            var mensajeRetorno = new MensajeRetorno { status = true, mensaje = "La mesa se dio de baja correctamente" };

            // Configura el comportamiento del mock
            mockBL.Setup(bl => bl.Baja_Mesa(It.IsAny<int>())).Returns(mensajeRetorno);
            // Act
            var resultado = mesaController.BajaMesa(validMesa.id_Mesa) as ObjectResult;
            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);
            if (resultado.Value != null)
            {
                Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)resultado.Value).StatusOk);
                Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)resultado.Value).StatusMessage);
            }
        }

        [Test]
        public void BajaMesa_IdInexistente_RetornaBadRequest()
        {
            // Arrange
            var mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Exepción no controlada" };

            // Configura el comportamiento del mock
            mockBL.Setup(bl => bl.Baja_Mesa(It.IsAny<int>())).Returns(mensajeRetorno);
            
            // Act
            var resultado = mesaController.BajaMesa(invalidMesa.id_Mesa) as ObjectResult;
           
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
            var mensajeRetorno = new MensajeRetorno { status = true, mensaje = "La mesa se guardo correctamente" };

            // Configura el comportamiento del mock
            mockBL.Setup(bl => bl.Modificar_Mesa(It.IsAny<DTMesa>())).Returns(mensajeRetorno);

            // Act
            var resultado = mesaController.Put(validMesa) as ObjectResult;

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
            var mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Exepción no controlada" };

            // Configura el comportamiento del mock
            mockBL.Setup(bl => bl.Modificar_Mesa(It.IsAny<DTMesa>())).Returns(mensajeRetorno);
            
            // Act
            var resultado = mesaController.Put(invalidMesa) as ObjectResult;

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
            var mensajeRetorno = new MensajeRetorno { status = true, mensaje = "La mesa se guardo correctamente" };
            
            // Configura el comportamiento del mock
            mockBL.Setup(bl => bl.Modificar_Precio_Mesa(It.IsAny<DTMesa>())).Returns(mensajeRetorno);

            // Act
            var resultado = mesaController.PutPrecio(validMesa) as ObjectResult;

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
            var mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Exepción no controlada" };

            // Configura el comportamiento del mock
            mockBL.Setup(bl => bl.Modificar_Precio_Mesa(It.IsAny<DTMesa>())).Returns(mensajeRetorno);
            // Act
            var resultado = mesaController.PutPrecio(invalidMesa) as ObjectResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(400, resultado.StatusCode);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)resultado.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)resultado.Value).StatusMessage);
        }
        [Test]
        public void CerarMesa_DatosValidos_RetornaByteArray()
        {
            // Act
            var resultado = mesaController.CerarMesa(validMesa);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.IsInstanceOf<byte[]>(resultado.Value);
        }

        [Test]
        public void Get_ReturnsListOfMesa_NotNullAndNoNullElements()
        {
            // Configura el comportamiento del mock
            mockBL.Setup(bl => bl.Listar_Mesas()).Returns(new List<DTMesa> ());

            // Act
            var result = mesaController.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<DTMesa>>(result);

            // Verifica que la lista no sea null
            Assert.IsNotNull(result);

            // Verifica que ninguno de los elementos en la lista sea null
            Assert.IsTrue(result.TrueForAll(mesa => mesa != null));
        }
    }
}
