using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorReservations.Migrations
{
    /// <inheritdoc />
    public partial class ejszakakSzamaIsBackNoVege : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vege",
                table: "Foglalasok");

            migrationBuilder.AddColumn<int>(
                name: "EjszakakSzama",
                table: "Foglalasok",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EjszakakSzama",
                table: "Foglalasok");

            migrationBuilder.AddColumn<DateTime>(
                name: "Vege",
                table: "Foglalasok",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
