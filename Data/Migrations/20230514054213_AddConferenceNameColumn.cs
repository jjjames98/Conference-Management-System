using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Conference_Management_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddConferenceNameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Conferences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Conferences");
        }
    }
}
