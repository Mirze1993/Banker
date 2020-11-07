using Banker.Model;
using MicroORM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.Repository
{
    public interface IAppUserRepositoty:ICRUD<AppUsers>
    {
        (AppUsers, bool) CheckUser(string email,string password);
    }
}
