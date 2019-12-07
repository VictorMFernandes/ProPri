using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProPri.Students.Data.Migrations
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
                    ImagePublicId = table.Column<string>(maxLength: 25, nullable: true)
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
