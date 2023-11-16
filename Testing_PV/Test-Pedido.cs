using BusinessLayer.Implementations;
using BusinessLayer.Interfaces;
using DataAccesLayer.Implementations;
using DataAccesLayer;
using DataAccesLayer.Interface;
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
using DataAccesLayer.Models;

namespace Testing_PV
{
    public class Test_Pedido
    {
        private PedidoController pedidoController;
        private DTPedido pedidoValido;
        private DTPedido pedidoInvalido;
        private Mock<IDAL_Pedido> mockDal;
        private Mock<IDAL_Casteo> mockCasteo;
        private Mock<IDAL_FuncionesExtras> mockFunciones;
        private B_Pedido bl;
        private int id_pedidoValido;
        private int id_pedidoInvalido;
        private int id_Mesa;

        [SetUp]
        public void Configuracion()
        {
            // Inicializa
            mockDal = new Mock<IDAL_Pedido>();
            mockCasteo = new Mock<IDAL_Casteo>();
            mockFunciones = new Mock<IDAL_FuncionesExtras>();
            bl = new(mockDal.Object, mockCasteo.Object, mockFunciones.Object);
            // Crea una instancia de Pedido con el servicio
            var _hub = new Mock<IHubContext<ChatHub>>();
            pedidoController = new PedidoController(bl,_hub.Object);
            pedidoValido = new DTPedido
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
            pedidoInvalido = new DTPedido
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

            id_pedidoValido = 7;
            id_pedidoInvalido = -1;
            id_Mesa = 1;
        }

        [Test]
        public async Task Agregar_ReturnOk()
        {
            // Configura el mock para simular que el usuario existe
            mockFunciones.Setup(fu => fu.existeUsuario(It.IsAny<string>()))
               .Returns(true);

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "El Pedido se guardo Correctamente" };
            mockDal.Setup(bl => bl.set_Pedido(pedidoValido)).Returns(true);

            var result = await pedidoController.Post(pedidoValido) as OkObjectResult;

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
        public async Task Agregar_ReturnBadRequest()
        {
            // Configura el mock para simular que el pedido existe
            mockFunciones.Setup(fu => fu.existePedido(It.IsAny<int>()))
               .Returns(true);

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Ya existe un pedido con los datos ingresados" };
            mockDal.Setup(bl => bl.set_Pedido(pedidoInvalido)).Returns(false);

            var result = await pedidoController.Post(pedidoInvalido) as BadRequestObjectResult;

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
            // Configura el mock para simular que el pedido existe
            mockFunciones.Setup(fu => fu.existePedido(It.IsAny<int>()))
               .Returns(true);
            // Configura el mock para simular que el usuarioe xiste
            mockFunciones.Setup(fu => fu.existeUsuario(It.IsAny<string>()))
               .Returns(true);
            // Configura el mock para simular que la mesa existe
            mockFunciones.Setup(fu => fu.existeMesa(It.IsAny<int>()))
               .Returns(true);

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "El Pedido se actualizo Correctamente" };
            mockDal.Setup(bl => bl.update_Pedido(pedidoValido)).Returns(true);

            var result = pedidoController.Put(pedidoValido) as OkObjectResult;

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
            // Configura el mock para simular que el pedido no existe
            mockFunciones.Setup(fu => fu.existePedido(It.IsAny<int>()))
               .Returns(false);

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "No existe un pedido con los datos ingresados" };
            mockDal.Setup(bl => bl.update_Pedido(pedidoInvalido)).Returns(false);

