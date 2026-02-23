using erp_sql.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using erp_sql.DTO;
using erp_sql.Model;

namespace erp_sql.Controllers
{

    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("getUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost("createUser")]
        public async Task<IActionResult> createUser([FromBody] usersDto usersDto)
        {
            var newUser = new User
            {
                Name = usersDto.Name
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User created successfully." });
        }

        [HttpPut("updateUserById/{id}")]
        public async Task<IActionResult> updateUserById(int id, updateUserDto updateUserDto)
        {
            if (id != updateUserDto.id)
                return BadRequest("ID in URL and body do not match.");

            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
                return NotFound();

            // Update fields - example with just Name, add more as needed
            existingUser.Name = updateUserDto.Name;
            // existingUser.Email = updatedUser.Email; // etc.

            await _context.SaveChangesAsync();

            return Ok(existingUser);
        }


        [HttpDelete("deleteUserById/{id}")]
        public async Task<IActionResult> deleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }
    }
}
