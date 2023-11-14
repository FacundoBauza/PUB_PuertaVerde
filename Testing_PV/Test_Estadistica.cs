using BusinessLayer.Interfaces;
using Domain.DT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi_PUB_PV.Controllers;

namespace Testing_PV
{
    [TestFixture]
    internal class Test_Estadistica
    {// Define una implementación Mock o Falsa de IB_Mesa para pruebas
        public EstadisticasController controller;
        public Mock<IB_Estadisticas> mockBusinessLayer;
        public DTProductoEstadistica request;

        [SetUp]
        public void Configuracion()
        {
            // Inicializa la implementación Mock o Falsa
            mockBusinessLayer = new Mock<IB_Estadisticas>();

            // Puedes utilizar una biblioteca de simulación como Moq para crear un mock
            // Crea una instancia de EstadisticasController con el servicio falso
            controller = new EstadisticasController(mockBusinessLayer.Object);

            // Simula la identidad del usuario autenticado
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "username"),
                new Claim(ClaimTypes.Email, "user@example.com"),
            };

            var identity = new ClaimsIdentity(claims, "TestAuth");
            var principal = new ClaimsPrincipal(identity);

            // Configura el contexto de HTTP para simular un usuario autenticado
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };


            request = new DTProductoEstadistica
            {
                cantidad = 0, // Valor de cantidad
                inicio = DateTime.Now.AddDays(-7), // Valor de inicio
                fin = DateTime.Now,// Valor de fin
                producto = new DTProducto
                {
                    id_Producto = 1, // Valor de id_Producto
                    nombre = "", // Valor de nombre 
                    descripcion = "", // Valor de descripción 
                    precio = 0, // Valor de precio
                    tipo = (Domain.Enums.Categoria)1, // Asignar la instancia de Categoria
                } //instancia de DTProducto
            };
        }
        [Test]
        public void Gettodoslosproductos_ReturnsListOfDTProductoEstadistica_NotNullAndNoNullElements()
        {

            // Configura el comportamiento del mock para todoslosproductos
            mockBusinessLayer.Setup(bl => bl.todoslosproductos(request)).Returns(new List<DTProductoEstadistica>());

            // Act
            var result = controller.Gettodoslosproductos(request);
            // Assert
            // Verifica que la lista no sea null
            Assert.IsNotNull(result);
            //verifica que sea una lista de DTProductoEstadistica
            Assert.IsInstanceOf<List<DTProductoEstadistica>>(result);
            // Verifica que ninguno de los elementos en la lista sea null
            Assert.IsTrue(result.TrueForAll(productoEstadistica => productoEstadistica != null));
        }

        [Test]
        public void Getproductotipo_ReturnsListOfDTProductoEstadistica_NotNullAndNoNullElements()
        {
            // Configura el comportamiento del mock para productostipo
            mockBusinessLayer.Setup(bl => bl.productostipo(request)).Returns(new List<DTProductoEstadistica>());

            // Act
            var result = controller.Getproductotipo(request);

            // Assert
            // Verifica que la lista no sea null
            Assert.IsNotNull(result);
            //verifica que sea una lista de DTProductoEstadistica
            Assert.IsInstanceOf<List<DTProductoEstadistica>>(result);
            // Verifica que ninguno de los elementos en la lista sea null
            Assert.IsTrue(result.TrueForAll(productoEstadistica => productoEstadistica != null));
        }

        [Test]
        public void Getproducto_ReturnsDTProductoEstadistica_NotNullAndNoNullElements()
        {
            // Configura el comportamiento del mock para producto
            mockBusinessLayer.Setup(bl => bl.producto(request)).Returns(new DTProductoEstadistica());

            // Act
            var result = controller.Getproducto(request);

            // Assert
            // Verifica que la lista no sea null
            Assert.IsNotNull(result);
            //verifica que sea una lista de DTProductoEstadistica
            Assert.IsInstanceOf<DTProductoEstadistica>(result);
        }

        /*[Test]
        public void Gettodoslosproductos_UserWithoutPermission_ReturnsForbidden()
        {
            // Configura el comportamiento del mock para todoslosproductos
            mockBusinessLayer.Setup(bl => bl.todoslosproductos(request)).Returns(new List<DTProductoEstadistica>());

            // Act
            var result = controller.Gettodoslosproductos(request);

            // Assert
            // Verifica que el resultado sea un objeto ForbiddenResult (código de respuesta 403)
            Assert.IsNull(result);
        } ESTA HAY QUE DERSCOMENTARLA CUANDO AGREGEMOS LOS ROLES*/ 

    }
}

