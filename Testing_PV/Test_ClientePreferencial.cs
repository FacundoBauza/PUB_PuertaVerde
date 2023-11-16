using BusinessLayer.Implementations;
using BusinessLayer.Interfaces;
using DataAccesLayer.Implementations;
using DataAccesLayer;
using DataAccesLayer.Interface;
using Domain.DT;
using Domain.Entidades;
using iText.StyledXmlParser.Jsoup.Helper;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
    public class Test_ClientePreferencial
    {
        private ClientePreferencialController clienteController;
        private DTCliente_Preferencial clienteValido;
        private DTCliente_Preferencial clienteInvalido;
        private Mock<IDAL_ClientePreferencial> mockDal;
        private Mock<IDAL_Casteo> mockCasteo;
        private Mock<IDAL_FuncionesExtras> mockFunciones;
        private B_ClientePreferencial bl;
        private int id_clienteValido;
        private int id_clienteInvalido;

        [SetUp]
        public void Configuracion()
        {
            // Inicializa
            mockDal = new Mock<IDAL_ClientePreferencial>();
            mockCasteo = new Mock<IDAL_Casteo>();
            mockFunciones = new Mock<IDAL_FuncionesExtras>();
            bl = new(mockDal.Object, mockCasteo.Object, mockFunciones.Object);
            // Crea una instancia de ClientePreferencialController con el servicio
            clienteController = new ClientePreferencialController(bl);
            clienteValido = new DTCliente_Preferencial
            {
                id_Cli_Preferencial = 0,
                nombre = "Ezequiel",
                apellido = "Martinez",
                telefono = "43459724",
                saldo = 0,
                registro_Activo = true,
                fichasCanje = 0
            };
            clienteInvalido = new DTCliente_Preferencial
            {
                id_Cli_Preferencial = -1,
                nombre = "Antoni",
                apellido = "Marcial",
                telefono = "097744565",
                saldo = 0,
                registro_Activo = false,
                fichasCanje = 0
            };

            id_clienteValido = 7;
            id_clienteInvalido = -1;
        }

        [Test]
        public void Agregar_ReturnOk()
        {
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "El Cliente se guardo correctamente" };
            mockDal.Setup(bl => bl.set_Cliente(clienteValido)).Returns(true);

            var result = clienteController.Post(clienteValido) as OkObjectResult;

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
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Exepción no controlada" };
            mockDal.Setup(bl => bl.set_Cliente(clienteInvalido)).Returns(false);

            var result = clienteController.Post(clienteInvalido) as BadRequestObjectResult;

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
            // Configura el mock para simular que el cliente existe
            mockFunciones.Setup(fu => fu.existeClienteId(It.IsAny<int>()))
               .Returns(true);

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "El Cliente se actualizo correctamente" };
            mockDal.Setup(bl => bl.update_Cliente(clienteValido)).Returns(true);

            var result = clienteController.Put(clienteValido) as OkObjectResult;

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
            // Configura el mock para simular que el cliente existe
            mockFunciones.Setup(fu => fu.existeClienteId(It.IsAny<int>()))
               .Returns(false);

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "No existe un Cliente con los datos aportados" };
            mockDal.Setup(bl => bl.update_Cliente(clienteInvalido)).Returns(false);

            var result = clienteController.Put(clienteInvalido) as BadRequestObjectResult;

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
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "El Cliente se dio de baja correctamente" };
            mockDal.Setup(bl => bl.baja_Cliente(id_clienteValido)).Returns(true);

            var result = clienteController.BajaCliente(id_clienteValido) as OkObjectResult;

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
            mockDal.Setup(bl => bl.baja_Cliente(id_clienteInvalido)).Returns(false);

            var result = clienteController.BajaCliente(id_clienteInvalido) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOf<StatusResponse>(result.Value);
            Assert.AreEqual(mensajeRetorno.status, ((StatusResponse)result.Value).StatusOk);
            Assert.AreEqual(mensajeRetorno.mensaje, ((StatusResponse)result.Value).StatusMessage);
        }

        [Test]
        public void ListarClientes_Validar()
        {
            var DC = new DataContext();
            var dal = new DAL_ClientePreferencial(DC);
            var cast = new DAL_Casteo();
            var fun = new DAL_FuncionesExtras(DC, cast);
            var bl = new B_ClientePreferencial(dal, cast, fun);
            var clienteController2 = new ClientePreferencialController(bl);

            // Act
            var result = clienteController2.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<DTCliente_Preferencial>>(result);
            // Verifica que la lista no sea null
            Assert.IsNotNull(result);
            // Verifica que ninguno de los elementos en la lista sea null
            Assert.IsTrue(result.TrueForAll(cliente => cliente != null));

        }

        [Test]
        public void CerarCuenta_WithValidData_ShouldReturnByteArray()
        {   
            var result = clienteController.cerarCuenta(clienteValido) as byte[];

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<byte[]>(result);
        }

        [Test]
        public void CerarCuenta_WithInvalidData_ShouldReturnBadRequest()
        {
            var result = clienteController.cerarCuenta(clienteInvalido);

            Assert.IsInstanceOf<byte[]>(result);
        }
    }
}
