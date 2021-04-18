using Microsoft.EntityFrameworkCore.Migrations;

namespace ScheduleApi.Migrations
{
    public partial class added_workstation_type_to_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WorkStationType",
                table: "Schedules",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkStationType",
                table: "Schedules");
        }
    }
}
