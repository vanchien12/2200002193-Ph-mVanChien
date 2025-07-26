using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Models;

namespace EmployeeManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed data
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, DepartmentName = "Sales", Address = "123 Le Loi St", OfficePhone = "0123456789" },
                new Department { DepartmentId = 2, DepartmentName = "Marketing", Address = "456 Hai Ba Trung St", OfficePhone = "0234567891" },
                new Department { DepartmentId = 3, DepartmentName = "IT", Address = "789 Ha Noi Rd", OfficePhone = "0345678912" },
                new Department { DepartmentId = 4, DepartmentName = "HR", Address = "321 Cach Mang Thang 8", OfficePhone = "0456789123" },
                new Department { DepartmentId = 5, DepartmentName = "Sales", Address = "280 An Duong Vuong", OfficePhone = "0456789123" },
                new Department { DepartmentId = 6, DepartmentName = "R&D", Address = "273 Nguyen Trai", OfficePhone = "0456789123" },
                new Department { DepartmentId = 7, DepartmentName = "Customer Service", Address = "321 Cach Mang Thang 8", OfficePhone = "0456789123" },
                new Department { DepartmentId = 8, DepartmentName = "Finance", Address = "987 Truong Dinh", OfficePhone = "0567891234" }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, EmployeeName = "Alice Johnson", Gender = false, Email = "alice@example.com", Phone = "012345678", DateOfBirth = new DateTime(1979, 12, 12), Salary = 1900, PhotoImagePath = "/images/photos/alice.jpg", DepartmentId = 1 },
                new Employee { EmployeeId = 2, EmployeeName = "Bob Smith", Gender = true, Email = "bob@example.com", Phone = "023456789", DateOfBirth = new DateTime(1997, 10, 12), Salary = 1100, PhotoImagePath = "/images/photos/bob.jpg", DepartmentId = 2 },
                new Employee { EmployeeId = 3, EmployeeName = "Carol Lee", Gender = false, Email = "carol@example.com", Phone = "034567891", DateOfBirth = new DateTime(1993, 12, 12), Salary = 1300, PhotoImagePath = "/images/photos/carol.jpg", DepartmentId = 3 },
                new Employee { EmployeeId = 4, EmployeeName = "David Kim", Gender = true, Email = "david@example.com", Phone = "045678912", DateOfBirth = new DateTime(1995, 12, 12), Salary = 1500, PhotoImagePath = "/images/photos/david.jpg", DepartmentId = 4 },
                new Employee { EmployeeId = 5, EmployeeName = "Eva Brown", Gender = false, Email = "eva@example.com", Phone = "056789123", DateOfBirth = new DateTime(1999, 12, 12), Salary = 1700, PhotoImagePath = "/images/photos/eva.jpg", DepartmentId = 5 }
            );
        }
    }
}