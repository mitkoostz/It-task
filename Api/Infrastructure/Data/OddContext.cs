using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data
{
    public class OddContext : DbContext
    {
        public OddContext(DbContextOptions<OddContext> options) : base(options)
        {
       
        }
        
        public DbSet<Event> Events { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly
                .GetExecutingAssembly());

            // var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            //     v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            // foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            // {
            //     foreach (var property in entityType.GetProperties())
            //     {
            //         // if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
            //         // {
            //         //     property.SetValueConverter(dateTimeConverter);
            //         // }

            //         if(property.ClrType == typeof(double) || property.ClrType == typeof(double?))
            //         {
            //              property.SetPrecision(2);
            //              property.SetScale(2);
            //         }
            //     }
            // }
        }
    }
}