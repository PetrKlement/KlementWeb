using KlementWeb.Classes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KlementWeb.Models
{
    [ExceptionsToMessageFilterAttribute]
    [AutoValidateAntiforgeryToken]
    public class ContactViewModel
    {
        [Display(Name = "Váš email")]
        [Required(ErrorMessage = "Vyplňte email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Špatný formát emailu")]
        public string SenderEmail { get; set; }

        [Display(Name = "Předmět")]
        [Required(ErrorMessage = "Vyplňte předmět")]
        [StringLength(maximumLength: 40, MinimumLength = 3, ErrorMessage = "Rozmezí 3 až 40 znaků")]
        public string Subject { get; set; }

        [Display(Name = "Text")]
        [Required(ErrorMessage = "Je třeba vyplnit")]
        [StringLength(maximumLength: 4000, MinimumLength = 3, ErrorMessage = "Rozmezí 3 až 4000 znaků")]
        public string MessageBody { get; set; }
    }
}
