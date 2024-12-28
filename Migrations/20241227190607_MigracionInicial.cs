using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentsAPI.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "appointment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientName = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    ClientAge = table.Column<byte>(type: "TINYINT", nullable: false),
                    ClientRUT = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    ClientEmail = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    ClientPhone = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    Goals = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    IsEmailVerify = table.Column<bool>(type: "BIT", nullable: false),
                    PrevDiagnostic = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: true),
                    IsCompleted = table.Column<bool>(type: "BIT", nullable: false),
                    Date = table.Column<DateTime>(type: "DATETIME", nullable: true)
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
