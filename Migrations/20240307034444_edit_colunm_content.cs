using Microsoft.EntityFrameworkCore.Migrations;

namespace use_open_source_fast_report.Migrations
{
    public partial class edit_colunm_content : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Report",
                newName: "Content");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Report",
                newName: "Description");
        }
    }
}
