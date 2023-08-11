﻿// <auto-generated />
using System;
using GeekShopping.CartApi.Model.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GeekShopping.CartApi.Migrations
{
    [DbContext(typeof(MySqlContextCart))]
    partial class MySqlContextCartModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GeekShopping.CartApi.Model.CartDetail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("CART_HEADER_ID")
                        .HasColumnType("bigint");

                    b.Property<long>("CartHeaderId")
                        .HasColumnType("bigint");

                    b.Property<int>("Count")
                        .HasColumnType("int")
                        .HasColumnName("COUNT");

                    b.Property<long?>("PRODUCT_ID")
                        .HasColumnType("bigint");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CART_HEADER_ID");

                    b.HasIndex("PRODUCT_ID");

                    b.ToTable("CART_DETAIL");
                });

            modelBuilder.Entity("GeekShopping.CartApi.Model.CartHeader", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("CouponCode")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("COUPON_CODE");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("USER_ID");

                    b.HasKey("Id");

                    b.ToTable("CART_HEADER");
                });

            modelBuilder.Entity("GeekShopping.CartApi.Model.Product", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnName("ID");

                    b.Property<string>("CategoryName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("CATEGORY_NAME");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("DESCRIPTION");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)")
                        .HasColumnName("IMAGE_URL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("NANE");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,0)");

                    b.HasKey("Id");

                    b.ToTable("PRODUCTS");
                });

            modelBuilder.Entity("GeekShopping.CartApi.Model.CartDetail", b =>
                {
                    b.HasOne("GeekShopping.CartApi.Model.CartHeader", "CartHeader")
                        .WithMany()
                        .HasForeignKey("CART_HEADER_ID");

                    b.HasOne("GeekShopping.CartApi.Model.Product", "Product")
                        .WithMany()
                        .HasForeignKey("PRODUCT_ID");

                    b.Navigation("CartHeader");

                    b.Navigation("Product");
                });
#pragma warning restore 612, 618
        }
    }
}
