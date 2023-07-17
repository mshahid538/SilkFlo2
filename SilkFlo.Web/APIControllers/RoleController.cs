using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Web.Controllers
{
    public class RoleController : AbstractClient
    {
        public RoleController(Data.Core.IUnitOfWork unitOfWork,
                              Services.ViewToString viewToString,
                              IAuthorizationService authorization) : base(unitOfWork, viewToString, authorization) { }

        [HttpGet("/api/Settings/People/SystemRoles")]
        public async Task<IActionResult> GetSystemRoles()
        {
            if (!(await AuthorizeAsync("Manage Tenant User Roles")).Succeeded)
                return await PageApiAsync("<h1 class=\"text-danger\">Error: Unauthorised</h1>");


            try
            {
                var client = await GetClientAsync();

                IEnumerable<Data.Core.Domain.Role> roles;

                if (client.TypeId == Data.Core.Enumerators.ClientType.Client39.ToString())
                    roles = await _unitOfWork.Roles.FindAsync(x => x.Name.ToLower().IndexOf("agency", StringComparison.Ordinal) == -1
                                                                   && x.Name.ToLower().IndexOf("can backup dataset", StringComparison.Ordinal) == -1
                                                                   && x.Name.ToLower().IndexOf("administrator", StringComparison.Ordinal) == -1
                                                                   && x.Name.ToLower().IndexOf("uat tester", StringComparison.Ordinal) == -1);
                else if (client.TypeId == Data.Core.Enumerators.ClientType.ResellerAgency45.ToString())
                {
                    roles = await _unitOfWork.Roles.FindAsync(x => x.Name.ToLower().IndexOf("can backup dataset", StringComparison.Ordinal) == -1
                                                                   && x.Name.ToLower().IndexOf("administrator", StringComparison.Ordinal) == -1
                                                                   && x.Name.ToLower().IndexOf("uat tester", StringComparison.Ordinal) == -1);
                }
                else
                    roles = await _unitOfWork.Roles.FindAsync(x => x.Name.ToLower().IndexOf("agency", StringComparison.Ordinal) > -1
                                                                       && x.Name.ToLower().IndexOf("can backup dataset", StringComparison.Ordinal) == -1
                                                                       && x.Name.ToLower().IndexOf("administrator", StringComparison.Ordinal) == -1
                                                                       && x.Name.ToLower().IndexOf("uat tester", StringComparison.Ordinal) == -1);


                var model = Models.Role.Create(roles);


                var html = await _viewToString.PartialAsync("Shared/Roles/_List.cshtml", model);

                return Content(html);
            }
            catch (Exception ex)
            {
                Log(ex);
                return BadRequest();
            }
        }
    }
}
