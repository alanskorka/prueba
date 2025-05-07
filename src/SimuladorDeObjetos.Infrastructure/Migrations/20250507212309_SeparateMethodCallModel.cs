using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimuladorDeObjetos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeparateMethodCallModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MethodCallModel");

            migrationBuilder.CreateTable(
                name: "MethodCalls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MethodName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceType = table.Column<int>(type: "int", nullable: false),
                    ParentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferenceName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MethodCalls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MethodCalls_Methods_ParentMethodId",
                        column: x => x.ParentMethodId,
                        principalTable: "Methods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MethodCalls_ParentMethodId",
                table: "MethodCalls",
                column: "ParentMethodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MethodCalls");

            migrationBuilder.CreateTable(
                name: "MethodCallModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MethodModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MethodName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferenceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MethodCallModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MethodCallModel_Methods_MethodModelId",
                        column: x => x.MethodModelId,
                        principalTable: "Methods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MethodCallModel_MethodModelId",
                table: "MethodCallModel",
                column: "MethodModelId");
        }
    }
}
