using Desafio_AccesoDatos.Models;
using Desafio_AccesoDatos.Service;

namespace Desafio_AccesoDatos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Producto> todosLosProductos = ProductoService.ObtenerTodosLosProductos();

            Console.WriteLine("Todos los productos:");
            foreach (var producto in todosLosProductos)
            {
                Console.WriteLine($"ID: {producto.Id}, Descripción: {producto.Descripciones}, Precio: {producto.PrecioVenta}");
            }

            int idABuscar = 2;
            Producto productoPorID = ProductoService.ObtenerProductoPorID(idABuscar);

            if (productoPorID != null)
            {
                Console.WriteLine($"Producto encontrado - ID: {productoPorID.Id}, Descripción: {productoPorID.Descripciones}, Precio: {productoPorID.PrecioVenta}");
            }
            else
            {
                Console.WriteLine($"No se encontró un producto con ID {idABuscar}");
            }

            Producto nuevoProducto = new Producto
            {
                Descripciones = "Nuevo Producto",
                Costo = 10.99m,
                PrecioVenta = 19.99m,
                Stock = 50,
                IdUsuario = 1 // Reemplaza con el ID del usuario correspondiente
            };

            bool agregado = ProductoService.AgregarProducto(nuevoProducto);

            if (agregado)
            {
                Console.WriteLine("Nuevo producto agregado con éxito.");
            }
            else
            {
                Console.WriteLine("Error al agregar el nuevo producto.");
            }

            int idAModificar = 2;
            Producto productoModificado = new Producto
            {
                Descripciones = "Descripción Modificada",
                Costo = 15.99m,
                PrecioVenta = 29.99m,
                Stock = 75
            };

            bool modificado = ProductoService.ModificarProductoPorId(productoModificado, idAModificar);

            if (modificado)
            {
                Console.WriteLine($"Producto con ID {idAModificar} modificado con éxito.");
            }
            else
            {
                Console.WriteLine($"Error al modificar el producto con ID {idAModificar}.");
            }

            Console.ReadLine();
        }
    }
}