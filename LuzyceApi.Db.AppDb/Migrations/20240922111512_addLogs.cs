﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LuzyceApi.Db.AppDb.Migrations
{
    /// <inheritdoc />
    public partial class addLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IpAddress = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Symbol = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DocumentsDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentsDefinitions", x => x.Id);
                    table.CheckConstraint("CK_DocumentsDefinitions_Code_MinLength", "LENGTH(`Code`) >= 1");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Errors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShortName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Errors", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Lampshades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lampshades", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LampshadeVariants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShortName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LampshadeVariants", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                    table.CheckConstraint("CK_Warehouses_Code_MinLength", "LENGTH(`Code`) >= 1");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrdersForProduction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OriginalNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersForProduction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdersForProduction_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LampshadeNorms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LampshadeId = table.Column<int>(type: "int", nullable: false),
                    VariantId = table.Column<int>(type: "int", nullable: false),
                    QuantityPerChange = table.Column<int>(type: "int", nullable: true),
                    WeightBrutto = table.Column<double>(type: "double", nullable: true),
                    WeightNetto = table.Column<double>(type: "double", nullable: true),
                    MethodOfPackaging = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuantityPerPack = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LampshadeNorms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LampshadeNorms_LampshadeVariants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "LampshadeVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LampshadeNorms_Lampshades_LampshadeId",
                        column: x => x.LampshadeId,
                        principalTable: "Lampshades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Login = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Hash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrderPositionsForProduction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    OrderNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Symbol = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrderPositionLp = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    QuantityInStock = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Unit = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SerialNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductSymbol = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductDescription = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPositionsForProduction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPositionsForProduction_Lampshades_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Lampshades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderPositionsForProduction_OrdersForProduction_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrdersForProduction",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CustomerLampshades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    LampshadeId = table.Column<int>(type: "int", nullable: false),
                    LampshadeNormId = table.Column<int>(type: "int", nullable: false),
                    LampshadeDekor = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerSymbol = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLampshades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerLampshades_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerLampshades_LampshadeNorms_LampshadeNormId",
                        column: x => x.LampshadeNormId,
                        principalTable: "LampshadeNorms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerLampshades_Lampshades_LampshadeId",
                        column: x => x.LampshadeId,
                        principalTable: "Lampshades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    Operation = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Hash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Data = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logs_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Logs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    ShiftNumber = table.Column<int>(type: "int", nullable: false),
                    ShiftSupervisorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                    table.CheckConstraint("CK_Shifts_ShiftNumber_Range", "`ShiftNumber` BETWEEN 1 AND 3");
                    table.ForeignKey(
                        name: "FK_Shifts_Users_ShiftSupervisorId",
                        column: x => x.ShiftSupervisorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProductionPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: true),
                    Team = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    HeadsOfMetallurgicalTeamsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionPlans", x => x.Id);
                    table.CheckConstraint("CK_ProductionPlans_Team_Range", "`Team` BETWEEN 1 AND 3");
                    table.ForeignKey(
                        name: "FK_ProductionPlans_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductionPlans_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionPlans_Users_HeadsOfMetallurgicalTeamsId",
                        column: x => x.HeadsOfMetallurgicalTeamsId,
                        principalTable: "Users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DocumentItemRelationships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ParentDocumentId = table.Column<int>(type: "int", nullable: false),
                    SubordinateDocumentId = table.Column<int>(type: "int", nullable: false),
                    ParentPositionId = table.Column<int>(type: "int", nullable: false),
                    SubordinatePositionId = table.Column<int>(type: "int", nullable: false),
                    NetQuantityParent = table.Column<int>(type: "int", nullable: false),
                    QuantityLossParent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentItemRelationships", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DocumentPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    QuantityNetto = table.Column<int>(type: "int", nullable: false),
                    QuantityLoss = table.Column<int>(type: "int", nullable: false),
                    QuantityToImprove = table.Column<int>(type: "int", nullable: false),
                    QuantityGross = table.Column<int>(type: "int", nullable: false),
                    OperatorId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LampshadeId = table.Column<int>(type: "int", nullable: false),
                    LampshadeNormId = table.Column<int>(type: "int", nullable: true),
                    LampshadeDekor = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remarks = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrderPositionForProductionId = table.Column<int>(type: "int", nullable: true),
                    po_NumberOfChanges = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    po_QuantityMade = table.Column<int>(type: "int", nullable: true),
                    po_SubiektProductId = table.Column<int>(type: "int", nullable: true),
                    po_Priority = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentPositions_LampshadeNorms_LampshadeNormId",
                        column: x => x.LampshadeNormId,
                        principalTable: "LampshadeNorms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumentPositions_Lampshades_LampshadeId",
                        column: x => x.LampshadeId,
                        principalTable: "Lampshades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentPositions_OrderPositionsForProduction_OrderPositionF~",
                        column: x => x.OrderPositionForProductionId,
                        principalTable: "OrderPositionsForProduction",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumentPositions_Users_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProductionPlanPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductionPlanId = table.Column<int>(type: "int", nullable: false),
                    DocumentPositionId = table.Column<int>(type: "int", nullable: false),
                    NumberOfHours = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionPlanPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionPlanPositions_DocumentPositions_DocumentPositionId",
                        column: x => x.DocumentPositionId,
                        principalTable: "DocumentPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionPlanPositions_ProductionPlans_ProductionPlanId",
                        column: x => x.ProductionPlanId,
                        principalTable: "ProductionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DocNumber = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DocumentsDefinitionId = table.Column<int>(type: "int", nullable: false),
                    OperatorId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ClosedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    LockedById = table.Column<int>(type: "int", nullable: true),
                    po_OrderId = table.Column<int>(type: "int", nullable: true),
                    kw_ProductionPlanPositionsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Clients_LockedById",
                        column: x => x.LockedById,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Documents_DocumentsDefinitions_DocumentsDefinitionId",
                        column: x => x.DocumentsDefinitionId,
                        principalTable: "DocumentsDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_OrdersForProduction_po_OrderId",
                        column: x => x.po_OrderId,
                        principalTable: "OrdersForProduction",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Documents_ProductionPlanPositions_kw_ProductionPlanPositions~",
                        column: x => x.kw_ProductionPlanPositionsId,
                        principalTable: "ProductionPlanPositions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Documents_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_Users_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DocumentRelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ParentDocumentId = table.Column<int>(type: "int", nullable: false),
                    SubordinateDocumentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentRelations_Documents_ParentDocumentId",
                        column: x => x.ParentDocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentRelations_Documents_SubordinateDocumentId",
                        column: x => x.SubordinateDocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    OperatorId = table.Column<int>(type: "int", nullable: false),
                    QuantityNetDelta = table.Column<int>(type: "int", nullable: false),
                    QuantityLossDelta = table.Column<int>(type: "int", nullable: false),
                    QuantityToImproveDelta = table.Column<int>(type: "int", nullable: false),
                    ErrorCodeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operations_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Operations_Errors_ErrorCodeId",
                        column: x => x.ErrorCodeId,
                        principalTable: "Errors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Operations_Users_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Name", "Symbol" },
                values: new object[] { 1, "Test", "TST" });

            migrationBuilder.InsertData(
                table: "DocumentsDefinitions",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "KW", "Kwit" },
                    { 2, "ZP", "Zlecenie Produkcji" }
                });

            migrationBuilder.InsertData(
                table: "LampshadeVariants",
                columns: new[] { "Id", "Name", "ShortName" },
                values: new object[,]
                {
                    { 1, "Opal", "" },
                    { 2, "Opal Mat", "M" },
                    { 3, "Opal Alabaster", "AL" },
                    { 4, "Opal Falbanka", "FA" },
                    { 5, "Jasny", "J" },
                    { 6, "Jasny Kier", "J-KR" },
                    { 7, "Jasny Pladry", "J-PL" },
                    { 8, "Jasny Antiko", "J-AC" },
                    { 9, "Jasny Alabaster", "J-AL" },
                    { 10, "Jasny Mat", "J-M" },
                    { 11, "Jasny Mrożony", "J-MR" }
                });

            migrationBuilder.InsertData(
                table: "Lampshades",
                columns: new[] { "Id", "Code" },
                values: new object[] { 1, "KL4124" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" },
                    { 3, "Hutmustrz" },
                    { 4, "Hutnik" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name", "Priority" },
                values: new object[,]
                {
                    { 1, "Otwarty", 10 },
                    { 2, "Anulowany", 20 },
                    { 3, "Zamknięty", 30 },
                    { 4, "Anulowany", 40 }
                });

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "M", "Magazyn" },
                    { 2, "P", "Produkcja" }
                });

            migrationBuilder.InsertData(
                table: "LampshadeNorms",
                columns: new[] { "Id", "LampshadeId", "MethodOfPackaging", "QuantityPerChange", "QuantityPerPack", "VariantId", "WeightBrutto", "WeightNetto" },
                values: new object[] { 1, 1, "300x300x110", 50, 16, 1, 3.0, 0.45000000000000001 });

            migrationBuilder.InsertData(
                table: "OrdersForProduction",
                columns: new[] { "Id", "CustomerId", "Date", "DeliveryDate", "Number", "OriginalNumber" },
                values: new object[] { 1, 1, new DateTime(2024, 9, 22, 13, 15, 12, 120, DateTimeKind.Local).AddTicks(3759), null, "1", "1" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Hash", "LastName", "Login", "Name", "Password", "RoleId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 22, 13, 15, 12, 11, DateTimeKind.Local).AddTicks(7744), "admin@gmail.com", "admin", "Admin", "admin", "Admin", "$2a$11$kga8cb95dNTGYGrikfcb5OFQ0dNL1SuL/VIyKsw1Eq/xyarU7zbCG", 1 },
                    { 2, new DateTime(2024, 9, 22, 13, 15, 12, 120, DateTimeKind.Local).AddTicks(2862), null, "", "Hutmustrz", "", "Przykładowy", "", 3 },
                    { 3, new DateTime(2024, 9, 22, 13, 15, 12, 120, DateTimeKind.Local).AddTicks(2915), null, "", "Hutnik", "", "Przykładowy", "", 4 }
                });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "Id", "ClosedAt", "CreatedAt", "DocNumber", "DocumentsDefinitionId", "LockedById", "Number", "OperatorId", "po_OrderId", "kw_ProductionPlanPositionsId", "StatusId", "UpdatedAt", "WarehouseId", "Year" },
                values: new object[] { 2, null, new DateTime(2024, 9, 22, 13, 15, 12, 120, DateTimeKind.Local).AddTicks(3564), 1, 2, null, "P/0001/ZP/2024", 1, null, null, 1, new DateTime(2024, 9, 22, 13, 15, 12, 120, DateTimeKind.Local).AddTicks(3566), 2, 2024 });

            migrationBuilder.InsertData(
                table: "OrderPositionsForProduction",
                columns: new[] { "Id", "Description", "OrderId", "OrderNumber", "OrderPositionLp", "ProductDescription", "ProductId", "ProductName", "ProductSymbol", "Quantity", "QuantityInStock", "SerialNumber", "Symbol", "Unit" },
                values: new object[] { 1, "Test", 1, "1", 1, "Test", 1, "KL4124", "KL4124", 1m, 1m, "1", "TEST", "szt" });

            migrationBuilder.InsertData(
                table: "Shifts",
                columns: new[] { "Id", "Date", "ShiftNumber", "ShiftSupervisorId" },
                values: new object[] { 1, new DateOnly(2024, 9, 22), 1, 1 });

            migrationBuilder.InsertData(
                table: "DocumentPositions",
                columns: new[] { "Id", "DocumentId", "EndTime", "LampshadeDekor", "LampshadeId", "LampshadeNormId", "OperatorId", "OrderPositionForProductionId", "po_Priority", "QuantityGross", "QuantityLoss", "QuantityNetto", "QuantityToImprove", "Remarks", "StartTime", "po_SubiektProductId", "po_NumberOfChanges", "po_QuantityMade" },
                values: new object[] { 2, 2, null, "F", 1, 1, 1, 1, null, 0, 0, 0, 0, "Test", new DateTime(2024, 9, 22, 13, 15, 12, 120, DateTimeKind.Local).AddTicks(3926), 2628, 1m, 0 });

            migrationBuilder.InsertData(
                table: "ProductionPlans",
                columns: new[] { "Id", "Date", "HeadsOfMetallurgicalTeamsId", "ShiftId", "StatusId", "Team" },
                values: new object[] { 1, new DateOnly(2024, 9, 22), 1, 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "ProductionPlanPositions",
                columns: new[] { "Id", "DocumentPositionId", "NumberOfHours", "ProductionPlanId", "Quantity" },
                values: new object[] { 1, 2, 8, 1, 50 });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "Id", "ClosedAt", "CreatedAt", "DocNumber", "DocumentsDefinitionId", "LockedById", "Number", "OperatorId", "po_OrderId", "kw_ProductionPlanPositionsId", "StatusId", "UpdatedAt", "WarehouseId", "Year" },
                values: new object[] { 1, null, new DateTime(2024, 9, 22, 13, 15, 12, 120, DateTimeKind.Local).AddTicks(3517), 1, 1, null, "M/0001/KW/2024", 1, null, 1, 1, new DateTime(2024, 9, 22, 13, 15, 12, 120, DateTimeKind.Local).AddTicks(3559), 1, 2023 });

            migrationBuilder.InsertData(
                table: "DocumentPositions",
                columns: new[] { "Id", "DocumentId", "EndTime", "LampshadeDekor", "LampshadeId", "LampshadeNormId", "OperatorId", "OrderPositionForProductionId", "po_Priority", "QuantityGross", "QuantityLoss", "QuantityNetto", "QuantityToImprove", "Remarks", "StartTime", "po_SubiektProductId", "po_NumberOfChanges", "po_QuantityMade" },
                values: new object[] { 1, 1, null, "", 1, null, 1, null, null, 0, 0, 0, 0, "", new DateTime(2024, 9, 22, 13, 15, 12, 120, DateTimeKind.Local).AddTicks(3921), null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLampshades_CustomerId",
                table: "CustomerLampshades",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLampshades_LampshadeId",
                table: "CustomerLampshades",
                column: "LampshadeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLampshades_LampshadeNormId",
                table: "CustomerLampshades",
                column: "LampshadeNormId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentItemRelationships_ParentDocumentId",
                table: "DocumentItemRelationships",
                column: "ParentDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentItemRelationships_ParentPositionId",
                table: "DocumentItemRelationships",
                column: "ParentPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentItemRelationships_SubordinateDocumentId",
                table: "DocumentItemRelationships",
                column: "SubordinateDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentItemRelationships_SubordinatePositionId",
                table: "DocumentItemRelationships",
                column: "SubordinatePositionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentPositions_DocumentId",
                table: "DocumentPositions",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentPositions_LampshadeId",
                table: "DocumentPositions",
                column: "LampshadeId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentPositions_LampshadeNormId",
                table: "DocumentPositions",
                column: "LampshadeNormId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentPositions_OperatorId",
                table: "DocumentPositions",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentPositions_OrderPositionForProductionId",
                table: "DocumentPositions",
                column: "OrderPositionForProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRelations_ParentDocumentId",
                table: "DocumentRelations",
                column: "ParentDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRelations_SubordinateDocumentId",
                table: "DocumentRelations",
                column: "SubordinateDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentsDefinitionId",
                table: "Documents",
                column: "DocumentsDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_kw_ProductionPlanPositionsId",
                table: "Documents",
                column: "kw_ProductionPlanPositionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_LockedById",
                table: "Documents",
                column: "LockedById");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_OperatorId",
                table: "Documents",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_po_OrderId",
                table: "Documents",
                column: "po_OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_StatusId",
                table: "Documents",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_WarehouseId",
                table: "Documents",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_LampshadeNorms_LampshadeId",
                table: "LampshadeNorms",
                column: "LampshadeId");

            migrationBuilder.CreateIndex(
                name: "IX_LampshadeNorms_VariantId",
                table: "LampshadeNorms",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_ClientId",
                table: "Logs",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_UserId",
                table: "Logs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_DocumentId",
                table: "Operations",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_ErrorCodeId",
                table: "Operations",
                column: "ErrorCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_OperatorId",
                table: "Operations",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPositionsForProduction_OrderId",
                table: "OrderPositionsForProduction",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPositionsForProduction_ProductId",
                table: "OrderPositionsForProduction",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersForProduction_CustomerId",
                table: "OrdersForProduction",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionPlanPositions_DocumentPositionId",
                table: "ProductionPlanPositions",
                column: "DocumentPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionPlanPositions_ProductionPlanId",
                table: "ProductionPlanPositions",
                column: "ProductionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionPlans_HeadsOfMetallurgicalTeamsId",
                table: "ProductionPlans",
                column: "HeadsOfMetallurgicalTeamsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionPlans_ShiftId",
                table: "ProductionPlans",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionPlans_StatusId",
                table: "ProductionPlans",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_ShiftSupervisorId",
                table: "Shifts",
                column: "ShiftSupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentItemRelationships_DocumentPositions_ParentPositionId",
                table: "DocumentItemRelationships",
                column: "ParentPositionId",
                principalTable: "DocumentPositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentItemRelationships_DocumentPositions_SubordinatePosit~",
                table: "DocumentItemRelationships",
                column: "SubordinatePositionId",
                principalTable: "DocumentPositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentItemRelationships_Documents_ParentDocumentId",
                table: "DocumentItemRelationships",
                column: "ParentDocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentItemRelationships_Documents_SubordinateDocumentId",
                table: "DocumentItemRelationships",
                column: "SubordinateDocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentPositions_Documents_DocumentId",
                table: "DocumentPositions",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdersForProduction_Customers_CustomerId",
                table: "OrdersForProduction");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentPositions_LampshadeNorms_LampshadeNormId",
                table: "DocumentPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentPositions_Lampshades_LampshadeId",
                table: "DocumentPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderPositionsForProduction_Lampshades_ProductId",
                table: "OrderPositionsForProduction");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductionPlanPositions_DocumentPositions_DocumentPositionId",
                table: "ProductionPlanPositions");

            migrationBuilder.DropTable(
                name: "CustomerLampshades");

            migrationBuilder.DropTable(
                name: "DocumentItemRelationships");

            migrationBuilder.DropTable(
                name: "DocumentRelations");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "Errors");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "LampshadeNorms");

            migrationBuilder.DropTable(
                name: "LampshadeVariants");

            migrationBuilder.DropTable(
                name: "Lampshades");

            migrationBuilder.DropTable(
                name: "DocumentPositions");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "OrderPositionsForProduction");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "DocumentsDefinitions");

            migrationBuilder.DropTable(
                name: "ProductionPlanPositions");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "OrdersForProduction");

            migrationBuilder.DropTable(
                name: "ProductionPlans");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
