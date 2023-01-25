using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorkoutTracker.Common.DataEntity;

[Table("WorkoutSession")]
public class WorkoutSession
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ?WorkoutSessionId { get; set; }

    public double WorkoutScore { get; set; }

    [Column(TypeName = "date")]
    public DateTime WorkoutDate { get; set; }
    public int UserId { get; set; }

    [InverseProperty("WorkoutSession")]
    public virtual ICollection<Exercise> Exercises { get; } = new List<Exercise>();

    [ForeignKey("UserId")]
    [InverseProperty("WorkoutSessions")]
    public virtual User? User { get; set; } = null!;
}
