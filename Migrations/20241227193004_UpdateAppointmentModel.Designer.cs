﻿// <auto-generated />
using System;
using AppointmentsAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AppointmentsAPI.Migrations
{
    [DbContext(typeof(AppointmentsDbContext))]
    [Migration("20241227193004_UpdateAppointmentModel")]
    partial class UpdateAppointmentModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AppointmentsAPI.Models.Appointment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("ClientAge")
                        .HasColumnType("TINYINT");

                    b.Property<string>("ClientEmail")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("ClientPhone")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("ClientRUT")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("DATETIME");

                    b.Property<string>("Goals")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("BIT");

                    b.Property<bool>("IsEmailVerify")
                        .HasColumnType("BIT");

                    b.Property<string>("PrevDiagnostic")
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR");

                    b.Property<Guid>("TrackingID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("appointment", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
