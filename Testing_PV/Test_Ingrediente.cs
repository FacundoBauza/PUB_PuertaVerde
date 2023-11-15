using System.Collections.Generic;
using BusinessLayer.Implementations;
using BusinessLayer.Interfaces;
using DataAccesLayer.Implementations;
using DataAccesLayer;
using DataAccesLayer.Interface;
using Domain.DT;
using Domain.Entidades;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebApi_PUB_PV.Controllers;
using WebApi_PUB_PV.Models;

namespace Testing_PV
{

    [TestFixture]
    public class Test_Ingrediente
    {
        // Define una implementación Mock o Falsa de IB_Mesa para pruebas
        private IngredienteController controller;
        private DTIngrediente validIngrediente;
        private DTIngrediente invalidIngrediente;
        private Mock<IDAL_Ingrediente> mockDal;
        private Mock<IDAL_Casteo> mockCasteo;
        private Mock<IDAL_FuncionesExtras> mockFunciones;
        private B_Ingrediente bl;


        [SetUp]
        public void Configuracion()
        {
            // Inicializa
            mockDal = new Mock<IDAL_Ingrediente>();
            mockCasteo = new Mock<IDAL_Casteo>();
            mockFunciones = new Mock<IDAL_FuncionesExtras>();
            bl = new(mockDal.Object, mockCasteo.Object, mockFunciones.Object);
            // Crea una instancia de IngredienteController con el servicio
            controller = new IngredienteController(bl);
            //datos necesarios para todos los test
            validIngrediente = new DTIngrediente
            {
                id_Ingrediente = 0,
                nombre = "test",
                stock = 0,
                id_Categoria = 1,
            };
            invalidIngrediente = new DTIngrediente
            {
                id_Ingrediente = -1,
                nombre = "test",
                stock = 0,
                id_Categoria = 1,
            };
        }

        [Test]
        public void Post_ValidIngrediente_ReturnsOkResult()
        {
            // Configura el comportamiento del mock para Agregar_Ingrediente
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "El Ingrediente se guardo correctamente" };
            mockDal.Setup(bl => bl.set_Ingrediente(validIngrediente)).Returns(true);

            // Act
            var result = controller.Post(validIngrediente) as OkObjectResult;

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
        public void Post_InvalidIngrediente_ReturnsBadRequestResult()
        {
            // Configura el comportamiento del mock para Agregar_Ingrediente
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Exepción no controlada" };
            mockDal.Setup(bl => bl.set_Ingrediente(invalidIngrediente)).Returns(false);

            // Act
            var result = controller.Post(invalidIngrediente) as BadRequestObjectResult;

            // Assert
            // Añade aserciones específicas según tu comportamiento esperado
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<StatusResponse>(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }

        [Test]
        public void Delete_ValidId_ReturnsOkResult()
        {
            // Configura el comportamiento del mock para Eliminar_Ingrediente
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "El ingrediente fue eliminado" };
            mockDal.Setup(bl => bl.Eliminar_Ingredente(validIngrediente.id_Ingrediente)).Returns(mensajeRetorno);

            // Act
            var result = controller.Delete(validIngrediente.id_Ingrediente) as OkObjectResult;

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
        public void Delete_InvalidId_ReturnsBadRequestResult()
        {
            // Configura el comportamiento del mock para Eliminar_Ingrediente con un ID inválido
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "El ingrediente pertenece a un producto por lo que no es posible eliminarlo" };
            mockDal.Setup(bl => bl.Eliminar_Ingredente(invalidIngrediente.id_Ingrediente)).Returns(mensajeRetorno);

            // Act
            var result = controller.Delete(invalidIngrediente.id_Ingrediente) as BadRequestObjectResult;

            // Assert
            // Añade aserciones específicas según tu comportamiento esperado
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<StatusResponse>(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }

        [Test]
        public void Put_ValidIngrediente_ReturnsOkResult()
        {
            // Configura el comportamiento del mock para Modificar_Ingrediente
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "El Ingrediente se modifico correctamente" };
            mockDal.Setup(bl => bl.modificar_Ingrediente(validIngrediente)).Returns(true);

            // Act
            var result = controller.Put(validIngrediente) as OkObjectResult;

            // Assert
            // Añade
            // aserciones específicas según tu comportamiento esperado
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<StatusResponse>(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }

        [Test]
        public void Put_InvalidIngrediente_ReturnsBadRequestResult()
        {
            // Configura el comportamiento del mock para Modificar_Ingrediente con un DTO inválido
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Exepción no controlada" };
            mockDal.Setup(bl => bl.modificar_Ingrediente(invalidIngrediente)).Returns(false);

            // Act
            var result = controller.Put(invalidIngrediente) as BadRequestObjectResult;

            // Assert
            // Añade aserciones específicas según tu comportamiento esperado
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<StatusResponse>(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }
        [Test]
        public void Get_ReturnsListOfIngrediente_NotNullAndNoNullElements()
        {
            var DC = new DataContext();
            var dal = new DAL_Ingrediente(DC);
            var cast = new DAL_Casteo();
            var fun = new DAL_FuncionesExtras(DC, cast);
            var bl = new B_Ingrediente(dal, cast, fun);
            var controllerget = new IngredienteController(bl);

            // Act
            var result = controllerget.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<DTIngrediente>>(result);

            // Verifica que la lista no sea null
            Assert.IsNotNull(result);

            // Verifica que ninguno de los elementos en la lista sea null
            Assert.IsTrue(result.TrueForAll(ingrediente => ingrediente != null));
        }
    }
}
