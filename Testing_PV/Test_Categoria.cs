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
using System.Text;
using System.Threading.Tasks;
using WebApi_PUB_PV.Controllers;
using WebApi_PUB_PV.Models;

namespace Testing_PV
{
    public class Test_Categoria
    {
        private CategoriaController categoriaController;
        private DTCategoria categoriaValida;
        private DTCategoria categoriaInvalida;
        private Mock<IDAL_Categoria> mockDal;
        private Mock<IDAL_Casteo> mockCasteo;
        private Mock<IDAL_FuncionesExtras> mockFunciones;
        private B_Categoria bl;
        private int id_categoriaValida;
        private int id_categoriaInvalida;

        [SetUp]
        public void Configuracion()
        {
            // Inicializa
            mockDal = new Mock<IDAL_Categoria>();
            mockCasteo = new Mock<IDAL_Casteo>();
            mockFunciones = new Mock<IDAL_FuncionesExtras>();
            bl = new(mockDal.Object, mockCasteo.Object, mockFunciones.Object);
            // Crea una instancia de Categoria con el servicio
            categoriaController = new CategoriaController(bl);
            categoriaValida = new DTCategoria
            {
                id_Categoria = 0,
                nombre = "Articulos extras",
                registro_Activo = true,
                ingredientes = { }
            };
            categoriaInvalida = new DTCategoria
            {
                id_Categoria = -1,
                nombre = "Articulos extras",
                registro_Activo = true,
                ingredientes = { }
            };

            id_categoriaValida = 7;
            id_categoriaInvalida = -1;
        }

        [Test]
        public void Agregar_ReturnOk()
        {
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "La Categoria se guardo correctamente" };
            mockDal.Setup(bl => bl.set_Categoria(categoriaValida)).Returns(true);

            var result = categoriaController.Post(categoriaValida) as OkObjectResult;

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
            mockDal.Setup(bl => bl.set_Categoria(categoriaInvalida)).Returns(false);

            var result = categoriaController.Post(categoriaInvalida) as BadRequestObjectResult;

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
            MensajeRetorno mensajeRetorno = new MensajeRetorno { status = true, mensaje = "La Categoria se a dado de baja correctamente" };
            mockDal.Setup(bl => bl.baja_Categoria(id_categoriaValida)).Returns(true);

            var result = categoriaController.BajaCategoria(id_categoriaValida) as OkObjectResult;

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
            mockDal.Setup(bl => bl.baja_Categoria(id_categoriaInvalida)).Returns(false);

            var result = categoriaController.BajaCategoria(id_categoriaInvalida) as BadRequestObjectResult;

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
            var DC = new DataContext();
            var dal = new DAL_Categoria(DC);
            var cast = new DAL_Casteo();
            var fun = new DAL_FuncionesExtras(DC, cast);
            var bl = new B_Categoria(dal, cast, fun);
            var _hub = new Mock<IHubContext<ChatHub>>();
            var categoriaController2 = new CategoriaController(bl);
         
            // Act
            var result = categoriaController2.Get();

            // Verifica que ninguno de los elementos en la lista sea null
            Assert.IsTrue(result.TrueForAll(categoria => categoria != null));
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<DTCategoria>>(result);
        }
    }
}
