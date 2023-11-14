using System.Collections.Generic;
using BusinessLayer.Interfaces;
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
        public IB_Ingrediente Service;
        public IngredienteController controller;
        public Mock<IB_Ingrediente> mockBusinessLayer;
        public DTIngrediente validIngrediente;
        public DTIngrediente invalidIngrediente;

        [SetUp]
        public void Configuracion()
        {
            // Inicializa la implementación Mock o Falsa
            mockBusinessLayer = new Mock<IB_Ingrediente>();
            // utiliza biblioteca de simulación como Moq para crear un mock
            // Crea una instancia de MesaController con el servicio falso
            controller = new IngredienteController(mockBusinessLayer.Object);
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
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "Ingrediente agregado correctamente" };
            mockBusinessLayer.Setup(bl => bl.Agregar_Ingrediente(validIngrediente)).Returns(mensajeRetorno);

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
            // Arrange
            var mockBusinessLayer = new Mock<IB_Ingrediente>();
            var controller = new IngredienteController(mockBusinessLayer.Object);

            // Configura el comportamiento del mock para Agregar_Ingrediente
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Error al agregar el ingrediente" };
            mockBusinessLayer.Setup(bl => bl.Agregar_Ingrediente(invalidIngrediente)).Returns(mensajeRetorno);

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
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "Ingrediente eliminado correctamente" };
            mockBusinessLayer.Setup(bl => bl.Eliminar_Ingredente(validIngrediente.id_Ingrediente)).Returns(mensajeRetorno);

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
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Error al eliminar el ingrediente" };
            mockBusinessLayer.Setup(bl => bl.Eliminar_Ingredente(invalidIngrediente.id_Ingrediente)).Returns(mensajeRetorno);

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
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "Ingrediente modificado correctamente" };
            mockBusinessLayer.Setup(bl => bl.Modificar_Ingrediente(validIngrediente)).Returns(mensajeRetorno);

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
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Error al modificar el ingrediente" };
            mockBusinessLayer.Setup(bl => bl.Modificar_Ingrediente(invalidIngrediente)).Returns(mensajeRetorno);

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
            // Configura el comportamiento del mock para Listar_Ingrediente
            mockBusinessLayer.Setup(bl => bl.Listar_Ingrediente()).Returns(new List<DTIngrediente> ());

            // Act
            var result = controller.Get();

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
