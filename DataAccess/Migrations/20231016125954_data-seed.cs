using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class dataseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "Name", "Status" },
                values: new object[,]
                {
                    { -2, "Erkek", true },
                    { -1, "Kadın", true }
                });

            migrationBuilder.InsertData(
                table: "MaritalStatuses",
                columns: new[] { "Id", "Name", "Status" },
                values: new object[,]
                {
                    { -2, "Bekar", true },
                    { -1, "Evli", true }
                });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "Name", "Status" },
                values: new object[,]
                {
                    { -2, "User", true },
                    { -1, "Admin", true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "MaritalStatuses",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "MaritalStatuses",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: -1);
        }
    }
}
