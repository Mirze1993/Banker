using MicroORM;
using Models.Inistances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.Repository
{
    public class Ins_BiznesOverdraftReposotory : CRUD<Ins_BiznesOverdraft>
    {
        public Ins_BiznesOverdraft GetByIdJoinUsers(int id)
        {
            using (var commander = DBContext.CreateCommander())
            {
                var (t,b) = commander.ReaderFist<Ins_BiznesOverdraft>(
                    commandText: "SelectIns_BiznesOverdraftById",
                    parameters: new List<System.Data.Common.DbParameter>() { commander.SetParametr("Id", id) },
                    commandType: System.Data.CommandType.StoredProcedure
                    );
                if (t.BranchId > 0)
                    t.Branch = base.GetByColumNameFist<Models.ProsessObjects.Branch>("Id", t.BranchId).Item1;
                if (!string.IsNullOrEmpty(t.DovruyelerJson)) t.Dovruyeler = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Ins_BiznesOverdraft_DovrueList>>(t.DovruyelerJson);
                return t;                
            }            
        }

    }
}
