using DataAccesLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer
{
    public class DataContext: IdentityDbContext<Usuarios>
    {
        public DataContext(){}
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured){
                optionsBuilder.UseNpgsql("Host=containers-us-west-163.railway.app;Port=7561;Username=postgres;Password=9zP2UTHu6mu6NTW2HSwW;Database=railway");
            }
        }

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<ClientesPreferenciales> ClientesPreferenciales { get; set; }
        public DbSet<Ingredientes> Ingredientes { get; set; }
        public DbSet<Mesas> Mesas { get; set; }
        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<Pedidos_Productos> Pedidos_Productos { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Productos_Ingredientes> Productos_Ingredientes { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Productos_Ingredientes>()
                .HasOne(pi => pi.Productos)
                .WithMany(p => p.ProductoIngredientes)
                .HasForeignKey(pi => pi.id_Producto);

            builder.Entity<Productos_Ingredientes>()
                .HasOne(pi => pi.Ingredientes)
                .WithMany(i => i.ProductoIngredientes)
                .HasForeignKey(pi => pi.id_Ingrediente);

            base.OnModelCreating(builder);
        }
    }
}
