using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Common.Entities;


namespace ToDoList.Data
{
    public partial class ToDoListDbContext : DbContext
    {

        public ToDoListDbContext()
            //: base("name=ToDoListDbContext") //don't work in asp.mvc
            : base("ToDoListDbContext")
        {

        }

        public virtual DbSet<ToDoListTask> ToDoListTasks { get; set; }
        public virtual DbSet<ToDoListItem> ToDoListItems { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(e=> e.Id);
            modelBuilder.Entity<User>()
               .Property(e => e.Id)
               .HasColumnName("UserId");

            modelBuilder.Entity<User>()
                .Property(e => e.Username)
                .HasColumnName("Username")
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Entity<User>()
               .Property(e => e.Password)
               .HasColumnName("Password")
               .IsUnicode(false)
               .IsRequired();

            modelBuilder.Entity<User>()
               .Property(e => e.FirstName)
               .HasColumnName("FirstName")
               .IsUnicode(false)
               .IsRequired();

            modelBuilder.Entity<User>()
               .Property(e => e.LastName)
               .HasColumnName("LastName")
               .IsUnicode(false)
               .IsRequired();

            modelBuilder.Entity<User>()
               .Property(e => e.EmailAddress)
               .HasColumnName("EmailAdress")
               .IsUnicode(false)
               .IsRequired();

            modelBuilder.Entity<ToDoListItem>().HasRequired(u => u.User).WithMany().Map(m => m.MapKey("UserId"));
            
            modelBuilder.Entity<ToDoListItem>().ToTable("Todolists");
            modelBuilder.Entity<ToDoListItem>().HasKey(e => e.Id);
            modelBuilder.Entity<ToDoListItem>()
             .Property(e => e.Id)
             .HasColumnName("ListId");

            modelBuilder.Entity<ToDoListItem>()
                .ToTable("Todolists")
               .Property(e => e.Name)
               .HasColumnName("Title")
               .IsUnicode(false);

            modelBuilder.Entity<ToDoListItem>()
              .Property(e => e.CreateDate)
              .HasColumnName("CreateDate");

            modelBuilder.Entity<ToDoListItem>().HasMany<ToDoListTask>(u => u.Tasks).WithRequired(u => u.ToDoListItem).Map(m => m.MapKey("TodolistId"));

            modelBuilder.Entity<ToDoListTask>().ToTable("Tasks");
            modelBuilder.Entity<ToDoListTask>().HasKey(e => e.Id);
            modelBuilder.Entity<ToDoListTask>()
              .Property(e => e.Id)
              .HasColumnName("TaskId");

            modelBuilder.Entity<ToDoListTask>()
               .Property(e => e.Text)
               .HasColumnName("TextTask")
               .IsUnicode(false);

            modelBuilder.Entity<ToDoListTask>()
               .Property(e => e.IsCompleted)
               .HasColumnName("IsCompleted")
               .HasColumnType("bit");

            modelBuilder.Entity<ToDoListTask>().HasRequired(t => t.ToDoListItem).WithMany(t => t.Tasks);
            //modelBuilder.Entity<ToDoListTask>().HasRequired<ToDoListItem>(i => i.ToDoListItem).WithMany(e=> e.Tasks).HasForeignKey(e => e.ToDoListItem);

            modelBuilder.Entity<ToDoListTask>()
               .Property(e => e.CreateDate)
               .HasColumnName("StartDate");

            modelBuilder.Entity<ToDoListTask>()
               .Property(e => e.CompleteDate)
               .HasColumnName("EndDate");

        }

    }
}
