using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using UserManagementApp.Enums;
using UserManagementApp.Models;

namespace UserManagementApp.Services;

public class UserStatusHandler : AuthorizationHandler<StatusRequirement>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserStatusHandler(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        StatusRequirement requirement)
    {
        var user = await _userManager.GetUserAsync(context.User);
        if (user is {Status: Status.Unblocked})
        {
            context.Succeed(requirement);
            return;
        }

        await _signInManager.SignOutAsync();
        var redirectContext = context.Resource as DefaultHttpContext;
        redirectContext?.Response.Redirect("/Account/Login", false);
        context.Succeed(requirement);
        
    }
}

public class StatusRequirement : IAuthorizationRequirement 
{
    //since we handle the authorization in our service, we can leave this empty
}
