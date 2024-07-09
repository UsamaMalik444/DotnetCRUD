using Microsoft.EntityFrameworkCore;
using react_crud_be.Models;

namespace react_crud_be.EmployeeContext
{
    public class MyDbContext : DbContext
    {
   
        public MyDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Student> Student { get; set; }
    }
}

