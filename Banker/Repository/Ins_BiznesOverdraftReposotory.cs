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
        public Ins_BiznesOverdraft GetById(int id)
        {
            var (t, b) = base.GetByColumNameFist("Id", id);
            if (t == null) return null;
            if (!string.IsNullOrEmpty(t.DovruyelerJson)) t.Dovruyeler = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Ins_BiznesOverdraft_DovrueList>>(t.DovruyelerJson);
            return t;

        }

        public int GetPosInsId(int prosessId)
        {
            var id = 0;
            using (var comander=DBContext.CreateCommander())
            {
                var (i,b)=comander.Scaller(commandText: $"Select Id from Pos_Ins_User where ProsessId={prosessId}");
                if (b) id= Convert.ToInt32(i);
                return int.Parse(id.ToString());
            }
        }

    }
}
