using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "status_id",
                table: "prosthetics",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "prosthetic_statuses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prosthetic_statuses", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_prosthetics_status_id",
                table: "prosthetics",
                column: "status_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prosthetics_Statuses",
                table: "prosthetics",
                column: "status_id",
                principalTable: "prosthetic_statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prosthetics_Statuses",
                table: "prosthetics");

            migrationBuilder.DropTable(
                name: "prosthetic_statuses");

            migrationBuilder.DropIndex(
                name: "ix_prosthetics_status_id",
                table: "prosthetics");

            migrationBuilder.DropColumn(
                name: "status_id",
                table: "prosthetics");
        }
    }
}
