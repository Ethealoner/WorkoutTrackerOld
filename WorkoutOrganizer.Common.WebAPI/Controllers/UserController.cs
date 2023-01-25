using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using WorkoutTracker.Common.DataContext;
using WorkoutTracker.Common.DataEntity;

namespace WorkoutTracker.Common.WebAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {

        private WorkoutDatabase workoutDatabase;
        private JwtHandler jwtHandler;

        public UserController(WorkoutDatabase workoutDatabase, JwtHandler jwtHandler)
        {
            this.workoutDatabase = workoutDatabase;
            this.jwtHandler = jwtHandler;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User newUser)
        {
            // Set Id to null so that DB can auto increment this value
            newUser.UserId = null;
            if (workoutDatabase.Users.Any(u => u.Username == newUser.Username || u.Email == newUser.Email))
            {
                return BadRequest("User already exists");
            }

            await workoutDatabase.Users.AddAsync(newUser);

            _ = workoutDatabase.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            User userToLogin = await workoutDatabase.Users.FirstOrDefaultAsync(u => u.Username == user.Username && u.Password == user.Password);

            if(userToLogin == null)
            {
                return Unauthorized(new AuthResponse { ErrorMessage = "Invalid Authentication"});
            }
            var claims = jwtHandler.GetClaims(new IdentityUser(userToLogin.Username));
            var tokenOptions = jwtHandler.GenerateTokenOptions(
                jwtHandler.GetSigningCredentianls(),
                claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new AuthResponse { IsAuthSuccessful = true, Token = token, UserId = userToLogin.UserId});

        }
    }
}
