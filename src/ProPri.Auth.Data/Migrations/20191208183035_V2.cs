using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProPri.Users.Data.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "tb_user",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDate",
                table: "tb_user",
                nullable: false,
                defaultValue: new DateTime(2019, 12, 8, 15, 30, 34, 827, DateTimeKind.Local).AddTicks(3898));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "tb_user");

            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "tb_user");
        }
    }
}
