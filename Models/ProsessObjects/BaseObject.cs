using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ProsessObjects
{
    public class BaseObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DataCreateId { get; set; }
        public int DataModifeId { get; set; }
    }
}
