using Models.Inistances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.UIModel
{
    public class UIReprotProsess
    {
        public string ProsesName { get; set; }
        public int? Id { get; set; }
        public int? InitiatorId { get; set; }
        public int? ResponsibleId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public List<Ins_Base> Response { get; set; }
        public UIReprotProsess()
        {
            Response = new List<Ins_Base>();
        }
    }
}
