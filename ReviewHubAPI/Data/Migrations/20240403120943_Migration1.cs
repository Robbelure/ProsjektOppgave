using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReviewHubAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: -1,
                columns: new[] { "DateCreated", "PasswordHash" },
                values: new object[] { new DateTime(2024, 4, 3, 12, 9, 42, 993, DateTimeKind.Utc).AddTicks(5858), "$2a$11$xcNu80R0G2Qg62bPgrCs3eMYKPdHDwY1UZt8MxYKlGANEP6H7aSpy" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: -1,
                columns: new[] { "DateCreated", "PasswordHash" },
                values: new object[] { new DateTime(2024, 4, 3, 12, 3, 54, 155, DateTimeKind.Utc).AddTicks(5581), "$2a$11$Y89scj6.8.wDZc.fApVjKuTqqwKnP8bCjN0SUv7c3Ygaw/P0qczL2" });
        }
    }
}
