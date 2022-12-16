using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Common.DataContext;
using WorkoutTracker.Common.DataEntity;

namespace WorkoutTracker.Common.WebAPI.Controllers
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

        [HttpPost]
        public async Task<IActionResult> AddExercise([FromBody] Exercise exercise)
        {
            await workoutDatabase.Exercises.AddAsync(exercise);

            workoutDatabase.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateExercise([FromRoute] int id, Exercise updateExercise)
        {
            Exercise exercise = await workoutDatabase.Exercises.FindAsync(id);

            if (exercise == null)
            {
                return NotFound();
            }
            exercise.Repetition = updateExercise.Repetition;
            exercise.TypeOfExercise = updateExercise.TypeOfExercise;
            exercise.Name = updateExercise.Name;
            await workoutDatabase.SaveChangesAsync();

            return Ok(exercise);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetExercises([FromRoute] int id)
        {
            var exercise = await workoutDatabase.Exercises.FirstOrDefaultAsync(x => x.Id == id);

            if (exercise == null)
            {
                return NotFound();
            }
            return Ok(exercise);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteExercise([FromRoute] int id)
        {
            var exercise = await workoutDatabase.Exercises.FirstOrDefaultAsync(x => x.Id == id);

            if (exercise == null)
            {
                return NotFound();
            }
            workoutDatabase.Exercises.Remove(exercise);
            await workoutDatabase.SaveChangesAsync();
            return Ok();

        }
    }
}
