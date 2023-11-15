using BusinessLayer.Implementations;
using BusinessLayer.Interfaces;
using DataAccesLayer.Implementations;
using DataAccesLayer;
using DataAccesLayer.Interface;
using Domain.DT;
using Domain.Entidades;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi_PUB_PV.Controllers;
using WebApi_PUB_PV.Models;
using DataAccesLayer.Models;

namespace Testing_PV
{
    public class Test_Producto
    {
        private ProductoController productoController;
        private DTProducto productoValido;
        private DTProducto productoInvalido;
        private Mock<IDAL_Producto> mockDal;
        private Mock<IDAL_Casteo> mockCasteo;
        private Mock<IDAL_FuncionesExtras> mockFunciones;
        private B_Producto bl;
        private int id_productoValido;
        private int id_productoInvalido;

        [SetUp]
        public void Configuracion()
        {
            // Inicializa
            mockDal = new Mock<IDAL_Producto>();
            mockCasteo = new Mock<IDAL_Casteo>();
            mockFunciones = new Mock<IDAL_FuncionesExtras>();
            bl = new(mockDal.Object, mockCasteo.Object, mockFunciones.Object);
            // Crea una instancia de Producto con el servicio
            productoController = new ProductoController(bl);
            productoValido = new DTProducto
            {
                id_Producto = 0,
                nombre = "Milanesa de Pescado",
                descripcion = "Rebozada con pan rayado integral",
                precio = 150,
                tipo = Domain.Enums.Categoria.comida
            };
            productoInvalido = new DTProducto
            {
                id_Producto = -1,
                nombre = "Sprite",
                descripcion = "bebida gaseosa",
                precio = 150,
                tipo = Domain.Enums.Categoria.bebida
            };

            id_productoValido = 7;
            id_productoInvalido = -1;
        }

        [Test]
        public void Agregar_ReturnOk()
        {
            // Configura el mock para simular que el producto no existe
            mockFunciones.Setup(fu => fu.existeProducto(It.IsAny<string>()))
               .Returns(false);

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "El producto se guardo correctamente" };
            mockDal.Setup(bl => bl.set_Producto(productoValido)).Returns(10);

            var result = productoController.Post(productoValido) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<StatusResponse>(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            string expectedMessage = mensajeRetorno.mensaje;
            string actualMessage = ((StatusResponse)result.Value).StatusMessage;
            Assert.IsTrue(actualMessage.Contains(expectedMessage));
        }

        [Test]
        public void Agregar_ReturnBadRequest()
        {
            // Configura el mock para simular que el producto existe
            mockFunciones.Setup(fu => fu.existeProducto(It.IsAny<string>()))
               .Returns(true);

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Ya existe el Producto" };
            mockDal.Setup(bl => bl.set_Producto(productoInvalido)).Returns(10);

            var result = productoController.Post(productoInvalido) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<StatusResponse>(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }

        [Test]
        public void Modificar_ReturnOk()
        {
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "El Producto se modifico correctamente" };
            mockDal.Setup(bl => bl.modificar_Producto(productoValido)).Returns(true);

            var result = productoController.Put(productoValido) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<StatusResponse>(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }

        [Test]
        public void Modificar_ReturnBadRequest()
        {
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Exepción no controlada" };
            mockDal.Setup(bl => bl.modificar_Producto(productoInvalido)).Returns(false);

            var result = productoController.Put(productoInvalido) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<StatusResponse>(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }

        [Test]
        public void ListarProducto_Validar()
        {
            var DC = new DataContext();
            var dal = new DAL_Producto(DC);
            var cast = new DAL_Casteo();
            var fun = new DAL_FuncionesExtras(DC, cast);
            var bl = new B_Producto(dal, cast, fun);
            var productoController2 = new ProductoController(bl);

            // Act
            var result = productoController2.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<DTProducto>>(result);
            // Verifica que ninguno de los elementos en la lista sea null
            Assert.IsTrue(result.TrueForAll(producto => producto != null));
        }

        [Test]
        public void ListarProductoPorTipo_Validar()
        {
            var tipo = (Domain.Enums.Categoria) 1;
            var DC = new DataContext();
            var dal = new DAL_Producto(DC);
            var cast = new DAL_Casteo();
            var fun = new DAL_FuncionesExtras(DC, cast);
            var bl = new B_Producto(dal, cast, fun);
            var productoController2 = new ProductoController(bl);

            // Act
            var result = productoController2.GetProductosPorTipo(tipo);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<DTProducto>>(result);
            // Verifica que ninguno de los elementos en la lista sea null
            Assert.IsFalse(result.Any(producto => producto == null));
        }
    }
}
