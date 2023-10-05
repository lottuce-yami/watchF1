﻿// <auto-generated />
using System;
using F1Project.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace F1Project.Migrations
{
    [DbContext(typeof(WatchF1Context))]
    partial class WatchF1ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("F1Project.Models.ConstructorStanding", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("name");

                    b.Property<string>("Logo")
                        .HasColumnType("text")
                        .HasColumnName("logo");

                    b.Property<short>("Points")
                        .HasColumnType("smallint")
                        .HasColumnName("points");

                    b.Property<short>("Position")
                        .HasColumnType("smallint")
                        .HasColumnName("position");

                    b.HasKey("Name")
                        .HasName("constructor_standings_pkey");

                    b.ToTable("constructor_standings");
                });

            modelBuilder.Entity("F1Project.Models.DriverStanding", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("name");

                    b.Property<string>("Flag")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("flag");

                    b.Property<short>("Points")
                        .HasColumnType("smallint")
                        .HasColumnName("points");

                    b.Property<short>("Position")
                        .HasColumnType("smallint")
                        .HasColumnName("position");

                    b.HasKey("Name")
                        .HasName("driver_standings_pkey");

                    b.ToTable("driver_standings");
                });

            modelBuilder.Entity("F1Project.Models.Event", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(8)
                        .HasColumnType("character varying(8)")
                        .HasColumnName("id");

                    b.Property<bool?>("Featured")
                        .HasColumnType("boolean")
                        .HasColumnName("featured");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("time");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("events_pkey");

                    b.ToTable("events");
                });

            modelBuilder.Entity("F1Project.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("last_name");

                    b.Property<string>("Photo")
                        .HasColumnType("text")
                        .HasColumnName("photo");

                    b.Property<short>("Subscription")
                        .HasColumnType("smallint")
                        .HasColumnName("subscription");

                    b.Property<string>("Username")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("users_pkey");

                    b.ToTable("users");
                });

            modelBuilder.Entity("F1Project.Models.Video", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(8)
                        .HasColumnType("character varying(8)")
                        .HasColumnName("id");

                    b.Property<string>("Championship")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("championship");

                    b.Property<string>("GrandPrix")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("grand_prix");

                    b.Property<int>("Index")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("index");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Index"));

                    b.Property<string>("Season")
                        .HasMaxLength(4)
                        .HasColumnType("character varying(4)")
                        .HasColumnName("season");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("title");

                    b.Property<string>("Type")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("videos_pkey");

                    b.ToTable("videos");
                });
#pragma warning restore 612, 618
        }
    }
}
