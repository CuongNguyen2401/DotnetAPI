﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyApiNetCore8.Data;

#nullable disable

namespace MyApiNetCore8.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20240603090917_tanngu")]
    partial class tanngu
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("MyApiNetCore8.Model.Category", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("SYSDATE()");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("SYSDATE()");

                    b.Property<string>("description")
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("ENUM('ACTIVE', 'INACTIVE')");

                    b.HasKey("id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("MyApiNetCore8.Model.Coupon", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Code")
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("SYSDATE()");

                    b.Property<double>("Discount")
                        .HasColumnType("double");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("SYSDATE()");

                    b.HasKey("Id");

                    b.ToTable("Coupon");
                });

            modelBuilder.Entity("MyApiNetCore8.Model.Order", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("SYSDATE()");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("SYSDATE()");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("customer_name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("note")
                        .HasColumnType("longtext");

                    b.Property<string>("order_status")
                        .IsRequired()
                        .HasColumnType("ENUM('PENDING', 'CONFIRMED', 'SHIPPING', 'DELIVERED', 'CANCELLED')");

                    b.Property<string>("phone_number")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("total_pay")
                        .HasColumnType("double");

                    b.HasKey("id");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("MyApiNetCore8.Model.OrderItem", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("SYSDATE()");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("SYSDATE()");

                    b.Property<long?>("Productid")
                        .HasColumnType("bigint");

                    b.Property<long>("order_id")
                        .HasColumnType("bigint");

                    b.Property<double>("price")
                        .HasColumnType("double");

                    b.Property<long>("product_id")
                        .HasColumnType("bigint");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("Productid");

                    b.HasIndex("order_id");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("MyApiNetCore8.Model.Permission", b =>
                {
                    b.Property<string>("name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("description")
                        .HasColumnType("longtext");

                    b.HasKey("name");

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("MyApiNetCore8.Model.Product", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("SYSDATE()");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("SYSDATE()");

                    b.Property<long>("category_id")
                        .HasColumnType("bigint");

                    b.Property<string>("description")
                        .HasColumnType("longtext");

                    b.Property<string>("image")
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<double>("price")
                        .HasColumnType("double");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<double>("sale_price")
                        .HasColumnType("double");

                    b.Property<string>("slug")
                        .HasColumnType("longtext");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("ENUM('ACTIVE', 'INACTIVE')");

                    b.HasKey("id");

                    b.HasIndex("category_id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("MyApiNetCore8.Model.Rating", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("SYSDATE()");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("SYSDATE()");

                    b.Property<long>("product_id")
                        .HasColumnType("bigint");

                    b.Property<int>("rate")
                        .HasColumnType("int");

                    b.Property<long>("user_id")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("product_id");

                    b.HasIndex("user_id");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("MyApiNetCore8.Model.Role", b =>
                {
                    b.Property<string>("name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("description")
                        .HasColumnType("longtext");

                    b.HasKey("name");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("MyApiNetCore8.Model.User", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("SYSDATE()");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValueSql("SYSDATE()");

                    b.Property<DateTime>("date_of_birth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("first_name")
                        .HasColumnType("longtext");

                    b.Property<int>("gender")
                        .HasColumnType("int");

                    b.Property<string>("last_name")
                        .HasColumnType("longtext");

                    b.Property<string>("password")
                        .HasColumnType("longtext");

                    b.Property<string>("phone_number")
                        .HasColumnType("longtext");

                    b.Property<string>("username")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("MyApiNetCore8.Model.UserRole", b =>
                {
                    b.Property<long>("user_id")
                        .HasColumnType("bigint");

                    b.Property<string>("roles_name")
                        .HasColumnType("varchar(255)");

                    b.HasKey("user_id", "roles_name");

                    b.HasIndex("roles_name");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("MyApiNetCore8.Model.OrderItem", b =>
                {
                    b.HasOne("MyApiNetCore8.Model.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("Productid");

                    b.HasOne("MyApiNetCore8.Model.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("order_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("MyApiNetCore8.Model.Product", b =>
                {
                    b.HasOne("MyApiNetCore8.Model.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("category_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("MyApiNetCore8.Model.Rating", b =>
                {
                    b.HasOne("MyApiNetCore8.Model.Product", "Product")
                        .WithMany("Ratings")
                        .HasForeignKey("product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyApiNetCore8.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyApiNetCore8.Model.UserRole", b =>
                {
                    b.HasOne("MyApiNetCore8.Model.Role", "Role")
                        .WithMany()
                        .HasForeignKey("roles_name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyApiNetCore8.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyApiNetCore8.Model.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("MyApiNetCore8.Model.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("MyApiNetCore8.Model.Product", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}