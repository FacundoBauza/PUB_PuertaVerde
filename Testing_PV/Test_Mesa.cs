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
using BusinessLayer.Implementations;
using DataAccesLayer.Interface;
using DataAccesLayer.Implementations;
using DataAccesLayer;

namespace Testing_PV
{
    [TestFixture]
    public class Test_Mesa
    {
        
        private MesaController mesaController;
        private DTMesa validMesa;
        private DTMesa invalidMesa; 
        private Mock<IDAL_Mesa> mockDal;
        private Mock<IDAL_Casteo> mockCasteo;
        private Mock<IDAL_FuncionesExtras>mockFunciones;
        private B_Mesa bl;

        [SetUp]
        public void Configuracion()
        {
            // Inicializa
            mockDal = new Mock<IDAL_Mesa>();
            mockCasteo = new Mock<IDAL_Casteo>();
            mockFunciones = new Mock<IDAL_FuncionesExtras>();
            bl = new(mockDal.Object, mockCasteo.Object, mockFunciones.Object);
            // Crea una instancia de MesaController con el servicio
            mesaController = new MesaController(bl);
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
            var mensajeRetorno = new MensajeRetorno { status = true, mensaje = "La mesa se guardo correctamente" };

            // Configura el comportamiento del mock
            mockDal.Setup(bl => bl.Set_Mesa(It.IsAny<DTMesa>())).Returns(true);

            // Act
            var result = mesaController.Post(validMesa) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }

        [Test]
        public void Post_InvalidMesa_RetornaBadRequest()
        {
            // Arrange
            var mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Exepción no controlada" };

            // Configura el comportamiento del mock
            mockDal.Setup(bl => bl.Set_Mesa(It.IsAny<DTMesa>())).Returns(false);

            // Act
            var result = mesaController.Post(invalidMesa) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }

        [Test]
        public void BajaMesa_IdExistente_RetornaOk()
        {
            // Arrange
            var mensajeRetorno = new MensajeRetorno { status = true, mensaje = "La mesa se dio de baja correctamente" };

            // Configura el comportamiento del mock
            mockDal.Setup(bl => bl.Baja_Mesa(It.IsAny<int>())).Returns(true);
            // Act
            var resultado = mesaController.BajaMesa(validMesa.id_Mesa) as ObjectResult;
            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);
            Assert.IsNotNull(resultado.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)resultado.Value).StatusOk);
                Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)resultado.Value).StatusMessage);
        }

        [Test]
        public void BajaMesa_IdInexistente_RetornaBadRequest()
        {
            // Arrange
            var mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Exepción no controlada" };

            // Configura el comportamiento del mock
            mockDal.Setup(bl => bl.Baja_Mesa(It.IsAny<int>())).Returns(false);
            
            // Act
            var resultado = mesaController.BajaMesa(invalidMesa.id_Mesa) as ObjectResult;
           
            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(400, resultado.StatusCode);
            Assert.IsNotNull(resultado.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)resultado.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)resultado.Value).StatusMessage);
        }
        [Test]
        public void Put_DatosValidos_RetornaOk()
        {
            // Arrange
            var mensajeRetorno = new MensajeRetorno { status = true, mensaje = "La mesa se guardo correctamente" };

            // Configura el comportamiento del mock
            mockDal.Setup(bl => bl.Modificar_Mesas(It.IsAny<DTMesa>())).Returns(true);

            // Act
            var resultado = mesaController.Put(validMesa) as ObjectResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);
            Assert.IsNotNull(resultado.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)resultado.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)resultado.Value).StatusMessage);
        }
        [Test]
        public void Put_DatosValidos_RetornaBadRequest()
        {
            // Arrange
            var mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Exepción no controlada" };

            // Configura el comportamiento del mock
            mockDal.Setup(bl => bl.Modificar_Mesas(It.IsAny<DTMesa>())).Returns(false);
            
            // Act
            var resultado = mesaController.Put(invalidMesa) as ObjectResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(400, resultado.StatusCode);
            Assert.IsNotNull(resultado.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)resultado.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)resultado.Value).StatusMessage);
        }
        [Test]
        public void PutPrecio_DatosValidos_RetornaOk()
        {
            // Arrange
            var mensajeRetorno = new MensajeRetorno { status = true, mensaje = "La mesa se guardo correctamente" };
            
            // Configura el comportamiento del mock
            mockDal.Setup(bl => bl.Modificar_Precio_Mesas(It.IsAny<DTMesa>())).Returns(true);

            // Act
            var resultado = mesaController.PutPrecio(validMesa) as ObjectResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);
            Assert.IsNotNull(resultado.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)resultado.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)resultado.Value).StatusMessage);
        }
        [Test]
        public void PutPrecio_DatosValidos_RetornaBadRequest()
        {
            // Arrange
            var mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Exepción no controlada" };

            // Configura el comportamiento del mock
            mockDal.Setup(bl => bl.Modificar_Precio_Mesas(It.IsAny<DTMesa>())).Returns(false);
            // Act
            var resultado = mesaController.PutPrecio(invalidMesa) as ObjectResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(400, resultado.StatusCode);
            Assert.IsNotNull(resultado.Value);
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
            var DC = new DataContext();
            var dal = new DAL_Mesa(DC);
            var cast = new DAL_Casteo();
            var fun = new DAL_FuncionesExtras(DC,cast);
            var bl = new B_Mesa(dal, cast, fun);
            var controllerget = new MesaController(bl);

            // Act
            var result = controllerget.Get();

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
