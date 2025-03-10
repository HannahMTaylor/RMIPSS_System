using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMIPSS_System.Migrations
{
    /// <inheritdoc />
    public partial class referralfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Referrals_ReferrerPeople_ReferrerID",
                table: "Referrals");

            migrationBuilder.RenameColumn(
                name: "ReferrerID",
                table: "Referrals",
                newName: "ReferrerId");

            migrationBuilder.RenameIndex(
                name: "IX_Referrals_ReferrerID",
                table: "Referrals",
                newName: "IX_Referrals_ReferrerId");

            migrationBuilder.AlterColumn<string>(
                name: "RelationshipToStudent",
                table: "ReferrerPeople",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "ReferrerPeople",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "ReferrerPeople",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "ReferrerPeople",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "ReferrerPeople",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "ReferrerPeople",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Recommendation",
                table: "Referrals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Disposition",
                table: "Referrals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AreasOfConcernAndHelpNeededDescription",
                table: "Referrals",
                type: "nvarchar(560)",
                maxLength: 560,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(560)",
                oldMaxLength: 560);

            migrationBuilder.AddColumn<string>(
                name: "OtherReasonsForReferral",
                table: "Referrals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TeachingLicenseType",
                table: "Employees",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Affiliation",
                table: "Employees",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "PdfUploads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Length = table.Column<int>(type: "int", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PdfUploads", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Referrals_ReferrerPeople_ReferrerId",
                table: "Referrals",
                column: "ReferrerId",
                principalTable: "ReferrerPeople",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Referrals_ReferrerPeople_ReferrerId",
                table: "Referrals");

            migrationBuilder.DropTable(
                name: "PdfUploads");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "ReferrerPeople");

            migrationBuilder.DropColumn(
                name: "OtherReasonsForReferral",
                table: "Referrals");

            migrationBuilder.RenameColumn(
                name: "ReferrerId",
                table: "Referrals",
                newName: "ReferrerID");

            migrationBuilder.RenameIndex(
                name: "IX_Referrals_ReferrerId",
                table: "Referrals",
                newName: "IX_Referrals_ReferrerID");

            migrationBuilder.AlterColumn<string>(
                name: "RelationshipToStudent",
                table: "ReferrerPeople",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "ReferrerPeople",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "ReferrerPeople",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "ReferrerPeople",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "ReferrerPeople",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Recommendation",
                table: "Referrals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Disposition",
                table: "Referrals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AreasOfConcernAndHelpNeededDescription",
                table: "Referrals",
                type: "nvarchar(560)",
                maxLength: 560,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(560)",
                oldMaxLength: 560,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TeachingLicenseType",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Affiliation",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddForeignKey(
                name: "FK_Referrals_ReferrerPeople_ReferrerID",
                table: "Referrals",
                column: "ReferrerID",
                principalTable: "ReferrerPeople",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
