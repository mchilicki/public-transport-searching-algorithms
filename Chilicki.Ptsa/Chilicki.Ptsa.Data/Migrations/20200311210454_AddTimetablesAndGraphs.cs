using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chilicki.Ptsa.Data.Migrations
{
    public partial class AddTimetablesAndGraphs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Graphs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newid()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Graphs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stops",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    GtfsId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    GtfsId = table.Column<string>(nullable: true),
                    AgencyId = table.Column<Guid>(nullable: false),
                    ShortName = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Routes_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vertices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    GraphId = table.Column<Guid>(nullable: false),
                    StopId = table.Column<Guid>(nullable: false),
                    StopName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vertices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vertices_Graphs_GraphId",
                        column: x => x.GraphId,
                        principalTable: "Graphs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vertices_Stops_StopId",
                        column: x => x.StopId,
                        principalTable: "Stops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    GtfsId = table.Column<string>(nullable: true),
                    RouteId = table.Column<Guid>(nullable: false),
                    HeadSign = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SimilarVertices",
                columns: table => new
                {
                    VertexId = table.Column<Guid>(nullable: false),
                    SimilarId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimilarVertices", x => new { x.VertexId, x.SimilarId });
                    table.ForeignKey(
                        name: "FK_SimilarVertices_Vertices_SimilarId",
                        column: x => x.SimilarId,
                        principalTable: "Vertices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SimilarVertices_Vertices_VertexId",
                        column: x => x.VertexId,
                        principalTable: "Vertices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StopTimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    TripId = table.Column<Guid>(nullable: false),
                    StopId = table.Column<Guid>(nullable: false),
                    DepartureTime = table.Column<long>(nullable: false),
                    StopSequence = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StopTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StopTimes_Stops_StopId",
                        column: x => x.StopId,
                        principalTable: "Stops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StopTimes_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Connections",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    GraphId = table.Column<Guid>(nullable: false),
                    TripId = table.Column<Guid>(nullable: true),
                    StartVertexId = table.Column<Guid>(nullable: false),
                    EndVertexId = table.Column<Guid>(nullable: false),
                    StartStopTimeId = table.Column<Guid>(nullable: false),
                    DepartureTime = table.Column<long>(nullable: false),
                    EndStopTimeId = table.Column<Guid>(nullable: false),
                    ArrivalTime = table.Column<long>(nullable: false),
                    IsTransfer = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Connections_StopTimes_EndStopTimeId",
                        column: x => x.EndStopTimeId,
                        principalTable: "StopTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Connections_Vertices_EndVertexId",
                        column: x => x.EndVertexId,
                        principalTable: "Vertices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Connections_Graphs_GraphId",
                        column: x => x.GraphId,
                        principalTable: "Graphs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Connections_StopTimes_StartStopTimeId",
                        column: x => x.StartStopTimeId,
                        principalTable: "StopTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Connections_Vertices_StartVertexId",
                        column: x => x.StartVertexId,
                        principalTable: "Vertices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Connections_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Connections_EndStopTimeId",
                table: "Connections",
                column: "EndStopTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_EndVertexId",
                table: "Connections",
                column: "EndVertexId");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_GraphId",
                table: "Connections",
                column: "GraphId");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_StartStopTimeId",
                table: "Connections",
                column: "StartStopTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_StartVertexId",
                table: "Connections",
                column: "StartVertexId");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_TripId",
                table: "Connections",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_AgencyId",
                table: "Routes",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_SimilarVertices_SimilarId",
                table: "SimilarVertices",
                column: "SimilarId");

            migrationBuilder.CreateIndex(
                name: "IX_StopTimes_StopId",
                table: "StopTimes",
                column: "StopId");

            migrationBuilder.CreateIndex(
                name: "IX_StopTimes_TripId",
                table: "StopTimes",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_RouteId",
                table: "Trips",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Vertices_GraphId",
                table: "Vertices",
                column: "GraphId");

            migrationBuilder.CreateIndex(
                name: "IX_Vertices_StopId",
                table: "Vertices",
                column: "StopId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Connections");

            migrationBuilder.DropTable(
                name: "SimilarVertices");

            migrationBuilder.DropTable(
                name: "StopTimes");

            migrationBuilder.DropTable(
                name: "Vertices");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Graphs");

            migrationBuilder.DropTable(
                name: "Stops");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Agencies");
        }
    }
}
