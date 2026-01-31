using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nowyprzewodnik.Migrations
{
    /// <inheritdoc />
    public partial class NaprawaRelacji : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connections_Waypoints_TargetId",
                table: "Connections");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Waypoints",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Direction",
                table: "Connections",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_Waypoints_TargetId",
                table: "Connections",
                column: "TargetId",
                principalTable: "Waypoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connections_Waypoints_TargetId",
                table: "Connections");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Waypoints",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Direction",
                table: "Connections",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_Waypoints_TargetId",
                table: "Connections",
                column: "TargetId",
                principalTable: "Waypoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
