using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SilkFlo.Web
{
    // ToDo: Implement Permission class
    public class Permission
    {
        public Permission(Func<string, bool, Task<AuthorizationResult>> authorizeAsync)
        {
            AuthorizeAsync = authorizeAsync;
        }

        public Func<string, bool, Task<AuthorizationResult>> AuthorizeAsync { get; set; }
    }
}