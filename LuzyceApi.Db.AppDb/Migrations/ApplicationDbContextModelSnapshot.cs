﻿// <auto-generated />
using System;
using LuzyceApi.Db.AppDb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LuzyceApi.Db.AppDb.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Models.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("ClosedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DocNumber")
                        .HasColumnType("int");

                    b.Property<int>("DocumentsDefinitionId")
                        .HasColumnType("int");

                    b.Property<string>("LockedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("OperatorId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DocumentsDefinitionId");

                    b.HasIndex("OperatorId");

                    b.HasIndex("StatusId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Documents");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2024, 7, 9, 14, 16, 24, 475, DateTimeKind.Local).AddTicks(2513),
                            DocNumber = 1,
                            DocumentsDefinitionId = 1,
                            Number = "M/0001/KW/2024",
                            OperatorId = 1,
                            StatusId = 1,
                            UpdatedAt = new DateTime(2024, 7, 9, 14, 16, 24, 475, DateTimeKind.Local).AddTicks(2566),
                            WarehouseId = 1,
                            Year = 2023
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2024, 7, 9, 14, 16, 24, 475, DateTimeKind.Local).AddTicks(3021),
                            DocNumber = 1,
                            DocumentsDefinitionId = 2,
                            Number = "P/0001/ZP/2024",
                            OperatorId = 1,
                            StatusId = 1,
                            UpdatedAt = new DateTime(2024, 7, 9, 14, 16, 24, 475, DateTimeKind.Local).AddTicks(3027),
                            WarehouseId = 2,
                            Year = 2024
                        });
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Models.DocumentItemRelationships", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("NetQuantityParent")
                        .HasColumnType("int");

                    b.Property<int>("ParentDocumentId")
                        .HasColumnType("int");

                    b.Property<int>("ParentPositionId")
                        .HasColumnType("int");

                    b.Property<int>("QuantityLossParent")
                        .HasColumnType("int");

                    b.Property<int>("SubordinateDocumentId")
                        .HasColumnType("int");

                    b.Property<int>("SubordinatePositionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentDocumentId");

                    b.HasIndex("ParentPositionId");

                    b.HasIndex("SubordinateDocumentId");

                    b.HasIndex("SubordinatePositionId");

                    b.ToTable("DocumentItemRelationships");
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Models.DocumentPositions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DocumentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LampshadeDekor")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("LampshadeId")
                        .HasColumnType("int");

                    b.Property<int?>("LampshadeNormId")
                        .HasColumnType("int");

                    b.Property<int>("OperatorId")
                        .HasColumnType("int");

                    b.Property<int?>("OrderPositionForProductionId")
                        .HasColumnType("int");

                    b.Property<int>("QuantityGross")
                        .HasColumnType("int");

                    b.Property<int>("QuantityLoss")
                        .HasColumnType("int");

                    b.Property<int>("QuantityNetto")
                        .HasColumnType("int");

                    b.Property<int>("QuantityToImprove")
                        .HasColumnType("int");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("po_MethodOfPackaging")
                        .HasColumnType("longtext");

                    b.Property<int?>("po_NumberOfChanges")
                        .HasColumnType("int");

                    b.Property<int?>("po_QuantityMade")
                        .HasColumnType("int");

                    b.Property<int?>("po_QuantityPerPack")
                        .HasColumnType("int");

                    b.Property<int?>("po_SubiektProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.HasIndex("LampshadeId");

                    b.HasIndex("LampshadeNormId");

                    b.HasIndex("OperatorId");

                    b.HasIndex("OrderPositionForProductionId");

                    b.ToTable("DocumentPositions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DocumentId = 1,
                            LampshadeDekor = "",
                            LampshadeId = 1,
                            OperatorId = 1,
                            QuantityGross = 0,
                            QuantityLoss = 0,
                            QuantityNetto = 0,
                            QuantityToImprove = 0,
                            Remarks = "",
                            StartTime = new DateTime(2024, 7, 9, 14, 16, 24, 475, DateTimeKind.Local).AddTicks(2746)
                        },
                        new
                        {
                            Id = 2,
                            DocumentId = 2,
                            LampshadeDekor = "F",
                            LampshadeId = 1,
                            LampshadeNormId = 1,
                            OperatorId = 1,
                            OrderPositionForProductionId = 1,
                            QuantityGross = 0,
                            QuantityLoss = 0,
                            QuantityNetto = 0,
                            QuantityToImprove = 0,
                            Remarks = "Test",
                            StartTime = new DateTime(2024, 7, 9, 14, 16, 24, 475, DateTimeKind.Local).AddTicks(3092),
                            po_MethodOfPackaging = "300x300x110",
                            po_NumberOfChanges = 1,
                            po_QuantityMade = 0,
                            po_QuantityPerPack = 16,
                            po_SubiektProductId = 2628
                        });
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Models.DocumentRelations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ParentDocumentId")
                        .HasColumnType("int");

                    b.Property<int>("SubordinateDocumentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentDocumentId");

                    b.HasIndex("SubordinateDocumentId");

                    b.ToTable("DocumentRelations");
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Models.DocumentsDefinition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("DocumentsDefinitions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "KW",
                            Name = "Kwit"
                        },
                        new
                        {
                            Id = 2,
                            Code = "ZP",
                            Name = "Zlecenie Produkcji"
                        });
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Models.Error", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Errors");
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Models.Lampshade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Lampshades");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "KL4124"
                        });
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Models.LampshadeNorm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("LampshadeId")
                        .HasColumnType("int");

                    b.Property<int?>("QuantityPerChange")
                        .HasColumnType("int");

                    b.Property<int>("VariantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LampshadeId");

                    b.HasIndex("VariantId");

                    b.ToTable("LampshadeNorms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            LampshadeId = 1,
                            QuantityPerChange = 50,
                            VariantId = 1
                        });
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Models.LampshadeVariant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("LampshadeVariants");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Opal",
                            ShortName = ""
                        },
                        new
                        {
                            Id = 2,
                            Name = "Opal Mat",
                            ShortName = "M"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Opal Alabaster",
                            ShortName = "AL"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Opal Falbanka",
                            ShortName = "FA"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Jasny",
                            ShortName = "J"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Jasny Kier",
                            ShortName = "J-KR"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Jasny Pladry",
                            ShortName = "J-PL"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Jasny Antiko",
                            ShortName = "J-AC"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Jasny Alabaster",
                            ShortName = "J-AL"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Jasny Mat",
                            ShortName = "J-M"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Jasny Mrożony",
                            ShortName = "J-MR"
                        });
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Models.Operation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DocumentId")
                        .HasColumnType("int");

                    b.Property<int?>("ErrorCodeId")
                        .HasColumnType("int");

                    b.Property<int>("OperatorId")
                        .HasColumnType("int");

                    b.Property<int>("QuantityLossDelta")
                        .HasColumnType("int");

                    b.Property<int>("QuantityNetDelta")
                        .HasColumnType("int");

                    b.Property<int>("QuantityToImproveDelta")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.HasIndex("ErrorCodeId");

                    b.HasIndex("OperatorId");

                    b.ToTable("Operations");
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Models.OrderForProduction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CustomerSymbol")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("OrdersForProduction");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CustomerId = 1,
                            CustomerName = "Testowanie",
                            CustomerSymbol = "TEST",
                            Date = new DateTime(2024, 7, 9, 14, 16, 24, 475, DateTimeKind.Local).AddTicks(2772),
                            Number = "1"
                        });
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Models.OrderPositionForProduction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("OrderPositionLp")
                        .HasColumnType("int");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ProductSymbol")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("QuantityInStock")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Unit")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderPositionsForProduction");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Test",
                            OrderId = 1,
                            OrderNumber = "1",
                            OrderPositionLp = 1,
                            ProductDescription = "Test",
                            ProductId = 1,
                            ProductName = "KL4124",
                            ProductSymbol = "KL4124",
                            Quantity = 1m,
                            QuantityInStock = 1m,
                            SerialNumber = "1",
                            Symbol = "TEST",
                            Unit = "szt"
                        });
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Otwarty",
                            Priority = 10
                        },
                        new
                        {
                            Id = 2,
                            Name = "Anulowany",
                            Priority = 20
                        },
                        new
                        {
                            Id = 3,
                            Name = "Zamknięty",
                            Priority = 30
                        },
                        new
                        {
                            Id = 4,
                            Name = "Anulowany",
                            Priority = 40
                        });
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2024, 7, 9, 14, 16, 24, 363, DateTimeKind.Local).AddTicks(2288),
                            Email = "admin@gmail.com",
                            Hash = "admin",
                            LastName = "Admin",
                            Login = "admin",
                            Name = "Admin",
                            Password = "$2a$11$tU02AEPCub7rDqw8dHJMzu71bi6YTyrnzLIHRQZDC0uTs/BOczAkS",
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Models.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Warehouses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "M",
                            Name = "Magazyn"
                        },
                        new
                        {
                            Id = 2,
                            Code = "P",
                            Name = "Produkcja"
                        });
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Models.Document", b =>
                {
                    b.HasOne("LuzyceApi.Db.AppDb.Models.DocumentsDefinition", "DocumentsDefinition")
                        .WithMany()
                        .HasForeignKey("DocumentsDefinitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuzyceApi.Db.AppDb.Models.User", "Operator")
                        .WithMany()
                        .HasForeignKey("OperatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuzyceApi.Db.AppDb.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuzyceApi.Db.AppDb.Models.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocumentsDefinition");

                    b.Navigation("Operator");

                    b.Navigation("Status");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Models.DocumentItemRelationships", b =>
                {
                    b.HasOne("LuzyceApi.Db.AppDb.Models.Document", "ParentDocument")
                        .WithMany()
                        .HasForeignKey("ParentDocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuzyceApi.Db.AppDb.Models.DocumentPositions", "ParentPosition")
                        .WithMany()
                        .HasForeignKey("ParentPositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuzyceApi.Db.AppDb.Models.Document", "SubordinateDocument")
                        .WithMany()
                        .HasForeignKey("SubordinateDocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuzyceApi.Db.AppDb.Models.DocumentPositions", "SubordinatePosition")
                        .WithMany()
                        .HasForeignKey("SubordinatePositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentDocument");

                    b.Navigation("ParentPosition");

                    b.Navigation("SubordinateDocument");

                    b.Navigation("SubordinatePosition");
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Models.DocumentPositions", b =>
                {
                    b.HasOne("LuzyceApi.Db.AppDb.Models.Document", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuzyceApi.Db.AppDb.Models.Lampshade", "Lampshade")
                        .WithMany()
                        .HasForeignKey("LampshadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuzyceApi.Db.AppDb.Models.LampshadeNorm", "LampshadeNorm")
                        .WithMany()
                        .HasForeignKey("LampshadeNormId");

                    b.HasOne("LuzyceApi.Db.AppDb.Models.User", "Operator")
                        .WithMany()
                        .HasForeignKey("OperatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuzyceApi.Db.AppDb.Models.OrderPositionForProduction", "OrderPositionForProduction")
                        .WithMany()
                        .HasForeignKey("OrderPositionForProductionId");

                    b.Navigation("Document");

                    b.Navigation("Lampshade");

                    b.Navigation("LampshadeNorm");

                    b.Navigation("Operator");

                    b.Navigation("OrderPositionForProduction");
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Models.DocumentRelations", b =>
                {
                    b.HasOne("LuzyceApi.Db.AppDb.Models.Document", "ParentDocument")
                        .WithMany()
                        .HasForeignKey("ParentDocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuzyceApi.Db.AppDb.Models.Document", "SubordinateDocument")
                        .WithMany()
                        .HasForeignKey("SubordinateDocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentDocument");

                    b.Navigation("SubordinateDocument");
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Models.LampshadeNorm", b =>
                {
                    b.HasOne("LuzyceApi.Db.AppDb.Models.Lampshade", "Lampshade")
                        .WithMany()
                        .HasForeignKey("LampshadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuzyceApi.Db.AppDb.Models.LampshadeVariant", "Variant")
                        .WithMany()
                        .HasForeignKey("VariantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lampshade");

                    b.Navigation("Variant");
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Models.Operation", b =>
                {
                    b.HasOne("LuzyceApi.Db.AppDb.Models.Document", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuzyceApi.Db.AppDb.Models.Error", "ErrorCode")
                        .WithMany()
                        .HasForeignKey("ErrorCodeId");

                    b.HasOne("LuzyceApi.Db.AppDb.Models.User", "Operator")
                        .WithMany()
                        .HasForeignKey("OperatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("ErrorCode");

                    b.Navigation("Operator");
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Models.OrderPositionForProduction", b =>
                {
                    b.HasOne("LuzyceApi.Db.AppDb.Models.OrderForProduction", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.HasOne("LuzyceApi.Db.AppDb.Models.Lampshade", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Models.User", b =>
                {
                    b.HasOne("LuzyceApi.Db.AppDb.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
