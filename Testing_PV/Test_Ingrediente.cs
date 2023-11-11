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

        [Test]
        public void Post_ValidIngrediente_ReturnsOkResult()
        {
            // Arrange
            var mockBusinessLayer = new Mock<IB_Ingrediente>();
            var controller = new IngredienteController(mockBusinessLayer.Object);
            var validIngrediente = new DTIngrediente
            {
                id_Ingrediente = 0,
                nombre = "test",
                stock = 0,
                id_Categoria = 1,
            };

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
            var invalidIngrediente = new DTIngrediente
            {
                id_Ingrediente = -1,
                nombre = "test",
                stock = 0,
                id_Categoria = 1,
            };

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
            // Arrange
            var mockBusinessLayer = new Mock<IB_Ingrediente>();
            var controller = new IngredienteController(mockBusinessLayer.Object);
            var validId = 1;

            // Configura el comportamiento del mock para Eliminar_Ingrediente
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "Ingrediente eliminado correctamente" };
            mockBusinessLayer.Setup(bl => bl.Eliminar_Ingredente(validId)).Returns(mensajeRetorno);

            // Act
            var result = controller.Delete(validId) as OkObjectResult;

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
            // Arrange
            var mockBusinessLayer = new Mock<IB_Ingrediente>();
            var controller = new IngredienteController(mockBusinessLayer.Object);
            var invalidId = -1;

            // Configura el comportamiento del mock para Eliminar_Ingrediente con un ID inválido
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Error al eliminar el ingrediente" };
            mockBusinessLayer.Setup(bl => bl.Eliminar_Ingredente(invalidId)).Returns(mensajeRetorno);

            // Act
            var result = controller.Delete(invalidId) as BadRequestObjectResult;

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
            // Arrange
            var mockBusinessLayer = new Mock<IB_Ingrediente>();
            var controller = new IngredienteController(mockBusinessLayer.Object);
            var validIngrediente = new DTIngrediente
            {
                id_Ingrediente = 0,
                nombre = "test",
                stock = 0,
                id_Categoria = 1,
            };

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
            // Arrange
            var mockBusinessLayer = new Mock<IB_Ingrediente>();
            var controller = new IngredienteController(mockBusinessLayer.Object);
            var invalidIngrediente = new DTIngrediente
            {
                id_Ingrediente = -1,
                nombre = "test",
                stock = 0,
                id_Categoria = 1,
            };

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
    }
}