            var result = pedidoController.Put(pedidoInvalido) as BadRequestObjectResult;

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
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "El Pedido se dio de baja correctamente" };
            mockDal.Setup(bl => bl.baja_Pedido(id_pedidoValido)).Returns(true);

            var result = pedidoController.BajaPedido(id_pedidoValido) as OkObjectResult;

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
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Exepción no controlada" };
            mockDal.Setup(bl => bl.baja_Pedido(id_pedidoInvalido)).Returns(false);

            var result = pedidoController.BajaPedido(id_pedidoInvalido) as BadRequestObjectResult;

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
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "El Pedido se finalizo correctamente" };
            mockDal.Setup(bl => bl.finalizar_Pedido(id_pedidoValido)).Returns(true);

            var result = pedidoController.finalizarPedido(id_pedidoValido) as OkObjectResult;

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
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Exepción no controlada" };
            mockDal.Setup(bl => bl.finalizar_Pedido(id_pedidoInvalido)).Returns(false);

            var result = pedidoController.finalizarPedido(id_pedidoInvalido) as BadRequestObjectResult;

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
            var DC = new DataContext();
            var dal = new DAL_Pedido(DC);
            var cast = new DAL_Casteo();
            var fun = new DAL_FuncionesExtras(DC, cast);
            var bl = new B_Pedido(dal, cast, fun);
            var _hub = new Mock<IHubContext<ChatHub>>();
            var pedidoController2 = new PedidoController(bl, _hub.Object);

            // Act
            var result = pedidoController2.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<DTPedido>>(result);

            // Verifica que ninguno de los elementos en la lista sea null
            Assert.IsTrue(result.TrueForAll(pedido => pedido != null));
        }

        [Test]
        public void GetPedido_ReturnsCorrectPedido()
        {
            // Arrange
            var idPedido = 1;
            // Simula la respuesta esperada desde DAL y Casteo
            var expectedDTPedido = new DTPedido { /* propiedades esperadas */ };
            mockDal.Setup(d => d.get_Pedido(idPedido)).Returns(new Pedidos());
            mockCasteo.Setup(c => c.CastDTPedido(It.IsAny<Pedidos>())).Returns(expectedDTPedido);

            // Act
            var result = pedidoController.GetPedido(idPedido);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedDTPedido, result);
        }


        [Test]
        public void ListarPedidosActivos_Validar()
        {
            var DC = new DataContext();
            var dal = new DAL_Pedido(DC);
            var cast = new DAL_Casteo();
            var fun = new DAL_FuncionesExtras(DC, cast);
            var bl = new B_Pedido(dal, cast, fun);
            var _hub = new Mock<IHubContext<ChatHub>>();
            var pedidoController2 = new PedidoController(bl, _hub.Object);

            // Act
            var result = pedidoController2.GetActivos();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<DTPedido>>(result);

            // Verifica que ninguno de los elementos en la lista sea null
            Assert.IsTrue(result.TrueForAll(pedido => pedido != null));
        }

        [Test]
        public void ListarPedidosMesa_Validar()
        {
            var DC = new DataContext();
            var dal = new DAL_Pedido(DC);
            var cast = new DAL_Casteo();
            var fun = new DAL_FuncionesExtras(DC, cast);
            var bl = new B_Pedido(dal, cast, fun);
            var _hub = new Mock<IHubContext<ChatHub>>();
            var pedidoController2 = new PedidoController(bl, _hub.Object);

            // Act
            var result = pedidoController2.GetPedidosPorMesa(id_Mesa);

            // Verifica que ninguno de los elementos en la lista sea null
            Assert.IsTrue(result.TrueForAll(pedido => pedido != null));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.TrueForAll(result => result != null));
            Assert.IsTrue(result.TrueForAll(result => result.id_Mesa == id_Mesa));
            Assert.IsInstanceOf<List<DTPedido>>(result);
        }

        [Test]
        public void ListarPedidosTipo_Validar()
        {
            var DC = new DataContext();
            var dal = new DAL_Pedido(DC);
            var cast = new DAL_Casteo();
            var fun = new DAL_FuncionesExtras(DC, cast);
            var bl = new B_Pedido(dal, cast, fun);
            var _hub = new Mock<IHubContext<ChatHub>>();
            var pedidoController2 = new PedidoController(bl, _hub.Object);
            var tipo = (Domain.Enums.Categoria)1;
            // Act
            var result = pedidoController2.GetPedidosPorTipo(tipo);

            // Verifica que ninguno de los elementos en la lista sea null
            Assert.IsTrue(result.TrueForAll(pedido => pedido != null));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.TrueForAll(result => result != null));
            Assert.IsTrue(result.TrueForAll(result => result.tipo == tipo));
            Assert.IsInstanceOf<List<DTPedido>>(result);
        }
    }
}
