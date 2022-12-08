using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorkoutOrganizer.Common.DataEntity;

[Table("ExercisesList")]
public class ExercisesList
{
    [Key]
    public int ExercisesId { get; set; }

    public int WorkoutSessionId { get; set; }

    [InverseProperty("ExerciseNavigation")]
    public virtual ICollection<Exercise> Exercises { get; } = new List<Exercise>();

    [ForeignKey("WorkoutSessionId")]
    [InverseProperty("ExercisesLists")]
    public virtual WorkoutSession WorkoutSession { get; set; } = null!;
}
