using Microsoft.EntityFrameworkCore;

namespace HomeWork8.Data
{
    public class CarContext : DbContext
    {
        public CarContext(DbContextOptions<CarContext> options) : base(options)
        {

        }

        public DbSet<Model.Car> Cars { get; set; }


    }
}
