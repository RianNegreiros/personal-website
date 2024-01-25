using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Infrastructure.Data.Migrations
{
    public partial class RemovePersistentToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "PersistentToken",
            table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
