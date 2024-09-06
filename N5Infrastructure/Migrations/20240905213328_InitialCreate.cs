using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N5Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PermissionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeForename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PermissionTypeId = table.Column<int>(type: "int", nullable: false),
                    PermissionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_PermissionTypes_PermissionTypeId",
                        column: x => x.PermissionTypeId,
                        principalTable: "PermissionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.InsertData(
                table: "PermissionTypes",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                                { 1, "Vacation" },
                                { 2, "Sick Leave" },
                                { 3, "Maternity Leave" },
                                { 4, "Paternity Leave" },
                                { 5, "Bereavement Leave" },
                                { 6, "Other" },
                });
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "PermissionDate", "EmployeeForename", "PermissionTypeId","EmployeeSurname" },
                values: new object[,]
                {
                                { 1, new DateTime(2023, 7, 26, 9, 27, 38, 730, DateTimeKind.Local).AddTicks(5718), "ForeName 1 ", 1, "SurName 1 " },
                                { 2, new DateTime(2023, 7, 26, 9, 27, 38, 730, DateTimeKind.Local).AddTicks(5730), "ForeName 2 ", 1, "SurName 2 " },
                                { 3, new DateTime(2023, 7, 26, 9, 27, 38, 730, DateTimeKind.Local).AddTicks(5732), "ForeName 3 ", 2, "SurName 3 " },
                                { 4, new DateTime(2023, 7, 26, 9, 27, 38, 730, DateTimeKind.Local).AddTicks(5733), "ForeName 4 ", 3, "SurName 4 " },
                                { 5, new DateTime(2023, 7, 26, 9, 27, 38, 730, DateTimeKind.Local).AddTicks(5732), "ForeName 5 ", 2, "SurName 5 " },
                                { 6, new DateTime(2023, 7, 26, 9, 27, 38, 730, DateTimeKind.Local).AddTicks(5733), "ForeName 6 ", 3, "SurName 6 " },
                                { 7, new DateTime(2023, 7, 26, 9, 27, 38, 730, DateTimeKind.Local).AddTicks(5732), "ForeName 7 ", 2, "SurName 7 " },
                                { 8, new DateTime(2023, 7, 26, 9, 27, 38, 730, DateTimeKind.Local).AddTicks(5733), "ForeName 8 ", 3, "SurName 8 " }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_PermissionTypeId",
                table: "Permissions",
                column: "PermissionTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "PermissionTypes");
        }
    }
}
