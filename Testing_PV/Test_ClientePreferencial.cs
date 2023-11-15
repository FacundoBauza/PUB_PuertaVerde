using BusinessLayer.Interfaces;
using Domain.DT;
using Domain.Entidades;
using iText.StyledXmlParser.Jsoup.Helper;
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
    public class Test_ClientePreferencial
    {
        [Test]
        public void Agregar_ReturnOk()
        {
            var mockBusinessLayer = new Mock<IB_ClientePreferencial>();
            var controller = new ClientePreferencialController(mockBusinessLayer.Object);
            var aux = new DTCliente_Preferencial
            {
                id_Cli_Preferencial = 0,
                nombre = "Ezequiel",
                apellido = "Martinez",
                telefono = "43459724",
                saldo = 0,
                registro_Activo = true,
                fichasCanje = 0
            };  

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "El Cliente se dio de alta correctamente" };
            mockBusinessLayer.Setup(bl => bl.agregar_ClientePreferencial(aux)).Returns(mensajeRetorno);

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
            var mockBusinessLayer = new Mock<IB_ClientePreferencial>();
            var controller = new ClientePreferencialController(mockBusinessLayer.Object);
            var aux = new DTCliente_Preferencial
            {
                id_Cli_Preferencial = 11,
                nombre = "Antoni",
                apellido = "Marcial",
                telefono = "097744565",
                saldo = 0,
                registro_Activo = false,
                fichasCanje = 0
            };

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Error al dar de alta al CLiente" };
            mockBusinessLayer.Setup(bl => bl.agregar_ClientePreferencial(aux)).Returns(mensajeRetorno);

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
            var mockBusinessLayer = new Mock<IB_ClientePreferencial>();
            var controller = new ClientePreferencialController(mockBusinessLayer.Object);
            var aux = new DTCliente_Preferencial
            {
                id_Cli_Preferencial = 11,
                nombre = "Antoni",
                apellido = "Marcial",
                telefono = "097744565",
                saldo = -1500,
                registro_Activo = true,
                fichasCanje = 10
            };

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "El Cliente se modifico correctamente" };
            mockBusinessLayer.Setup(bl => bl.actualizar_ClientePreferencial(aux)).Returns(mensajeRetorno);

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
            var mockBusinessLayer = new Mock<IB_ClientePreferencial>();
            var controller = new ClientePreferencialController(mockBusinessLayer.Object);
            var aux = new DTCliente_Preferencial
            {
                id_Cli_Preferencial = 25,
                nombre = "Antoni",
                apellido = "Marcial",
                telefono = "097744565",
                saldo = -1500,
                registro_Activo = true,
                fichasCanje = 10
            };

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Error al intentar modificar al CLiente" };
            mockBusinessLayer.Setup(bl => bl.actualizar_ClientePreferencial(aux)).Returns(mensajeRetorno);

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
            var mockBusinessLayer = new Mock<IB_ClientePreferencial>();
            var controller = new ClientePreferencialController(mockBusinessLayer.Object);
            var aux = 7;

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "El Cliente se dio de baja correctamente" };
            mockBusinessLayer.Setup(bl => bl.baja_ClientePreferencial(aux)).Returns(mensajeRetorno);

            var result = controller.BajaCliente(aux) as OkObjectResult;

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
            var mockBusinessLayer = new Mock<IB_ClientePreferencial>();
            var controller = new ClientePreferencialController(mockBusinessLayer.Object);
            var aux = 50;

            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = false, mensaje = "Error al intentar dar de baja al CLiente" };
            mockBusinessLayer.Setup(bl => bl.baja_ClientePreferencial(aux)).Returns(mensajeRetorno);

            var result = controller.BajaCliente(aux) as BadRequestObjectResult;

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
            var mockBusinessLayer = new Mock<IB_ClientePreferencial>();
            var controller = new ClientePreferencialController(mockBusinessLayer.Object);

            mockBusinessLayer.Setup(bl => bl.listar_ClientePreferencial())
            .Returns(new List<DTCliente_Preferencial>
            {
                new DTCliente_Preferencial
                {
                    id_Cli_Preferencial = 6,
                    nombre = "Leandro",
                    apellido = "Marrero",
                    telefono = "0914324573",
                    saldo = -1800,
                    registro_Activo = true,
                    fichasCanje = 0
                },
                new DTCliente_Preferencial
                {
                    id_Cli_Preferencial = 12,
                    nombre = "Mirco",
                    apellido = "Santana",
                    telefono = "091453215",
                    saldo = 150,
                    registro_Activo = true,
                    fichasCanje = 7
                },
                new DTCliente_Preferencial
                {
                    id_Cli_Preferencial = 9,
                    nombre = "Jose Lautaro",
                    apellido = "Gonzale",
                    telefono = "097885532",
                    saldo = 700,
                    registro_Activo = true,
                    fichasCanje = 15
                },
            });

            var result = controller.Get();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<DTCliente_Preferencial>>(result);

            CollectionAssert.AllItemsAreNotNull(result);

            foreach (var cliente in result)
            {
                Assert.IsNotNull(cliente.id_Cli_Preferencial);
                Assert.IsNotNull(cliente.nombre);
                Assert.IsNotNull(cliente.apellido);
                Assert.IsNotNull(cliente.telefono);
                Assert.IsNotNull(cliente.saldo);
                Assert.IsNotNull(cliente.registro_Activo);
                Assert.IsTrue(cliente.registro_Activo);
                Assert.IsNotNull(cliente.fichasCanje);
            }
        }

        [Test]
        public void CerarCuenta_WithValidData_ShouldReturnByteArray()
        {
            // Arrange
            var mockBusinessLayer = new Mock<IB_ClientePreferencial>();
            var controller = new ClientePreferencialController(mockBusinessLayer.Object);
            var aux = new DTCliente_Preferencial
            {
                id_Cli_Preferencial = 11,
                nombre = "Antoni",
                apellido = "Marcial",
                telefono = "097744565",
                saldo = -1500,
                registro_Activo = true,
                fichasCanje = 10
            };

            var result = controller.cerarCuenta(aux) as ActionResult<byte[]>;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ActionResult<byte[]>>(result);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public void CerarCuenta_WithInvalidData_ShouldReturnBadRequest()
        {
            var mockBusinessLayer = new Mock<IB_ClientePreferencial>();
            var controller = new ClientePreferencialController(mockBusinessLayer.Object);
            var aux = new DTCliente_Preferencial
            {
                id_Cli_Preferencial = 11,
                nombre = "Antoni",
                apellido = "Marcial",
                telefono = "097744565",
                saldo = -1500,
                registro_Activo = true,
                fichasCanje = 10
            };

            var result = controller.cerarCuenta(aux) as ActionResult<byte[]>;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ActionResult<byte[]>>(result);
        }
    }
}
