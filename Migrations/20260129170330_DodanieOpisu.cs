using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nowyprzewodnik.Migrations
{
    /// <inheritdoc />
    public partial class DodanieOpisu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Waypoints",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Waypoints");
        }
    }
}
