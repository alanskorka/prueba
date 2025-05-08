using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimuladorDeObjetos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SegundaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentMethodId",
                table: "MethodCallModel",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ReferenceName",
                table: "MethodCallModel",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentMethodId",
                table: "MethodCallModel");

            migrationBuilder.DropColumn(
                name: "ReferenceName",
                table: "MethodCallModel");
        }
    }
}
