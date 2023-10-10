using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace F1Project.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "events_pkey",
                table: "events");

            migrationBuilder.RenameTable(
                name: "events",
                newName: "schedule");

            migrationBuilder.AddPrimaryKey(
                name: "schedule_pkey",
                table: "schedule",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "schedule_pkey",
                table: "schedule");

            migrationBuilder.RenameTable(
                name: "schedule",
                newName: "events");

            migrationBuilder.AddPrimaryKey(
                name: "events_pkey",
                table: "events",
                column: "id");
        }
    }
}
