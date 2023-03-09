using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    [ExcludeFromCodeCoverage]
    /// <inheritdoc />
    public partial class _001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Archives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(2000)", nullable: false),
                    Directory = table.Column<string>(type: "varchar(2000)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentsPayment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Department = table.Column<string>(type: "varchar(2000)", nullable: false),
                    MonthTerm = table.Column<string>(type: "varchar(2000)", nullable: false),
                    YearTerm = table.Column<int>(type: "int", nullable: false),
                    TotalPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDiscounts = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalExtras = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ArchiveId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentsPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentsPayment_Archives_ArchiveId",
                        column: x => x.ArchiveId,
                        principalTable: "Archives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(2000)", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    TotalReceive = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Overtime = table.Column<double>(type: "float", nullable: false),
                    DebitHours = table.Column<double>(type: "float", nullable: false),
                    MissingDays = table.Column<double>(type: "float", nullable: false),
                    ExtraDays = table.Column<double>(type: "float", nullable: false),
                    WorkedDays = table.Column<double>(type: "float", nullable: false),
                    DepartmentPaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_DepartmentsPayment_DepartmentPaymentId",
                        column: x => x.DepartmentPaymentId,
                        principalTable: "DepartmentsPayment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentsPayment_ArchiveId",
                table: "DepartmentsPayment",
                column: "ArchiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentPaymentId",
                table: "Employees",
                column: "DepartmentPaymentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "DepartmentsPayment");

            migrationBuilder.DropTable(
                name: "Archives");
        }
    }
}
