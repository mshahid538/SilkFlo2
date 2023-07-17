using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using SilkFlo.Data.Core.Domain;
using SilkFlo.Data.Persistence;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Web.Controllers
{
    public class Test : Controller
    {
        public IActionResult index()
        {
            return View();
        }

       public IActionResult End()
        {
            return View("Subscription/_EndNotification");
        }  
        public IActionResult gracePeriod()
        {
            return View("Subscription/GracePeriod");
        } 
        public IActionResult pre2day()
        {
            return View("Subscription/PreEnd2day");
        }
        public IActionResult pre7day()
        {
            return View("Subscription/preEnd7day");
        }
        public IActionResult preLast()
        {
            return View("Subscription/PreEndLast");
        }
    }

   
}
