using EasyOrder.API.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EasyOrder.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        //public DbSet<OrderDetail<Food>> OrderDetails { get; set; }


        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Brand> Brands { get; set; }

        public DbSet<Table> Tables { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Bill> Bills { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Food> Foods { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // modelBuilder za OrderDetailItems za Items

            modelBuilder.Entity<OrderDetailItem<Food>>()
                .HasKey(odf => new { odf.OrderDetailId, odf.ItemId });

            //modelBuilder.Entity<OrderDetailItem<Drink>>()
            //.HasKey(odd => new { odd.OrderDetailId, odd.ItemId });

            //modelBuilder za OrderDetailItems i OrderDetails

            modelBuilder.Entity<OrderDetailItem<Food>>()
                .HasOne(odf => odf.OrderDetail)
                .WithMany(od => od.Foods)
                .HasForeignKey(odf => odf.OrderDetailId);

            //modelBuilder.Entity<OrderDetailItem<Drink>>()
            //   .HasOne(odd => odd.OrderDetail)
            //   .WithMany(od => od.Drinks)
            //   .HasForeignKey(odd => odd.OrderDetailId);

            modelBuilder.Entity<Order>()
                .HasOne(e => e.Employee)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.EmployeeId)  
                .OnDelete(DeleteBehavior.Restrict);

           // modelBuilder.Entity<OrderDetail>()
                //.HasKey(od => od.Id);

            //modelBuilder za OrderDetails

            modelBuilder.Entity<Order>()
               .HasOne(o => o.OrderDetails)
               .WithOne(od => od.Order)
               .HasForeignKey<OrderDetail>(od => od.OrderId);


            //modelBuilder za Cities

            modelBuilder.Entity<City>()
                .HasMany(c => c.Suppliers)
                .WithOne(s => s.City)
                .HasForeignKey(s => s.CityId);

            modelBuilder.Entity<City>()
                .HasMany(c => c.Employees)
                .WithOne(e => e.City)
                .HasForeignKey(e => e.CityId);


            //modelBuilder.Entity<User>()
            //    .HasOne(e => e.Administrator)
            //    .WithOne(e => e.User)
            //    .HasForeignKey<Administrator>(e => e.Id)
            //    .IsRequired();
            //modelBuilder.Entity<User>()
            //    .HasOne(e => e.Employee)
            //    .WithOne(e => e.User)
            //    .HasForeignKey<Employee>(e => e.Id)
            //    .IsRequired();

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

            //modelBuilder.Entity<User>().ToTable("users");

            base.OnModelCreating(modelBuilder);
        }
    }
}
