using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Inistances
{
    public class Pos_Ins_User
    {
        public int Id { get; set; }
        public int ProsessId { get; set; }
        public string Role { get; set; }
        public int UserId { get; set; }
        public string PosesName { get; set; }
        public string Step { get; set; }
    }
}
