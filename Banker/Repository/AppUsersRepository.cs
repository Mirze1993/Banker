using Banker.Model;
using Banker.Tools;
using MicroORM;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Banker.Repository
{
    public class AppUsersRepository : CRUD<AppUsers>, IAppUserRepositoty
    {
        public (AppUsers, bool) CheckUser(string email, string password)
        {
            var (user,b) = GetByColumNameFist("Email", email);
            if (user==null||!b) return (null, b);
            b = new HashCreate().VerfiyPassword(password, user.Password);
            return (b ? user : null, b);
        }

        public override (int, bool) Insert(AppUsers t, DbTransaction transaction = null)
        {
            using var comander = new DBContext().CreateCommander();
            var p = comander.SetParametrs(t);
            var result = comander.SetOutputParametr();
            p.Add(result);
            var b=comander.NonQuery("InsertAppUser",parameters:p,commandType:System.Data.CommandType.StoredProcedure);
            var r=(int)result.Value;
            return (r, b); 
        }

        public List<UserClaims> GetUserRoles(int userId)
        {
            using var comander = new DBContext().CreateCommander();
            var (claims, b) = comander.Reader<UserClaims>($"Select * from UserClaims c where c.UserId={userId} and c.Type='{System.Security.Claims.ClaimTypes.Role}'");
            return claims;
        }

    }
}
