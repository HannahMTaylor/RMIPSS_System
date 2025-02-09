using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMIPSS_System.Migrations
{
    /// <inheritdoc />
    public partial class SE2Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SE2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    CompletedByName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompletedByRelationship = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompletedByPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CompletedByEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CompletedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PhysicalConcerns = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OtherPhysicalConcerns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisionConcerns = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OtherVisionConcerns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HearingConcerns = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OtherHearingConcerns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageSpeechConcerns = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OtherLanguageSpeechConcerns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BehaviorConcerns = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OtherBehaviorConcerns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcademicConcerns = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OtherAcademicConcerns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherConcerns = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OtherOtherConcerns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SE2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SE2_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SE2_StudentId",
                table: "SE2",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SE2");
        }
    }
}
