using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MonitoringApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ilk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IntegrationTypes",
                columns: table => new
                {
                    IntegrationTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IntegrationTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IntegrationTypeDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationTypes", x => x.IntegrationTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ApplicationUrl = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    MonitorInterval = table.Column<int>(type: "int", nullable: false),
                    IntegrationTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_Applications_IntegrationTypes_IntegrationTypeId",
                        column: x => x.IntegrationTypeId,
                        principalTable: "IntegrationTypes",
                        principalColumn: "IntegrationTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    HashPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "IntegrationTypes",
                columns: new[] { "IntegrationTypeId", "CreatedDate", "IntegrationTypeDescription", "IntegrationTypeName" },
                values: new object[] { 1, new DateTime(2023, 4, 7, 14, 44, 49, 685, DateTimeKind.Local).AddTicks(5954), "when the app has down, send an email", "email" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreatedDate", "RoleName" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 4, 7, 14, 44, 49, 685, DateTimeKind.Local).AddTicks(5854), "Standart" },
                    { 2, new DateTime(2023, 4, 7, 14, 44, 49, 685, DateTimeKind.Local).AddTicks(5862), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "ApplicationId", "ApplicationName", "ApplicationUrl", "CreatedBy", "CreatedDate", "IntegrationTypeId", "MonitorInterval" },
                values: new object[] { 1, "Google", "www.google.com", 1, new DateTime(2023, 4, 7, 14, 44, 49, 685, DateTimeKind.Local).AddTicks(5969), 1, 30 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AccountName", "CreatedDate", "HashPassword", "RoleId" },
                values: new object[,]
                {
                    { 1, "Ebru", new DateTime(2023, 4, 7, 14, 44, 49, 685, DateTimeKind.Local).AddTicks(5944), "123", 1 },
                    { 2, "admin", new DateTime(2023, 4, 7, 14, 44, 49, 685, DateTimeKind.Local).AddTicks(5946), "123", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_IntegrationTypeId",
                table: "Applications",
                column: "IntegrationTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "IntegrationTypes");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
