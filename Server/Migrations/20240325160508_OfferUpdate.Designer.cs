﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server.Data;

#nullable disable

namespace Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240325160508_OfferUpdate")]
    partial class OfferUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ModelLibrary.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsBanned")
                        .HasColumnType("tinyint(1)");

                    b.Property<long>("UserId1")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId2")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasAlternateKey("UserId1", "UserId2");

                    b.HasIndex("UserId1");

                    b.HasIndex("UserId2");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("ModelLibrary.Message", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("ChatId")
                        .HasColumnType("int");

                    b.Property<long>("FromUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("ImageKey")
                        .HasColumnType("longtext");

                    b.Property<string>("MessageText")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("SendTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("ToUserId")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("TradeId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("TradeId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("ModelLibrary.Offer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("CommentCount")
                        .HasColumnType("int");

                    b.Property<int>("LikeCount")
                        .HasColumnType("int");

                    b.Property<string>("OfferMessage")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("PostTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("UpTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PostTime");

                    b.HasIndex("UpTime");

                    b.HasIndex("UserId");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("ModelLibrary.OfferWeapon", b =>
                {
                    b.Property<Guid>("OfferId")
                        .HasColumnType("char(36)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("WeaponClassId")
                        .HasColumnType("bigint");

                    b.Property<long>("AssetId")
                        .HasColumnType("bigint");

                    b.HasKey("OfferId", "UserId", "WeaponClassId", "AssetId");

                    b.HasIndex("UserId", "WeaponClassId", "AssetId");

                    b.ToTable("OfferWeapons");
                });

            modelBuilder.Entity("ModelLibrary.Trade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<long>("UserFromId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserToId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserFromId");

                    b.HasIndex("UserToId");

                    b.ToTable("Trades");
                });

            modelBuilder.Entity("ModelLibrary.TradeComment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("MessageText")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("PostTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("TradeId")
                        .HasColumnType("char(36)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TradeId");

                    b.HasIndex("UserId");

                    b.ToTable("TradeComments");
                });

            modelBuilder.Entity("ModelLibrary.TradeWeapon", b =>
                {
                    b.Property<Guid>("TradeId")
                        .HasColumnType("char(36)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("WeaponClassId")
                        .HasColumnType("bigint");

                    b.Property<long>("AssetId")
                        .HasColumnType("bigint");

                    b.HasKey("TradeId", "UserId", "WeaponClassId", "AssetId");

                    b.HasIndex("UserId", "WeaponClassId", "AssetId");

                    b.ToTable("TradeWeapons");
                });

            modelBuilder.Entity("ModelLibrary.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("AvatarHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("HasPremium")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("InventoryUpgradeTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateOnly?>("PremiumDate")
                        .HasColumnType("date");

                    b.Property<string>("Status")
                        .HasColumnType("longtext");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("InventoryUpgradeTime");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ModelLibrary.UserWeapon", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("WeaponClassId")
                        .HasColumnType("bigint")
                        .HasAnnotation("Relational:JsonPropertyName", "classid");

                    b.Property<long>("AssetId")
                        .HasColumnType("bigint");

                    b.Property<int>("Count")
                        .HasColumnType("int")
                        .HasAnnotation("Relational:JsonPropertyName", "amount");

                    b.Property<bool>("IsInAuction")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("UserId", "WeaponClassId", "AssetId");

                    b.HasIndex("WeaponClassId");

                    b.ToTable("UsersWeapons");
                });

            modelBuilder.Entity("ModelLibrary.Weapon", b =>
                {
                    b.Property<long>("ClassId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("IconUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsTradable")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<string>("Type")
                        .HasColumnType("longtext");

                    b.HasKey("ClassId");

                    b.ToTable("Weapons");
                });

            modelBuilder.Entity("ModelLibrary.Message", b =>
                {
                    b.HasOne("ModelLibrary.Trade", "Trade")
                        .WithMany()
                        .HasForeignKey("TradeId");

                    b.Navigation("Trade");
                });

            modelBuilder.Entity("ModelLibrary.Offer", b =>
                {
                    b.HasOne("ModelLibrary.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ModelLibrary.OfferWeapon", b =>
                {
                    b.HasOne("ModelLibrary.Offer", "Offer")
                        .WithMany("OfferWeapons")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ModelLibrary.UserWeapon", "UserWeapon")
                        .WithMany()
                        .HasForeignKey("UserId", "WeaponClassId", "AssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Offer");

                    b.Navigation("UserWeapon");
                });

            modelBuilder.Entity("ModelLibrary.Trade", b =>
                {
                    b.HasOne("ModelLibrary.User", "UserFrom")
                        .WithMany()
                        .HasForeignKey("UserFromId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ModelLibrary.User", "UserTo")
                        .WithMany()
                        .HasForeignKey("UserToId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserFrom");

                    b.Navigation("UserTo");
                });

            modelBuilder.Entity("ModelLibrary.TradeComment", b =>
                {
                    b.HasOne("ModelLibrary.Trade", "Trade")
                        .WithMany()
                        .HasForeignKey("TradeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ModelLibrary.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trade");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ModelLibrary.TradeWeapon", b =>
                {
                    b.HasOne("ModelLibrary.Trade", "Trade")
                        .WithMany("TradeWeapons")
                        .HasForeignKey("TradeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ModelLibrary.UserWeapon", "UserWeapon")
                        .WithMany()
                        .HasForeignKey("UserId", "WeaponClassId", "AssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trade");

                    b.Navigation("UserWeapon");
                });

            modelBuilder.Entity("ModelLibrary.UserWeapon", b =>
                {
                    b.HasOne("ModelLibrary.User", "User")
                        .WithMany("Weapons")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ModelLibrary.Weapon", "Weapon")
                        .WithMany()
                        .HasForeignKey("WeaponClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Weapon");
                });

            modelBuilder.Entity("ModelLibrary.Offer", b =>
                {
                    b.Navigation("OfferWeapons");
                });

            modelBuilder.Entity("ModelLibrary.Trade", b =>
                {
                    b.Navigation("TradeWeapons");
                });

            modelBuilder.Entity("ModelLibrary.User", b =>
                {
                    b.Navigation("Weapons");
                });
#pragma warning restore 612, 618
        }
    }
}
