using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMIPSS_System.Migrations
{
    /// <inheritdoc />
    public partial class AddPdfUploadToReferral : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MIDScoringSheetId",
                table: "Referrals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Referrals_MIDScoringSheetId",
                table: "Referrals",
                column: "MIDScoringSheetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Referrals_PdfUploads_MIDScoringSheetId",
                table: "Referrals",
                column: "MIDScoringSheetId",
                principalTable: "PdfUploads",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Referrals_PdfUploads_MIDScoringSheetId",
                table: "Referrals");

            migrationBuilder.DropIndex(
                name: "IX_Referrals_MIDScoringSheetId",
                table: "Referrals");

            migrationBuilder.DropColumn(
                name: "MIDScoringSheetId",
                table: "Referrals");
        }
    }
}
