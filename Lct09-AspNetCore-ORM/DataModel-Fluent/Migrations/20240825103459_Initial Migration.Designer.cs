﻿// <auto-generated />
using System;
using DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataModel.Migrations
{
    [DbContext(typeof(DataModelContext))]
    [Migration("20240825103459_Initial Migration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("DataModel.Entities.WeatherForecast", b =>
                {
                    b.Property<DateOnly>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Summary")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("TemperatureC")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(10)
                        .HasColumnName("TemperatureCelsius");

                    b.HasKey("Date");

                    b.ToTable("WeatherForecast");
                });
#pragma warning restore 612, 618
        }
    }
}
