using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorkoutTracker.Common.DataEntity;

[Table("User")]
public class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? UserId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<WorkoutSession> WorkoutSessions { get; } = new List<WorkoutSession>();
}

public class AuthResponse
{
    public bool IsAuthSuccessful { get; set; }
    public string? ErrorMessage { get; set; }
    public string? Token { get; set; }

    public int? UserId { get; set; }
}
