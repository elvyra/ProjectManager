using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Domains;

namespace ToDoList.Data
{
    public class ToDoListDbContext : IdentityDbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<FAQ> FAQ { get; set; }

        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProjectTeam>()
            .HasKey(t => new { t.Id, t.Email});

            builder.Entity<ProjectTeam>()
                .HasOne(pt => pt.Project)
                .WithMany(p => p.Team)
                .HasForeignKey(pt => pt.Id);

            builder.Entity<ProjectTeam>()
                .HasOne(pt => pt.Person)
                .WithMany(t => t.InTeams)
                .HasForeignKey(pt => pt.Email);

            builder.Entity<Project>()
                .HasOne(p => p.Client)
                .WithMany(p => p.AsClient)
                .HasForeignKey(p => p.ClientEmail);

            base.OnModelCreating(builder);
        }
    }
}
