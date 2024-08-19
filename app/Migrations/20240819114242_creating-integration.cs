using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class creatingintegration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deck_AspNetUsers_UserId",
                table: "Deck");

            migrationBuilder.DropForeignKey(
                name: "FK_DeckCard_Cards_CardId",
                table: "DeckCard");

            migrationBuilder.DropForeignKey(
                name: "FK_DeckCard_Deck_DeckId",
                table: "DeckCard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeckCard",
                table: "DeckCard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Deck",
                table: "Deck");

            migrationBuilder.RenameTable(
                name: "DeckCard",
                newName: "DeckCards");

            migrationBuilder.RenameTable(
                name: "Deck",
                newName: "Decks");

            migrationBuilder.RenameIndex(
                name: "IX_DeckCard_DeckId",
                table: "DeckCards",
                newName: "IX_DeckCards_DeckId");

            migrationBuilder.RenameIndex(
                name: "IX_Deck_UserId",
                table: "Decks",
                newName: "IX_Decks_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Multiverseid",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeckCards",
                table: "DeckCards",
                columns: new[] { "CardId", "DeckId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Decks",
                table: "Decks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeckCards_Cards_CardId",
                table: "DeckCards",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeckCards_Decks_DeckId",
                table: "DeckCards",
                column: "DeckId",
                principalTable: "Decks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Decks_AspNetUsers_UserId",
                table: "Decks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeckCards_Cards_CardId",
                table: "DeckCards");

            migrationBuilder.DropForeignKey(
                name: "FK_DeckCards_Decks_DeckId",
                table: "DeckCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Decks_AspNetUsers_UserId",
                table: "Decks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Decks",
                table: "Decks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeckCards",
                table: "DeckCards");

            migrationBuilder.RenameTable(
                name: "Decks",
                newName: "Deck");

            migrationBuilder.RenameTable(
                name: "DeckCards",
                newName: "DeckCard");

            migrationBuilder.RenameIndex(
                name: "IX_Decks_UserId",
                table: "Deck",
                newName: "IX_Deck_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DeckCards_DeckId",
                table: "DeckCard",
                newName: "IX_DeckCard_DeckId");

            migrationBuilder.AlterColumn<int>(
                name: "Multiverseid",
                table: "Cards",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deck",
                table: "Deck",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeckCard",
                table: "DeckCard",
                columns: new[] { "CardId", "DeckId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Deck_AspNetUsers_UserId",
                table: "Deck",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeckCard_Cards_CardId",
                table: "DeckCard",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeckCard_Deck_DeckId",
                table: "DeckCard",
                column: "DeckId",
                principalTable: "Deck",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
