using KlementWeb.Classes;
using KlementWeb.Data.Models;
using KlementWeb.Models.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KlementWeb.Controllers
{
    [Authorize]
    [ExceptionsToMessageFilter]
    public class UserController : Controller
    {
        private readonly UserManager<WebUser> userManager;
        private readonly SignInManager<WebUser> signInManager;

        public UserController(UserManager<WebUser> userManager, SignInManager<WebUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Administration()
        {
                return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
                return View(model);

            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                this.AddFlashMessage(new FlashMessage("Jste přihlášen", FlashMessageType.Success));
                RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Neplatné údaje");
            }

            return RedirectToAction("Administration", "User");
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            this.AddFlashMessage(new FlashMessage("Jste odhlášen", FlashMessageType.Success));
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                if (await userManager.FindByEmailAsync(model.Email) == null)
                {
                    var user = new WebUser { UserName = model.Email, Email = model.Email };
                    var result = await userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);

                        return string.IsNullOrEmpty(returnUrl)
                            ? RedirectToAction("Index", "Home")
                            : RedirectToLocal(returnUrl);
                    }

                    AddErrors(result);
                }

                AddErrors(IdentityResult.Failed(new IdentityError() { Description = $"Email {model.Email} je již zaregistrován" }));
            }
            this.AddFlashMessage(new FlashMessage("Registrace proběhla úspěšně", FlashMessageType.Success));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await userManager.GetUserAsync(User)
                ?? throw new ApplicationException($"Nepodařilo se načíst uživatele s ID {userManager.GetUserId(User)}.");

            var model = new ChangePasswordViewModel();
           
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await userManager.GetUserAsync(User)
                ?? throw new ApplicationException($"Nepodařilo se načíst uživatele s ID: {userManager.GetUserId(User)}.");

            var changePasswordResult = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                AddErrors(changePasswordResult);
                return View(model);
            }

            await signInManager.SignInAsync(user, isPersistent: false);
            this.AddFlashMessage(new FlashMessage("Změna hesla proběhla úspěšně", FlashMessageType.Success));
            return RedirectToAction("Administration");
        }

        #region Help methods
        private IActionResult RedirectToLocal(string returnUrl)
        {
            return Url.IsLocalUrl(returnUrl)
                  ? Redirect(returnUrl)
                  : (IActionResult)RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
        }
        #endregion
    }
}
