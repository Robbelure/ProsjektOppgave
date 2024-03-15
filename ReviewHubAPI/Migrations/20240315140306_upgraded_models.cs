using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReviewHubAPI.Migrations
{
    /// <inheritdoc />
    public partial class upgraded_models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "Users",
                type: "longblob",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: -1,
                columns: new[] { "DateCreated", "PasswordHash", "ProfilePicture" },
                values: new object[] { new DateTime(2024, 3, 15, 14, 3, 5, 300, DateTimeKind.Utc).AddTicks(2403), "$2a$11$VSJUlcYPs69Y0dI4f7/pluhpsWEdjZbX4is/rHKX9.qNIRZrt.9oG", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: -1,
                columns: new[] { "DateCreated", "PasswordHash" },
                values: new object[] { new DateTime(2024, 3, 14, 19, 55, 0, 493, DateTimeKind.Utc).AddTicks(1540), "$2a$11$3PGAWxZ3KXabYcHQGiBYTOxi7WQ0dDee/5SLOpU5EomdVdBJfPzJW" });
        }
    }
}
