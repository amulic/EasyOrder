using EasyOrderAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyOrderAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Administrators> Administrators { get; set; }
        public DbSet<Employees> Employees { get; set; }

        public DbSet<JobPositions> JobPositions { get; set; }
        public DbSet<JobPositionsEmployees> JobPositionsEmployees { get; set; }

        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<Bills> Bills { get; set; }
        public DbSet<PaymentMethods> PaymentMethods { get; set; }

        public DbSet<Products> Products { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }

        public DbSet<Countries> Countries { get; set; }
        public DbSet<Cities> Cities { get; set; }

        public DbSet<Tables> Tables { get; set; }
        public DbSet<Ratings> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobPositionsEmployees>()
                .HasKey(jpe => new { jpe.EmployeeId, jpe.JobPositionId });
            modelBuilder.Entity<JobPositionsEmployees>()
                .HasOne(e => e.Employee)
                .WithMany(jpe => jpe.JobPositionsEmployees)
                .HasForeignKey(jpe => jpe.EmployeeId);
            modelBuilder.Entity<JobPositionsEmployees>()
                .HasOne(e => e.JobPosition)
                .WithMany(jpe => jpe.JobPositionsEmployees)
                .HasForeignKey(jpe => jpe.JobPositionId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
