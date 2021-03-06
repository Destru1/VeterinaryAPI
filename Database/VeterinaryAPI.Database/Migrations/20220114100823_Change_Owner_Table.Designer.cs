// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VeterinaryAPI.Database;

namespace VeterinaryAPI.Database.Migrations
{
    [DbContext(typeof(VeterinaryAPIDbcontext))]
    [Migration("20220114100823_Change_Owner_Table")]
    partial class Change_Owner_Table
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VeterinaryAPI.Database.Models.Veterinary.Owner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("VeterinaryAPI.Database.Models.Veterinary.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Breed")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("VeterinaryAPI.Database.Models.Veterinary.Veterinarian", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Veterinarians");
                });

            modelBuilder.Entity("VeterinaryAPI.Database.Models.Veterinary.VeterinarianPetMapping", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("VeterinarianId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PetId");

                    b.HasIndex("VeterinarianId", "PetId")
                        .IsUnique();

                    b.ToTable("VeterinariansPetsMapping");
                });

            modelBuilder.Entity("VeterinaryAPI.Database.Models.Veterinary.Pet", b =>
                {
                    b.HasOne("VeterinaryAPI.Database.Models.Veterinary.Owner", "Owner")
                        .WithMany("Pets")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("VeterinaryAPI.Database.Models.Veterinary.VeterinarianPetMapping", b =>
                {
                    b.HasOne("VeterinaryAPI.Database.Models.Veterinary.Pet", "Pet")
                        .WithMany("Veterinarians")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinaryAPI.Database.Models.Veterinary.Veterinarian", "Veterinarian")
                        .WithMany("Pets")
                        .HasForeignKey("VeterinarianId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pet");

                    b.Navigation("Veterinarian");
                });

            modelBuilder.Entity("VeterinaryAPI.Database.Models.Veterinary.Owner", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("VeterinaryAPI.Database.Models.Veterinary.Pet", b =>
                {
                    b.Navigation("Veterinarians");
                });

            modelBuilder.Entity("VeterinaryAPI.Database.Models.Veterinary.Veterinarian", b =>
                {
                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
