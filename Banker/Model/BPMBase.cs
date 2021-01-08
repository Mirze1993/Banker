using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.Model
{
    public class BPMBase
    {
        public int Id { get; set; }
        public string Initiator { get; set; }
        public string Responsible { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
    }

    public static class ProcessStatus
    {
        public static string Active = "Active";
        public static string Deactive = "Deactive";

    }
}
