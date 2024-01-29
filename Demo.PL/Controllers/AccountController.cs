using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        #region Register(Create Account/Sign-Up)

        public IActionResult Register()
        {
            return View(); // View of Register , layout of Security => Take template from Internet
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
  
            }
            return View(model);
        }

        #endregion

        #region Login(Sign-In)

        #endregion
    }
}
