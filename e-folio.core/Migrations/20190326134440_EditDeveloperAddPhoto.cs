using Microsoft.EntityFrameworkCore.Migrations;

namespace e_folio.core.Migrations
{
    public partial class EditDeveloperAddPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoLink",
                table: "Developers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoLink",
                table: "Developers");
        }
    }
}
