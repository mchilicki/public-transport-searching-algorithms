﻿using Chilicki.Ptsa.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chilicki.Ptsa.Data.Configurations.EntityConfigurations
{
    public class SimilarVertexConfiguration : IEntityTypeConfiguration<SimilarVertex>
    {
        public void Configure(EntityTypeBuilder<SimilarVertex> builder)
        {
            builder.ToTable("SimilarVertices");
            builder.HasKey(p => new { p.VertexId, p.SimilarId });
            builder.HasOne(p => p.Vertex)
                .WithMany(p => p.SimilarVertices)
                .HasForeignKey(p => p.VertexId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Similar)
                .WithMany()
                .HasForeignKey(p => p.SimilarId)
                .IsRequired();
        }
    }
}
