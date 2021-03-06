﻿// <auto-generated />
using Kooboos.API.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kooboos.API.Migrations
{
    [DbContext(typeof(RecipeContext))]
    [Migration("20201030092234_Add some more dummy data to the schema")]
    partial class Addsomemoredummydatatotheschema
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Kooboos.API.Entities.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Ingredients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Spice up your life",
                            Name = "Black Pepper"
                        },
                        new
                        {
                            Id = 2,
                            Description = "For those who like it hot",
                            Name = "Hot Chili"
                        });
                });

            modelBuilder.Entity("Kooboos.API.Entities.IngredientsList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId")
                        .IsUnique();

                    b.ToTable("IngredientsLists");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RecipeId = 1
                        });
                });

            modelBuilder.Entity("Kooboos.API.Entities.IngredientsListItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<int>("IngredientsListId")
                        .HasColumnType("int");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("IngredientsListId");

                    b.HasIndex("UnitId");

                    b.ToTable("IngredientsListItems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IngredientId = 1,
                            IngredientsListId = 1,
                            Quantity = 1f,
                            UnitId = 1
                        },
                        new
                        {
                            Id = 2,
                            IngredientId = 2,
                            IngredientsListId = 1,
                            Quantity = 5f,
                            UnitId = 2
                        });
                });

            modelBuilder.Entity("Kooboos.API.Entities.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Instruction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Recipes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Instruction = "Just some Instructions",
                            Title = "WTF Recipe"
                        });
                });

            modelBuilder.Entity("Kooboos.API.Entities.Unit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Unit");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Spoon"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Dash"
                        });
                });

            modelBuilder.Entity("Kooboos.API.Entities.IngredientsList", b =>
                {
                    b.HasOne("Kooboos.API.Entities.Recipe", "Recipe")
                        .WithOne("IngredientsList")
                        .HasForeignKey("Kooboos.API.Entities.IngredientsList", "RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Kooboos.API.Entities.IngredientsListItem", b =>
                {
                    b.HasOne("Kooboos.API.Entities.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kooboos.API.Entities.IngredientsList", "IngredientsList")
                        .WithMany("IngredientsListItems")
                        .HasForeignKey("IngredientsListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kooboos.API.Entities.Unit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
