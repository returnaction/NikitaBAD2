using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NikitaBAD2.Migrations
{
    /// <inheritdoc />
    public partial class EditPlayerGamesClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BestResult",
                table: "PlayerGames",
                newName: "TotalAnswers");

            migrationBuilder.AddColumn<int>(
                name: "LongestCorrectAsnwerStreak",
                table: "PlayerGames",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LongestCorrectAsnwerStreak",
                table: "PlayerGames");

            migrationBuilder.RenameColumn(
                name: "TotalAnswers",
                table: "PlayerGames",
                newName: "BestResult");
        }
    }
}
