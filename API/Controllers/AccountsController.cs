using API.Dtos;
using API.Errors;
using Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountsController : BaseApiController
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    
    public AccountsController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserIdentityDto>> Login(UserLoginDto loginData)
    {
        var user = await _userManager.FindByEmailAsync(loginData.Email);
        if (user == null)
            return Unauthorized(new ApiResponse(401));

        var loginResult = await _signInManager.CheckPasswordSignInAsync(user, loginData.Password, true);
        if(!loginResult.Succeeded)
            return Unauthorized(new ApiResponse(401));

        return new UserIdentityDto { Email = loginData.Email, FirstName = user.DisplayName, Token = "" };
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserIdentityDto>> Register(UserRegisterDto registerData)
    {
        var user = new User
        {
            DisplayName = registerData.DisplayName,
            UserName = registerData.DisplayName,
            Email = registerData.Email
        };

        var registerResult = await _userManager.CreateAsync(user, registerData.Password);
        if (!registerResult.Succeeded)
            return BadRequest(new ApiResponse(400));
        
        return new UserIdentityDto { Email = registerData.Email, FirstName = user.DisplayName, Token = "" };
    }
}