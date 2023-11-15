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
    public class Test_Categoria
    {
        [Test]
        public void Agregar_ReturnOk()
        {
            var mockBusinessLayer = new Mock<IB_Categoria>();
            var controller = new CategoriaController(mockBusinessLayer.Object);
            var aux = new DTCategoria
            {
                id_Categoria = 0,
                nombre = "Articulos extras",
                registro_Activo = true,
                ingredientes = {}
            };

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "La Categoria se dio de alta correctamente" };
            mockBusinessLayer.Setup(bl => bl.agregar_Categoria(aux)).Returns(mensajeRetorno);

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
            var mockBusinessLayer = new Mock<IB_Categoria>();
            var controller = new CategoriaController(mockBusinessLayer.Object);
            var aux = new DTCategoria
            {
                id_Categoria = -1,
                nombre = "Articulos extras",
                registro_Activo = true,
                ingredientes = { }
            };

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Error al agregar la Categoria" };
            mockBusinessLayer.Setup(bl => bl.agregar_Categoria(aux)).Returns(mensajeRetorno);

            var result = controller.Post(aux) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<StatusResponse>(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }

        [Test]
        public void Eliminar_ReturnOk()
        {
            var mockBusinessLayer = new Mock<IB_Categoria>();
            var controller = new CategoriaController(mockBusinessLayer.Object);
            var aux = 4;

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "La categoria se dio de baja correctamente" };
            mockBusinessLayer.Setup(bl => bl.baja_Categoria(aux)).Returns(mensajeRetorno);

            var result = controller.BajaCategoria(aux) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<StatusResponse>(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }

        [Test]
        public void Eliminar_ReturnBadRequest()
        {
            var mockBusinessLayer = new Mock<IB_Categoria>();
            var controller = new CategoriaController(mockBusinessLayer.Object);
            var aux = 50;

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Error al intentar dar de baja la Categoria" };
            mockBusinessLayer.Setup(bl => bl.baja_Categoria(aux)).Returns(mensajeRetorno);

            var result = controller.BajaCategoria(aux) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<StatusResponse>(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }

        [Test]
        public void ListarCategorias_Validar()
        {
            var mockBusinessLayer = new Mock<IB_Categoria>();
            var controller = new CategoriaController(mockBusinessLayer.Object);

            mockBusinessLayer.Setup(bl => bl.listar_Categoria())
            .Returns(new List<DTCategoria>
            {
                new DTCategoria
                {
                    id_Categoria = 1,
                    nombre = "Bebida",
                    registro_Activo = true
                },
                new DTCategoria
                {
                     id_Categoria = 2,
                    nombre = "Comida",
                    registro_Activo = true
                },
            });

            var result = controller.Get();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<DTCategoria>>(result);

            CollectionAssert.AllItemsAreNotNull(result);

            foreach (var categoria in result)
            {
                Assert.IsNotNull(categoria.id_Categoria);
                Assert.IsNotNull(categoria.nombre);
                Assert.IsTrue(categoria.registro_Activo);
            }
        }
    }
}
