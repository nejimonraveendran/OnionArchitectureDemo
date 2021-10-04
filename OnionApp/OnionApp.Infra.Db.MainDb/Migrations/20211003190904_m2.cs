using Microsoft.EntityFrameworkCore.Migrations;

namespace OnionApp.Infra.Db.MainDb.Migrations
{
    public partial class m2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Role");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Role",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
