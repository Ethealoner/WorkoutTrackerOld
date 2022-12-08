using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorkoutOrganizer.Common.DataEntity;

[Table("WorkoutSession")]
public class WorkoutSession
{
    [Key]
    public int WorkoutSessionId { get; set; }

    public double WorkoutScore { get; set; }

    [Column(TypeName = "date")]
    public DateTime WorkoutDate { get; set; }

    [InverseProperty("WorkoutSession")]
    public virtual ICollection<ExercisesList> ExercisesLists { get; } = new List<ExercisesList>();
}
