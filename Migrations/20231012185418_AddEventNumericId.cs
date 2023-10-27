using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace F1Project.Migrations
{
    /// <inheritdoc />
    public partial class AddEventNumericId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "schedule_pkey",
                table: "schedule");

            migrationBuilder.AddColumn<short>(
                name: "id",
                table: "schedule",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

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

            migrationBuilder.DropColumn(
                name: "id",
                table: "schedule");

            migrationBuilder.AddPrimaryKey(
                name: "schedule_pkey",
                table: "schedule",
                column: "time");
        }
    }
}
