using API.Dtos;
using API.Errors;
using Core.Interfaces;
using Core.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountsController : BaseApiController
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;
    
    public AccountsController(UserManager<User> userManager,
        SignInManager<User> signInManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UserIdentityDto>> Login(UserLoginDto loginData)
    {
        var user = await _userManager.FindByEmailAsync(loginData.Email);
        if (user == null)
            return Unauthorized(new ApiResponse(401));

        var loginResult = await _signInManager.CheckPasswordSignInAsync(user, loginData.Password, true);
        if(!loginResult.Succeeded)
            return Unauthorized(new ApiResponse(401));

        var userIdentityDto = 
            new UserIdentityDto
            {
                Email = loginData.Email, 
                FirstName = user.DisplayName, 
                Token = _tokenService.CreateToken(user)
            };
        
        return userIdentityDto;
    }

    [AllowAnonymous]
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
        
        return new UserIdentityDto
        {
            Email = registerData.Email,
            FirstName = user.DisplayName, 
            Token = _tokenService.CreateToken(user)
        };
    }
}