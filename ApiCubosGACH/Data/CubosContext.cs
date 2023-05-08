using ApiCubosGACH.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCubosGACH.Data {
    public class CubosContext : DbContext {

        public CubosContext(DbContextOptions options) :base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cubo> Cubos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }

    }
}
