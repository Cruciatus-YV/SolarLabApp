using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pozdravlyator.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class add_avatar_extention : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarExtention",
                table: "Users",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarExtention",
                table: "Users");
        }
    }
}
