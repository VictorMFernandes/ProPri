using Microsoft.EntityFrameworkCore.Migrations;

namespace Rise.Users.Data.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePublicId",
                table: "tb_user",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "tb_user",
                maxLength: 160,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePublicId",
                table: "tb_user");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "tb_user");
        }
    }
}
