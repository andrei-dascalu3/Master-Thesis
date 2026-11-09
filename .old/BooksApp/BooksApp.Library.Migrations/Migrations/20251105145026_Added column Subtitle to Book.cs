using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksApp.Library.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddedcolumnSubtitletoBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Subtitle",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subtitle",
                table: "Books");
        }
    }
}
