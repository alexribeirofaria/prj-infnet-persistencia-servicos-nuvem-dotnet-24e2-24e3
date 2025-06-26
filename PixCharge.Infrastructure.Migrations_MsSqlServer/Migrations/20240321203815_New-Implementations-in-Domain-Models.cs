using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PixCharge.Infrastructure.Migrations_MsSqlServer.Migrations
{
    public partial class NewImplementationsinDomainModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Charge_Customer_CorrelationID",
                table: "Charge");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Customer_CorrelationId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Charge_CorrelationID",
                table: "Charge");

            migrationBuilder.DropColumn(
                name: "Customer_Email",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Customer");

            migrationBuilder.AddColumn<Guid>(
                name: "PIXId",
                table: "Transaction",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TaxID",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<Guid>(
                name: "CorrelationID",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Charge",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FlatId",
                table: "Charge",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Flat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Monetary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DtCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DtActivation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flat_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PIX",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Monetary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PIX", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PIX_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DtCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_PIXId",
                table: "Transaction",
                column: "PIXId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UserId",
                table: "Customer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Charge_CustomerId",
                table: "Charge",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Charge_FlatId",
                table: "Charge",
                column: "FlatId");

            migrationBuilder.CreateIndex(
                name: "IX_Flat_CustomerId",
                table: "Flat",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PIX_CustomerId",
                table: "PIX",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Charge_Customer_CustomerId",
                table: "Charge",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Charge_Flat_FlatId",
                table: "Charge",
                column: "FlatId",
                principalTable: "Flat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_User_UserId",
                table: "Customer",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Customer_CorrelationId",
                table: "Transaction",
                column: "CorrelationId",
                principalTable: "Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_PIX_PIXId",
                table: "Transaction",
                column: "PIXId",
                principalTable: "PIX",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Charge_Customer_CustomerId",
                table: "Charge");

            migrationBuilder.DropForeignKey(
                name: "FK_Charge_Flat_FlatId",
                table: "Charge");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_User_UserId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Customer_CorrelationId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_PIX_PIXId",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "Flat");

            migrationBuilder.DropTable(
                name: "PIX");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_PIXId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Customer_UserId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Charge_CustomerId",
                table: "Charge");

            migrationBuilder.DropIndex(
                name: "IX_Charge_FlatId",
                table: "Charge");

            migrationBuilder.DropColumn(
                name: "PIXId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Charge");

            migrationBuilder.DropColumn(
                name: "FlatId",
                table: "Charge");

            migrationBuilder.AlterColumn<string>(
                name: "TaxID",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customer",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "CorrelationID",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Customer_Email",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Customer",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Charge_CorrelationID",
                table: "Charge",
                column: "CorrelationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Charge_Customer_CorrelationID",
                table: "Charge",
                column: "CorrelationID",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Customer_CorrelationId",
                table: "Transaction",
                column: "CorrelationId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
