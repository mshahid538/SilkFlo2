using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SilkFlo.Web.Controllers
{
    public partial class HomeController
    {
        [HttpGet("api/GetNavigation")]
        public async Task<IActionResult> GetNavigation()
        {
            var core = await GetClientAsync();

            if (core == null)
            {
                core = await GetClientAsync(false);

                if (core == null)
                    return NegativeFeedback("No Client");

                var htmlNoSubscription = await PartialAsync("Shared/_NavigationNoSubscription.cshtml");
                return Content(htmlNoSubscription);

            }

            var model = new Models.Business.Client(core);

            var html = await _viewToString.PartialAsync("Shared/_Navigation.cshtml", model);
            return Content(html);
        }

        [HttpGet("api/GetHotSpotContent/{name}")]
        public async Task<IActionResult> GetHotSpotContent(string name)
        {
            var hotSpot = await _unitOfWork.ApplicationHotSpots.GetByNameAsync(name);

            var bmkDomainEmail = "{domain email}";
            if (hotSpot != null && hotSpot.Text.IndexOf(bmkDomainEmail, StringComparison.OrdinalIgnoreCase) > -1)
            {
                var client = await GetClientAsync(false);
                if (client != null)
                    hotSpot.Text = hotSpot.Text.Replace(bmkDomainEmail, "@" + client.Website);
            }

            return Ok(hotSpot?.Text);
        }
    }
}