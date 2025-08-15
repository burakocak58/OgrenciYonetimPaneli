using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student.API.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Student",
                newName: "Mobile");

            migrationBuilder.RenameColumn(
                name: "PostalAdres",
                table: "Address",
                newName: "PostalAdress");

            migrationBuilder.RenameColumn(
                name: "PhysicalAdres",
                table: "Address",
                newName: "PhysicalAdress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mobile",
                table: "Student",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "PostalAdress",
                table: "Address",
                newName: "PostalAdres");

            migrationBuilder.RenameColumn(
                name: "PhysicalAdress",
                table: "Address",
                newName: "PhysicalAdres");
        }
    }
}
