using ASPNETCoreReactAppointmentManager.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreReactAppointmentManager.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Appointment> Appointments { get; set; }
    }
}
