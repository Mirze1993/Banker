using Microsoft.AspNetCore.Mvc.Rendering;
using Models.ProsessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.ObjectsSelectListItem
{
    public static class CreateSelectListItems
    {
        public static List<SelectListItem> GetSelectListItems(List<Branch> branches)
        {
            List<SelectListItem> selectListItems=new List<SelectListItem>();
            branches?.ForEach(x => selectListItems.Add(new SelectListItem {
                Text = x.Name, Value = x.Id.ToString()
            }));
            return selectListItems;
        }
       
    }
}
