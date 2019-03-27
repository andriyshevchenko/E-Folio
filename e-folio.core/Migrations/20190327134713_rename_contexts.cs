using Microsoft.EntityFrameworkCore.Migrations;

namespace e_folio.core.Migrations
{
    public partial class rename_contexts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FolioFiles_Contexsts_ContextEntityId",
                table: "FolioFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Contexsts_ContextId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ContextId",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contexsts",
                table: "Contexsts");

            migrationBuilder.RenameTable(
                name: "Contexsts",
                newName: "Contexts");

            migrationBuilder.AlterColumn<int>(
                name: "ContextId",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContextEntityId",
                table: "FolioFiles",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contexts",
                table: "Contexts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ContextId",
                table: "Projects",
                column: "ContextId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FolioFiles_Contexts_ContextEntityId",
                table: "FolioFiles",
                column: "ContextEntityId",
                principalTable: "Contexts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Contexts_ContextId",
                table: "Projects",
                column: "ContextId",
                principalTable: "Contexts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FolioFiles_Contexts_ContextEntityId",
                table: "FolioFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Contexts_ContextId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ContextId",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contexts",
                table: "Contexts");

            migrationBuilder.RenameTable(
                name: "Contexts",
                newName: "Contexsts");

            migrationBuilder.AlterColumn<int>(
                name: "ContextId",
                table: "Projects",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ContextEntityId",
                table: "FolioFiles",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contexsts",
                table: "Contexsts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ContextId",
                table: "Projects",
                column: "ContextId");

            migrationBuilder.AddForeignKey(
                name: "FK_FolioFiles_Contexsts_ContextEntityId",
                table: "FolioFiles",
                column: "ContextEntityId",
                principalTable: "Contexsts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Contexsts_ContextId",
                table: "Projects",
                column: "ContextId",
                principalTable: "Contexsts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
