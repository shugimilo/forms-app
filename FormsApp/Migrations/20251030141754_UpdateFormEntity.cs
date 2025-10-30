using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormsApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFormEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Forms",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RequireLogin",
                table: "Forms",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Forms_UserId",
                table: "Forms",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_Users_UserId",
                table: "Forms",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forms_Users_UserId",
                table: "Forms");

            migrationBuilder.DropIndex(
                name: "IX_Forms_UserId",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "RequireLogin",
                table: "Forms");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Forms",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
