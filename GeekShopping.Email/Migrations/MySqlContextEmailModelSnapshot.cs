﻿// <auto-generated />
using System;
using GeekShopping.Email.Model.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GeekShopping.Email.Migrations
{
    [DbContext(typeof(MySqlContextEmail))]
    partial class MySqlContextEmailModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GeekShopping.Email.Model.EmailLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("EMAIL");

                    b.Property<string>("Log")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LOG");

                    b.Property<DateTime>("SentDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("SENT_DATE");

                    b.HasKey("Id");

                    b.ToTable("EMAIL_LOGS");
                });
#pragma warning restore 612, 618
        }
    }
}
