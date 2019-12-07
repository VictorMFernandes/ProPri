using Microsoft.EntityFrameworkCore.Migrations;

namespace ProPri.Students.Data.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressCity",
                table: "tb_student",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressComplement",
                table: "tb_student",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressDistrict",
                table: "tb_student",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressNumber",
                table: "tb_student",
                maxLength: 6,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressState",
                table: "tb_student",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressStreet",
                table: "tb_student",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressZipCode",
                table: "tb_student",
                maxLength: 8,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressCity",
                table: "tb_student");

            migrationBuilder.DropColumn(
                name: "AddressComplement",
                table: "tb_student");

            migrationBuilder.DropColumn(
                name: "AddressDistrict",
                table: "tb_student");

            migrationBuilder.DropColumn(
                name: "AddressNumber",
                table: "tb_student");

            migrationBuilder.DropColumn(
                name: "AddressState",
                table: "tb_student");

            migrationBuilder.DropColumn(
                name: "AddressStreet",
                table: "tb_student");

            migrationBuilder.DropColumn(
                name: "AddressZipCode",
                table: "tb_student");
        }
    }
}
