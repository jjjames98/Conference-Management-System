using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Conference_Management_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addfilefieldtopaper : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "Papers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "Papers");
        }
    }
}
