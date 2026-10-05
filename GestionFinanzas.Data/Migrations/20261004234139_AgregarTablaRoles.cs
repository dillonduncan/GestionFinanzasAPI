using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionFinanzas.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregarTablaRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreRol = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numero_identificacion = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    nombre_usuario = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    apellido_usuario = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    correo_usuario = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    contraseña_usuario = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    estado_activo_usuario = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    id_categoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_usuario = table.Column<int>(type: "int", nullable: true),
                    nombre_categoria = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    estado_activo_categoria = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.id_categoria);
                    table.ForeignKey(
                        name: "FK_Categorias_Usuarios",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "Metas",
                columns: table => new
                {
                    id_meta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    nombre_meta = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    monto_objetivo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    saldo_actual = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    fecha_limite = table.Column<DateTime>(type: "datetime2", nullable: true),
                    estado_activo_meta = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metas", x => x.id_meta);
                    table.ForeignKey(
                        name: "FK_Metas_Usuarios",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "UsuariosRoles",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdRol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosRoles", x => new { x.IdRol, x.IdUsuario });
                    table.ForeignKey(
                        name: "FK_UsuariosRoles_Roles_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosRoles_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transacciones",
                columns: table => new
                {
                    id_transaccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    id_categoria = table.Column<int>(type: "int", nullable: false),
                    tipo_transaccion = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    monto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    fecha_transaccion = table.Column<DateTime>(type: "datetime", nullable: true),
                    descripcion_transaccion = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    estado_activo_transaccion = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacciones", x => x.id_transaccion);
                    table.ForeignKey(
                        name: "FK_Transacciones_Categorias",
                        column: x => x.id_categoria,
                        principalTable: "Categorias",
                        principalColumn: "id_categoria");
                    table.ForeignKey(
                        name: "FK_Transacciones_Usuarios",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_id_usuario",
                table: "Categorias",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Metas_id_usuario",
                table: "Metas",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_id_categoria",
                table: "Transacciones",
                column: "id_categoria");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_id_usuario",
                table: "Transacciones",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRoles_IdUsuario",
                table: "UsuariosRoles",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Metas");

            migrationBuilder.DropTable(
                name: "Transacciones");

            migrationBuilder.DropTable(
                name: "UsuariosRoles");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
