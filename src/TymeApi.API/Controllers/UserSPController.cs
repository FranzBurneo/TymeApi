using Microsoft.AspNetCore.Mvc;
using TymeApi.Application.DTOs;
using TymeApi.Application.Interfaces;

namespace TymeApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserSPController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUserWithSP([FromBody] UserDto dto)
    {
        var rowsAffected = await userService.CreateWithStoredProcedureAsync(dto);
        return Ok(new { RowsAffected = rowsAffected, Message = "User created via stored procedure." });
    }
}