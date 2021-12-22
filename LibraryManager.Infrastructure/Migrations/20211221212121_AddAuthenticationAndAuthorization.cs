using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryManager.Infrastructure.Migrations
{
    public partial class AddAuthenticationAndAuthorization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "Transaction",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2021, 12, 21, 21, 21, 20, 858, DateTimeKind.Unspecified).AddTicks(6595), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 49, 53, 376, DateTimeKind.Unspecified).AddTicks(9725), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "Teacher",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2021, 12, 21, 21, 21, 20, 857, DateTimeKind.Unspecified).AddTicks(6058), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 49, 53, 375, DateTimeKind.Unspecified).AddTicks(9150), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Teacher",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Teacher",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "Student",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2021, 12, 21, 21, 21, 20, 856, DateTimeKind.Unspecified).AddTicks(4855), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 49, 53, 374, DateTimeKind.Unspecified).AddTicks(7747), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "Book",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2021, 12, 21, 21, 21, 20, 854, DateTimeKind.Unspecified).AddTicks(5634), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 49, 53, 372, DateTimeKind.Unspecified).AddTicks(9012), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Book",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "Author",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2021, 12, 21, 21, 21, 20, 844, DateTimeKind.Unspecified).AddTicks(8977), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 49, 53, 363, DateTimeKind.Unspecified).AddTicks(847), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Book");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "Transaction",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 49, 53, 376, DateTimeKind.Unspecified).AddTicks(9725), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2021, 12, 21, 21, 21, 20, 858, DateTimeKind.Unspecified).AddTicks(6595), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "Teacher",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 49, 53, 375, DateTimeKind.Unspecified).AddTicks(9150), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2021, 12, 21, 21, 21, 20, 857, DateTimeKind.Unspecified).AddTicks(6058), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "Student",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 49, 53, 374, DateTimeKind.Unspecified).AddTicks(7747), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2021, 12, 21, 21, 21, 20, 856, DateTimeKind.Unspecified).AddTicks(4855), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "Book",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 49, 53, 372, DateTimeKind.Unspecified).AddTicks(9012), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2021, 12, 21, 21, 21, 20, 854, DateTimeKind.Unspecified).AddTicks(5634), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreateDate",
                table: "Author",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2021, 10, 5, 14, 49, 53, 363, DateTimeKind.Unspecified).AddTicks(847), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2021, 12, 21, 21, 21, 20, 844, DateTimeKind.Unspecified).AddTicks(8977), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
