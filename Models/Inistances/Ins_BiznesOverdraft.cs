using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CommonTool;
using Models.ProsessObjects;

namespace Models.Inistances
{
    public class Ins_BiznesOverdraft : Ins_Base
    {
        public string Name { get; set; }
        public float Amount { get; set; }
        public string Cif { get; set; }
        public string Voen { get; set; }
        public Ins_BiznesOverdraft_Enum_DovruyyeNovu DovruyyeNovu { get; set; }
        public Ins_BiznesOverdraft_Enum_Aylar Aylar { get; set; }
        public int BranchId { get; set; }
        public float Rate { get; set; }

        [DbMaping(DbMap.noMaping)]
        public Branch Branch { get; set; }
        [DbMaping(DbMap.noMaping)]
        public List<Ins_BiznesOverdraft_DovrueList> Dovruyeler;

    }

    public enum Ins_BiznesOverdraft_Enum_Aylar
    {
        [Display(Name = "3 aydan az")]
        UcAydanAz,
        [Display(Name = "3 və 6 ay arası")]
        UcVeAltiAy,
        [Display(Name = "6 aydan çox")]
        alyiAycox      
    }

    public enum Ins_BiznesOverdraft_Enum_DovruyyeNovu
    {        
        PostTerminal,        
        DaxilOlma,        
    }
}
