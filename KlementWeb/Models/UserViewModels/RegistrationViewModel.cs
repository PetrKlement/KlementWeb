using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KlementWeb.Models.UserViewModels
{
    public class RegistrationViewModel
    {
        [Display(Name = "Heslo")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Musíte zadat heslo")]
        [StringLength(100, ErrorMessage = "Heslo musí být alespoň 10 znaků dlouhé", MinimumLength = 10)]
        public string Password { get; set; }

        [Display(Name = "Znovu heslo")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Heslo je povinné")]
        [Compare("Password", ErrorMessage = "Heslo se neshoduje")]
        public string ConfirmPassword { get; set; }
       
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Musíte zadat email")]
        public string Email { get; set; }

    }
}
