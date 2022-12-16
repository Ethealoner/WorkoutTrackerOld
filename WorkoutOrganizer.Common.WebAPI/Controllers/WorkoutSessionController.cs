using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Common.DataContext;

namespace WorkoutTracker.Common.WebAPI.Controllers
{
    [ApiController]
    [Route("api/workoutsession")]
    public class WorkoutSessionController : Controller
    {
        private WorkoutDatabase workoutDatabase;

        public WorkoutSessionController(WorkoutDatabase workoutDatabase)
        {
            this.workoutDatabase = workoutDatabase;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllExercises()
        {
            var workoutSessions = await workoutDatabase.WorkoutSessions.ToListAsync();
            return Ok(workoutSessions);
        }
    }
}
