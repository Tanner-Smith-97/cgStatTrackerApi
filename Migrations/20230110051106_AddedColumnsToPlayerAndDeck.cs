using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StatTracker.Migrations
{
    public partial class AddedColumnsToPlayerAndDeck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Decks_Players_PlayerId",
                table: "Decks");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Decks_DeckId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_PlayerId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_DeckId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_PlayerId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Decks_PlayerId",
                table: "Decks");

            migrationBuilder.AddColumn<int>(
                name: "GamesPlayed",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GamesWon",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeckEntityId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlayerEntityId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GamesPlayed",
                table: "Decks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GamesWon",
                table: "Decks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlayerEntityId",
                table: "Decks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Games_DeckEntityId",
                table: "Games",
                column: "DeckEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PlayerEntityId",
                table: "Games",
                column: "PlayerEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Decks_PlayerEntityId",
                table: "Decks",
                column: "PlayerEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Decks_Players_PlayerEntityId",
                table: "Decks",
                column: "PlayerEntityId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Decks_DeckEntityId",
                table: "Games",
                column: "DeckEntityId",
                principalTable: "Decks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_PlayerEntityId",
                table: "Games",
                column: "PlayerEntityId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Decks_Players_PlayerEntityId",
                table: "Decks");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Decks_DeckEntityId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_PlayerEntityId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_DeckEntityId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_PlayerEntityId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Decks_PlayerEntityId",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "GamesPlayed",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GamesWon",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "DeckEntityId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "PlayerEntityId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GamesPlayed",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "GamesWon",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "PlayerEntityId",
                table: "Decks");

            migrationBuilder.CreateIndex(
                name: "IX_Games_DeckId",
                table: "Games",
                column: "DeckId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PlayerId",
                table: "Games",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Decks_PlayerId",
                table: "Decks",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Decks_Players_PlayerId",
                table: "Decks",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Decks_DeckId",
                table: "Games",
                column: "DeckId",
                principalTable: "Decks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_PlayerId",
                table: "Games",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
