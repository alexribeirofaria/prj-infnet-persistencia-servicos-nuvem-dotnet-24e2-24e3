using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PixCharge.Infrastructure.Migrations_MsSqlServer.Migrations
{
    public partial class Update_Project_Models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Customer_Id",
                table: "Transaction");

            migrationBuilder.AlterColumn<Guid>(
                name: "CorrelationID",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Charge",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorrelationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Monetary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentLinkID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    ValueWithDiscount = table.Column<int>(type: "int", nullable: false),
                    ExpiresDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresIn = table.Column<int>(type: "int", nullable: false),
                    PixKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentLinkUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QrCodeImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GlobalID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Charge_Customer_CorrelationID",
                        column: x => x.CorrelationID,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CorrelationId",
                table: "Transaction",
                column: "CorrelationId");

            migrationBuilder.CreateIndex(
                name: "IX_Charge_CorrelationID",
                table: "Charge",
                column: "CorrelationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Customer_CorrelationId",
                table: "Transaction",
                column: "CorrelationId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Customer_CorrelationId",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "Charge");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_CorrelationId",
                table: "Transaction");

            migrationBuilder.AlterColumn<string>(
                name: "CorrelationID",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Customer_Id",
                table: "Transaction",
                column: "Id",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
