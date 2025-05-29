using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Department.Shared.Data.Migrations
{
    /// <inheritdoc />
    public partial class InsertData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create Departments table
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InfrastructureId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            // Create Projects table
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            // Create Infrastructure table
            migrationBuilder.CreateTable(
                name: "Infrastructure",
                columns: table => new
                {
                    InfrastructureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfficeLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfDesks = table.Column<int>(type: "int", nullable: false),
                    NumberOfComputers = table.Column<int>(type: "int", nullable: false),
                    HasMeetingRooms = table.Column<bool>(type: "bit", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Infrastructure", x => x.InfrastructureId);
                    table.ForeignKey(
                        name: "FK_Infrastructure_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create Employees table
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create EmployeeProjects table (many-to-many)
            migrationBuilder.CreateTable(
                name: "EmployeeProjects",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProjects", x => new { x.EmployeeId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_EmployeeProjects_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeProjects_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Indexes
            migrationBuilder.CreateIndex(
                name: "IX_Infrastructure_DepartmentId",
                table: "Infrastructure",
                column: "DepartmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ProjectId",
                table: "Employees",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProjects_ProjectId",
                table: "EmployeeProjects",
                column: "ProjectId");

            // Seed data (as before)
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name", "Location" },
                values: new object[] { 1, "TI", "Bloco A" }
            );
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name", "Location" },
                values: new object[] { 2, "RH", "Bloco B" }
            );

            migrationBuilder.InsertData(
                table: "Infrastructure",
                columns: new[] { "InfrastructureId", "OfficeLocation", "NumberOfDesks", "NumberOfComputers", "HasMeetingRooms", "DepartmentId" },
                values: new object[] { 1, "Bloco A", 10, 8, true, 1 }
            );
            migrationBuilder.InsertData(
                table: "Infrastructure",
                columns: new[] { "InfrastructureId", "OfficeLocation", "NumberOfDesks", "NumberOfComputers", "HasMeetingRooms", "DepartmentId" },
                values: new object[] { 2, "Bloco B", 5, 3, false, 2 }
            );

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Title", "Description", "StartDate", "EndDate" },
                values: new object[] { 1, "Sistema Interno", "Desenvolvimento do sistema interno", new DateTime(2024, 6, 1), new DateTime(2024, 12, 31) }
            );
            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Title", "Description", "StartDate", "EndDate" },
                values: new object[] { 2, "Recrutamento Digital", "Plataforma de recrutamento", new DateTime(2024, 7, 1), new DateTime(2024, 11, 30) }
            );

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Name", "Position", "DepartmentId", "ProjectId" },
                values: new object[] { 1, "João Silva", "Desenvolvedor", 1, 1 }
            );
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Name", "Position", "DepartmentId", "ProjectId" },
                values: new object[] { 2, "Maria Souza", "Analista RH", 2, 2 }
            );

            migrationBuilder.InsertData(
                table: "EmployeeProjects",
                columns: new[] { "EmployeeId", "ProjectId" },
                values: new object[] { 1, 1 }
            );
            migrationBuilder.InsertData(
                table: "EmployeeProjects",
                columns: new[] { "EmployeeId", "ProjectId" },
                values: new object[] { 2, 2 }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "EmployeeProjects");
            migrationBuilder.DropTable(name: "Employees");
            migrationBuilder.DropTable(name: "Infrastructure");
            migrationBuilder.DropTable(name: "Projects");
            migrationBuilder.DropTable(name: "Departments");
        }
    }
}
