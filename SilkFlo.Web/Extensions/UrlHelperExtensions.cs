/*********************************************************
       Code Generated By  .  .  .  .  Delaney's ScriptBot
       WWW .  .  .  .  .  .  .  .  .  www.scriptbot.io
       Template Name.  .  .  .  .  .  Project Green 3.0
       Template Version.  .  .  .  .  20220508 002
       Author .  .  .  .  .  .  .  .  Delaney

                          .___,
                       ___('v')___
                       `"-\._./-"'
                           ^ ^

 *********************************************************/

namespace Microsoft.AspNetCore.Mvc
{
    public static class UrlHelperExtensions
    {
        public static string EmailConfirmationLink(
            this IUrlHelper urlHelper,
            string userId,
            string confirmationToken,
            string scheme)
        {
            return urlHelper.Action(
                action:     nameof(SilkFlo.Web.Controllers.AccountController.ConfirmEmail),
                controller: "Account",
                values:     new { userId, confirmationToken },
                protocol:   scheme);
        }

        public static string CompleteSignUp(
            this IUrlHelper urlHelper,
            string clientId,
            string scheme)
        {
            return urlHelper.Action(
                action: "Continue",
                controller: "Subscribe",
                values: new { clientId },
                protocol: scheme);

        }
    }
}