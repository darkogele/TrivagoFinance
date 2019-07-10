using Microsoft.EntityFrameworkCore.Migrations;

namespace TrivagoFinance.Ui.Migrations
{
    public partial class changesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AproveStatus",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "AprovalStatus",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "E3456BC1F4D270F4A97933758645FDC21E39642B31CA343C1818F7972AC27906");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AprovalStatus",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "AproveStatus",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "645978287991a6f40bcaf5840f5653b89b75b3bd1ea78cf9f39192e2400ac23e");
        }
    }
}
