using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentsAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "appointment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    public_id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    client_name = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    client_age = table.Column<byte>(type: "TINYINT", nullable: false),
                    client_rut = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    client_email = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    client_phone = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    goals = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    is_email_verified = table.Column<bool>(type: "BIT", nullable: false),
                    previous_diagnostic = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    is_completed = table.Column<bool>(type: "BIT", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    appointment_date = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointment", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointment");
        }
    }
}
