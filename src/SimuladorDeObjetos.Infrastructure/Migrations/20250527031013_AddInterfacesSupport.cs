using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimuladorDeObjetos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInterfacesSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InterfaceMethodModelId",
                table: "Params",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InterfaceModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClassModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterfaceModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterfaceModels_Classes_ClassModelId",
                        column: x => x.ClassModelId,
                        principalTable: "Classes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InterfaceMethodModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReturnType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InterfaceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterfaceMethodModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterfaceMethodModels_InterfaceModels_InterfaceId",
                        column: x => x.InterfaceId,
                        principalTable: "InterfaceModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Params_InterfaceMethodModelId",
                table: "Params",
                column: "InterfaceMethodModelId");

            migrationBuilder.CreateIndex(
                name: "IX_InterfaceMethodModels_InterfaceId",
                table: "InterfaceMethodModels",
                column: "InterfaceId");

            migrationBuilder.CreateIndex(
                name: "IX_InterfaceModels_ClassModelId",
                table: "InterfaceModels",
                column: "ClassModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Params_InterfaceMethodModels_InterfaceMethodModelId",
                table: "Params",
                column: "InterfaceMethodModelId",
                principalTable: "InterfaceMethodModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Params_InterfaceMethodModels_InterfaceMethodModelId",
                table: "Params");

            migrationBuilder.DropTable(
                name: "InterfaceMethodModels");

            migrationBuilder.DropTable(
                name: "InterfaceModels");

            migrationBuilder.DropIndex(
                name: "IX_Params_InterfaceMethodModelId",
                table: "Params");

            migrationBuilder.DropColumn(
                name: "InterfaceMethodModelId",
                table: "Params");
        }
    }
}
