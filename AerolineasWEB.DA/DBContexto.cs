
using Microsoft.EntityFrameworkCore;

namespace AerolineasWEB.DA
{
    public class DBContexto : DbContext
    {
        public DBContexto(DbContextOptions<DBContexto> options) : base(options)
        { 
        }

        public DbSet<Model.Aerolinea> Aerolinea { get; set;}
        public DbSet<Model.Avion> Avion { get; set; }
        public DbSet<Model.Propietario> Propietario { get; set; }
    }
}
