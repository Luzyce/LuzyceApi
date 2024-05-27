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

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Data.Models.Document", b =>
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

                    b.Property<string>("lockedBy")
                        .HasColumnType("longtext");

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
                            CreatedAt = new DateTime(2024, 5, 27, 14, 54, 51, 421, DateTimeKind.Local).AddTicks(1375),
                            DocNumber = 1,
                            DocumentsDefinitionId = 1,
                            Number = "0001/M/2024",
                            OperatorId = 1,
                            StatusId = 1,
                            UpdatedAt = new DateTime(2024, 5, 27, 14, 54, 51, 421, DateTimeKind.Local).AddTicks(1441),
                            WarehouseId = 1,
                            Year = 2023
                        });
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Data.Models.DocumentItemRelationships", b =>
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

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Data.Models.DocumentPositions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DocumentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("LampshadeId")
                        .HasColumnType("int");

                    b.Property<int>("OperatorId")
                        .HasColumnType("int");

                    b.Property<int>("QuantityGross")
                        .HasColumnType("int");

                    b.Property<int>("QuantityLoss")
                        .HasColumnType("int");

                    b.Property<int>("QuantityNetto")
                        .HasColumnType("int");

                    b.Property<int>("QuantityToImprove")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.HasIndex("LampshadeId");

                    b.HasIndex("OperatorId");

                    b.HasIndex("StatusId");

                    b.ToTable("DocumentPositions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DocumentId = 1,
                            LampshadeId = 1,
                            OperatorId = 1,
                            QuantityGross = 0,
                            QuantityLoss = 0,
                            QuantityNetto = 0,
                            QuantityToImprove = 0,
                            StartTime = new DateTime(2024, 5, 27, 14, 54, 51, 421, DateTimeKind.Local).AddTicks(1500),
                            StatusId = 1
                        });
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Data.Models.DocumentRelations", b =>
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

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Data.Models.DocumentsDefinition", b =>
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
                        });
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Data.Models.Error", b =>
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

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Data.Models.Lampshade", b =>
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

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Data.Models.Operation", b =>
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

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Data.Models.Status", b =>
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
                            Priority = 1
                        });
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Admin")
                        .HasColumnType("tinyint(1)");

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

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Admin = true,
                            CreatedAt = new DateTime(2024, 5, 27, 14, 54, 51, 311, DateTimeKind.Local).AddTicks(4784),
                            Email = "admin@gmail.com",
                            Hash = "admin",
                            LastName = "Admin",
                            Login = "admin",
                            Name = "Admin",
                            Password = "$2a$11$U9KkcjwKkDPpdnZc5SLq/.ioKeo5ep9zp1SN.3CMlB94zVgAKx6bK"
                        });
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Data.Models.Warehouse", b =>
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
                        });
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Data.Models.Document", b =>
                {
                    b.HasOne("LuzyceApi.Db.AppDb.Data.Models.DocumentsDefinition", "DocumentsDefinition")
                        .WithMany()
                        .HasForeignKey("DocumentsDefinitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuzyceApi.Db.AppDb.Data.Models.User", "Operator")
                        .WithMany()
                        .HasForeignKey("OperatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuzyceApi.Db.AppDb.Data.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuzyceApi.Db.AppDb.Data.Models.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocumentsDefinition");

                    b.Navigation("Operator");

                    b.Navigation("Status");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Data.Models.DocumentItemRelationships", b =>
                {
                    b.HasOne("LuzyceApi.Db.AppDb.Data.Models.Document", "ParentDocument")
                        .WithMany()
                        .HasForeignKey("ParentDocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuzyceApi.Db.AppDb.Data.Models.DocumentPositions", "ParentPosition")
                        .WithMany()
                        .HasForeignKey("ParentPositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuzyceApi.Db.AppDb.Data.Models.Document", "SubordinateDocument")
                        .WithMany()
                        .HasForeignKey("SubordinateDocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuzyceApi.Db.AppDb.Data.Models.DocumentPositions", "SubordinatePosition")
                        .WithMany()
                        .HasForeignKey("SubordinatePositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentDocument");

                    b.Navigation("ParentPosition");

                    b.Navigation("SubordinateDocument");

                    b.Navigation("SubordinatePosition");
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Data.Models.DocumentPositions", b =>
                {
                    b.HasOne("LuzyceApi.Db.AppDb.Data.Models.Document", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuzyceApi.Db.AppDb.Data.Models.Lampshade", "Lampshade")
                        .WithMany()
                        .HasForeignKey("LampshadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuzyceApi.Db.AppDb.Data.Models.User", "Operator")
                        .WithMany()
                        .HasForeignKey("OperatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuzyceApi.Db.AppDb.Data.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("Lampshade");

                    b.Navigation("Operator");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Data.Models.DocumentRelations", b =>
                {
                    b.HasOne("LuzyceApi.Db.AppDb.Data.Models.Document", "ParentDocument")
                        .WithMany()
                        .HasForeignKey("ParentDocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuzyceApi.Db.AppDb.Data.Models.Document", "SubordinateDocument")
                        .WithMany()
                        .HasForeignKey("SubordinateDocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentDocument");

                    b.Navigation("SubordinateDocument");
                });

            modelBuilder.Entity("LuzyceApi.Db.AppDb.Data.Models.Operation", b =>
                {
                    b.HasOne("LuzyceApi.Db.AppDb.Data.Models.Document", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuzyceApi.Db.AppDb.Data.Models.Error", "ErrorCode")
                        .WithMany()
                        .HasForeignKey("ErrorCodeId");

                    b.HasOne("LuzyceApi.Db.AppDb.Data.Models.User", "Operator")
                        .WithMany()
                        .HasForeignKey("OperatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("ErrorCode");

                    b.Navigation("Operator");
                });
#pragma warning restore 612, 618
        }
    }
}
