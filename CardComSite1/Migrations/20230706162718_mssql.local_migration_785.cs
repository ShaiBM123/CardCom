using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardComSite1.Migrations
{
    public partial class mssqllocal_migration_785 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Person",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Person",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Person",
                newName: "gender");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Person",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Person",
                newName: "dateOfBirth");

            migrationBuilder.RenameColumn(
                name: "CitizenId",
                table: "Person",
                newName: "citizenId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "phone",
                table: "Person",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Person",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "gender",
                table: "Person",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Person",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "dateOfBirth",
                table: "Person",
                newName: "DateOfBirth");

            migrationBuilder.RenameColumn(
                name: "citizenId",
                table: "Person",
                newName: "CitizenId");
        }
    }
}
