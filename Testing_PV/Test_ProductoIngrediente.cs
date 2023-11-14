
using System.Collections.Generic;
using BusinessLayer.Implementations;
using BusinessLayer.Interfaces;
using DataAccesLayer;
using DataAccesLayer.Implementations;
using DataAccesLayer.Interface;
using DataAccesLayer.Models;
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
        private Productos_IngredientesController controller;
        private DTProductos_Ingredientes validProductosIngredientes;
        private DTProductos_Ingredientes invalidProductosIngredientes;
        private Mock<IDAL_ProductoIngrediente> mockDal;
        private Mock<IDAL_Casteo> mockCasteo;
        private Mock<IDAL_FuncionesExtras> mockFunciones;
        private B_Productos_Ingredientes bl;

        [SetUp]
        public void Configuracion()
        {
            // Inicializa
            mockDal = new Mock<IDAL_ProductoIngrediente>();
            mockCasteo = new Mock<IDAL_Casteo>();
            mockFunciones = new Mock<IDAL_FuncionesExtras>();
            bl = new(mockDal.Object, mockCasteo.Object, mockFunciones.Object);
            // Crea una instancia de MesaController con el servicio
            controller = new Productos_IngredientesController(bl);
            
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
            mockDal.Setup(bl => bl.ProductoIngrediente(validProductosIngredientes.id_Producto, validProductosIngredientes.id_Ingrediente)).Returns(true);

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
            mockDal.Setup(bl => bl.ProductoIngrediente(invalidProductosIngredientes.id_Producto, validProductosIngredientes.id_Ingrediente)).Returns(false);

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
            mockDal.Setup(bl => bl.bajaProductoIngrediente(validProductosIngredientes.id_Producto,validProductosIngredientes.id_Ingrediente)).Returns(true);

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
            mockDal.Setup(bl => bl.bajaProductoIngrediente(validProductosIngredientes.id_Producto,invalidProductosIngredientes.id_Ingrediente)).Returns(false);

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
            var DC = new DataContext();
            var dal = new DAL_ProductoIngrediente(DC);
            var cast = new DAL_Casteo();
            var fun = new DAL_FuncionesExtras(DC, cast);
            var bl = new B_Productos_Ingredientes(dal, cast, fun);
            var controllerget = new Productos_IngredientesController(bl);

            // Act
            var result = controllerget.Get(validProductosIngredientes.id_Ingrediente);

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
