using Microsoft.EntityFrameworkCore;
using WebApi.Models;


namespace WebApi.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    public DbSet<Device> Devices { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
  }
}