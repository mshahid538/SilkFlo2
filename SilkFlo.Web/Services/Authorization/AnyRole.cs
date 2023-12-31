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
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Web.Services.Authorization
{
    public class AnyRoleRequirement : IAuthorizationRequirement { }

    public class AnyRoleHandler : AuthorizationHandler<AnyRoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       AnyRoleRequirement requirement)
        {
            var role = context.User
                              .Claims
                              .FirstOrDefault(x => x.Type == "Role");

            if(role == null)
                context.Fail();
            else
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}