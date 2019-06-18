using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using NameThatTuneBot.Entities;

namespace NameThatTuneBot.Database
{
    public sealed class NameThatTuneDatabase:DbContext
    {
        private static string Options { get; } =
            @"Server=(localdb)\mssqllocaldb;Database=BotDatabase;Trusted_Connection=True;";

        
        public NameThatTuneDatabase() 
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MusicTrack>().HasKey(u => u.Id);
            modelBuilder.Entity<MusicTrack>().Property(u => u.Id).ValueGeneratedNever();
            modelBuilder.Entity<MusicVersion>().HasKey(u => u.Id);
        }

        internal DbSet<MusicTrack> MusicTrack { get; set; }
        internal DbSet<MusicVersion> MusicVersions { get; set; }
    }
}
