using LoginSystem.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Login;

namespace LoginSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController: ControllerBase
{
    private readonly AppDbContext appDbContext;
    
    public UserController(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Users>>> Get() => Ok(await appDbContext.Users.ToListAsync());
    
    [HttpPost]
    public async Task<ActionResult<Users>> Add(Users user)
    {
        if (user != null)
        {
            var result = appDbContext.Users.Add(user).Entity;
            await appDbContext.SaveChangesAsync();
            return Ok(result);
        }
        return BadRequest("Invalid Request");
    }
    
    [HttpGet("{email}/{password}")]
    public async Task<ActionResult<Users>> Login(string email, string password)
    {
        if (email is not null && password is not null)
        {
            var user = await appDbContext.Users
                .Where(x => x.Email!.ToLower().Equals(email.ToLower()) && x.Password == password)
                .FirstOrDefaultAsync();
            return user != null ? Ok(user) : NotFound("User not found");
        }
        return BadRequest("Invalid Request");
    }
    
    [HttpGet("byUsername/{username}/{password}")]
    public async Task<ActionResult<Users>> LoginWithUsername(string username, string password)
    {
        if (username is not null && password is not null)
        {
            var user = await appDbContext.Users
                .Where(x => x.FullName!.ToLower().Equals(username.ToLower()) && x.Password == password)
                .FirstOrDefaultAsync();
            return user != null ? Ok(user) : NotFound("User not found");
        }
        return BadRequest("Invalid Request");
    }
    
    [HttpDelete]
    public async Task<ActionResult<Users>> DeleteById(int id)
    {
        if (id > 0)
        {
            var user = await appDbContext.Users.FindAsync(id);
            if (user != null)
            {
                appDbContext.Users.Remove(user);
                await appDbContext.SaveChangesAsync();
                return Ok(user);
            }
            return NotFound("User not found");
        }
        return BadRequest("Invalid Request");
    }
}