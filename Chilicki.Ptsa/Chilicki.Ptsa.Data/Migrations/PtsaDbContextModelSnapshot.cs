﻿// <auto-generated />
using System;
using Chilicki.Ptsa.Data.Databases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Chilicki.Ptsa.Data.Migrations
{
    [DbContext(typeof(PtsaDbContext))]
    partial class PtsaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Chilicki.Ptsa.Data.Entities.Agency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Agency");
                });

            modelBuilder.Entity("Chilicki.Ptsa.Data.Entities.Connection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.Property<Guid?>("EndStopTimeId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("EndVertexId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("GraphId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsTransfer")
                        .HasColumnType("bit");

                    b.Property<Guid?>("StartStopTimeId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StartVertexId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TripId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EndStopTimeId");

                    b.HasIndex("EndVertexId");

                    b.HasIndex("GraphId");

                    b.HasIndex("StartStopTimeId");

                    b.HasIndex("StartVertexId");

                    b.HasIndex("TripId");

                    b.ToTable("Connection");
                });

            modelBuilder.Entity("Chilicki.Ptsa.Data.Entities.Graph", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.HasKey("Id");

                    b.ToTable("Graph");
                });

            modelBuilder.Entity("Chilicki.Ptsa.Data.Entities.Route", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.Property<Guid?>("AgencyId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GtfsId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AgencyId");

                    b.ToTable("Route");
                });

            modelBuilder.Entity("Chilicki.Ptsa.Data.Entities.Stop", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.Property<string>("GtfsId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Stop");
                });

            modelBuilder.Entity("Chilicki.Ptsa.Data.Entities.StopTime", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.Property<long>("DepartureTime")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("StopId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("StopSequence")
                        .HasColumnType("int");

                    b.Property<Guid?>("TripId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StopId");

                    b.HasIndex("TripId");

                    b.ToTable("StopTime");
                });

            modelBuilder.Entity("Chilicki.Ptsa.Data.Entities.Trip", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.Property<string>("GtfsId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeadSign")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RouteId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("Trip");
                });

            modelBuilder.Entity("Chilicki.Ptsa.Data.Entities.Vertex", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.Property<Guid?>("GraphId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SimilarVertexId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StopId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GraphId");

                    b.HasIndex("SimilarVertexId");

                    b.HasIndex("StopId");

                    b.ToTable("Vertex");
                });

            modelBuilder.Entity("Chilicki.Ptsa.Data.Entities.Connection", b =>
                {
                    b.HasOne("Chilicki.Ptsa.Data.Entities.StopTime", "EndStopTime")
                        .WithMany()
                        .HasForeignKey("EndStopTimeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Chilicki.Ptsa.Data.Entities.Vertex", "EndVertex")
                        .WithMany()
                        .HasForeignKey("EndVertexId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Chilicki.Ptsa.Data.Entities.Graph", "Graph")
                        .WithMany("Connections")
                        .HasForeignKey("GraphId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Chilicki.Ptsa.Data.Entities.StopTime", "StartStopTime")
                        .WithMany()
                        .HasForeignKey("StartStopTimeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Chilicki.Ptsa.Data.Entities.Vertex", "StartVertex")
                        .WithMany("Connections")
                        .HasForeignKey("StartVertexId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Chilicki.Ptsa.Data.Entities.Trip", "Trip")
                        .WithMany()
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Chilicki.Ptsa.Data.Entities.Route", b =>
                {
                    b.HasOne("Chilicki.Ptsa.Data.Entities.Agency", "Agency")
                        .WithMany("Routes")
                        .HasForeignKey("AgencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Chilicki.Ptsa.Data.Entities.StopTime", b =>
                {
                    b.HasOne("Chilicki.Ptsa.Data.Entities.Stop", "Stop")
                        .WithMany("StopTimes")
                        .HasForeignKey("StopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Chilicki.Ptsa.Data.Entities.Trip", "Trip")
                        .WithMany("StopTimes")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Chilicki.Ptsa.Data.Entities.Trip", b =>
                {
                    b.HasOne("Chilicki.Ptsa.Data.Entities.Route", "Route")
                        .WithMany("Trips")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Chilicki.Ptsa.Data.Entities.Vertex", b =>
                {
                    b.HasOne("Chilicki.Ptsa.Data.Entities.Graph", "Graph")
                        .WithMany("Vertices")
                        .HasForeignKey("GraphId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Chilicki.Ptsa.Data.Entities.Vertex", null)
                        .WithMany("SimilarVertices")
                        .HasForeignKey("SimilarVertexId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Chilicki.Ptsa.Data.Entities.Stop", "Stop")
                        .WithMany()
                        .HasForeignKey("StopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
