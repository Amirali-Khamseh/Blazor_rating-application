using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalBlazorIndivisualUser.Server.Data.Migrations
{
    public partial class CategoryModelWithRelationPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "AspNetPosts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AspNetCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetPosts_CategoryId",
                table: "AspNetPosts",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetPosts_AspNetCategories_CategoryId",
                table: "AspNetPosts",
                column: "CategoryId",
                principalTable: "AspNetCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetPosts_AspNetCategories_CategoryId",
                table: "AspNetPosts");

            migrationBuilder.DropTable(
                name: "AspNetCategories");

            migrationBuilder.DropIndex(
                name: "IX_AspNetPosts_CategoryId",
                table: "AspNetPosts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "AspNetPosts");
        }
    }
}
