using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KlementWeb.Models.UserViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Heslo")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Zadejte heslo")]
        public string Password { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Zadejte email")]
        public string Email { get; set; }
        
        [Display(Name = "Pamatovat")]
        public bool RememberMe { get; set; }
    }
}
