﻿// <auto-generated />
using System;
using CMS_1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CMS_1.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CMS_1.Models.Barcode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("BarCode")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IdCampaign")
                        .HasColumnType("int");

                    b.Property<bool>("IsScanned")
                        .HasColumnType("bit");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("QRcode")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("ScannedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IdCampaign");

                    b.ToTable("Barcode");
                });

            modelBuilder.Entity("CMS_1.Models.Campaignn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("AutoUpdate")
                        .HasColumnType("bit");

                    b.Property<int>("CodeLength")
                        .HasColumnType("int");

                    b.Property<int>("CodeUsageLimit")
                        .HasColumnType("int");

                    b.Property<int>("CountCode")
                        .HasColumnType("int");

                    b.Property<string>("Decription")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("IdCharset")
                        .HasColumnType("int");

                    b.Property<int?>("IdProgramSize")
                        .HasColumnType("int");

                    b.Property<bool>("JoinOnlyOne")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Postfix")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Prefix")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("Unlimited")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("IdCharset");

                    b.HasIndex("IdProgramSize");

                    b.ToTable("Campaign");
                });

            modelBuilder.Entity("CMS_1.Models.Charset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Charset");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Numbers"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Character"
                        });
                });

            modelBuilder.Entity("CMS_1.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DoB")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsBlock")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeOfBusiness")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("CMS_1.Models.Gift", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("GiftCode")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int?>("IdCampaign")
                        .HasColumnType("int");

                    b.Property<int?>("IdGiftCategory")
                        .HasColumnType("int");

                    b.Property<bool>("Usage")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("IdCampaign");

                    b.HasIndex("IdGiftCategory");

                    b.ToTable("Gift");
                });

            modelBuilder.Entity("CMS_1.Models.GiftCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("Decription")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("GiftCategory");
                });

            modelBuilder.Entity("CMS_1.Models.ProgramSize", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ProgramSize");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Bulk codes"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Standalone code"
                        });
                });

            modelBuilder.Entity("CMS_1.Models.RepeatSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("RepeatSchedule");
                });

            modelBuilder.Entity("CMS_1.Models.RuleOfGift", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<bool>("AllDay")
                        .HasColumnType("bit");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("GiftCount")
                        .HasColumnType("int");

                    b.Property<int?>("IdGiftCategory")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<int>("Probability")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IdGiftCategory");

                    b.ToTable("RuleOfGift");
                });

            modelBuilder.Entity("CMS_1.Models.TimeFrame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("EndDay")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<int?>("IdCampaign")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDay")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("IdCampaign");

                    b.ToTable("TimeFrame");
                });

            modelBuilder.Entity("CMS_1.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "123@gmail.com",
                            Password = "123Aaa"
                        },
                        new
                        {
                            Id = 2,
                            Email = "abc@gmail.com",
                            Password = "123Qwe"
                        });
                });

            modelBuilder.Entity("CMS_1.Models.ValueSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("IdRepeat")
                        .HasColumnType("int");

                    b.Property<int?>("IdRule")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("IdRepeat");

                    b.HasIndex("IdRule");

                    b.ToTable("ValueSchedule");
                });

            modelBuilder.Entity("CMS_1.Models.Winner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("IdCustomer")
                        .HasColumnType("int");

                    b.Property<int?>("IdGift")
                        .HasColumnType("int");

                    b.Property<bool>("SendGiftStatus")
                        .HasColumnType("bit");

                    b.Property<DateTime>("WinDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IdCustomer");

                    b.HasIndex("IdGift");

                    b.ToTable("Winner");
                });

            modelBuilder.Entity("CMS_1.Models.Barcode", b =>
                {
                    b.HasOne("CMS_1.Models.Campaignn", "Campaign")
                        .WithMany("Barcodes")
                        .HasForeignKey("IdCampaign");

                    b.Navigation("Campaign");
                });

            modelBuilder.Entity("CMS_1.Models.Campaignn", b =>
                {
                    b.HasOne("CMS_1.Models.Charset", "Charset")
                        .WithMany("Campaigns")
                        .HasForeignKey("IdCharset");

                    b.HasOne("CMS_1.Models.ProgramSize", "ProgramSize")
                        .WithMany("Campaigns")
                        .HasForeignKey("IdProgramSize");

                    b.Navigation("Charset");

                    b.Navigation("ProgramSize");
                });

            modelBuilder.Entity("CMS_1.Models.Gift", b =>
                {
                    b.HasOne("CMS_1.Models.Campaignn", "Campaign")
                        .WithMany("Gifts")
                        .HasForeignKey("IdCampaign");

                    b.HasOne("CMS_1.Models.GiftCategory", "GiftCategory")
                        .WithMany("Gifts")
                        .HasForeignKey("IdGiftCategory");

                    b.Navigation("Campaign");

                    b.Navigation("GiftCategory");
                });

            modelBuilder.Entity("CMS_1.Models.RuleOfGift", b =>
                {
                    b.HasOne("CMS_1.Models.GiftCategory", "GiftCategory")
                        .WithMany("RuleOfGifts")
                        .HasForeignKey("IdGiftCategory");

                    b.Navigation("GiftCategory");
                });

            modelBuilder.Entity("CMS_1.Models.TimeFrame", b =>
                {
                    b.HasOne("CMS_1.Models.Campaignn", "Campaign")
                        .WithMany("TimeFrames")
                        .HasForeignKey("IdCampaign");

                    b.Navigation("Campaign");
                });

            modelBuilder.Entity("CMS_1.Models.ValueSchedule", b =>
                {
                    b.HasOne("CMS_1.Models.RepeatSchedule", "RepeatSchedule")
                        .WithMany("ValueSchedules")
                        .HasForeignKey("IdRepeat");

                    b.HasOne("CMS_1.Models.RuleOfGift", "RuleOfGift")
                        .WithMany("ValueSchedules")
                        .HasForeignKey("IdRule");

                    b.Navigation("RepeatSchedule");

                    b.Navigation("RuleOfGift");
                });

            modelBuilder.Entity("CMS_1.Models.Winner", b =>
                {
                    b.HasOne("CMS_1.Models.Customer", "Customer")
                        .WithMany("Winners")
                        .HasForeignKey("IdCustomer");

                    b.HasOne("CMS_1.Models.Gift", "Gift")
                        .WithMany("Winners")
                        .HasForeignKey("IdGift");

                    b.Navigation("Customer");

                    b.Navigation("Gift");
                });

            modelBuilder.Entity("CMS_1.Models.Campaignn", b =>
                {
                    b.Navigation("Barcodes");

                    b.Navigation("Gifts");

                    b.Navigation("TimeFrames");
                });

            modelBuilder.Entity("CMS_1.Models.Charset", b =>
                {
                    b.Navigation("Campaigns");
                });

            modelBuilder.Entity("CMS_1.Models.Customer", b =>
                {
                    b.Navigation("Winners");
                });

            modelBuilder.Entity("CMS_1.Models.Gift", b =>
                {
                    b.Navigation("Winners");
                });

            modelBuilder.Entity("CMS_1.Models.GiftCategory", b =>
                {
                    b.Navigation("Gifts");

                    b.Navigation("RuleOfGifts");
                });

            modelBuilder.Entity("CMS_1.Models.ProgramSize", b =>
                {
                    b.Navigation("Campaigns");
                });

            modelBuilder.Entity("CMS_1.Models.RepeatSchedule", b =>
                {
                    b.Navigation("ValueSchedules");
                });

            modelBuilder.Entity("CMS_1.Models.RuleOfGift", b =>
                {
                    b.Navigation("ValueSchedules");
                });
#pragma warning restore 612, 618
        }
    }
}
