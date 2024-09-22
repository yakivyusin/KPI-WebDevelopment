using Identity_DataModel.Entities;
using Identity_WebApp.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity_WebApp.Controllers;

[ApiController]
[Route("account")]
[Authorize]
public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public AccountController(UserManager<User> userManager) => _userManager = userManager;

    [HttpPost("manage/age")]
    public async Task ManageUserAge(ManageAgeDto dto)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        user.Age = dto.NewAge;

        await _userManager.UpdateAsync(user);
    }
}
