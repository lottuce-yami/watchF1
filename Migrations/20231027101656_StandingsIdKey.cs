using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace F1Project.Migrations
{
    /// <inheritdoc />
    public partial class StandingsIdKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "driver_standings_pkey",
                table: "driver_standings");

            migrationBuilder.DropPrimaryKey(
                name: "constructor_standings_pkey",
                table: "constructor_standings");

            migrationBuilder.AddColumn<string>(
                name: "id",
                table: "driver_standings",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "id",
                table: "constructor_standings",
                type: "character varying(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "driver_standings_pkey",
                table: "driver_standings",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "constructor_standings_pkey",
                table: "constructor_standings",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "driver_standings_pkey",
                table: "driver_standings");

            migrationBuilder.DropPrimaryKey(
                name: "constructor_standings_pkey",
                table: "constructor_standings");

            migrationBuilder.DropColumn(
                name: "id",
                table: "driver_standings");

            migrationBuilder.DropColumn(
                name: "id",
                table: "constructor_standings");

            migrationBuilder.AddPrimaryKey(
                name: "driver_standings_pkey",
                table: "driver_standings",
                column: "name");

            migrationBuilder.AddPrimaryKey(
                name: "constructor_standings_pkey",
                table: "constructor_standings",
                column: "name");
        }
    }
}
