using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ProsessObjects
{
    public class Branch:BaseObject
    {
        public string BranchCode { get; set; }
        public string Adress { get; set; }
        public int DirectorId { get; set; }
    }
}
