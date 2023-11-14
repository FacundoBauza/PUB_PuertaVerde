using BusinessLayer.Interfaces;
using Domain.DT;
using Domain.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Moq;
using SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using WebApi_PUB_PV.Controllers;
using WebApi_PUB_PV.Models;

namespace Testing_PV
{
    public class Test_Pedido
    {
        [Test]
        public async Task Agregar_ReturnOk()
        {
            var _hub = new Mock<IHubContext<ChatHub>>();
            var mockBusinessLayer = new Mock<IB_Pedido>();
            var controller = new PedidoController(mockBusinessLayer.Object, _hub.Object);
            var aux = new DTPedido
            {
                id_Pedido = 0,
                valorPedido = 540,
                pago = false,
                username = "fbauza2014@gmail.com",
                id_Cli_Preferencial = 0,
                id_Mesa = 2,
                estadoProceso = true,
                fecha_ingreso = new DateTime(),
                numero_movil = "",
                tipo = 0,
                list_IdProductos = { }
            };

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "El Pedido se dio de alta correctamente" };
            mockBusinessLayer.Setup(bl => bl.agregar_Pedido(aux)).Returns(mensajeRetorno);

            var result = await controller.Post(aux) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<StatusResponse>(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }

        [Test]
        public async Task Agregar_ReturnBadRequest()
        {
            var _hub = new Mock<IHubContext<ChatHub>>();
            var mockBusinessLayer = new Mock<IB_Pedido>();
            var controller = new PedidoController(mockBusinessLayer.Object, _hub.Object);
            var aux = new DTPedido
            {
                id_Pedido = -1,
                valorPedido = 411,
                pago = false,
                username = "fbauza2014@gmail.com",
                id_Cli_Preferencial = 0,
                id_Mesa = 9,
                estadoProceso = true,
                fecha_ingreso = new DateTime(),
                numero_movil = "",
                tipo = 0,
                list_IdProductos = { }
            };

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Error al dar de alta el Pedido" };
            mockBusinessLayer.Setup(bl => bl.agregar_Pedido(aux)).Returns(mensajeRetorno);

            var result = await controller.Post(aux) as BadRequestObjectResult;

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
            var _hub = new Mock<IHubContext<ChatHub>>();
            var mockBusinessLayer = new Mock<IB_Pedido>();
            var controller = new PedidoController(mockBusinessLayer.Object, _hub.Object);
            var aux = new DTPedido
            {
                id_Pedido = 59,
                valorPedido = 800,
                pago = false,
                username = "fbauza2014@gmail.com",
                id_Cli_Preferencial = 8,
                id_Mesa = 9,
                estadoProceso = true,
                fecha_ingreso = new DateTime(),
                numero_movil = "",
                tipo = 0,
                list_IdProductos = { }
            };

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "El Pedido se modifico correctamente" };
            mockBusinessLayer.Setup(bl => bl.actualizar_Pedido(aux)).Returns(mensajeRetorno);

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
            var _hub = new Mock<IHubContext<ChatHub>>();
            var mockBusinessLayer = new Mock<IB_Pedido>();
            var controller = new PedidoController(mockBusinessLayer.Object, _hub.Object);
            var aux = new DTPedido
            {
                id_Pedido = 0,
                valorPedido = 540,
                pago = false,
                username = "fbauza2014@gmail.com",
                id_Cli_Preferencial = 0,
                id_Mesa = 2,
                estadoProceso = true,
                fecha_ingreso = new DateTime(),
                numero_movil = "",
                tipo = 0,
                list_IdProductos = { }
            };

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Error al intentar modificar el Pedido" };
            mockBusinessLayer.Setup(bl => bl.actualizar_Pedido(aux)).Returns(mensajeRetorno);

            var result = controller.Put(aux) as BadRequestObjectResult;

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
            var _hub = new Mock<IHubContext<ChatHub>>();
            var mockBusinessLayer = new Mock<IB_Pedido>();
            var controller = new PedidoController(mockBusinessLayer.Object, _hub.Object);
            var aux = 59;

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "El Pedido se dio de baja correctamente" };
            mockBusinessLayer.Setup(bl => bl.baja_Pedido(aux)).Returns(mensajeRetorno);

            var result = controller.BajaPedido(aux) as OkObjectResult;

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
            var _hub = new Mock<IHubContext<ChatHub>>();
            var mockBusinessLayer = new Mock<IB_Pedido>();
            var controller = new PedidoController(mockBusinessLayer.Object, _hub.Object);
            var aux = 100;

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Error al intentar dar de baja el Pedido" };
            mockBusinessLayer.Setup(bl => bl.baja_Pedido(aux)).Returns(mensajeRetorno);

