using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.UIModel
{
    public class Login
    {
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parol doldurulmalıdır")]
        [StringLength(255, ErrorMessage = "Parol minumum 5 simbol olmalıdır", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RebemberMe { get; set; }
    }
}
