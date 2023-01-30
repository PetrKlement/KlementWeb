using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KlementWeb.Models.UserViewModels
{
    public class ChangePasswordViewModel
    {
        [Display(Name = "Staré Heslo")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Musíte zadat staré heslo")]
        public string OldPassword { get; set; }

        [Display(Name = "Nové heslo")]
        [Required(ErrorMessage = "Musíte zadat nové heslo")]
        [StringLength(100, ErrorMessage = "Heslo musí mít nejnéně 10 znaků ", MinimumLength = 10)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Znovu nového heslo")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Heslo nejsou stejná")]
        public string ConfirmPassword { get; set; }
    }
}
