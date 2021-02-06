using Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.UIModel
{
    public class UIAppUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewConfirmPassword { get; set; }
        public List<UserClaims> userClaims { get; set; }
        public UIAppUser()
        {
            userClaims = new List<UserClaims>();
        }
        
    }
}
