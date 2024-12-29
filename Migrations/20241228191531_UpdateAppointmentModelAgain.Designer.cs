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
    [Migration("20241228191531_UpdateAppointmentModelAgain")]
    partial class UpdateAppointmentModelAgain
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

                    b.Property<DateTime>("AppointmentDateTime")
                        .HasColumnType("DATETIME")
                        .HasColumnName("appointment_date");

                    b.Property<byte>("ClientAge")
                        .HasColumnType("TINYINT")
                        .HasColumnName("client_age");

                    b.Property<string>("ClientEmail")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("client_email");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("client_name");

                    b.Property<string>("ClientPhone")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("client_phone");

                    b.Property<string>("ClientRUT")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("client_rut");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Goals")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("goals");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("BIT")
                        .HasColumnName("is_completed");

                    b.Property<bool>("IsEmailVerified")
                        .HasColumnType("BIT")
                        .HasColumnName("is_email_verified");

                    b.Property<string>("PrevDiagnostic")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("previous_diagnostic");

                    b.Property<Guid>("TrackingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdateDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("appointment", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
