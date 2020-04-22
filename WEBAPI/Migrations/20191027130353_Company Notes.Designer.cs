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
    [Migration("20191027130353_Company Notes")]
    partial class CompanyNotes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.Act", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CreatedBy");

                    b.Property<int>("JobId");

                    b.Property<string>("Path");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("Acts");
                });

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.AllowedUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AllowedUserId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AllowedUsers");
                });

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.Break", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DateEnd");

                    b.Property<DateTime?>("DateStart");

                    b.Property<bool>("Enabled");

                    b.Property<int>("JobId");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("Breaks");
                });

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

                    b.Property<string>("Notes");

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

                    b.Property<DateTime?>("FromWork");

                    b.Property<bool>("GpsRequired");

                    b.Property<int>("MaximumWorkMinutes");

                    b.Property<bool>("ObjectRequired");

                    b.Property<int>("SubtractBreakWorkMinutes");

                    b.Property<bool>("SumRequired");

                    b.Property<DateTime?>("ToWork");

                    b.Property<bool>("WorkAtNigth");

                    b.Property<bool>("WorkRequired");

                    b.Property<bool>("WorkTypeRequired");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId")
                        .IsUnique();

                    b.ToTable("CompanySettings");
                });

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.Error", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<string>("Message");

                    b.HasKey("Id");

                    b.ToTable("Errors");
                });

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answer");

                    b.Property<DateTime>("ApproveDateTime");

                    b.Property<int?>("ApproverId");

                    b.Property<string>("CheckPhoto");

                    b.Property<string>("Comment");

                    b.Property<int>("CompanyId");

                    b.Property<string>("ContactEmail");

                    b.Property<string>("ContactPhone");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime?>("DateEnd");

                    b.Property<bool>("DateEndManuallyChanged");

                    b.Property<DateTime>("DateStart");

                    b.Property<bool>("DateStartManuallyChanged");

                    b.Property<bool>("Deleted");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<int?>("DeletedBy");

                    b.Property<int?>("EndLocationId");

                    b.Property<int>("JobStatus");

                    b.Property<string>("ManagerComment");

                    b.Property<string>("Photo");

                    b.Property<string>("Signature");

                    b.Property<string>("SignerPhoto");

                    b.Property<int?>("StartLocationId");

                    b.Property<decimal>("Sum");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("EndLocationId");

                    b.HasIndex("StartLocationId");

                    b.HasIndex("UserId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.HasKey("Id");

                    b.ToTable("Locations");
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

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.TableSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Act");

                    b.Property<bool>("AutomaticRefresh");

                    b.Property<bool>("Break");

                    b.Property<bool>("Comment");

                    b.Property<bool>("Date");

                    b.Property<bool>("Duration");

                    b.Property<bool>("Email");

                    b.Property<bool>("End");

                    b.Property<bool>("GpsEnd");

                    b.Property<bool>("GpsStart");

                    b.Property<bool>("GroupByDate");

                    b.Property<bool>("GroupByWorker");

                    b.Property<bool>("ManagerComment");

                    b.Property<bool>("Phone");

                    b.Property<bool>("Photo");

                    b.Property<bool>("Sign");

                    b.Property<bool>("Start");

                    b.Property<bool>("Summ");

                    b.Property<int>("UserId");

                    b.Property<bool>("Worker");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("TableSettings");
                });

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.TrustedIpAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<string>("IpAddress");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("TrustedIpAddresses");
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

                    b.Property<DateTime>("LastUpdate");

                    b.Property<int>("PasswordFailCount");

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

                    b.Property<bool>("CanAdministratorAddTime");

                    b.Property<bool>("CanAdministratorAllFunctionality");

                    b.Property<bool>("CanAdministratorCalendar");

                    b.Property<bool>("CanAdministratorComment");

                    b.Property<bool>("CanAdministratorOnlyMonitoring");

                    b.Property<bool>("CanAdministratorPhoto");

                    b.Property<bool>("CanAdministratorSeeOnlyOnlineWorkers");

                    b.Property<bool>("CanAdministratorSettings");

                    b.Property<bool>("CanAdministratorSignature");

                    b.Property<bool>("CanAdministratorWorkers");

                    b.Property<bool>("CanAdministratorWriteContactEmail");

                    b.Property<bool>("CanAdministratorWritePhone");

                    b.Property<bool>("CanDeleteWork");

                    b.Property<bool>("CanEditWork");

                    b.Property<bool>("CanSeeWorkingHours");

                    b.Property<int?>("DaysCanSee");

                    b.Property<bool>("ExcludePublicHolidays");

                    b.Property<bool>("ExcludeWeekends");

                    b.Property<DateTime?>("FromTimeCanSee");

                    b.Property<int>("ManagerType");

                    b.Property<bool>("ManagerTypeOne");

                    b.Property<bool>("ManagerTypeTwo");

                    b.Property<bool>("NotificationIfAbsentByEmail");

                    b.Property<DateTime?>("ToTimeCanSee");

                    b.Property<int?>("UserId");

                    b.Property<bool>("WorkingShift");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("UserSettings");
                });

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.Act", b =>
                {
                    b.HasOne("WEBAPI.Model.DatabaseModels.Job", "Job")
                        .WithMany("Acts")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.AllowedUser", b =>
                {
                    b.HasOne("WEBAPI.Model.DatabaseModels.User", "User")
                        .WithMany("AllowedUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.Break", b =>
                {
                    b.HasOne("WEBAPI.Model.DatabaseModels.Job", "Job")
                        .WithMany("Breaks")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.CompanySettings", b =>
                {
                    b.HasOne("WEBAPI.Model.DatabaseModels.Company", "Company")
                        .WithOne("CompanySettings")
                        .HasForeignKey("WEBAPI.Model.DatabaseModels.CompanySettings", "CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.Job", b =>
                {
                    b.HasOne("WEBAPI.Model.DatabaseModels.Location", "EndLocation")
                        .WithMany("EndLocations")
                        .HasForeignKey("EndLocationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WEBAPI.Model.DatabaseModels.Location", "StartLocation")
                        .WithMany("StartLocations")
                        .HasForeignKey("StartLocationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WEBAPI.Model.DatabaseModels.User", "User")
                        .WithMany("Jobs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.RefreshToken", b =>
                {
                    b.HasOne("WEBAPI.Model.DatabaseModels.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.TableSettings", b =>
                {
                    b.HasOne("WEBAPI.Model.DatabaseModels.User", "User")
                        .WithOne("TableSettings")
                        .HasForeignKey("WEBAPI.Model.DatabaseModels.TableSettings", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.TrustedIpAddress", b =>
                {
                    b.HasOne("WEBAPI.Model.DatabaseModels.User", "User")
                        .WithMany("TrustedIpAddresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WEBAPI.Model.DatabaseModels.User", b =>
                {
                    b.HasOne("WEBAPI.Model.DatabaseModels.Company", "Company")
                        .WithMany("Users")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict);

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
