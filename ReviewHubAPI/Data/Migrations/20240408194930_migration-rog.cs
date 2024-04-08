using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReviewHubAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class migrationrog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "DateCreated", "PasswordHash" },
                values: new object[] { new DateTime(2024, 4, 8, 19, 49, 29, 610, DateTimeKind.Utc).AddTicks(1036), "$2a$11$v2szFWMd2V0HA4vUcpWmQOt9h23nxJlbYYQPllxi/6eIfvC4UM7fy" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "DateCreated", "PasswordHash" },
                values: new object[] { new DateTime(2024, 4, 8, 9, 29, 56, 852, DateTimeKind.Utc).AddTicks(7092), "$2a$11$vm353BdbjasMaPx7pgWjQuyE6H5KeeGZWMhwnroobTcdPd3ejcxSC" });
        }
    }
}
