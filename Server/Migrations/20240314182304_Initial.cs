using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId1 = table.Column<long>(type: "bigint", nullable: false),
                    UserId2 = table.Column<long>(type: "bigint", nullable: false),
                    IsBanned = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                    table.UniqueConstraint("AK_Chats_UserId1_UserId2", x => new { x.UserId1, x.UserId2 });
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "longtext", nullable: false),
                    AvatarHash = table.Column<string>(type: "longtext", nullable: false),
                    InventoryUpgradeTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: true),
                    HasPremium = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PremiumDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Weapons",
                columns: table => new
                {
                    ClassId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IsTradable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IconUrl = table.Column<string>(type: "longtext", nullable: false),
                    Type = table.Column<string>(type: "longtext", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Price = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapons", x => x.ClassId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    PostTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Trades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserFromId = table.Column<long>(type: "bigint", nullable: false),
                    UserToId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trades_Users_UserFromId",
                        column: x => x.UserFromId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trades_Users_UserToId",
                        column: x => x.UserToId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UsersWeapons",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    WeaponClassId = table.Column<long>(type: "bigint", nullable: false),
                    AssetId = table.Column<long>(type: "bigint", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersWeapons", x => new { x.UserId, x.WeaponClassId, x.AssetId });
                    table.ForeignKey(
                        name: "FK_UsersWeapons_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersWeapons_Weapons_WeaponClassId",
                        column: x => x.WeaponClassId,
                        principalTable: "Weapons",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SendTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ChatId = table.Column<int>(type: "int", nullable: false),
                    FromUserId = table.Column<long>(type: "bigint", nullable: false),
                    ToUserId = table.Column<long>(type: "bigint", nullable: false),
                    MessageText = table.Column<string>(type: "longtext", nullable: false),
                    ImageKey = table.Column<string>(type: "longtext", nullable: true),
                    TradeId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Trades_TradeId",
                        column: x => x.TradeId,
                        principalTable: "Trades",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TradeComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    TradeId = table.Column<Guid>(type: "char(36)", nullable: false),
                    PostTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MessageText = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradeComments_Trades_TradeId",
                        column: x => x.TradeId,
                        principalTable: "Trades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TradeComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OfferWeapons",
                columns: table => new
                {
                    OfferId = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    WeaponClassId = table.Column<long>(type: "bigint", nullable: false),
                    AssetId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferWeapons", x => new { x.OfferId, x.UserId, x.WeaponClassId, x.AssetId });
                    table.ForeignKey(
                        name: "FK_OfferWeapons_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferWeapons_UsersWeapons_UserId_WeaponClassId_AssetId",
                        columns: x => new { x.UserId, x.WeaponClassId, x.AssetId },
                        principalTable: "UsersWeapons",
                        principalColumns: new[] { "UserId", "WeaponClassId", "AssetId" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TradeWeapons",
                columns: table => new
                {
                    TradeId = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    WeaponClassId = table.Column<long>(type: "bigint", nullable: false),
                    AssetId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeWeapons", x => new { x.TradeId, x.UserId, x.WeaponClassId, x.AssetId });
                    table.ForeignKey(
                        name: "FK_TradeWeapons_Trades_TradeId",
                        column: x => x.TradeId,
                        principalTable: "Trades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TradeWeapons_UsersWeapons_UserId_WeaponClassId_AssetId",
                        columns: x => new { x.UserId, x.WeaponClassId, x.AssetId },
                        principalTable: "UsersWeapons",
                        principalColumns: new[] { "UserId", "WeaponClassId", "AssetId" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserId1",
                table: "Chats",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserId2",
                table: "Chats",
                column: "UserId2");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatId",
                table: "Messages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_TradeId",
                table: "Messages",
                column: "TradeId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferWeapons_UserId_WeaponClassId_AssetId",
                table: "OfferWeapons",
                columns: new[] { "UserId", "WeaponClassId", "AssetId" });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_PostTime",
                table: "Offers",
                column: "PostTime");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_UpTime",
                table: "Offers",
                column: "UpTime");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_UserId",
                table: "Offers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeComments_TradeId",
                table: "TradeComments",
                column: "TradeId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeComments_UserId",
                table: "TradeComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeWeapons_UserId_WeaponClassId_AssetId",
                table: "TradeWeapons",
                columns: new[] { "UserId", "WeaponClassId", "AssetId" });

            migrationBuilder.CreateIndex(
                name: "IX_Trades_UserFromId",
                table: "Trades",
                column: "UserFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_UserToId",
                table: "Trades",
                column: "UserToId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_InventoryUpgradeTime",
                table: "Users",
                column: "InventoryUpgradeTime");

            migrationBuilder.CreateIndex(
                name: "IX_UsersWeapons_WeaponClassId",
                table: "UsersWeapons",
                column: "WeaponClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "OfferWeapons");

            migrationBuilder.DropTable(
                name: "TradeComments");

            migrationBuilder.DropTable(
                name: "TradeWeapons");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Trades");

            migrationBuilder.DropTable(
                name: "UsersWeapons");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Weapons");
        }
    }
}
