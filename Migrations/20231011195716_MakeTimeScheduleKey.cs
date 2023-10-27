using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace F1Project.Migrations
{
    /// <inheritdoc />
    public partial class MakeTimeScheduleKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "schedule_pkey",
                table: "schedule");

            migrationBuilder.DropColumn(
                name: "id",
                table: "schedule");

            migrationBuilder.AddPrimaryKey(
                name: "schedule_pkey",
                table: "schedule",
                column: "time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "schedule_pkey",
                table: "schedule");

            migrationBuilder.AddColumn<string>(
                name: "id",
                table: "schedule",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "schedule_pkey",
                table: "schedule",
                column: "id");
        }
    }
}
