using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireApp.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppointmentTableCollaboratorId3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Collaborators_NewCollaboratorId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "NewCollaboratorId",
                table: "Appointments",
                newName: "CollaboratorId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_NewCollaboratorId",
                table: "Appointments",
                newName: "IX_Appointments_CollaboratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Collaborators_CollaboratorId",
                table: "Appointments",
                column: "CollaboratorId",
                principalTable: "Collaborators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Collaborators_CollaboratorId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "CollaboratorId",
                table: "Appointments",
                newName: "NewCollaboratorId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_CollaboratorId",
                table: "Appointments",
                newName: "IX_Appointments_NewCollaboratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Collaborators_NewCollaboratorId",
                table: "Appointments",
                column: "NewCollaboratorId",
                principalTable: "Collaborators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
