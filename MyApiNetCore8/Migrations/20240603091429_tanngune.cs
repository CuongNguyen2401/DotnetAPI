using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApiNetCore8.Migrations
{
    /// <inheritdoc />
    public partial class tanngune : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    roles_name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    permissions_name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => new { x.permissions_name, x.roles_name });
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission_permissions_name",
                        column: x => x.permissions_name,
                        principalTable: "Permission",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_roles_name",
                        column: x => x.roles_name,
                        principalTable: "Role",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_roles_name",
                table: "RolePermission",
                column: "roles_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermission");
        }
    }
}
