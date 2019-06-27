using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SistemaAC.Data.Migrations
{
    public partial class Migracion3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "estado",
                table: "Curso",
                newName: "Estado");

            migrationBuilder.RenameColumn(
                name: "costo",
                table: "Curso",
                newName: "Costo");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Curso",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    Codigo = table.Column<string>(nullable: true),
                    especialidad = table.Column<string>(nullable: true),
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Apellidos = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Documento = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    FechaNacimiento = table.Column<DateTime>(nullable: false),
                    Nombres = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "Curso",
                newName: "estado");

            migrationBuilder.RenameColumn(
                name: "Costo",
                table: "Curso",
                newName: "costo");

            migrationBuilder.AlterColumn<int>(
                name: "Nombre",
                table: "Curso",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
