using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireApp.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsActiveToCollaborator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Collaborators",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Collaborators");
        }
    }
}
