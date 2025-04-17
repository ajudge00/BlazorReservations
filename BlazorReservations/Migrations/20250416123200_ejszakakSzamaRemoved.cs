using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorReservations.Migrations
{
    /// <inheritdoc />
    public partial class ejszakakSzamaRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EjszakakSzama",
                table: "Foglalasok");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EjszakakSzama",
                table: "Foglalasok",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
