using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimuladorDeObjetos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddParentCallIdToMethodCall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentCallId",
                table: "MethodCalls",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentCallId",
                table: "MethodCalls");
        }
    }
}
