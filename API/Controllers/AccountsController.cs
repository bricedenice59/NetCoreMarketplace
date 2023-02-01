using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Interfaces;
using Core.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountsController : BaseApiController
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    
    public AccountsController(UserManager<User> userManager,
        SignInManager<User> signInManager,
        ITokenService tokenService,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _mapper = mapper;
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
        if (await _userManager.FindByEmailAsync(registerData.Email) != null)
            return new BadRequestObjectResult(new ApiResponse (400, "Email address is in use"));
        
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
    
    [API.Attributes.Authorize]
    [HttpGet]
    public ActionResult<UserIdentityDto> GetCurrentUser()
    {
        var context = HttpContext;
        var user = context.Items["User"] as User;

        return new UserIdentityDto
        {
            Email = user!.Email,
            Token = _tokenService.CreateToken(user),
            FirstName = user!.DisplayName
        };
    }
    
    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
    {
        return await _userManager.FindByEmailAsync(email) != null;
    }

    [API.Attributes.Authorize]
    [HttpGet("address")]
    public async Task<ActionResult<UserAddressDto>> GetUserAddress()
    {
        var context = HttpContext;
        var userFromContext = context.Items["User"] as User;
        
        var user =  await _userManager.Users.Include(x => x.Address)
            .SingleOrDefaultAsync(x => x.Email == userFromContext.Email);

        if(user == null)
            return NotFound(new ApiResponse(401));
        
        return _mapper.Map<UserAddress, UserAddressDto>(user.Address);
    }

    [API.Attributes.Authorize]
    [HttpPut("address")]
    public async Task<ActionResult<UserAddressDto>> UpdateUserAddress(UserAddressDto address)
    {
        var context = HttpContext;
        var userFromContext = context.Items["User"] as User;
        var user =  await _userManager.Users.Include(x => x.Address)
            .SingleOrDefaultAsync(x => x.Email == userFromContext.Email);
        
        if(user == null)
            return NotFound(new ApiResponse(401));
        
        user.Address = _mapper.Map<UserAddressDto, UserAddress>(address);

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded) return Ok(_mapper.Map<UserAddressDto>(user.Address));

        return BadRequest("Problem updating the user");
    }
}