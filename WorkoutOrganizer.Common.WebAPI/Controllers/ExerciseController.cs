﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutOrganizer.Common.DataContext;

namespace WorkoutOrganizer.Common.WebAPI.Controllers
{
    [ApiController]
    [Route("api/exercise")]
    public class ExerciseController : Controller
    {
        private WorkoutDatabase workoutDatabase;

        public ExerciseController(WorkoutDatabase workoutDatabase)
        {
            this.workoutDatabase = workoutDatabase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExercises()
        {
            var exercises = await workoutDatabase.Exercises.ToListAsync();
            return Ok(exercises);
        }
    }
}