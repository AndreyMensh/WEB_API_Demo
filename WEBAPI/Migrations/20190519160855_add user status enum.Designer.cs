﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WEBAPI.Model;

namespace WEBAPI.Migrations
{
    [DbContext(typeof(ApplicationDatabaseContext))]
    [Migration("20190519160855_add user status enum")]
    partial class adduserstatusenum
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BillEmail");

                    b.Property<string>("ContactEmail");

                    b.Property<string>("ContactName");

                    b.Property<string>("ContactPhone");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("GeneralUserId");

                    b.Property<bool>("IsBlocked");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.CompanySettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ActRequired");

                    b.Property<bool>("AutomaticBreak");

                    b.Property<int>("BreakTimeMinutes");

                    b.Property<int>("CompanyId");

                    b.Property<bool>("ContactNameRequired");

                    b.Property<bool>("EditCommentRequired");

                    b.Property<bool>("GpsRequired");

                    b.Property<int>("MaximumWorkMinutes");

                    b.Property<bool>("ObjectRequired");

                    b.Property<int>("SubtractBreakWorkMinutes");

                    b.Property<bool>("WorkAtNigth");

                    b.Property<bool>("WorkRequired");

                    b.Property<bool>("WorkTypeRequired");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId")
                        .IsUnique();

                    b.ToTable("CompanySettings");
                });

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("UserId");

                    b.Property<DateTime>("ValidFrom");

                    b.Property<DateTime>("ValidTo");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("NormalName");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<DateTime>("CodeExpire");

                    b.Property<int?>("CompanyId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("ExportCode");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<int>("RoleId");

                    b.Property<int>("UserStatus");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.UserSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AfterWorkSubtractBreakMinutes");

                    b.Property<bool>("AutomaticSchedule");

                    b.Property<int>("BreakDurationMinutes");

                    b.Property<bool>("CanAdministratorAct");

                    b.Property<bool>("CanAdministratorAllFunctionality");

                    b.Property<bool>("CanAdministratorCalendar");

                    b.Property<bool>("CanAdministratorComment");

                    b.Property<bool>("CanAdministratorOnlyMonitoring");

                    b.Property<bool>("CanAdministratorPhoto");

                    b.Property<bool>("CanAdministratorSeeAllWorkers");

                    b.Property<bool>("CanAdministratorSignature");

                    b.Property<bool>("CanSeeWorkingHours");

                    b.Property<bool>("ExcludePublicHolidays");

                    b.Property<bool>("ExcludeWeekends");

                    b.Property<bool>("NotificationIfAbsentByEmail");

                    b.Property<int?>("UserId");

                    b.Property<bool>("WorkingShift");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("UserSettings");
                });

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.CompanySettings", b =>
                {
                    b.HasOne("WEBAPI.Model.DatabaseModels.Company", "Company")
                        .WithOne("CompanySettings")
                        .HasForeignKey("WEBAPI.Model.DatabaseModels.CompanySettings", "CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.RefreshToken", b =>
                {
                    b.HasOne("WEBAPI.Model.DatabaseModels.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.User", b =>
                {
                    b.HasOne("WEBAPI.Model.DatabaseModels.Company", "Company")
                        .WithMany("Users")
                        .HasForeignKey("CompanyId");

                    b.HasOne("WEBAPI.Model.DatabaseModels.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.UserSettings", b =>
                {
                    b.HasOne("WEBAPI.Model.DatabaseModels.User", "User")
                        .WithOne("UserSettings")
                        .HasForeignKey("WEBAPI.Model.DatabaseModels.UserSettings", "UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
