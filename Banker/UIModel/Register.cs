using Banker.Tools.CustomerAtirubute;
using System;

using System.ComponentModel.DataAnnotations;

namespace Banker.UIModel
{
    public class Register
    {
        [EmailAddress]
        [EmailCheck(new string[2] {"a.az","gamil.com" })]
        public string Email { get; set; }
        public string Name { get; set; }

        [Required(ErrorMessage = "Parol doldurulmalıdır")]
        [StringLength(255, ErrorMessage = "Parol minumum 5 simbol olmalıdır", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Parol doldurulmalıdır")]        
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Parol və təkrar parol eyni deyil")]
        public string ConfirmPassword { get; set; }
    }
}
