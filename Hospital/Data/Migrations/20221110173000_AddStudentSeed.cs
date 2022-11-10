using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Data.Migrations
{
    public partial class AddStudentSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "Id",
                keyValue: new Guid("6196e017-dfb8-4f2e-a394-309d44d98841"));

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "Id",
                keyValue: new Guid("7e584603-e746-4325-9f2c-0e3c0398f2b7"));

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "Id",
                keyValue: new Guid("b391087f-2ecb-4dd6-aac6-32a6b0d39cf9"));

            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "Id", "Department", "Name" },
                values: new object[] { new Guid("1c02f3a8-d05b-4390-b33c-603aaf114f30"), "ESE", "Rahim" });

            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "Id", "Department", "Name" },
                values: new object[] { new Guid("3d21ee91-15f8-4532-afa8-4a011d001680"), "CSE", "Sadit" });

            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "Id", "Department", "Name" },
                values: new object[] { new Guid("5cf7aa6a-294f-4e7f-8227-0bcf06058495"), "EEE", "Amin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "Id",
                keyValue: new Guid("1c02f3a8-d05b-4390-b33c-603aaf114f30"));

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "Id",
                keyValue: new Guid("3d21ee91-15f8-4532-afa8-4a011d001680"));

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "Id",
                keyValue: new Guid("5cf7aa6a-294f-4e7f-8227-0bcf06058495"));

            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "Id", "Department", "Name" },
                values: new object[] { new Guid("6196e017-dfb8-4f2e-a394-309d44d98841"), "ESE", "Rahim" });

            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "Id", "Department", "Name" },
                values: new object[] { new Guid("7e584603-e746-4325-9f2c-0e3c0398f2b7"), "EEE", "Amin" });

            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "Id", "Department", "Name" },
                values: new object[] { new Guid("b391087f-2ecb-4dd6-aac6-32a6b0d39cf9"), "CSE", "Sadit" });
        }
    }
}
