using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Common.DataEntity;

namespace WorkoutTracker.Common.DataContext;

public class WorkoutDatabase : DbContext
{
    public WorkoutDatabase()
    {
    }

    public WorkoutDatabase(DbContextOptions<WorkoutDatabase> options)
        : base(options)
    {
    }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<WorkoutSession> WorkoutSessions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=WorkoutDatabase;Integrated Security=true;Encrypt=False;MultipleActiveResultSets=true");
                           // LogTo(s => System.Diagnostics.Debug.WriteLine(s))
                           // .EnableDetailedErrors(true)
                           // .EnableSensitiveDataLogging(true);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.WorkoutSession).WithMany(p => p.Exercises)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Exercise_WorkoutSession");
        });

        modelBuilder.Entity<WorkoutSession>(entity =>
        {
            entity.Property(e => e.WorkoutSessionId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.User).WithMany(p => p.WorkoutSessions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkoutSession_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).ValueGeneratedOnAdd();
        });

    }
}
