using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserContextService;
public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetUserEmail()
    {
        var email = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email);
        return email.Value;
    }

    public string GetUserId()
    {
         var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null)
                return null;
            return userId.Value;
    }
}
