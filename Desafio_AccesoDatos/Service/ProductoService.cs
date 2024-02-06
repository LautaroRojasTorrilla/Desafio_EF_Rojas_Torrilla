using Desafio_AccesoDatos.Database;
using Desafio_AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
                // Lanzar una nueva excepción para manejar el error en un nivel superior.
                throw new Exception($"Error al obtener todos los productos: {ex.Message}", ex);
            }
        }

        public static Producto ObtenerProductoPorID(int id)
        {
            try
            {
                using (CoderContext contexto = new CoderContext())
                {
                    Producto productoBuscado = contexto.Productos.FirstOrDefault(p => p.Id == id)
                        ?? throw new Exception($"No se encontró un producto con ID {id}");

                    return productoBuscado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el producto por ID: {ex.Message}", ex);
            }
        }

        public static bool AgregarProducto(Producto producto)
        {
            try
            {
                using (CoderContext contexto = new CoderContext())
                {
                    contexto.Productos.Add(producto);
                    contexto.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al agregar el producto: {ex.Message}", ex);
            }
        }

        public static bool ModificarProductoPorId(Producto producto, int id)
        {
            try
            {
                using (CoderContext contexto = new CoderContext())
                {
                    Producto productoBuscado = contexto.Productos.FirstOrDefault(p => p.Id == id)
                        ?? throw new Exception($"No se encontró un producto con ID {id}");

                    productoBuscado.Descripciones = producto.Descripciones;
                    productoBuscado.Costo = producto.Costo;
                    productoBuscado.PrecioVenta = producto.PrecioVenta;
                    productoBuscado.Stock = producto.Stock;

                    contexto.Productos.Update(productoBuscado);
                    contexto.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al modificar el producto: {ex.Message}", ex);
            }
        }

        public static bool EliminarProductoPorID(int Id)
        {
            try
            {
                using (CoderContext contexto = new CoderContext())
                {
                    Producto productoAEliminar = contexto.Productos
                        .Include(p => p.ProductoVendidos)
                        .FirstOrDefault(p => p.Id == Id)
                        ?? throw new Exception($"No se encontró un producto con ID {Id}");

                    contexto.Productos.Remove(productoAEliminar);
                    contexto.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el producto: {ex.Message}", ex);
            }
        }

    }
}
