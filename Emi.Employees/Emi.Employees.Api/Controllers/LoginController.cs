using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Emi.Employees.Application.Abstraction.Requests;
using Emi.Employees.Domain.Entities;

namespace Emi.Employees.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly UserManager<Employee> _employeeManager;
    private readonly RoleManager<Position> _positionManager;
    private readonly SignInManager<Employee> _signInManager;
    private readonly IConfiguration _configuration;

    public LoginController(UserManager<Employee> employeeManager, 
        SignInManager<Employee> signInManager,
        RoleManager<Position> positionManager,
        IConfiguration configuration)
    {
        _employeeManager = employeeManager;
        _positionManager = positionManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        var employee = await _employeeManager.FindByNameAsync(model.Username);
        if (employee != null && await _employeeManager.CheckPasswordAsync(employee, model.Password))
        {
            var position = await _positionManager.FindByIdAsync(employee.PositionId.ToString());

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, employee.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            authClaims.Add(new Claim(ClaimTypes.Role, position.Name));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
        return Unauthorized();
    }
}