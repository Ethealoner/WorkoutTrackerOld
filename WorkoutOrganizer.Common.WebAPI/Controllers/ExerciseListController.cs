using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Common.DataContext;

namespace WorkoutTracker.Common.WebAPI.Controllers
{
    [ApiController]
    [Route("api/exerciselist")]
    public class ExerciseListController : Controller
    {
        private WorkoutDatabase workoutDatabase;

        public ExerciseListController(WorkoutDatabase workoutDatabase)
        {
            this.workoutDatabase = workoutDatabase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExercises()
        {
            var exercisesList = await workoutDatabase.ExercisesLists.ToListAsync();
            return Ok(exercisesList);
        }
    }
}
