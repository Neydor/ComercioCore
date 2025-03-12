using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComercioCore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Municipio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reporte",
                columns: table => new
                {
                    RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Municipio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantidadEstablecimientos = table.Column<int>(type: "int", nullable: false),
                    TotalIngresos = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CantidadEmpleados = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CorreoElectronico = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Contrasena = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Rol = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario__3214EC276995779B", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Comerciante",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RazonSocial = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    MunicipioId = table.Column<int>(type: "int", nullable: false),
                    Telefono = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    CorreoElectronico = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Estado = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioActualizacion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Comercia__3214EC27F410B0B9", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comerciante_Municipio_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Establecimiento",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEstablecimiento = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Ingresos = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumeroEmpleados = table.Column<int>(type: "int", nullable: false),
                    ComercianteID = table.Column<int>(type: "int", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioActualizacion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Establec__3214EC27AF100268", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Estableci__Comer__3F466844",
                        column: x => x.ComercianteID,
                        principalTable: "Comerciante",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comerciante_MunicipioId",
                table: "Comerciante",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_Establecimiento_ComercianteID",
                table: "Establecimiento",
                column: "ComercianteID");

            migrationBuilder.CreateIndex(
                name: "UQ__Usuario__531402F38D061136",
                table: "Usuario",
                column: "CorreoElectronico",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Establecimiento");

            migrationBuilder.DropTable(
                name: "Reporte");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Comerciante");

            migrationBuilder.DropTable(
                name: "Municipio");
        }
    }
}
