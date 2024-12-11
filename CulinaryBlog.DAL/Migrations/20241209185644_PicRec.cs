using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CulinaryBlog.DAL.Migrations
{
    public partial class PicRec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "picture_recipes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_recipes = table.Column<Guid>(type: "uuid", nullable: false),
                    path_img = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pictures_recipes", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "picture_recipes");
        }
    }
}
