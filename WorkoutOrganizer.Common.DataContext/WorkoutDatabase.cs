using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WorkoutOrganizer.Common.DataEntity;

namespace WorkoutOrganizer.Common.DataContext;

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

    public virtual DbSet<ExercisesList> ExercisesLists { get; set; }

    public virtual DbSet<WorkoutSession> WorkoutSessions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=WorkoutDatabase;Integrated Security=true;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.ExerciseNavigation).WithMany(p => p.Exercises)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Exercise_ExercisesList");
        });

        modelBuilder.Entity<ExercisesList>(entity =>
        {
            entity.Property(e => e.ExercisesId).ValueGeneratedNever();

            entity.HasOne(d => d.WorkoutSession).WithMany(p => p.ExercisesLists)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExercisesList_WorkoutSession");
        });

        modelBuilder.Entity<WorkoutSession>(entity =>
        {
            entity.Property(e => e.WorkoutSessionId).ValueGeneratedNever();
        });

    }
}
