using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chilicki.Ptsa.Data.Migrations
{
    public partial class AddGraphsVerticesAndConnections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Graph",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newid()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Graph", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vertex",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    GraphId = table.Column<Guid>(nullable: false),
                    StopId = table.Column<Guid>(nullable: false),
                    SimilarVertexId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vertex", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vertex_Graph_GraphId",
                        column: x => x.GraphId,
                        principalTable: "Graph",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vertex_Vertex_SimilarVertexId",
                        column: x => x.SimilarVertexId,
                        principalTable: "Vertex",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vertex_Stop_StopId",
                        column: x => x.StopId,
                        principalTable: "Stop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Connection",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    TripId = table.Column<Guid>(nullable: true),
                    StartVertexId = table.Column<Guid>(nullable: false),
                    EndVertexId = table.Column<Guid>(nullable: false),
                    StartStopTimeId = table.Column<Guid>(nullable: false),
                    EndStopTimeId = table.Column<Guid>(nullable: false),
                    IsTransfer = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Connection_StopTime_EndStopTimeId",
                        column: x => x.EndStopTimeId,
                        principalTable: "StopTime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Connection_Vertex_EndVertexId",
                        column: x => x.EndVertexId,
                        principalTable: "Vertex",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Connection_StopTime_StartStopTimeId",
                        column: x => x.StartStopTimeId,
                        principalTable: "StopTime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Connection_Vertex_StartVertexId",
                        column: x => x.StartVertexId,
                        principalTable: "Vertex",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Connection_Trip_TripId",
                        column: x => x.TripId,
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Connection_EndStopTimeId",
                table: "Connection",
                column: "EndStopTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Connection_EndVertexId",
                table: "Connection",
                column: "EndVertexId");

            migrationBuilder.CreateIndex(
                name: "IX_Connection_StartStopTimeId",
                table: "Connection",
                column: "StartStopTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Connection_StartVertexId",
                table: "Connection",
                column: "StartVertexId");

            migrationBuilder.CreateIndex(
                name: "IX_Connection_TripId",
                table: "Connection",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Vertex_GraphId",
                table: "Vertex",
                column: "GraphId");

            migrationBuilder.CreateIndex(
                name: "IX_Vertex_SimilarVertexId",
                table: "Vertex",
                column: "SimilarVertexId");

            migrationBuilder.CreateIndex(
                name: "IX_Vertex_StopId",
                table: "Vertex",
                column: "StopId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Connection");

            migrationBuilder.DropTable(
                name: "Vertex");

            migrationBuilder.DropTable(
                name: "Graph");
        }
    }
}
