using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Inistances
{
    public class Ins_BiznesOverdraft_DovrueList
    {
        public int Id { get; set; }
        public int Ins_BiznesOverdraftId { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
