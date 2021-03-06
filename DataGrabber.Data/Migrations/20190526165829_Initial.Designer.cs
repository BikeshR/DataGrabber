﻿// <auto-generated />
using System;
using DataGrabber.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataGrabber.Data.Migrations
{
    [DbContext(typeof(DailyPriceContext))]
    [Migration("20190526165829_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataGrabber.Model.DailyPrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AdjClose");

                    b.Property<decimal>("Close");

                    b.Property<decimal>("DividendAmount");

                    b.Property<decimal>("High");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<decimal>("Low");

                    b.Property<decimal>("Open");

                    b.Property<DateTime>("PriceDate");

                    b.Property<decimal>("SplitCoefficient");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(4);

                    b.Property<int>("Volume");

                    b.HasKey("Id");

                    b.ToTable("DailyPrices");
                });
#pragma warning restore 612, 618
        }
    }
}
