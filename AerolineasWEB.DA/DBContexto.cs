
using Microsoft.EntityFrameworkCore;

namespace AerolineasWEB.DA
{
    public class DBContexto : DbContext
    {
        public DBContexto(DbContextOptions<DBContexto> options) : base(options)
        { 
        }

        public DbSet<Model.Aerolinea> aerolinea { get; set;}
        public DbSet<Model.Avion> avion { get; set; }
        public DbSet<Model.Propietario> propietario { get; set; }
    }
}
