using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;

namespace Emi.Employees.App;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;

    public JwtMiddleware(RequestDelegate next, IConfiguration configuration, ILogger logger)
    {
        _next = next;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            AttachPoliciesToContext(context, token);
        }

        await _next(context);
    }

    private void AttachPoliciesToContext(HttpContext context, string token)
    {
        try
        {
            List<Claim> policies;
            var tokenHandler = new JwtSecurityTokenHandler();
            var decodedToken = tokenHandler.ReadJwtToken(token);
            var claimUserId = decodedToken.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;

            var userType = string.IsNullOrEmpty(claimUserId) ? Enums.UserType.Component : Enums.UserType.Crushbank;
            string companyId;

            if (userType == Enums.UserType.Crushbank)
            {
                tokenHandler = new JwtSecurityTokenHandler();
                var secretKey = _configuration.GetValue<string>("ApiSettings:SecretKey");
                var key = Encoding.ASCII.GetBytes(secretKey);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                policies = jwtToken.Claims.Where(x => x.Type == ClaimTypes.AuthorizationDecision).ToList();
                companyId = jwtToken.Claims.FirstOrDefault(x => x.Type == "CompanyId")?.Value;

                var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
                var hasInsightAccess = jwtToken.Claims.FirstOrDefault(x => x.Type == "InsightAccess")?.Value;
                var hasAdminAccess = jwtToken.Claims.FirstOrDefault(x => x.Type == "AdminAccess")?.Value;
                var firstName = jwtToken.Claims.FirstOrDefault(x => x.Type == "FirstName")?.Value;
                var lastName = jwtToken.Claims.FirstOrDefault(x => x.Type == "LastName")?.Value;
                var email = jwtToken.Claims.FirstOrDefault(x => x.Type == "email")?.Value;
                var roles = jwtToken.Claims.FirstOrDefault(x => x.Type == "Roles")?.Value;
                var hasCustomModelOption = jwtToken.Claims.FirstOrDefault(x => x.Type == "HasCustomModelOption")?.Value;

                context.Items["UserId"] = userId;
                context.Items["FirstName"] = firstName;
                context.Items["LastName"] = lastName;
                context.Items["InsightAccess"] = hasInsightAccess;
                context.Items["AdminAccess"] = hasAdminAccess;
                context.Items["Email"] = email;
                context.Items["Roles"] = roles;
                context.Items["HasCustomModelOption"] = hasCustomModelOption;
            }
            else
            {
                policies = decodedToken.Claims.Where(x => x.Type == ClaimTypes.AuthorizationDecision).ToList();
                companyId = decodedToken.Claims.FirstOrDefault(x => x.Type == "CompanyId")?.Value;
            }

            context.Items["Policies"] = policies;
            context.Items["CompanyId"] = companyId;
        }
        catch (SecurityTokenException ex)
        {
            _logger.Error(ex, $"Error: validating the token {ex.Message}");
            throw new SecurityTokenException("Invalid token");
        }
    }
}
