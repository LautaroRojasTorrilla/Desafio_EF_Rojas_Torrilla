using Desafio_AccesoDatos.Database;
using Desafio_AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_AccesoDatos.Service
{
    public static class UsuarioService
    {
        public static List<Usuario> ObtenerTodosLosUsuarios()
        {
            using (CoderContext contexto = new CoderContext())
            {
                List<Usuario> usuarios = contexto.Usuarios.ToList();

                return usuarios;
            }
        }

        public static Usuario ObtenerUsuarioporID(int id)
        {
            using (CoderContext contexto = new CoderContext())
            {
                //añ where le paso una fn criterio
                Usuario? usuarioBuscado = contexto.Usuarios.Where(u => u.Id == id).FirstOrDefault();

                return usuarioBuscado;
            }
        }

        public static Usuario ObtenerUsuarioporID2(int id)
        {
            List<Usuario> usuarios = UsuarioService.ObtenerTodosLosUsuarios();

            foreach (Usuario item in usuarios)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }

            return null;
        }

        public static bool AgregarUsuario(Usuario usuario)
        {
            using (CoderContext contexto = new CoderContext())
            {
                contexto.Usuarios.Add(usuario);

                //si no hago esta linea no tiene impacto en la base de datos.
                contexto.SaveChanges();
                return true;
            }
        }

        public static bool ModificarUsuarioPorId(Usuario usuario, int id)
        {

            using (CoderContext contexto = new CoderContext())
            {
                Usuario? usuarioBuscado = contexto.Usuarios.Where(u => u.Id == id).FirstOrDefault();

                usuarioBuscado.Nombre = usuario.Nombre;
                usuarioBuscado.NombreUsuario = usuario.NombreUsuario;
                usuarioBuscado.Apellido = usuario.Apellido;
                usuarioBuscado.Mail = usuario.Mail;

                contexto.Usuarios.Update(usuarioBuscado);

                contexto.SaveChanges();

                return true;
            }
        }

        public static bool EliminarUsuarioPorID(int id)
        {
            using (CoderContext contexto = new CoderContext())
            {
                Usuario? usuarioAEliminar = contexto.Usuarios.FirstOrDefault(u => u.Id == id);

                if (usuarioAEliminar is not null)
                {
                    contexto.Usuarios.Remove(usuarioAEliminar);
                    contexto.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public static bool EliminarProductoPorID(int Id)
        {
            using (CoderContext contexto = new CoderContext())
            {
                Producto? productoAEliminar = contexto.Productos.Include(p => p.ProductoVendidos).
                    Where(p => p.Id == Id).FirstOrDefault();

                if (productoAEliminar != null)
                {
                    contexto.Productos.Remove(productoAEliminar);
                    contexto.SaveChanges();
                    return true;
                }

                return false;
            }
        }
    }
}
