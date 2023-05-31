using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace ContactManager.Models
{
    public class ManagerContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public ManagerContext(DbContextOptions<ManagerContext> options) : base(options)
        {
        }
    }
}
