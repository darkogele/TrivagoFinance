using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrivagoFinance.Ui.Migrations
{
    public partial class inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    UserRole = table.Column<int>(nullable: false),
                    Department = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PhotoPath = table.Column<string>(nullable: true),
                    AprovalStatus = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Department", "Email", "FirstName", "LastName", "PasswordHash", "UserRole" },
                values: new object[,]
                {
                    { 1, 9, "employee@trivago.com", "John", "Do", "2FDC0177057D3A5C6C2C0821E01F4FA8D90F9A3BB7AFD82B0DB526AF98D68DE8", 1 },
                    { 2, 9, "lead@trivago.com", "Lead", "TeachLeadGuy", "E3456BC1F4D270F4A97933758645FDC21E39642B31CA343C1818F7972AC27906", 2 },
                    { 3, 7, "finance@trivago.com", "Finance", "FinanceGuy", "EAB762A03FD979A04CC4706E6536D382BC89D2D1356AFCD054A16B2235ECD471", 3 },
                    { 4, 9, "employee2@trivago.com", "John", "Smith", "e823a44aca1edda7551208a4c1c1559f61d30a821862b311df3a76ab2b901bce", 1 },
                    { 5, 9, "employee3@trivago.com", "Rebeka", "Week", "e823a44aca1edda7551208a4c1c1559f61d30a821862b311df3a76ab2b901bce", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
