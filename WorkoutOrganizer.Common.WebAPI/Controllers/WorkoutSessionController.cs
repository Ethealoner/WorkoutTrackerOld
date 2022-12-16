using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Common.DataContext;
using WorkoutTracker.Common.DataEntity;

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
        [HttpPost]
        public async Task<IActionResult> AddWorkoutSessions([FromBody] WorkoutSession workoutSessions)
        {
            await workoutDatabase.WorkoutSessions.AddAsync(workoutSessions);

            workoutDatabase.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateExercise([FromRoute] int id, WorkoutSession updateWorkoutSessions)
        {
            WorkoutSession workoutSessions = await workoutDatabase.WorkoutSessions.FindAsync(id);

            if (workoutSessions == null)
            {
                return NotFound();
            }
            workoutSessions.WorkoutScore = updateWorkoutSessions.WorkoutScore;
            workoutSessions.WorkoutDate = updateWorkoutSessions.WorkoutDate;
            await workoutDatabase.SaveChangesAsync();

            return Ok(workoutSessions);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetExercises([FromRoute] int id)
        {
            var workoutSessions = await workoutDatabase.WorkoutSessions.FirstOrDefaultAsync(x => x.WorkoutSessionId == id);

            if (workoutSessions == null)
            {
                return NotFound();
            }
            return Ok(workoutSessions);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteExercise([FromRoute] int id)
        {
            var workoutSessions = await workoutDatabase.WorkoutSessions.FirstOrDefaultAsync(x => x.WorkoutSessionId == id);

            if (workoutSessions == null)
            {
                return NotFound();
            }
            workoutDatabase.WorkoutSessions.Remove(workoutSessions);
            await workoutDatabase.SaveChangesAsync();
            return Ok();

        }
    }
}
