﻿// <auto-generated />
using GeekShopping.CouponApi.Model.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GeekShopping.CouponApi.Migrations
{
    [DbContext(typeof(MySqlContextCoupon))]
    [Migration("20230816141632_SeddCouponDataBase")]
    partial class SeddCouponDataBase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GeekShopping.CouponApi.Model.Coupon", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("CouponCode")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("COUPON_CODE");

                    b.Property<decimal>("DiscountAmount")
                        .HasColumnType("decimal(18,0)")
                        .HasColumnName("DISCOUNT_AMOUNT");

                    b.HasKey("Id");

                    b.ToTable("COUPONS");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CouponCode = "KBULOSO_23_20",
                            DiscountAmount = 20m
                        },
                        new
                        {
                            Id = 2L,
                            CouponCode = "KBULOSO_23_30",
                            DiscountAmount = 30m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
