﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ReceiptWriteOff.Infrastructure.EntityFramework.Implementation;

#nullable disable

namespace ReceiptWriteOff.Infrastructure.EntityFramework.Migration.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20250403054105_BookInstanceForeingKeyFix")]
    partial class BookInstanceForeingKeyFix
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ReceiptWriteOff.Domain.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.HasKey("Id");

                    b.ToTable("Books", (string)null);
                });

            modelBuilder.Entity("ReceiptWriteOff.Domain.Entities.BookInstance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.Property<int>("InventoryNumber")
                        .HasMaxLength(100)
                        .HasColumnType("integer");

                    b.Property<int>("ReceipdFactId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("BookInstances");
                });

            modelBuilder.Entity("ReceiptWriteOff.Domain.Entities.ReceiptFact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BookInstanceId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BookInstanceId")
                        .IsUnique();

                    b.ToTable("ReceiptFacts");
                });

            modelBuilder.Entity("ReceiptWriteOff.Domain.Entities.WriteOffFact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BookInstanceId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("WriteOffReasonId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BookInstanceId")
                        .IsUnique();

                    b.HasIndex("WriteOffReasonId");

                    b.ToTable("WriteOffFacts");
                });

            modelBuilder.Entity("ReceiptWriteOff.Domain.Entities.WriteOffReason", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.HasKey("Id");

                    b.ToTable("WriteOffReasons");
                });

            modelBuilder.Entity("ReceiptWriteOff.Domain.Entities.BookInstance", b =>
                {
                    b.HasOne("ReceiptWriteOff.Domain.Entities.Book", "Book")
                        .WithMany("BookInstances")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("ReceiptWriteOff.Domain.Entities.ReceiptFact", b =>
                {
                    b.HasOne("ReceiptWriteOff.Domain.Entities.BookInstance", "BookInstance")
                        .WithOne("ReceiptFact")
                        .HasForeignKey("ReceiptWriteOff.Domain.Entities.ReceiptFact", "BookInstanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookInstance");
                });

            modelBuilder.Entity("ReceiptWriteOff.Domain.Entities.WriteOffFact", b =>
                {
                    b.HasOne("ReceiptWriteOff.Domain.Entities.BookInstance", "BookInstance")
                        .WithOne("WriteOffFact")
                        .HasForeignKey("ReceiptWriteOff.Domain.Entities.WriteOffFact", "BookInstanceId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("ReceiptWriteOff.Domain.Entities.WriteOffReason", "WriteOffReason")
                        .WithMany("WriteOffFacts")
                        .HasForeignKey("WriteOffReasonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BookInstance");

                    b.Navigation("WriteOffReason");
                });

            modelBuilder.Entity("ReceiptWriteOff.Domain.Entities.Book", b =>
                {
                    b.Navigation("BookInstances");
                });

            modelBuilder.Entity("ReceiptWriteOff.Domain.Entities.BookInstance", b =>
                {
                    b.Navigation("ReceiptFact")
                        .IsRequired();

                    b.Navigation("WriteOffFact");
                });

            modelBuilder.Entity("ReceiptWriteOff.Domain.Entities.WriteOffReason", b =>
                {
                    b.Navigation("WriteOffFacts");
                });
#pragma warning restore 612, 618
        }
    }
}
