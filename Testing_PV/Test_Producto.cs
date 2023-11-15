using BusinessLayer.Interfaces;
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

namespace Testing_PV
{
    public class Test_Producto
    {
        [Test]
        public void Agregar_ReturnOk()
        {
            var mockBusinessLayer = new Mock<IB_Producto>();
            var controller = new ProductoController(mockBusinessLayer.Object);
            var aux = new DTProducto
            {
                id_Producto = 0,
                nombre = "Milanesa de Pescado",
                descripcion = "Rebozada con pan rayado integral",
                precio = 150,
                tipo = Domain.Enums.Categoria.comida
            };

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "El Producto se dio de alta correctamente" };
            mockBusinessLayer.Setup(bl => bl.agregar_Producto(aux)).Returns(mensajeRetorno);

            var result = controller.Post(aux) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<StatusResponse>(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }

        [Test]
        public void Agregar_ReturnBadRequest()
        {
            var mockBusinessLayer = new Mock<IB_Producto>();
            var controller = new ProductoController(mockBusinessLayer.Object);
            var aux = new DTProducto
            {
                id_Producto = 9,
                nombre = "Sprite",
                descripcion = "bebida gaseosa",
                precio = 150,
                tipo = Domain.Enums.Categoria.bebida
            };

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Error al dar de alta el Producto" };
            mockBusinessLayer.Setup(bl => bl.agregar_Producto(aux)).Returns(mensajeRetorno);

            var result = controller.Post(aux) as BadRequestObjectResult;

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
            var mockBusinessLayer = new Mock<IB_Producto>();
            var controller = new ProductoController(mockBusinessLayer.Object);
            var aux = new DTProducto
            {
                id_Producto = 9,
                nombre = "Sprite",
                descripcion = "Bebida gaseosa de la compania CocaCola",
                precio = 120,
                tipo = Domain.Enums.Categoria.bebida
            };

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "El Producto se modifico correctamente" };
            mockBusinessLayer.Setup(bl => bl.Modificar_Producto(aux)).Returns(mensajeRetorno);

            var result = controller.Put(aux) as OkObjectResult;

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
            var mockBusinessLayer = new Mock<IB_Producto>();
            var controller = new ProductoController(mockBusinessLayer.Object);
            var aux = new DTProducto
            {
                id_Producto = 0,
                nombre = "Milanesa de Pescado",
                descripcion = "Rebozada con pan rayado integral",
                precio = 150,
                tipo = Domain.Enums.Categoria.comida
            };

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Error al intentar modificar el Producto" };
            mockBusinessLayer.Setup(bl => bl.Modificar_Producto(aux)).Returns(mensajeRetorno);

            var result = controller.Put(aux) as BadRequestObjectResult;

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
            var mockBusinessLayer = new Mock<IB_Producto>();
            var controller = new ProductoController(mockBusinessLayer.Object);

            mockBusinessLayer.Setup(bl => bl.listar_Productos())
            .Returns(new List<DTProducto>
            {
                new DTProducto
                {
                    id_Producto = 9,
                    nombre = "Sprite",
                    descripcion = "Bebida gaseosa",
                    precio = 400,
                    tipo = Domain.Enums.Categoria.bebida
                },
                new DTProducto
                {
                    id_Producto = 46,
                    nombre = "Milanesa con Puré",
                    descripcion = "Incluye ensalada de lechuga y tomate",
                    precio = 740,
                    tipo = Domain.Enums.Categoria.comida
                },
                new DTProducto
                {
                    id_Producto = 8,
                    nombre = "Chivito",
                    descripcion = "Chivito completo con papas",
                    precio = 400,
                    tipo = Domain.Enums.Categoria.comida
                }
            });



            var result = controller.Get();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<DTProducto>>(result);

            CollectionAssert.AllItemsAreNotNull(result);

            foreach (var producto in result)
            {
                Assert.IsNotNull(producto.id_Producto);
                Assert.IsNotNull(producto.nombre);
                Assert.IsNotNull(producto.descripcion);
                Assert.IsNotNull(producto.precio);
                Assert.IsNotNull(producto.tipo);
            }
        }

        [Test]
        public void ListarProductoPorTipo_Validar()
        {
            var mockBusinessLayer = new Mock<IB_Producto>();
            var controller = new ProductoController(mockBusinessLayer.Object);
            var tipo = Domain.Enums.Categoria.comida;

            mockBusinessLayer.Setup(bl => bl.listar_ProductosPorTipo(tipo))
            .Returns(new List<DTProducto>
            {
                new DTProducto
                {
                    id_Producto = 46,
                    nombre = "Milanesa con Puré",
                    descripcion = "Incluye ensalada de lechuga y tomate",
                    precio = 740,
                    tipo = Domain.Enums.Categoria.comida
                },
                new DTProducto
                {
                    id_Producto = 8,
                    nombre = "Chivito",
                    descripcion = "Chivito completo con papas",
                    precio = 400,
                    tipo = Domain.Enums.Categoria.comida
                }
            });

            var result = controller.GetProductosPorTipo(tipo);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<DTProducto>>(result);

            CollectionAssert.AllItemsAreNotNull(result);

            foreach (var producto in result)
            {
                Assert.IsNotNull(producto.id_Producto);
                Assert.IsNotNull(producto.nombre);
                Assert.IsNotNull(producto.descripcion);
                Assert.IsNotNull(producto.precio);
                Assert.IsNotNull(producto.tipo);

                Assert.AreEqual(tipo, producto.tipo);
            }
        }
    }
}
