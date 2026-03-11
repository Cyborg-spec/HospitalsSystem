using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalSystems.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorAppointmentTimeBounds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_appointments_doctor_id_date_time",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "date_time",
                table: "Appointments",
                newName: "start_time");

            migrationBuilder.AddColumn<DateTime>(
                name: "end_time",
                table: "Appointments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "ix_appointments_doctor_id_start_time_end_time",
                table: "Appointments",
                columns: new[] { "doctor_id", "start_time", "end_time" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_appointments_doctor_id_start_time_end_time",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "end_time",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "start_time",
                table: "Appointments",
                newName: "date_time");

            migrationBuilder.CreateIndex(
                name: "ix_appointments_doctor_id_date_time",
                table: "Appointments",
                columns: new[] { "doctor_id", "date_time" });
        }
    }
}
