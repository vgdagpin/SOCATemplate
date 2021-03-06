// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SOCATemplate.Infrastructure.Persistence;

#nullable disable

namespace SOCATemplate.DbMigration.SQLite.Migrations
{
    [DbContext(typeof(SOCATemplateDbContext))]
    [Migration("20211130215919_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("SOCATemplate.Application.Common.Entities.tbl_User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("tbl_tbl_User", "dbo");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "Test User"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
