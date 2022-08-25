using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Forum.Context
{
    public class ForumContext : DbContext
    {
        public ForumContext(DbContextOptions<ForumContext> options) : base(options)
        {
        }

        public virtual DbSet<Subforum> Subforums { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<User>().ToTable("Users", e => e.ExcludeFromMigrations());

            // Setting up the Id to be generated on Add

            modelBuilder.Entity<Subforum>()
                .Property(i => i.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Topic>()
                .Property(i => i.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Post>()
                .Property(i => i.Id)
                .ValueGeneratedOnAdd();

            // Foreign Keys

            modelBuilder.Entity<Topic>()
                .HasOne(i => i.Subforum)
                .WithMany(i => i.Topics)
                .HasForeignKey(i => i.SubforumId);

            modelBuilder.Entity<Post>()
                .HasOne(i => i.Topic)
                .WithMany(i => i.Posts)
                .HasForeignKey(i => i.TopicId);
        }
    }
}
