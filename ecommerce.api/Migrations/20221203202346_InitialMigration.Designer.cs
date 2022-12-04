﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ecommerce.api.Data;

#nullable disable

namespace ecommerce.api.Migrations
{
    [DbContext(typeof(EcommerceDbContext))]
    [Migration("20221203202346_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ecommerce.api.Classes.CartItemModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CartEntityId")
                        .HasColumnType("uuid");

                    b.Property<double>("Discount")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("PricePerUnit")
                        .HasColumnType("double precision");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<string>("Variant")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("isDiscounted")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("CartEntityId");

                    b.ToTable("CartItemModel");
                });

            modelBuilder.Entity("ecommerce.api.Classes.DiscountCodeModel", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<double>("PercentOff")
                        .HasColumnType("double precision");

                    b.HasKey("Code");

                    b.ToTable("DiscountCodeModel");
                });

            modelBuilder.Entity("ecommerce.api.Classes.StockModel", b =>
                {
                    b.Property<string>("Variant")
                        .HasColumnType("text");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<Guid?>("ProductEntityId")
                        .HasColumnType("uuid");

                    b.HasKey("Variant");

                    b.HasIndex("ProductEntityId");

                    b.ToTable("StockModel");
                });

            modelBuilder.Entity("ecommerce.api.Classes.UserModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("County")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LineOne")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LineThree")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LineTwo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserModel");
                });

            modelBuilder.Entity("ecommerce.api.Entities.CartEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AddedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DiscountCodeCode")
                        .HasColumnType("text");

                    b.Property<double?>("DiscountedTotal")
                        .HasColumnType("double precision");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<double>("Total")
                        .HasColumnType("double precision");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DiscountCodeCode");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("ecommerce.api.Entities.OrderEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AddedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CartId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsShipped")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ShippedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ecommerce.api.Entities.ProductEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AddedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Discount")
                        .HasColumnType("double precision");

                    b.Property<List<string>>("Images")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<bool>("IsDiscounted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("PricePerUnit")
                        .HasColumnType("double precision");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ecommerce.api.Classes.CartItemModel", b =>
                {
                    b.HasOne("ecommerce.api.Entities.CartEntity", null)
                        .WithMany("CartItems")
                        .HasForeignKey("CartEntityId");
                });

            modelBuilder.Entity("ecommerce.api.Classes.StockModel", b =>
                {
                    b.HasOne("ecommerce.api.Entities.ProductEntity", null)
                        .WithMany("Stock")
                        .HasForeignKey("ProductEntityId");
                });

            modelBuilder.Entity("ecommerce.api.Entities.CartEntity", b =>
                {
                    b.HasOne("ecommerce.api.Classes.DiscountCodeModel", "DiscountCode")
                        .WithMany()
                        .HasForeignKey("DiscountCodeCode");

                    b.Navigation("DiscountCode");
                });

            modelBuilder.Entity("ecommerce.api.Entities.OrderEntity", b =>
                {
                    b.HasOne("ecommerce.api.Entities.CartEntity", "Cart")
                        .WithMany()
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ecommerce.api.Classes.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ecommerce.api.Entities.CartEntity", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("ecommerce.api.Entities.ProductEntity", b =>
                {
                    b.Navigation("Stock");
                });
#pragma warning restore 612, 618
        }
    }
}
