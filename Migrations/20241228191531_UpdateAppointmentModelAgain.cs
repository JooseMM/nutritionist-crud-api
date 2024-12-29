using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentsAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppointmentModelAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "appointment");

            migrationBuilder.RenameColumn(
                name: "TrackingID",
                table: "appointment",
                newName: "TrackingId");

            migrationBuilder.RenameColumn(
                name: "Goals",
                table: "appointment",
                newName: "goals");

            migrationBuilder.RenameColumn(
                name: "PrevDiagnostic",
                table: "appointment",
                newName: "previous_diagnostic");

            migrationBuilder.RenameColumn(
                name: "IsCompleted",
                table: "appointment",
                newName: "is_completed");

            migrationBuilder.RenameColumn(
                name: "ClientRUT",
                table: "appointment",
                newName: "client_rut");

            migrationBuilder.RenameColumn(
                name: "ClientPhone",
                table: "appointment",
                newName: "client_phone");

            migrationBuilder.RenameColumn(
                name: "ClientName",
                table: "appointment",
                newName: "client_name");

            migrationBuilder.RenameColumn(
                name: "ClientEmail",
                table: "appointment",
                newName: "client_email");

            migrationBuilder.RenameColumn(
                name: "ClientAge",
                table: "appointment",
                newName: "client_age");

            migrationBuilder.RenameColumn(
                name: "IsEmailVerify",
                table: "appointment",
                newName: "is_email_verified");

            migrationBuilder.AlterColumn<string>(
                name: "previous_diagnostic",
                table: "appointment",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDateTime",
                table: "appointment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDateTime",
                table: "appointment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "appointment_date",
                table: "appointment",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDateTime",
                table: "appointment");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                table: "appointment");

            migrationBuilder.DropColumn(
                name: "appointment_date",
                table: "appointment");

            migrationBuilder.RenameColumn(
                name: "goals",
                table: "appointment",
                newName: "Goals");

            migrationBuilder.RenameColumn(
                name: "TrackingId",
                table: "appointment",
                newName: "TrackingID");

            migrationBuilder.RenameColumn(
                name: "previous_diagnostic",
                table: "appointment",
                newName: "PrevDiagnostic");

            migrationBuilder.RenameColumn(
                name: "is_completed",
                table: "appointment",
                newName: "IsCompleted");

            migrationBuilder.RenameColumn(
                name: "client_rut",
                table: "appointment",
                newName: "ClientRUT");

            migrationBuilder.RenameColumn(
                name: "client_phone",
                table: "appointment",
                newName: "ClientPhone");

            migrationBuilder.RenameColumn(
                name: "client_name",
                table: "appointment",
                newName: "ClientName");

            migrationBuilder.RenameColumn(
                name: "client_email",
                table: "appointment",
                newName: "ClientEmail");

            migrationBuilder.RenameColumn(
                name: "client_age",
                table: "appointment",
                newName: "ClientAge");

            migrationBuilder.RenameColumn(
                name: "is_email_verified",
                table: "appointment",
                newName: "IsEmailVerify");

            migrationBuilder.AlterColumn<string>(
                name: "PrevDiagnostic",
                table: "appointment",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "appointment",
                type: "DATETIME",
                nullable: true);
        }
    }
}
