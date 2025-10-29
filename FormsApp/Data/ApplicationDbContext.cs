using System;
using Microsoft.EntityFrameworkCore;
using FormsApp.Models;

namespace FormsApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
