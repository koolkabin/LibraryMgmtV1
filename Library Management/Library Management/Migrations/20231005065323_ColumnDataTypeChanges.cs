using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Management.Migrations
{
    /// <inheritdoc />
    public partial class ColumnDataTypeChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestStatus",
                table: "RequestBooks",
                type: "int",
                nullable: false,
                defaultValue: 0);
            //update status of request book records which has lent record to -> approved
            migrationBuilder.Sql(@"
            update RequestBooks set RequestStatus = 1 where Id in (
                select BookId from LentBooks
            );
            update LentBooks set returnDate = null;
");
            migrationBuilder.CreateTable(
                name: "RequestCancelledLog",
                columns: table => new
                {
                    RequestBookID = table.Column<int>(type: "int", nullable: false),
                    CancelledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestCancelledLog", x => x.RequestBookID);
                    table.ForeignKey(
                        name: "FK_RequestCancelledLog_RequestBooks_RequestBookID",
                        column: x => x.RequestBookID,
                        principalTable: "RequestBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestCancelledLog");

            migrationBuilder.DropColumn(
                name: "RequestStatus",
                table: "RequestBooks");
        }
    }
}
