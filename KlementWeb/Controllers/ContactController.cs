using KlementWeb.Business.Interfaces;
using KlementWeb.Classes;
using KlementWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KlementWeb.Controllers
{
    [ExceptionsToMessageFilterAttribute]
    [AutoValidateAntiforgeryToken]
    public class ContactController : Controller
    {
        private readonly IEmail email;
        private const string emailReceiver = "postmaster@klementpetr.cz";
        public ContactController(IEmail email) => this.email = email;
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View(); 
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index(ContactViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            email.SendEmail(emailReceiver, model.Subject, model.MessageBody, model.SenderEmail);
            this.AddFlashMessage(new FlashMessage("Zpráva byla odeslána", FlashMessageType.Success));

            return RedirectToAction("Index");
        }
    }
}
