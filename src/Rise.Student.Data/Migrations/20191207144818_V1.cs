using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Rise.Students.Data.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_student",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 3, nullable: true),
                    Surname = table.Column<string>(maxLength: 60, nullable: true),
                    Email = table.Column<string>(maxLength: 160, nullable: true),
                    Login = table.Column<string>(maxLength: 30, nullable: true),
                    Password = table.Column<string>(fixedLength: true, maxLength: 32, nullable: true),
                    Active = table.Column<bool>(nullable: true),
                    ImageUrl = table.Column<string>(maxLength: 160, nullable: true),
                    ImagePublicId = table.Column<string>(maxLength: 25, nullable: true),
                    AddressZipCode = table.Column<string>(maxLength: 8, nullable: true),
                    AddressStreet = table.Column<string>(maxLength: 30, nullable: true),
                    AddressNumber = table.Column<string>(maxLength: 6, nullable: true),
                    AddressComplement = table.Column<string>(maxLength: 60, nullable: true),
                    AddressDistrict = table.Column<string>(maxLength: 30, nullable: true),
                    AddressCity = table.Column<string>(maxLength: 30, nullable: true),
                    AddressState = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_student", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_student_Login",
                table: "tb_student",
                column: "Login",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_student");
        }
    }
}
