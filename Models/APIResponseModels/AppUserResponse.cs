using Models.DBModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.APIResponseModels
{
    public class AppUserResponse:ResponseBase
    {
        public AppUsers User { get; set; }
        public List<UserClaims> UserClaims { get; set; }
        public string Token { get; set; }
    }
}
