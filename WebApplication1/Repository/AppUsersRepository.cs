using CommonTool;
using MicroORM;
using Models.DBModel;
using System.Collections.Generic;
using System.Data.Common;

namespace WebApplication1.Repository
{
    public class AppUsersRepository : CRUD<AppUsers>, IAppUserRepositoty
    {
        public (AppUsers, string) CheckUser(string email, string password)
        {
            var (user,b) = GetByColumNameFist("Email", email);
            if (!b) return (null, "Error");
            if (user == null)return (null, "User Not Found");
            b = new HashCreate().VerfiyPassword(password, user.Password);
            if (!b) return (null, "Password incorrect");
            return (user , "Success");
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
