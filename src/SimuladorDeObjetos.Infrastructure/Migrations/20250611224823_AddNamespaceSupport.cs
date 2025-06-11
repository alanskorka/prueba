using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimuladorDeObjetos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNamespaceSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOverride",
                table: "Methods",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStatic",
                table: "Methods",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVirtual",
                table: "Methods",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStatic",
                table: "Attributes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Namespaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Namespaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Namespaces_Namespaces_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Namespaces",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Namespaces_ParentId",
                table: "Namespaces",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Namespaces");

            migrationBuilder.DropColumn(
                name: "IsOverride",
                table: "Methods");

            migrationBuilder.DropColumn(
                name: "IsStatic",
                table: "Methods");

            migrationBuilder.DropColumn(
                name: "IsVirtual",
                table: "Methods");

            migrationBuilder.DropColumn(
                name: "IsStatic",
                table: "Attributes");
        }
    }
}
