﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Relationships.Migrations.Books
{
    [DbContext(typeof(BooksContext))]
    [Migration("20220326083243_InitBooks")]
    partial class InitBooks
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("bk")
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"), 1L, 1);

                    b.Property<string>("Publisher")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("BookId");

                    b.ToTable("Books", "bk");
                });

            modelBuilder.Entity("BookPerson", b =>
                {
                    b.Property<int>("AuthorsPersonId")
                        .HasColumnType("int");

                    b.Property<int>("WrittenBooksBookId")
                        .HasColumnType("int");

                    b.HasKey("AuthorsPersonId", "WrittenBooksBookId");

                    b.HasIndex("WrittenBooksBookId");

                    b.ToTable("BookPerson", "bk");
                });

            modelBuilder.Entity("Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonId"), 1L, 1);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonId");

                    b.ToTable("People", "bk");
                });

            modelBuilder.Entity("BookPerson", b =>
                {
                    b.HasOne("Person", null)
                        .WithMany()
                        .HasForeignKey("AuthorsPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Book", null)
                        .WithMany()
                        .HasForeignKey("WrittenBooksBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Person", b =>
                {
                    b.OwnsOne("Address", "BusinessAddress", b1 =>
                        {
                            b1.Property<int>("PersonId")
                                .HasColumnType("int");

                            b1.Property<string>("LineOne")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("LineTwo")
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("AddressLineTwo");

                            b1.HasKey("PersonId");

                            b1.ToTable("People", "bk");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");

                            b1.OwnsOne("Location", "Location", b2 =>
                                {
                                    b2.Property<int>("AddressPersonId")
                                        .HasColumnType("int");

                                    b2.Property<string>("City")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("Country")
                                        .HasColumnType("nvarchar(max)");

                                    b2.HasKey("AddressPersonId");

                                    b2.ToTable("People", "bk");

                                    b2.WithOwner()
                                        .HasForeignKey("AddressPersonId");
                                });

                            b1.Navigation("Location");
                        });

                    b.OwnsOne("Address", "PrivateAddress", b1 =>
                        {
                            b1.Property<int>("PersonId")
                                .HasColumnType("int");

                            b1.Property<string>("LineOne")
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("LineTwo")
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.HasKey("PersonId");

                            b1.ToTable("PrivateAddresses", "bk");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");

                            b1.OwnsOne("Location", "Location", b2 =>
                                {
                                    b2.Property<int>("AddressPersonId")
                                        .HasColumnType("int");

                                    b2.Property<string>("City")
                                        .HasMaxLength(30)
                                        .HasColumnType("nvarchar(30)")
                                        .HasColumnName("City");

                                    b2.Property<string>("Country")
                                        .HasMaxLength(30)
                                        .HasColumnType("nvarchar(30)")
                                        .HasColumnName("Country");

                                    b2.HasKey("AddressPersonId");

                                    b2.ToTable("PrivateAddresses", "bk");

                                    b2.WithOwner()
                                        .HasForeignKey("AddressPersonId");
                                });

                            b1.Navigation("Location");
                        });

                    b.Navigation("BusinessAddress")
                        .IsRequired();

                    b.Navigation("PrivateAddress");
                });
#pragma warning restore 612, 618
        }
    }
}