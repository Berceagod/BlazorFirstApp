using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NuclearDataManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserEmailToResearchSubmissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "ResearchSubmissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "ResearchSubmissions");
        }
    }
}
