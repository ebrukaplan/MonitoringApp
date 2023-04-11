using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonitoringApp.Data.Migrations
{
    public partial class first : Migration
    {
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
                values: new object[] { 1, new DateTime(2023, 4, 11, 15, 15, 52, 776, DateTimeKind.Local).AddTicks(8332), "when the app has down, send an email", "email" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreatedDate", "RoleName" },
                values: new object[] { 1, new DateTime(2023, 4, 11, 15, 15, 52, 776, DateTimeKind.Local).AddTicks(8210), "Standart" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreatedDate", "RoleName" },
                values: new object[] { 2, new DateTime(2023, 4, 11, 15, 15, 52, 776, DateTimeKind.Local).AddTicks(8217), "Admin" });

            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "ApplicationId", "ApplicationName", "ApplicationUrl", "CreatedBy", "CreatedDate", "IntegrationTypeId", "MonitorInterval" },
                values: new object[,]
                {
                    { 1, "Google", "https://www.google.com/", 1, new DateTime(2023, 4, 11, 15, 15, 52, 776, DateTimeKind.Local).AddTicks(8344), 1, 30 },
                    { 2, "İşbank", "https://www.isbank.com.tr/", 1, new DateTime(2023, 4, 11, 15, 15, 52, 776, DateTimeKind.Local).AddTicks(8345), 1, 60 },
                    { 3, "Akbank", "https://www.akbank.com/tr-tr/sayfalar/default.aspx", 1, new DateTime(2023, 4, 11, 15, 15, 52, 776, DateTimeKind.Local).AddTicks(8346), 1, 60 },
                    { 4, "Garanti Bankası", "https://www.garantibbva.com.tr/", 1, new DateTime(2023, 4, 11, 15, 15, 52, 776, DateTimeKind.Local).AddTicks(8347), 1, 120 },
                    { 5, "StackOverFlow", "https://stackoverflow.co/explore-teams/?utm_source=adwords&utm_medium=ppc&utm_campaign=kb_teams_search_nb_dsa_targeted_audiences_emea-dach&_bt=646019453177&_bk=&_bm=&_bn=g&gclid=EAIaIQobChMI46KLhaGh_gIVjplRCh18BQVdEAAYASAAEgLIA_D_BwE", 1, new DateTime(2023, 4, 11, 15, 15, 52, 776, DateTimeKind.Local).AddTicks(8348), 1, 20 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AccountName", "CreatedDate", "HashPassword", "RoleId" },
                values: new object[,]
                {
                    { 1, "Ebru", new DateTime(2023, 4, 11, 15, 15, 52, 776, DateTimeKind.Local).AddTicks(8321), "PuB/hselpQNY0TYpY06RfnYVNjE=", 1 },
                    { 2, "admin", new DateTime(2023, 4, 11, 15, 15, 52, 776, DateTimeKind.Local).AddTicks(8322), "PuB/hselpQNY0TYpY06RfnYVNjE=", 2 }
                });

        }

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
