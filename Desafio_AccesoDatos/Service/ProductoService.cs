using Desafio_AccesoDatos.Database;
using Desafio_AccesoDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_AccesoDatos.Service
{
    public class ProductoService
    {
        public static List<Producto> ObtenerTodosLosProductos()
        {
            try
            {
                using (CoderContext contexto = new CoderContext())
                {
                    List<Producto> productos = contexto.Productos.ToList();
                    return productos;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todos los productos: {ex.Message}");
                return new List<Producto>();
            }
        }

        public static Producto ObtenerProductoPorID(int id)
        {
            try
            {
                using (CoderContext contexto = new CoderContext())
                {
                    Producto? productoBuscado = contexto.Productos.FirstOrDefault(p => p.Id == id);
                    return productoBuscado;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el producto por ID: {ex.Message}");
                return null; // Otra forma de manejar el error según tus necesidades.
            }
        }

        public static bool AgregarProducto(Producto producto)
        {
            using (CoderContext contexto = new CoderContext())
            {
                contexto.Productos.Add(producto);

                contexto.SaveChanges();
                return true;
            }
        }

        public static bool ModificarProductoPorId(Producto producto, int id)
        {

            try
            {
                using (CoderContext contexto = new CoderContext())
                {
                    Producto productoBuscado = contexto.Productos.FirstOrDefault(p => p.Id == id);

                    if (productoBuscado != null)
                    {
                        productoBuscado.Descripciones = producto.Descripciones;
                        productoBuscado.Costo = producto.Costo;
                        productoBuscado.PrecioVenta = producto.PrecioVenta;
                        productoBuscado.Stock = producto.Stock;

                        contexto.Productos.Update(productoBuscado);
                        contexto.SaveChanges();

                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"No se encontró un producto con ID {id}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al modificar el producto: {ex.Message}");
                return false;
            }
        }

    }
}
