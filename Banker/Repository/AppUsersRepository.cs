using Banker.Model;
using Banker.Tools;
using MicroORM;
using System;


namespace Banker.Repository
{
    public class AppUsersRepository : CRUD<AppUsers>, IAppUserRepositoty
    {
        public (AppUsers, bool) CheckUser(string email, string password)
        {
            var (user,b) = GetByColumNameFist("Email", email);
            if (user==null||b) return (null, b);
            b = new HashCreate().VerfiyPassword(password, user.Password);
            return (b ? user : null, b);
        }

    }
}
