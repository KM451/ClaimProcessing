using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimProcessing.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class m5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttachmentUrlClaim");

            migrationBuilder.DropTable(
                name: "ClaimFotoUrl");

            migrationBuilder.DropTable(
                name: "ClaimSerialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_SerialNumbers_ClaimId",
                table: "SerialNumbers",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_FotoUrls_ClaimId",
                table: "FotoUrls",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentUrls_ClaimId",
                table: "AttachmentUrls",
                column: "ClaimId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttachmentUrls_Claims_ClaimId",
                table: "AttachmentUrls",
                column: "ClaimId",
                principalTable: "Claims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FotoUrls_Claims_ClaimId",
                table: "FotoUrls",
                column: "ClaimId",
                principalTable: "Claims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SerialNumbers_Claims_ClaimId",
                table: "SerialNumbers",
                column: "ClaimId",
                principalTable: "Claims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttachmentUrls_Claims_ClaimId",
                table: "AttachmentUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_FotoUrls_Claims_ClaimId",
                table: "FotoUrls");

            migrationBuilder.DropForeignKey(
                name: "FK_SerialNumbers_Claims_ClaimId",
                table: "SerialNumbers");

            migrationBuilder.DropIndex(
                name: "IX_SerialNumbers_ClaimId",
                table: "SerialNumbers");

            migrationBuilder.DropIndex(
                name: "IX_FotoUrls_ClaimId",
                table: "FotoUrls");

            migrationBuilder.DropIndex(
                name: "IX_AttachmentUrls_ClaimId",
                table: "AttachmentUrls");

            migrationBuilder.CreateTable(
                name: "AttachmentUrlClaim",
                columns: table => new
                {
                    AttachmentUrlsId = table.Column<int>(type: "int", nullable: false),
                    ClaimsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentUrlClaim", x => new { x.AttachmentUrlsId, x.ClaimsId });
                    table.ForeignKey(
                        name: "FK_AttachmentUrlClaim_AttachmentUrls_AttachmentUrlsId",
                        column: x => x.AttachmentUrlsId,
                        principalTable: "AttachmentUrls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttachmentUrlClaim_Claims_ClaimsId",
                        column: x => x.ClaimsId,
                        principalTable: "Claims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClaimFotoUrl",
                columns: table => new
                {
                    ClaimsId = table.Column<int>(type: "int", nullable: false),
                    FotoUrlsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimFotoUrl", x => new { x.ClaimsId, x.FotoUrlsId });
                    table.ForeignKey(
                        name: "FK_ClaimFotoUrl_Claims_ClaimsId",
                        column: x => x.ClaimsId,
                        principalTable: "Claims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClaimFotoUrl_FotoUrls_FotoUrlsId",
                        column: x => x.FotoUrlsId,
                        principalTable: "FotoUrls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClaimSerialNumber",
                columns: table => new
                {
                    ClaimsId = table.Column<int>(type: "int", nullable: false),
                    SerialNumbersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimSerialNumber", x => new { x.ClaimsId, x.SerialNumbersId });
                    table.ForeignKey(
                        name: "FK_ClaimSerialNumber_Claims_ClaimsId",
                        column: x => x.ClaimsId,
                        principalTable: "Claims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClaimSerialNumber_SerialNumbers_SerialNumbersId",
                        column: x => x.SerialNumbersId,
                        principalTable: "SerialNumbers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentUrlClaim_ClaimsId",
                table: "AttachmentUrlClaim",
                column: "ClaimsId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimFotoUrl_FotoUrlsId",
                table: "ClaimFotoUrl",
                column: "FotoUrlsId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimSerialNumber_SerialNumbersId",
                table: "ClaimSerialNumber",
                column: "SerialNumbersId");
        }
    }
}
