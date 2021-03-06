﻿using CommonTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Inistances
{
    public class Ins_Base
    {
        public int Id { get; set; }
        public int InitiatorId { get; set; }
        public int ResponsibleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }       
        public string Log { get; set; }
    }

    public static class ProcessStatus
    {
        public static string Active { get; set; } = "Active";
        public static string Deactive { get; set; } = "Deactive";
    }
}
