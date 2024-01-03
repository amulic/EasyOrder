using EasyOrder.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EasyOrder.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Brand> Brands { get; set; }

        public DbSet<Table> Tables { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Bill> Bills { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(e => e.Employee)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.EmployeeId)  
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<User>()
                .HasOne(e => e.Administrator)
                .WithOne(e => e.User)
                .HasForeignKey<Administrator>(e => e.Id)
                .IsRequired();
            modelBuilder.Entity<User>()
                .HasOne(e => e.Employee)
                .WithOne(e => e.User)
                .HasForeignKey<Employee>(e => e.Id)
                .IsRequired();

            modelBuilder.Entity<EmployeeJobPosition>()
                .HasKey(jpe => new { jpe.EmployeeId, jpe.JobPositionId });
            modelBuilder.Entity<EmployeeJobPosition>()
                .HasOne(e => e.Employee)
                .WithMany(e => e.EmployeeJobPositions)
                .HasForeignKey(e => e.EmployeeId);
            modelBuilder.Entity<EmployeeJobPosition>()
                .HasOne(e => e.JobPosition)
                .WithMany(e => e.EmployeeJobPositions)
                .HasForeignKey(e => e.JobPositionId);

            for(var foreignkey in modelBuilder.Model.Ge)

        }
    }
}
