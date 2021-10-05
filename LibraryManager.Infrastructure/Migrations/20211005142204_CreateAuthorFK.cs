using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryManager.Infrastructure.Migrations
{
    public partial class CreateAuthorFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "Book",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 22, 4, 506, DateTimeKind.Unspecified).AddTicks(3776), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 18, 0, 446, DateTimeKind.Unspecified).AddTicks(6013), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "Author",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 22, 4, 490, DateTimeKind.Unspecified).AddTicks(8458), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 18, 0, 439, DateTimeKind.Unspecified).AddTicks(2003), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "Book",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 18, 0, 446, DateTimeKind.Unspecified).AddTicks(6013), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 22, 4, 506, DateTimeKind.Unspecified).AddTicks(3776), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "Author",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 18, 0, 439, DateTimeKind.Unspecified).AddTicks(2003), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 22, 4, 490, DateTimeKind.Unspecified).AddTicks(8458), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
