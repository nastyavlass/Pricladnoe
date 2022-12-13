using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyEmployess.Migrations
{
    public partial class AddedRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c7f7a63d-d76d-4b41-97eb-6f2bec7d664a", "1a0bc577-c71d-4f56-a951-7936ee80cfaf", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c97f08d6-5550-4e38-8dd7-d16b842bc02d", "ebd6e470-f061-4460-ada7-2adce8043785", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7f7a63d-d76d-4b41-97eb-6f2bec7d664a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c97f08d6-5550-4e38-8dd7-d16b842bc02d");
        }
    }
}
