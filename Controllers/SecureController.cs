
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("[controller]")]
public class SecureController : ControllerBase
{

    [HttpGet("secure-data")]
    [Authorize]
    public IActionResult GetSecureData()
    {
        return Ok("This data is for verified users only.");
    }


    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAdminData()
    {

        return Ok("This data is for admin only.");
    }

    [HttpPost("login")]
    public IActionResult Login([FromServices] IConfiguration configuration, string username, string password)
    {

        // Normally here you should  check the user from the database.
        if (username == "test" && password == "123")
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? "default_key_value"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(

                    claims: claims,
                    expires: DateTime.Now.AddMinutes(configuration["Jwt:ExpireMinutes"] != null ? Convert.ToDouble(configuration["Jwt:ExpireMinutes"]) : 30),
                    signingCredentials: creds

            );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        return Unauthorized();
    }


}
