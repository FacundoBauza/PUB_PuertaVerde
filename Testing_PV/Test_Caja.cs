using System.Collections.Generic;
using BusinessLayer.Implementations;
using BusinessLayer.Interfaces;
using DataAccesLayer;
using DataAccesLayer.Implementations;
using DataAccesLayer.Interface;
using DataAccesLayer.Migrations;
using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;
using iText.Kernel.Pdf.Canvas.Wmf;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebApi_PUB_PV.Controllers;
using WebApi_PUB_PV.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace Testing_PV
{
    [TestFixture]
    internal class Test_Caja
    {
        // Define una implementación Mock o Falsa de IB_Mesa para pruebas
        private IB_Caja Service;
        private CajaController controller;
        //public Mock<IB_Caja> mockBusinessLayer;
        private DTCaja validCaja;
        private DTCaja invalidCaja;
        private Mock<IDAL_Caja> mockDal;
        private new Mock<IDAL_Casteo> mockCasteo;
        private B_Caja bl;

        [SetUp]
        public void Configuracion()
        {
            // Inicializa la implementación Mock o Falsa
            //mockBusinessLayer = new Mock<IB_Caja>();
            // Puedes utilizar una biblioteca de simulación como Moq para crear un mock
            mockDal = new Mock<IDAL_Caja>();
            mockCasteo = new Mock<IDAL_Casteo>();
            //bCaja = new B_Caja(mockDal.Object, mockCasteo.Object);
            bl = new(mockDal.Object, mockCasteo.Object);

            // Configura el comportamiento simulado del DAL
            // Crea una instancia de MesaController con el servicio
            controller = new CajaController(bl);
            //datos necesarios para todos los test
            validCaja = new DTCaja
            {
                id = 1,
                fecha = new(),
                totalPrecios = 0,
                estado = false
            };
            invalidCaja = new DTCaja
            {
                id = -1,
                fecha = new(),
                totalPrecios = 0,
                estado = false
            };
        }
        [Test]
        public void Post_ValidCaja_ReturnsOkResult()
        {
            // Arrange
            

            // Configura el comportamiento del mock para Set_Cajas
            //mockBusinessLayer.Setup(bl => bl.Set_Cajas(validCaja)).Returns(mensajeRetorno);
            MensajeRetorno mensajeRetorno = new MensajeRetorno {status = true  , mensaje = "Caja agregada con exito" };
            mockDal.Setup(d => d.Set_Caja(It.IsAny<DTCaja>())).Returns(mensajeRetorno);
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
            // Configura el comportamiento del mock para Set_Cajas con un DTO inválido
            //MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Error al agregar la caja" };
            //mockBusinessLayer.Setup(bl => bl.Set_Cajas(invalidCaja)).Returns(mensajeRetorno);
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Ya hay na caja activa" };
            mockDal.Setup(d => d.Set_Caja(It.IsAny<DTCaja>())).Returns(mensajeRetorno);

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

            // Configura el comportamiento del mock para Modificar_Cajas
            MensajeRetorno mensajeRetorno =new MensajeRetorno { status = true, mensaje = "La caja se guardo correctamente" };
            //mockBusinessLayer.Setup(bl => bl.Modificar_Cajas(validCaja)).Returns(mensajeRetorno);
            mockDal.Setup(d => d.Modificar_Cajas(It.IsAny<DTCaja>())).Returns(true);

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
            // Configura el comportamiento del mock para Modificar_Cajas con un DTO inválido
            MensajeRetorno mensajeRetorno =new MensajeRetorno { status = false, mensaje = "Exepción no controlada" };
            //mockBusinessLayer.Setup(bl => bl.Modificar_Cajas(invalidCaja)).Returns(mensajeRetorno);
            mockDal.Setup(d => d.Modificar_Cajas(It.IsAny<DTCaja>())).Returns(false);

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
        public void Get_ReturnsListOfCaja_NotNullAndNoNullElements()
        {
            // Configura el comportamiento del mock para GetCajas
            //mockBusinessLayer.Setup(bl => bl.GetCajas()).Returns(new List<DTCaja> ());
            // Act
            var DC = new DataContext();
            var dal = new DAL_Caja(DC);
            var cast = new DAL_Casteo();
            var bl= new B_Caja(dal,cast);

            // Configura el comportamiento simulado del DAL
            // Crea una instancia de MesaController con el servicio
            var controllerget = new CajaController(bl);
            var result = controllerget.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<DTCaja>>(result);

            // Verifica que la lista no sea null
            Assert.IsNotNull(result);

            // Verifica que ninguno de los elementos en la lista sea null
            Assert.IsTrue(result.TrueForAll(caja => caja != null));
        }
        [Test]
        public void SumarPrecioCaja_ReturnsOkResult_WhenStatusIsTrue()
        {

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "Se sumo el precio correctamente" };
            // Configura el comportamiento del mock para SumarPrecioCaja
            //mockBusinessLayer.Setup(bl => bl.SumarPrecioCaja(100)).Returns(mensajeRetorno);

            mockDal.Setup(d => d.SumarPrecioCaja(It.IsAny<float>())).Returns(true);
            // Act
            var result = controller.SumarPrecioCaja(100) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<StatusResponse>(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
            // Añade aserciones específicas según tu comportamiento esperado
        }

        [Test]
        public void SumarPrecioCaja_ReturnsBadRequestResult_WhenStatusIsFalse()
        {

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Exepción no controlada" };
            // Configura el comportamiento del mock para SumarPrecioCaja
            //mockBusinessLayer.Setup(bl => bl.SumarPrecioCaja(100)).Returns(mensajeRetorno);

            mockDal.Setup(d => d.SumarPrecioCaja(It.IsAny<float>())).Returns(false);
            // Act
            var result = controller.SumarPrecioCaja(100) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<StatusResponse>(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
            // Añade aserciones específicas según tu comportamiento esperado
        }

        [Test]
        public void BajaCaja_ValidId_ReturnsOkResult()
        {
            // Configura el comportamiento del mock para Baja_Cajas
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "caja dada de baja" };
            //mockBusinessLayer.Setup(bl => bl.Baja_Cajas(validCaja.id)).Returns(mensajeRetorno);

            mockDal.Setup(d => d.Baja_Caja(It.IsAny<int>())).Returns(true);
            // Act
            var result = controller.BajaCaja(validCaja.id) as OkObjectResult;

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
            // Configura el comportamiento del mock para Baja_Cajas con un ID inválido
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "error al bajar caja" };
            //mockBusinessLayer.Setup(bl => bl.Baja_Cajas(invalidCaja.id)).Returns(mensajeRetorno);

            mockDal.Setup(d => d.Baja_Caja(It.IsAny<int>())).Returns(false);
            // Act
            var result = controller.BajaCaja(invalidCaja.id) as BadRequestObjectResult;

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
