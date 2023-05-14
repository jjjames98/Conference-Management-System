using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Conference_Management_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorFieldToPaper : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Papers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Papers");
        }
    }
}
