using MicroORM.Interface;
using Models.DBModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIRest.Repository
{
    public interface IAppUserRepositoty:ICRUD<AppUsers>
    {
        (AppUsers, string) CheckUser(string email,string password);
        List<UserClaims> GetUserRoles(int userId);
    }
    
}
