using Models.Inistances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.UIModel
{
    public class UIProsInfo
    {
        public List<Pos_Ins_User> AssingePosision { get; set; }
        public List<Pos_Ins_User> AssingeMe { get; set; }
        public UIProsInfo()
        {
            AssingePosision = new List<Pos_Ins_User>();
            AssingeMe = new List<Pos_Ins_User>();
        }
    }
}
