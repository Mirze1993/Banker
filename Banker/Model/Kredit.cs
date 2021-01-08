using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.Model
{
    public class Kredit:BPMBase
    {
        public string Name { get; set; }
        public double Amount { get; set; }       
    }
}
