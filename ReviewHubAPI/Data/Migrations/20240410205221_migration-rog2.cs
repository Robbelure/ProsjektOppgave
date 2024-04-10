using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReviewHubAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class migrationrog2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "DateCreated", "PasswordHash" },
                values: new object[] { new DateTime(2024, 4, 10, 20, 52, 20, 780, DateTimeKind.Utc).AddTicks(8304), "$2a$11$kOeDzQDFSX8oF9EQYXDkpuH9eGyOE/oZ8XeD7OmzDuvgBK3I/FvTu" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "DateCreated", "PasswordHash" },
                values: new object[] { new DateTime(2024, 4, 8, 19, 49, 29, 610, DateTimeKind.Utc).AddTicks(1036), "$2a$11$v2szFWMd2V0HA4vUcpWmQOt9h23nxJlbYYQPllxi/6eIfvC4UM7fy" });
        }
    }
}
