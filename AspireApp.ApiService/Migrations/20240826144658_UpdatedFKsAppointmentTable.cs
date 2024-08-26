using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireApp.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedFKsAppointmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CollaboratorId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CollaboratorId",
                table: "Appointments",
                column: "CollaboratorId");

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

            migrationBuilder.DropIndex(
                name: "IX_Appointments_CollaboratorId",
                table: "Appointments");

            migrationBuilder.AlterColumn<int>(
                name: "CollaboratorId",
                table: "Appointments",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
