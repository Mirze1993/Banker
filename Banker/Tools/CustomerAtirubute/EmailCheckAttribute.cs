using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.Tools.CustomerAtirubute
{
    public class EmailCheckAttribute:ValidationAttribute
    {
        public EmailCheckAttribute(string[] email)
        {
            Email = email;
        }

        public string[] Email { get; }

        public override bool IsValid(object value)
        {
            var mail = value.ToString().Split("@");
            foreach (var item in Email)            
                if (mail[1] == item) return true;
            
            return false;
        }
    }
}
