using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelListing.Migrations
{
    public partial class AddDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "932849bb-5a85-4b8d-b876-164d62d08a68", "656ccf1f-0ee8-47c1-a482-59264bc1b5d3", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f4a4a4fe-048e-4a8d-a44a-b746fbd866d1", "a3fa4911-c225-4a62-8ae6-964c8f340d93", "Administration", "SYSADM" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "932849bb-5a85-4b8d-b876-164d62d08a68");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4a4a4fe-048e-4a8d-a44a-b746fbd866d1");
        }
    }
}
