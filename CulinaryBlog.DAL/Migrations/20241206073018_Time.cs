using Microsoft.EntityFrameworkCore.Migrations;

namespace CulinaryBlog.DAL.Migrations
{
    public partial class Time : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<int>(
                name: "time",
                table: "recepies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "time",
                table: "recepies");

        }
    }
}