            var result = controller.BajaPedido(aux) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<StatusResponse>(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }

        [Test]
        public void Finalizar_ReturnOk()
        {
            var _hub = new Mock<IHubContext<ChatHub>>();
            var mockBusinessLayer = new Mock<IB_Pedido>();
            var controller = new PedidoController(mockBusinessLayer.Object, _hub.Object);
            var aux = 59;

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "El Pedido se finalizo correctamente" };
            mockBusinessLayer.Setup(bl => bl.finalizar_Pedido(aux)).Returns(mensajeRetorno);

            var result = controller.finalizarPedido(aux) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<StatusResponse>(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }

        [Test]
        public void Finalizar_ReturnBadRequest()
        {
            var _hub = new Mock<IHubContext<ChatHub>>();
            var mockBusinessLayer = new Mock<IB_Pedido>();
            var controller = new PedidoController(mockBusinessLayer.Object, _hub.Object);
            var aux = 100;

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Error al intentar finalizar el Pedido" };
            mockBusinessLayer.Setup(bl => bl.finalizar_Pedido(aux)).Returns(mensajeRetorno);

            var result = controller.finalizarPedido(aux) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<StatusResponse>(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }

        [Test]
        public void ListarPedidos_Validar()
        {
            var _hub = new Mock<IHubContext<ChatHub>>();
            var mockBusinessLayer = new Mock<IB_Pedido>();
            var controller = new PedidoController(mockBusinessLayer.Object, _hub.Object);

            mockBusinessLayer.Setup(bl => bl.listar_Pedidos()).Returns(new List<DTPedido>());

            var result = controller.Get();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.TrueForAll(result => result != null));
            Assert.IsInstanceOf<List<DTPedido>>(result);

            CollectionAssert.AllItemsAreNotNull(result);
        }

        [Test]
        public void GetPedidoID_Validar()
        {
            var _hub = new Mock<IHubContext<ChatHub>>();
            var mockBusinessLayer = new Mock<IB_Pedido>();
            var controller = new PedidoController(mockBusinessLayer.Object, _hub.Object);
            var aux = 59;

            mockBusinessLayer.Setup(bl => bl.Pedido(aux)).Returns(new DTPedido());

            var result = controller.GetPedido(aux);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<DTPedido>(result);

        }


        [Test]
        public void ListarPedidosActivos_Validar()
        {
            var _hub = new Mock<IHubContext<ChatHub>>();
            var mockBusinessLayer = new Mock<IB_Pedido>();
            var controller = new PedidoController(mockBusinessLayer.Object, _hub.Object);

            mockBusinessLayer.Setup(bl => bl.listar_PedidosActivos()).Returns(new List<DTPedido>());

            var result = controller.GetActivos();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.TrueForAll(result => result != null));
            Assert.IsTrue(result.TrueForAll(result => result.estadoProceso == true));
            Assert.IsInstanceOf<List<DTPedido>>(result);

            CollectionAssert.AllItemsAreNotNull(result);
        }

        [Test]
        public void ListarPedidosMesa_Validar()
        {
            var _hub = new Mock<IHubContext<ChatHub>>();
            var mockBusinessLayer = new Mock<IB_Pedido>();
            var controller = new PedidoController(mockBusinessLayer.Object, _hub.Object);
            var aux = 1;

            mockBusinessLayer.Setup(bl => bl.listar_PedidosPorMesa(aux)).Returns(new List<DTPedido>());

            var result = controller.GetPedidosPorMesa(aux);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.TrueForAll(result => result != null));
            Assert.IsTrue(result.TrueForAll(result => result.id_Mesa == aux));
            Assert.IsInstanceOf<List<DTPedido>>(result);

            CollectionAssert.AllItemsAreNotNull(result);
        }

        [Test]
        public void ListarPedidosTipo_Validar()
        {
            var _hub = new Mock<IHubContext<ChatHub>>();
            var mockBusinessLayer = new Mock<IB_Pedido>();
            var controller = new PedidoController(mockBusinessLayer.Object, _hub.Object);
            var aux = Domain.Enums.Categoria.comida;

            mockBusinessLayer.Setup(bl => bl.listar_PedidosPorTipo(aux)).Returns(new List<DTPedido>());

            var result = controller.GetPedidosPorTipo(aux);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.TrueForAll(result => result != null));
            Assert.IsTrue(result.TrueForAll(result => result.tipo == aux));
            Assert.IsInstanceOf<List<DTPedido>>(result);

            CollectionAssert.AllItemsAreNotNull(result);
        }
    }
}
