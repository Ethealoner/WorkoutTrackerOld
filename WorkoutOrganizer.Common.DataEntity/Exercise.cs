using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorkoutTracker.Common.DataEntity;

[Table("Exercise")]
public class Exercise
{
    [Key]
    public int Id { get; set; }

    public int WorkoutSessionId { get; set; }

    [Column(TypeName = "text")]
    public string Name { get; set; } = null!;

    [Column(TypeName = "text")]
    public string TypeOfExercise { get; set; } = null!;

    [Column(TypeName = "text")]
    public string Repetition { get; set; } = null!;

    [ForeignKey("WorkoutSessionId")]
    [InverseProperty("Exercises")]
    public virtual WorkoutSession? WorkoutSession { get; set; } = null!;
}
