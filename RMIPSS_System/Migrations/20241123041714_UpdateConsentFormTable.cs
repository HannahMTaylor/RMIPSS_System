using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMIPSS_System.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConsentFormTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Evaluation",
                table: "ConsentForms",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "ConsentForms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "ConsentForms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "SubmittedDate",
                table: "ConsentForms",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateIndex(
                name: "IX_ConsentForms_StudentId",
                table: "ConsentForms",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsentForms_Students_StudentId",
                table: "ConsentForms",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsentForms_Students_StudentId",
                table: "ConsentForms");

            migrationBuilder.DropIndex(
                name: "IX_ConsentForms_StudentId",
                table: "ConsentForms");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ConsentForms");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "ConsentForms");

            migrationBuilder.DropColumn(
                name: "SubmittedDate",
                table: "ConsentForms");

            migrationBuilder.AlterColumn<bool>(
                name: "Evaluation",
                table: "ConsentForms",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
