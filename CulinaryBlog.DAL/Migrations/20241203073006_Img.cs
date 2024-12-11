using Microsoft.EntityFrameworkCore.Migrations;

namespace CulinaryBlog.DAL.Migrations
{
    public partial class Img : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "pathimg",
                table: "categories",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pathimg",
                table: "categories");
        }
    }
}
