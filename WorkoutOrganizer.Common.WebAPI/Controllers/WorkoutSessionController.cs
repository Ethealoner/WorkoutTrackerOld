﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Common.DataContext;
using WorkoutTracker.Common.DataEntity;

namespace WorkoutTracker.Common.WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/workoutsession")]
    public class WorkoutSessionController : Controller
    {
        private WorkoutDatabase workoutDatabase;

        public WorkoutSessionController(WorkoutDatabase workoutDatabase)
        {
            this.workoutDatabase = workoutDatabase;
        }
        [HttpGet]
        [Route("usersWorkoutSessions/{userId}")]
        public async Task<IActionResult> GetAllWorkoutSessions([FromRoute] int userId)
        {
            var workoutSessions = await workoutDatabase.WorkoutSessions
                .Where(x => x.UserId == userId)
                .ToListAsync();
            return Ok(workoutSessions);
        }
        [HttpPost]
        public async Task<IActionResult> AddWorkoutSessions([FromBody] WorkoutSession workoutSessions)
        {
            workoutSessions.WorkoutSessionId = null;
            await workoutDatabase.WorkoutSessions.AddAsync(workoutSessions);

            _ = workoutDatabase.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateWorkoutSessions([FromRoute] int id, WorkoutSession updateWorkoutSessions)
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
        public async Task<IActionResult> GetWorkoutSession([FromRoute] int id)
        {
            var workoutSessions = await workoutDatabase.WorkoutSessions.
                Where(x => x.WorkoutSessionId == id)
                .Include(x => x.Exercises)
                .ToListAsync();

            if (workoutSessions == null)
            {
                return NotFound();
            }
            return Ok(workoutSessions.ElementAt(0));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteWorkoutSessions([FromRoute] int id)
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
