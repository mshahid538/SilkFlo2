using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

/// <summary>
/// Authorize Tag Helper for ASP.NET Core
/// The basic idea of this tag helper is to provide similar functionality to the [Authorize] attribute and it’s associated action filter in
/// ASP.NET Core MVC. The authorize tag helper will provide the same options as the [Authorize] attribute and the implementation will
/// be based on the authorize filter. In the MVC framework, the [Authorize] attribute provides data such as the names of roles and
/// policies while the authorize filter contains the logic to check for roles and policies as part of the request pipeline.
/// https://www.davepaquette.com/archive/2017/11/05/authorize-tag-helper.aspx
/// https://github.com/dpaquette/TagHelperSamples/blob/master/TagHelperSamples/src/TagHelperSamples.Authorization/AuthorizeTagHelper.cs
///
/// Place the following using statement at the top of your Razor page.
/// @using SilkFlo.Services.Authorization
/// 
/// Add
///   services.AddControllersWithViews();
///   services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
/// to the start of Startup.ConfigureServices();
/// </summary>

namespace SilkFlo.Web.Services.Authorization
{
    [HtmlTargetElement(Attributes = "asp-authorize")]
    [HtmlTargetElement(Attributes = "asp-authorize,asp-policy")]
    [HtmlTargetElement(Attributes = "asp-authorize,asp-roles")]
    [HtmlTargetElement(Attributes = "asp-authorize,asp-authentication-schemes")]
    public class AuthorizeTagHelper : TagHelper, IAuthorizeData
    {
        private readonly IAuthorizationPolicyProvider _policyProvider;
        private readonly IPolicyEvaluator _policyEvaluator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizeTagHelper(IHttpContextAccessor httpContextAccessor, IAuthorizationPolicyProvider policyProvider, IPolicyEvaluator policyEvaluator)
        {
            _httpContextAccessor = httpContextAccessor;
            _policyProvider = policyProvider;
            _policyEvaluator = policyEvaluator;
        }

        /// <summary>
        /// Gets or sets the policy name that determines access to the HTML block.
        /// </summary>
        [HtmlAttributeName("asp-policy")]
        public string Policy { get; set; }

        /// <summary>
        /// Gets or sets a comma delimited list of roles that are allowed to access the HTML  block.
        /// </summary>
        [HtmlAttributeName("asp-roles")]
        public string Roles { get; set; }

        /// <summary>
        /// Gets or sets a comma delimited list of schemes from which user information is constructed.
        /// </summary>
        [HtmlAttributeName("asp-authentication-schemes")]
        public string AuthenticationSchemes { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var policy = await AuthorizationPolicy.CombineAsync(_policyProvider, new[] { this });

            var authenticateResult = await _policyEvaluator.AuthenticateAsync(policy, _httpContextAccessor.HttpContext);

            var authorizeResult = await _policyEvaluator.AuthorizeAsync(policy, authenticateResult, _httpContextAccessor.HttpContext, null);

            if (!authorizeResult.Succeeded)
            {
                output.SuppressOutput();
            }

            if (output.Attributes.TryGetAttribute("asp-authorize", out TagHelperAttribute attribute))
                output.Attributes.Remove(attribute);
        }
    }
}