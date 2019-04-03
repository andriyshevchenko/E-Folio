using Microsoft.EntityFrameworkCore.Migrations;

namespace e_folio.core.Migrations
{
    public partial class addphotolinktoproject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoLink",
                table: "Projects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoLink",
                table: "Projects");
        }
    }
}
