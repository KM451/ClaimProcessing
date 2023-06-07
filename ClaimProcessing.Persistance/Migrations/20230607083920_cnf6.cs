using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimProcessing.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class cnf6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackingDetails",
                table: "Shipments");

            migrationBuilder.AddColumn<string>(
                name: "ShippingDocumentNo",
                table: "Shipments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Speditor",
                table: "Shipments",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingDocumentNo",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "Speditor",
                table: "Shipments");

            migrationBuilder.AddColumn<string>(
                name: "PackingDetails",
                table: "Shipments",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);
        }
    }
}
