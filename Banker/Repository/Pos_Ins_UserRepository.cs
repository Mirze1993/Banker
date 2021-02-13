using MicroORM;

using Models.Inistances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.Repository
{
    public class Pos_Ins_UserRepository:CRUD<Pos_Ins_User>
    {
        public List<Pos_Ins_User> getProsess(List<string> roles)
        {
            var query = "select * from Pos_Ins_User where ";
            for (int i = 0; i < roles.Count; i++)
            {
                if((i+1)==roles.Count) query += $"Role='{roles[i]}' ";
                else query += $"Role='{roles[i]}' or ";
            }
            using (var commander=DBContext.CreateCommander())            
                return commander.Reader<Pos_Ins_User>(query).Item1;
        }
    }
}
