using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimuladorDeObjetos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPolimorfismoSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Params_InterfaceMethodModels_InterfaceMethodModelId",
                table: "Params");

            migrationBuilder.DropColumn(
                name: "ParentCallId",
                table: "MethodCalls");

            migrationBuilder.AddColumn<Guid>(
                name: "ConcreteTypeId",
                table: "Params",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentMethodId",
                table: "MethodCalls",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "ConcreteParameterTypes",
                table: "MethodCalls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<Guid>(
                name: "ConcreteTypeId",
                table: "LocalVars",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ConcreteTypeId",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MethodCallConcreteParameters",
                columns: table => new
                {
                    ConcreteParametersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MethodCallModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MethodCallConcreteParameters", x => new { x.ConcreteParametersId, x.MethodCallModelId });
                    table.ForeignKey(
                        name: "FK_MethodCallConcreteParameters_Classes_ConcreteParametersId",
                        column: x => x.ConcreteParametersId,
                        principalTable: "Classes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MethodCallConcreteParameters_MethodCalls_MethodCallModelId",
                        column: x => x.MethodCallModelId,
                        principalTable: "MethodCalls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Params_ConcreteTypeId",
                table: "Params",
                column: "ConcreteTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalVars_ConcreteTypeId",
                table: "LocalVars",
                column: "ConcreteTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_ConcreteTypeId",
                table: "Attributes",
                column: "ConcreteTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MethodCallConcreteParameters_MethodCallModelId",
                table: "MethodCallConcreteParameters",
                column: "MethodCallModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Classes_ConcreteTypeId",
                table: "Attributes",
                column: "ConcreteTypeId",
                principalTable: "Classes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalVars_Classes_ConcreteTypeId",
                table: "LocalVars",
                column: "ConcreteTypeId",
                principalTable: "Classes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Params_Classes_ConcreteTypeId",
                table: "Params",
                column: "ConcreteTypeId",
                principalTable: "Classes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Params_InterfaceMethodModels_InterfaceMethodModelId",
                table: "Params",
                column: "InterfaceMethodModelId",
                principalTable: "InterfaceMethodModels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Classes_ConcreteTypeId",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalVars_Classes_ConcreteTypeId",
                table: "LocalVars");

            migrationBuilder.DropForeignKey(
                name: "FK_Params_Classes_ConcreteTypeId",
                table: "Params");

            migrationBuilder.DropForeignKey(
                name: "FK_Params_InterfaceMethodModels_InterfaceMethodModelId",
                table: "Params");

            migrationBuilder.DropTable(
                name: "MethodCallConcreteParameters");

            migrationBuilder.DropIndex(
                name: "IX_Params_ConcreteTypeId",
                table: "Params");

            migrationBuilder.DropIndex(
                name: "IX_LocalVars_ConcreteTypeId",
                table: "LocalVars");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_ConcreteTypeId",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "ConcreteTypeId",
                table: "Params");

            migrationBuilder.DropColumn(
                name: "ConcreteParameterTypes",
                table: "MethodCalls");

            migrationBuilder.DropColumn(
                name: "ConcreteTypeId",
                table: "LocalVars");

            migrationBuilder.DropColumn(
                name: "ConcreteTypeId",
                table: "Attributes");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentMethodId",
                table: "MethodCalls",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentCallId",
                table: "MethodCalls",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Params_InterfaceMethodModels_InterfaceMethodModelId",
                table: "Params",
                column: "InterfaceMethodModelId",
                principalTable: "InterfaceMethodModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
