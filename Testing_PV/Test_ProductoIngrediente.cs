
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
    internal class Test_ProductoIngrediente
    {
        [Test]
        public void Post_AgregarProductosIngredientes_ReturnsOkResult()
        {
            // Arrange
            var mockBusinessLayer = new Mock<IBProductos_Ingredientes>();
            var controller = new Productos_IngredientesController(mockBusinessLayer.Object);
            var validProductosIngredientes = new DTProductos_Ingredientes
            {
                id_Producto = 1,
                id_Ingrediente = 1
            };

            // Configura el comportamiento del mock para Productos_Ingredientes
            MensajeRetorno mensajeRetorno =new MensajeRetorno { status = true, mensaje = "Productos_Ingredientes agregados correctamente" };
            mockBusinessLayer.Setup(bl => bl.Productos_Ingredientes(validProductosIngredientes)).Returns(mensajeRetorno);

            // Act
            var result = controller.Post(validProductosIngredientes) as OkObjectResult;

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
        public void Post_InvalidProductosIngredientes_ReturnsBadRequestResult()
        {
            // Arrange
            var mockBusinessLayer = new Mock<IBProductos_Ingredientes>();
            var controller = new Productos_IngredientesController(mockBusinessLayer.Object);
            var invalidProductosIngredientes = new DTProductos_Ingredientes
            {
                id_Producto = -1,
                id_Ingrediente = 1
            };
            // Configura el comportamiento del mock para Productos_Ingredientes con un DTO inválido
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Error al agregar Productos_Ingredientes" };
            mockBusinessLayer.Setup(bl => bl.Productos_Ingredientes(invalidProductosIngredientes)).Returns(mensajeRetorno);

            // Act
            var result = controller.Post(invalidProductosIngredientes) as BadRequestObjectResult;

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
        public void DeleteProductosIngredientes_ReturnsOkResult()
        {
            // Arrange
            var mockBusinessLayer = new Mock<IBProductos_Ingredientes>();
            var controller = new Productos_IngredientesController(mockBusinessLayer.Object);
            var validProductosIngredientes = new DTProductos_Ingredientes
            {
                id_Producto = 1,
                id_Ingrediente = 1
            };

            // Configura el comportamiento del mock para quitarProductos_Ingredientes
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "Productos_Ingredientes quitados correctamente" };
            mockBusinessLayer.Setup(bl => bl.quitarProductos_Ingredientes(validProductosIngredientes)).Returns(mensajeRetorno);

            // Act
            var result = controller.Delete(validProductosIngredientes) as OkObjectResult;

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
        public void Delete_InvalidProductosIngredientes_ReturnsBadRequestResult()
        {
            // Arrange
            var mockBusinessLayer = new Mock<IBProductos_Ingredientes>();
            var controller = new Productos_IngredientesController(mockBusinessLayer.Object);
            var invalidProductosIngredientes = new DTProductos_Ingredientes
            {
                id_Producto = -1,
                id_Ingrediente = 1
            };

            // Configura el comportamiento del mock para quitarProductos_Ingredientes con un DTO inválido
            MensajeRetorno mensajeRetorno =new MensajeRetorno { status = false, mensaje = "Error al quitar Productos_Ingredientes" };
            mockBusinessLayer.Setup(bl => bl.quitarProductos_Ingredientes(invalidProductosIngredientes)).Returns(mensajeRetorno);

            // Act
            var result = controller.Delete(invalidProductosIngredientes) as BadRequestObjectResult;

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
