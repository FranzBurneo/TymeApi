using Microsoft.AspNetCore.Mvc;
using TymeApi.Application.DTOs;
using TymeApi.Application.Interfaces;

namespace TymeApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await userService.GetByIdAsync(id);
            return user is null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDto dto)
        {
            var id = await userService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, new { Id = id });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserDto dto)
        {
            var updated = await userService.UpdateAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await userService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
