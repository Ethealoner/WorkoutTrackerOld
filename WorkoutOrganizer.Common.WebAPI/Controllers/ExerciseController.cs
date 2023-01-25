using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WorkoutTracker.Common.DataContext;
using WorkoutTracker.Common.DataEntity;

namespace WorkoutTracker.Common.WebAPI.Controllers
{
    public class RepetitionData
    {
        public int repsNumber { get; set; }
        public int repsDifficulty { get; set; }
    }
    [ApiController]
    [Authorize]
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
            // Set Id to null so that DB can auto increment this value
            exercise.Id = null;

            await workoutDatabase.Exercises.AddAsync(exercise);
            if (await CalculateExerciseScore(exercise.WorkoutSessionId) == false)
            {
                return NotFound();
            }
            _ = workoutDatabase.SaveChangesAsync();
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
            WorkoutSession session = await workoutDatabase.WorkoutSessions
                .Where(x => x.WorkoutSessionId == exercise.WorkoutSessionId)
                .Include(x => x.Exercises)
                .FirstAsync();
            if (session == null)
            {
                return NotFound();
            }
            exercise.Repetition = updateExercise.Repetition;
            exercise.TypeOfExercise = updateExercise.TypeOfExercise;
            exercise.Name = updateExercise.Name;
            await workoutDatabase.SaveChangesAsync();

            if (await CalculateExerciseScore(exercise.WorkoutSessionId) == false)
            {
                return NotFound();
            }

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

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<bool> CalculateExerciseScore(int sessionId)
        {
            WorkoutSession session = await workoutDatabase.WorkoutSessions
                .Where(x => x.WorkoutSessionId == sessionId)
                .Include(x => x.Exercises)
                .FirstAsync();

            if (session == null)
            {
                return false;
            }

            int sessionScore = 0;
            foreach(Exercise exerciseItem in session.Exercises)
            {
                if (exerciseItem.Repetition.Length == 0)
                {
                    continue;
                }
                Console.WriteLine(exerciseItem.Repetition);
                RepetitionData[] repetitionData = JsonSerializer.Deserialize<RepetitionData[]>(exerciseItem.Repetition);
                if (repetitionData != null)
                {
                    foreach (RepetitionData data in repetitionData)
                    {
                        Console.WriteLine($"Reps: {data.repsNumber}");
                        Console.WriteLine($"Difficulty: {data.repsDifficulty}");
                        if (exerciseItem.TypeOfExercise == "weight")
                        {
                            sessionScore += data.repsNumber + (data.repsDifficulty * 2);
                        }
                        else
                        {
                            sessionScore += (data.repsNumber * 3) - (data.repsDifficulty * 2);
                        }
                    }
                }
            }
            session.WorkoutScore = sessionScore;
            await workoutDatabase.SaveChangesAsync();
            return true;
        }
    }
}
