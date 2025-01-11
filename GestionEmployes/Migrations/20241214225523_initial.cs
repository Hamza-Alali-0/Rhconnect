using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionEmployes.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "HR");

            migrationBuilder.CreateTable(
                name: "Departements",
                schema: "HR",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomDepartement = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departements", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "Employes",
                schema: "HR",
                columns: table => new
                {
                    EmployeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomEmploye = table.Column<string>(type: "varchar(200)", nullable: false),
                    UserImage = table.Column<string>(type: "varchar(200)", nullable: true),
                    DateNaissance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Salaire = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    DateEmbauche = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NationalId = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    DepartementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employes", x => x.EmployeId);
                    table.ForeignKey(
                        name: "FK_Employes_Departements_DepartementId",
                        column: x => x.DepartementId,
                        principalSchema: "HR",
                        principalTable: "Departements",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Taches",
                schema: "HR",
                columns: table => new
                {
                    TacheId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomTache = table.Column<string>(type: "varchar(200)", nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", nullable: true),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Statut = table.Column<string>(type: "varchar(20)", nullable: false),
                    EmployeId = table.Column<int>(type: "int", nullable: false),
                    Priorite = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taches", x => x.TacheId);
                    table.ForeignKey(
                        name: "FK_Taches_Employes_EmployeId",
                        column: x => x.EmployeId,
                        principalSchema: "HR",
                        principalTable: "Employes",
                        principalColumn: "EmployeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employes_DepartementId",
                schema: "HR",
                table: "Employes",
                column: "DepartementId");

            migrationBuilder.CreateIndex(
                name: "IX_Taches_EmployeId",
                schema: "HR",
                table: "Taches",
                column: "EmployeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Taches",
                schema: "HR");

            migrationBuilder.DropTable(
                name: "Employes",
                schema: "HR");

            migrationBuilder.DropTable(
                name: "Departements",
                schema: "HR");
        }
    }
}
