using Microsoft.EntityFrameworkCore;
using CrudUcbd.Models;

namespace CrudUcbd.Models
{
    public class Contexto : DbContext 
    {
        public DbSet<Pedido> Pedidos { get; set; }

        public Contexto(DbContextOptions<Contexto> opcoes): base(opcoes) { }

        public DbSet<CrudUcbd.Models.Desconto>? Descontos { get; set; }
    }
}
