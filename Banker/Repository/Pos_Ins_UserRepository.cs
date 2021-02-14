using MicroORM;

using Models.Inistances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.Repository
{
    public class Pos_Ins_UserRepository : CRUD<Pos_Ins_User>
    {
        public List<Pos_Ins_User> GetPosInsUser(List<string> roles)
        {
            var prList = new List<System.Data.Common.DbParameter>();
            using (var commander = DBContext.CreateCommander())
            {
                for (int i = 0; i < roles.Count; i++)
                {
                    prList.Add(commander.SetParametr("Role" + i.ToString(), roles[i]));
                }
                return commander.Reader<Pos_Ins_User>(commandText: "ActivePosInsByRoles",
                   commandType: System.Data.CommandType.StoredProcedure,
                   parameters: prList
                   ).Item1;
            }
        }

        public List<Ins_Base> GetProsess(UIModel.UIReprotProsess uIReprot)
        {
            var prList = new List<System.Data.Common.DbParameter>();

            using (var commander = DBContext.CreateCommander())
            {
                prList.Add(commander.SetParametr("ProsesName", "Ins_"+uIReprot.ProsesName));
                if (uIReprot.InitiatorId.HasValue) 
                    prList.Add(commander.SetParametr("InitiatorId", uIReprot.InitiatorId));
                if (uIReprot.ResponsibleId.HasValue) 
                    prList.Add(commander.SetParametr("ResponsibleId", uIReprot.ResponsibleId));
                if (uIReprot.Id.HasValue) 
                    prList.Add(commander.SetParametr("Id", uIReprot.Id));
                if (uIReprot.StartDate.HasValue)
                    prList.Add(commander.SetParametr("StartDate", uIReprot.StartDate));
                if (uIReprot.EndDate.HasValue)
                    prList.Add(commander.SetParametr("EndDate", uIReprot.EndDate));
                if (!string.IsNullOrEmpty(uIReprot.Status))
                    prList.Add(commander.SetParametr("Status", uIReprot.Status));
                return commander.Reader<Ins_Base>(commandText: "GetProsess",
                    commandType: System.Data.CommandType.StoredProcedure,
                    parameters: prList
                    ).Item1;
            }
        }

    }
}
