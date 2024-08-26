using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireApp.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppointmentTableCollaboratorId1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NewCollaboratorId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_NewCollaboratorId",
                table: "Appointments",
                column: "NewCollaboratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Collaborators_NewCollaboratorId",
                table: "Appointments",
                column: "NewCollaboratorId",
                principalTable: "Collaborators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Collaborators_NewCollaboratorId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_NewCollaboratorId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "NewCollaboratorId",
                table: "Appointments");
        }
    }
}
