using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LGym.Migrations
{
    /// <inheritdoc />
    public partial class dataseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "MemberId", "Email", "FirstName", "JoinDate", "LastName" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "John", new DateTime(2024, 7, 31, 11, 51, 39, 357, DateTimeKind.Local).AddTicks(9966), "Doe" },
                    { 2, "jane.smith@example.com", "Jane", new DateTime(2024, 7, 31, 11, 51, 39, 358, DateTimeKind.Local).AddTicks(3), "Smith" }
                });

            migrationBuilder.InsertData(
                table: "Trainers",
                columns: new[] { "TrainerId", "FeePer30Minutes", "FirstName", "HireDate", "LastName", "Speciality" },
                values: new object[,]
                {
                    { 1, 20.00m, "Emily", new DateTime(2024, 7, 31, 11, 51, 39, 358, DateTimeKind.Local).AddTicks(686), "Jones", "Yoga" },
                    { 2, 25.00m, "Michael", new DateTime(2024, 7, 31, 11, 51, 39, 358, DateTimeKind.Local).AddTicks(694), "Brown", "Weightlifting" }
                });

            migrationBuilder.InsertData(
                table: "Sessions",
                columns: new[] { "SessionId", "Duration", "MemberId", "SessionDate", "TrainerId" },
                values: new object[,]
                {
                    { 1, 60, 1, new DateTime(2024, 7, 31, 11, 51, 39, 358, DateTimeKind.Local).AddTicks(759), 1 },
                    { 2, 45, 2, new DateTime(2024, 7, 31, 11, 51, 39, 358, DateTimeKind.Local).AddTicks(770), 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "SessionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "SessionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "TrainerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "TrainerId",
                keyValue: 2);
        }
    }
}
