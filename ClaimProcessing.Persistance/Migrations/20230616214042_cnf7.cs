using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimProcessing.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class cnf7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Supliers_SupplierId",
                table: "Claims");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Supliers_SupplierId",
                table: "Shipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supliers",
                table: "Supliers");

            migrationBuilder.RenameTable(
                name: "Supliers",
                newName: "Suppliers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Suppliers_SupplierId",
                table: "Claims",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Suppliers_SupplierId",
                table: "Shipments",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Suppliers_SupplierId",
                table: "Claims");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Suppliers_SupplierId",
                table: "Shipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers");

            migrationBuilder.RenameTable(
                name: "Suppliers",
                newName: "Supliers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supliers",
                table: "Supliers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Supliers_SupplierId",
                table: "Claims",
                column: "SupplierId",
                principalTable: "Supliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Supliers_SupplierId",
                table: "Shipments",
                column: "SupplierId",
                principalTable: "Supliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
