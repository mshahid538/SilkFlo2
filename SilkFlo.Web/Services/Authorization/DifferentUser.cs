/*********************************************************
       Code Generated By  .  .  .  .  Delaney's ScriptBot
       WWW .  .  .  .  .  .  .  .  .  www.scriptbot.io
       Template Name.  .  .  .  .  .  Project Green 3.0
       Template Version.  .  .  .  .  20191111 001
       Author .  .  .  .  .  .  .  .  Delaney

                          .___,
                       ___('v')___
                       `"-\._./-"'
                           ^ ^

 *********************************************************/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Web.Services.Authorization
{
    public class DifferentUserRequirement : IAuthorizationRequirement { }

    public class DifferentUserHandler : AuthorizationHandler<DifferentUserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DifferentUserRequirement requirement)
        {
            try
            {
                var claim = context.User
                                  .Claims
                                  .FirstOrDefault(x => x.Type == "UserId");
                if(claim == null)
                {
                    context.Fail();
                    return Task.CompletedTask;
                }

                string s = claim.Value;
                if (!int.TryParse(s, out int userId))
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }

                if (context.Resource is AuthorizationFilterContext authContext)
                {
                    var value = authContext.RouteData.Values["Id"];

                    if (value == null)
                    {
                        context.Succeed(requirement);
                        return Task.CompletedTask;
                    }

                    if (!int.TryParse(value.ToString(), out int id))
                    {
                        context.Succeed(requirement);
                        return Task.CompletedTask;
                    }

                    if (userId == id)
                        context.Fail();
                    else
                        context.Succeed(requirement);
                }

                return Task.CompletedTask;
            }
            catch 
            { 
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
        }
    }
}