﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(OddContext))]
    [Migration("20201216224956_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Core.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("EventName")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("EventStartDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("OddsForDraw")
                        .HasColumnType("REAL");

                    b.Property<double>("OddsForFirstTeam")
                        .HasColumnType("REAL");

                    b.Property<double>("OddsForSecondTeam")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
