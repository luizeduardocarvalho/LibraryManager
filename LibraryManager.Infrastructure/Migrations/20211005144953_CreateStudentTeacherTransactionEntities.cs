using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LibraryManager.Infrastructure.Migrations
{
    public partial class CreateStudentTeacherTransactionEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsRemoved",
                table: "Transaction",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "Transaction",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 49, 53, 376, DateTimeKind.Unspecified).AddTicks(9725), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "Book",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 49, 53, 372, DateTimeKind.Unspecified).AddTicks(9012), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 24, 50, 547, DateTimeKind.Unspecified).AddTicks(754), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<long>(
                name: "StudentId",
                table: "Book",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "Author",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 49, 53, 363, DateTimeKind.Unspecified).AddTicks(847), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 24, 50, 537, DateTimeKind.Unspecified).AddTicks(5032), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 49, 53, 375, DateTimeKind.Unspecified).AddTicks(9150), new TimeSpan(0, 0, 0, 0, 0))),
                    IsRemoved = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    TeacherId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 49, 53, 374, DateTimeKind.Unspecified).AddTicks(7747), new TimeSpan(0, 0, 0, 0, 0))),
                    IsRemoved = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_StudentId",
                table: "Transaction",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_StudentId",
                table: "Book",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_TeacherId",
                table: "Student",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Student_StudentId",
                table: "Book",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Student_StudentId",
                table: "Transaction",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Student_StudentId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Student_StudentId",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_StudentId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Book_StudentId",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Book");

            migrationBuilder.AlterColumn<bool>(
                name: "IsRemoved",
                table: "Transaction",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "Transaction",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 49, 53, 376, DateTimeKind.Unspecified).AddTicks(9725), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "Book",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 24, 50, 547, DateTimeKind.Unspecified).AddTicks(754), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 49, 53, 372, DateTimeKind.Unspecified).AddTicks(9012), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "Author",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 24, 50, 537, DateTimeKind.Unspecified).AddTicks(5032), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 49, 53, 363, DateTimeKind.Unspecified).AddTicks(847), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
