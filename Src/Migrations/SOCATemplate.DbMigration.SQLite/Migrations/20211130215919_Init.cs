using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOCATemplate.DbMigration.SQLite.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "tbl_tbl_User",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_tbl_User", x => x.ID);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tbl_tbl_User",
                columns: new[] { "ID", "Name" },
                values: new object[] { 1, "Test User" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_tbl_User",
                schema: "dbo");
        }
    }
}
