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
                    PhotoPath = table.Column<string>(nullable: true),
                    AproveStatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AproveStatus", "Email", "FirstName", "LastName", "PasswordHash", "PhotoPath", "UserRole" },
                values: new object[] { 1, false, "darkogele@hotmail.com", "Darko", "Gelevski", "e823a44aca1edda7551208a4c1c1559f61d30a821862b311df3a76ab2b901bce", "bd323643-d2c5-4f80-8d7e-db27716d80b2_dareto.jpg", 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AproveStatus", "Email", "FirstName", "LastName", "PasswordHash", "PhotoPath", "UserRole" },
                values: new object[] { 2, false, "lead@trivago.com", "Lead", "TeachLeadGuy", "645978287991a6f40bcaf5840f5653b89b75b3bd1ea78cf9f39192e2400ac23e", null, 2 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AproveStatus", "Email", "FirstName", "LastName", "PasswordHash", "PhotoPath", "UserRole" },
                values: new object[] { 3, false, "finance@trivago.com", "Finance", "FinanceGuy", "b696d75511dc16f2b52563e3113a498311a79866f4672862197aa9a8c5c0da12", null, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
