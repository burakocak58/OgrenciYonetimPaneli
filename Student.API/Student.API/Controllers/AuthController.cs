using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using Student.API.DataModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Student.API.DomainModels;
using Student.API.DataModels;
using BCrypt.Net;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	private readonly StudentAdminContext _context;
	private readonly IConfiguration _configuration;

	public AuthController(StudentAdminContext context, IConfiguration configuration)
	{
		_context = context;
		_configuration = configuration;
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] Student.API.DomainModels.RegisterRequest request)
	{
		if (await _context.Users.AnyAsync(u => u.UserName == request.UserName))
			return BadRequest("Kullanıcı adı zaten mevcut.");

		var user = new AppUser
		{
			Id = Guid.NewGuid(),
			UserName = request.UserName,
			Email = request.Email,
			PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
		};

		_context.Users.Add(user);
		await _context.SaveChangesAsync();

		return Ok("Kayıt başarılı");
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] Student.API.DomainModels.LoginRequest request)
	{
		var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);

		if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
			return Unauthorized("Kullanıcı adı veya şifre hatalı.");

		var token = GenerateJwtToken(user);
		return Ok(new { token });
	}

	private string GenerateJwtToken(AppUser user)
	{
		var claims = new[]
		{
			new Claim(ClaimTypes.Name, user.UserName),
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			claims: claims,
			expires: DateTime.UtcNow.AddHours(2),
			signingCredentials: creds);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
