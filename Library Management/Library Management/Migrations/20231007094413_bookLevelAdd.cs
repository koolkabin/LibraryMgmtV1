using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Management.Migrations
{
    /// <inheritdoc />
    public partial class bookLevelAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "LevelId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BookLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookLevels", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_LevelId",
                table: "Books",
                column: "LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookLevels_LevelId",
                table: "Books",
                column: "LevelId",
                principalTable: "BookLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookLevels_LevelId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "BookLevels");

            migrationBuilder.DropIndex(
                name: "IX_Books_LevelId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
