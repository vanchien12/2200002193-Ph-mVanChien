using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DepartmentName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OfficePhone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmployeeName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhotoImagePath = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Salary = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employee_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "DepartmentId", "Address", "DepartmentName", "OfficePhone" },
                values: new object[,]
                {
                    { 1, "123 Le Loi St", "Sales", "0123456789" },
                    { 2, "456 Hai Ba Trung St", "Marketing", "0234567891" },
                    { 3, "789 Ha Noi Rd", "IT", "0345678912" },
                    { 4, "321 Cach Mang Thang 8", "HR", "0456789123" },
                    { 5, "280 An Duong Vuong", "Sales", "0456789123" },
                    { 6, "273 Nguyen Trai", "R&D", "0456789123" },
                    { 7, "321 Cach Mang Thang 8", "Customer Service", "0456789123" },
                    { 8, "987 Truong Dinh", "Finance", "0567891234" }
                });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "EmployeeId", "DateOfBirth", "DepartmentId", "Email", "EmployeeName", "Gender", "Phone", "PhotoImagePath", "Salary" },
                values: new object[,]
                {
                    { 1, new DateTime(1979, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "alice@example.com", "Alice Johnson", false, "012345678", "/images/photos/alice.jpg", 1900m },
                    { 2, new DateTime(1997, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "bob@example.com", "Bob Smith", true, "023456789", "/images/photos/bob.jpg", 1100m },
                    { 3, new DateTime(1993, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "carol@example.com", "Carol Lee", false, "034567891", "/images/photos/carol.jpg", 1300m },
                    { 4, new DateTime(1995, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "david@example.com", "David Kim", true, "045678912", "/images/photos/david.jpg", 1500m },
                    { 5, new DateTime(1999, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "eva@example.com", "Eva Brown", false, "056789123", "/images/photos/eva.jpg", 1700m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartmentId",
                table: "Employee",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
