using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMIPSS_System.Migrations
{
    /// <inheritdoc />
    public partial class ReferralDataReferrerAndEmployeeTableAddition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Affiliation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeachingLicenseType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReferrerPeople",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelationshipToStudent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateFilledReferral = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferrerPeople", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Referrals",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ReasonsForReferral = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreasOfConcernAndHelpNeededDescription = table.Column<string>(type: "nvarchar(560)", maxLength: 560, nullable: false),
                    ReferrerID = table.Column<int>(type: "int", nullable: false),
                    ReferralReceived = table.Column<DateOnly>(type: "date", nullable: false),
                    TeamRecommendation = table.Column<DateOnly>(type: "date", nullable: false),
                    DispositionNoticeToReferrer = table.Column<DateOnly>(type: "date", nullable: false),
                    ParentalConsentForEvaluation = table.Column<DateOnly>(type: "date", nullable: false),
                    EvaluationTeamRecommendation = table.Column<DateOnly>(type: "date", nullable: false),
                    ParentNoticeForMeeting = table.Column<DateOnly>(type: "date", nullable: false),
                    ReferredToChildStudyTeam = table.Column<DateOnly>(type: "date", nullable: false),
                    Disposition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DispositionNoticeToParent = table.Column<DateOnly>(type: "date", nullable: false),
                    ReferralToEvaluationTeam = table.Column<DateOnly>(type: "date", nullable: false),
                    Recommendation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IEPMeeting = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referrals", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Referrals_ReferrerPeople_ReferrerID",
                        column: x => x.ReferrerID,
                        principalTable: "ReferrerPeople",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Referrals_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Referrals_ReferrerID",
                table: "Referrals",
                column: "ReferrerID");

            migrationBuilder.CreateIndex(
                name: "IX_Referrals_StudentId",
                table: "Referrals",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Referrals");

            migrationBuilder.DropTable(
                name: "ReferrerPeople");
        }
    }
}
