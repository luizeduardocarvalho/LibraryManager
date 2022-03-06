﻿// <auto-generated />
using System;
using LibraryManager.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LibraryManager.Infrastructure.Migrations
{
    [DbContext(typeof(LibraryManagerDbContext))]
    [Migration("20220305161301_CreateEmailIndexOnTeacher")]
    partial class CreateEmailIndexOnTeacher
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("LibraryManager.Domain.Entities.Author", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2022, 3, 5, 16, 13, 1, 42, DateTimeKind.Unspecified).AddTicks(2915), new TimeSpan(0, 0, 0, 0, 0)));

                    b.Property<bool>("IsRemoved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Author");
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.Book", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<int>("CheckoutPeriod")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2022, 3, 5, 16, 13, 1, 54, DateTimeKind.Unspecified).AddTicks(1339), new TimeSpan(0, 0, 0, 0, 0)));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsRemoved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<int>("Reference")
                        .HasColumnType("integer");

                    b.Property<long?>("StudentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("StudentId");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.Student", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2022, 3, 5, 16, 13, 1, 57, DateTimeKind.Unspecified).AddTicks(2119), new TimeSpan(0, 0, 0, 0, 0)));

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("IsRemoved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.Property<long?>("TeacherId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.Teacher", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2022, 3, 5, 16, 13, 1, 59, DateTimeKind.Unspecified).AddTicks(2110), new TimeSpan(0, 0, 0, 0, 0)));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsRemoved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Reference")
                        .HasColumnType("integer");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.Transaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<long?>("BookId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2022, 3, 5, 16, 13, 1, 63, DateTimeKind.Unspecified).AddTicks(7330), new TimeSpan(0, 0, 0, 0, 0)));

                    b.Property<bool>("IsRemoved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<DateTimeOffset>("LendDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("ReturnDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("ReturnedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("StudentId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("StudentId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.Book", b =>
                {
                    b.HasOne("LibraryManager.Domain.Entities.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryManager.Domain.Entities.Student", null)
                        .WithMany("Books")
                        .HasForeignKey("StudentId");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.Student", b =>
                {
                    b.HasOne("LibraryManager.Domain.Entities.Teacher", "Teacher")
                        .WithMany("Students")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("LibraryManager.Domain.Entities.Book", "Book")
                        .WithMany("Transactions")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("LibraryManager.Domain.Entities.Student", "Student")
                        .WithMany("Transactions")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Book");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.Book", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.Student", b =>
                {
                    b.Navigation("Books");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("LibraryManager.Domain.Entities.Teacher", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
