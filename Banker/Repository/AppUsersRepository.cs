using Banker.Model;
using Banker.Tools;
using MicroORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.Repository
{
    public class AppUsersRepository : CRUD<AppUsers>, IAppUserRepositoty
    {
        public (AppUsers, bool) CheckUser(string email, string password)
        {
            var user = GetByColumName("Email", email);
            if (user.Count < 1) return (null, false);
            var b = new HashCreate().VerfiyPassword(password, user.FirstOrDefault().Password);
            return (b ? user.FirstOrDefault() : null, b);
        }
    }
}
