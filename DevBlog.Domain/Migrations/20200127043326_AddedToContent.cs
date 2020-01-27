using Microsoft.EntityFrameworkCore.Migrations;

namespace DevBlog.Domain.Migrations
{
    public partial class AddedToContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Contents");

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "Contents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Contents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Contents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Contents");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Contents",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
