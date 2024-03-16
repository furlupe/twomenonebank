﻿// <auto-generated />
using System;
using Bank.Credit.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bank.Credit.Persistance.Migrations
{
    [DbContext(typeof(BankCreditDbContext))]
    [Migration("20240316081129_AddCreditEventType")]
    partial class AddCreditEventType
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Bank.Credit.Domain.Credit.Credit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<int>("BaseAmount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Days")
                        .HasColumnType("integer");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastPaymentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("MissedPaymentPeriods")
                        .HasColumnType("integer");

                    b.Property<DateTime>("NextPaymentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Penalty")
                        .HasColumnType("integer");

                    b.Property<int>("PeriodicPayment")
                        .HasColumnType("integer");

                    b.Property<DateTime>("RateLastApplied")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("TariffId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TariffId");

                    b.HasIndex("UserId");

                    b.ToTable("Credits");
                });

            modelBuilder.Entity("Bank.Credit.Domain.Credit.Events.CreditEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AggregateId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("CreditId")
                        .HasColumnType("uuid");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(34)
                        .HasColumnType("character varying(34)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CreditId");

                    b.ToTable("CreditEvents");

                    b.HasDiscriminator<string>("Discriminator").HasValue("CreditEvent");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Bank.Credit.Domain.Tariff", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Rate")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Tariffs");
                });

            modelBuilder.Entity("Bank.Credit.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Bank.Credit.Domain.Credit.Events.CreditClosedEvent", b =>
                {
                    b.HasBaseType("Bank.Credit.Domain.Credit.Events.CreditEvent");

                    b.HasDiscriminator().HasValue("CreditClosedEvent");
                });

            modelBuilder.Entity("Bank.Credit.Domain.Credit.Events.CreditPaymentDateMovedEvent", b =>
                {
                    b.HasBaseType("Bank.Credit.Domain.Credit.Events.CreditEvent");

                    b.HasDiscriminator().HasValue("CreditPaymentDateMovedEvent");
                });

            modelBuilder.Entity("Bank.Credit.Domain.Credit.Events.CreditPaymentMadeEvent", b =>
                {
                    b.HasBaseType("Bank.Credit.Domain.Credit.Events.CreditEvent");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("CreditPaymentMadeEvent");
                });

            modelBuilder.Entity("Bank.Credit.Domain.Credit.Events.CreditPaymentMissedEvent", b =>
                {
                    b.HasBaseType("Bank.Credit.Domain.Credit.Events.CreditEvent");

                    b.HasDiscriminator().HasValue("CreditPaymentMissedEvent");
                });

            modelBuilder.Entity("Bank.Credit.Domain.Credit.Events.CreditPenaltyAddedEvent", b =>
                {
                    b.HasBaseType("Bank.Credit.Domain.Credit.Events.CreditEvent");

                    b.HasDiscriminator().HasValue("CreditPenaltyAddedEvent");
                });

            modelBuilder.Entity("Bank.Credit.Domain.Credit.Events.CreditPenaltyPaidEvent", b =>
                {
                    b.HasBaseType("Bank.Credit.Domain.Credit.Events.CreditEvent");

                    b.HasDiscriminator().HasValue("CreditPenaltyPaidEvent");
                });

            modelBuilder.Entity("Bank.Credit.Domain.Credit.Events.CreditRateAppliedEvent", b =>
                {
                    b.HasBaseType("Bank.Credit.Domain.Credit.Events.CreditEvent");

                    b.HasDiscriminator().HasValue("CreditRateAppliedEvent");
                });

            modelBuilder.Entity("Bank.Credit.Domain.Credit.Credit", b =>
                {
                    b.HasOne("Bank.Credit.Domain.Tariff", "Tariff")
                        .WithMany()
                        .HasForeignKey("TariffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bank.Credit.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tariff");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Bank.Credit.Domain.Credit.Events.CreditEvent", b =>
                {
                    b.HasOne("Bank.Credit.Domain.Credit.Credit", null)
                        .WithMany("Events")
                        .HasForeignKey("CreditId");
                });

            modelBuilder.Entity("Bank.Credit.Domain.Credit.Credit", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
