﻿// <auto-generated />
using BloGGG.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BloGGG.Migrations.Posts
{
    [DbContext(typeof(PostsContext))]
    [Migration("20230512203813_userID2")]
    partial class userID2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BloGGG.Models.PostModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("OwnerID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PostAuthor")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PostBody")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string[]>("PostTags")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("PostTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
