using ApiCubosGACH.Data;
using ApiCubosGACH.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCubosGACH.Repositories {
    public class RepositoryCubos {

        private CubosContext context;

        public RepositoryCubos(CubosContext context) {
            this.context = context;
        }

        #region CUBOS
        public async Task<List<Cubo>> GetCubosAsync() {
            return await this.context.Cubos.ToListAsync();
        }
        
        public async Task<List<Cubo>> GetCubosByMarcaAsync(string marca) {
            return await this.context.Cubos.Where(x => x.Marca == marca).ToListAsync();
        }

        public async Task<int> InsertCubo(string nombre, string marca, string imagen) {
            int newId = await this.context.Cubos.AnyAsync() ? await this.context.Cubos.MaxAsync(x => x.IdCubo) + 1 : 1;
            Cubo cubo = new Cubo {
                IdCubo = newId,
                Nombre = nombre,
                Marca = marca,
                Imagen = imagen,
                Precio = 0
            };

            this.context.Cubos.Add(cubo);
            await this.context.SaveChangesAsync();
            return newId;
        }
        #endregion

        #region USUARIOS
        public async Task<Usuario?> LogIn(string email, string password) {
            Usuario? usuario = await this.context.Usuarios.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
            return usuario;
        }

        public async Task<int> InsertUsuario(string nombre, string email, string password, string imagen) {
            int newId = await this.context.Usuarios.AnyAsync() ? await this.context.Usuarios.MaxAsync(x => x.IdUsuario) + 1 : 1;
            Usuario usuario = new Usuario {
                IdUsuario = newId,
                Nombre = nombre,
                Email = email,
                Password = password,
                Imagen = imagen
            };

            this.context.Usuarios.Add(usuario);
            await this.context.SaveChangesAsync();
            return newId;
        }

        public async Task<Usuario> PerfilUsuario(int idUsuario) {
            return await this.context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == idUsuario);
        }
        #endregion

        #region PEDIDOS
        public async Task<List<Pedido>> PedidosUsuario(int idUsuario) {
            return await this.context.Pedidos.Where(x => x.IdUsuario == idUsuario).ToListAsync();
        }

        public async Task<int> InsertPedido(int idUsuario, int idCubo) {
            int newId = await this.context.Pedidos.AnyAsync() ? await this.context.Pedidos.MaxAsync(x => x.IdPedido) + 1 : 1;
            Pedido pedido = new Pedido {
                IdPedido = newId,
                IdUsuario = idUsuario,
                IdCubo = idCubo,
                FechaPedido = DateTime.Now
            };

            this.context.Pedidos.Add(pedido);
            await this.context.SaveChangesAsync();
            return newId;
        }
        #endregion

    }
}
