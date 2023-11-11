
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
    {// Define una implementación Mock o Falsa de IB_Mesa para pruebas
        public IBProductos_Ingredientes Service;
        public Productos_IngredientesController controller;
        public Mock<IBProductos_Ingredientes> mockBusinessLayer;
        public DTProductos_Ingredientes validProductosIngredientes;
        public DTProductos_Ingredientes invalidProductosIngredientes;

        [SetUp]
        public void Configuracion()
        {
            // Inicializa la implementación Mock o Falsa
            mockBusinessLayer = new Mock<IBProductos_Ingredientes>();

            // Puedes utilizar una biblioteca de simulación como Moq para crear un mock
            // Crea una instancia de MesaController con el servicio falso
            controller = new Productos_IngredientesController(mockBusinessLayer.Object);
            
            //datos necesarios para todos los test
            validProductosIngredientes = new DTProductos_Ingredientes
            {
                id_Producto = 1,
                id_Ingrediente = 1
            };
            invalidProductosIngredientes = new DTProductos_Ingredientes
            {
                id_Producto = -1,
                id_Ingrediente = -1
            };
        }
            [Test]
        public void Post_AgregarProductosIngredientes_ReturnsOkResult()
        {
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
            // Configura el comportamiento del mock para Productos_Ingredientes con un DTO inválido
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Error al agregar Productos_Ingredientes" };
            mockBusinessLayer.Setup(bl => bl.Productos_Ingredientes(invalidProductosIngredientes)).Returns(mensajeRetorno);

            // Act
            var result = controller.Post(invalidProductosIngredientes) as BadRequestObjectResult;

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
            // Configura el comportamiento del mock para quitarProductos_Ingredientes
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "Productos_Ingredientes quitados correctamente" };
            mockBusinessLayer.Setup(bl => bl.quitarProductos_Ingredientes(validProductosIngredientes)).Returns(mensajeRetorno);

            // Act
            var result = controller.Delete(validProductosIngredientes) as OkObjectResult;

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
            // Configura el comportamiento del mock para quitarProductos_Ingredientes con un DTO inválido
            MensajeRetorno mensajeRetorno =new MensajeRetorno { status = false, mensaje = "Error al quitar Productos_Ingredientes" };
            mockBusinessLayer.Setup(bl => bl.quitarProductos_Ingredientes(invalidProductosIngredientes)).Returns(mensajeRetorno);

            // Act
            var result = controller.Delete(invalidProductosIngredientes) as BadRequestObjectResult;

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
            // Configura el comportamiento del mock para listar_IngredientesProducto
            mockBusinessLayer.Setup(bl => bl.listar_IngredientesProducto(validProductosIngredientes.id_Ingrediente)).Returns(new List<DTIngrediente> ());

            // Act
            var result = controller.Get(validProductosIngredientes.id_Ingrediente);

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
