using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimProcessing.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ClaimNo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClaimNumber",
                table: "Claims",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClaimNumber",
                table: "Claims");
        }
    }
}
