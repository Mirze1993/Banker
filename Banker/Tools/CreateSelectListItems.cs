using Banker.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.DBModel;
using Models.ProsessObjects;
using System;
using System.Collections.Generic;


namespace Banker.Tools
{
    public static class CreateSelectListItems
    {
        public static List<SelectListItem> ObjectToSelectList<T>() where T:BaseObject,new()
        {
            List<SelectListItem> selectListItems=new List<SelectListItem> {new SelectListItem()};
            var (branches, b) = new Pos_Ins_UserRepository().GetAll<T>("Id", "Name");
            branches?.ForEach(x => selectListItems.Add(new SelectListItem {
                Text = x.Name, 
                Value = x.Id.ToString()
            }));
            return selectListItems;
        }
        public static List<SelectListItem> UserToSelectList()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem> { new SelectListItem() };
            var (u,b)=new Pos_Ins_UserRepository().GetAll<AppUsers>("Id", "Email");
            u?.ForEach(x => selectListItems.Add(new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Email
            }));
            return selectListItems;
        }

        public static List<SelectListItem> GetSelectListItems(List<string> item)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem> { new SelectListItem() };
            item?.ForEach(x => selectListItems.Add(new SelectListItem
            {
                Text = x,
                Value = x
            }));
            return selectListItems;
        }
        /// <summary>
        /// only static class
        /// </summary>
        /// <param name="t">static class</param>
        /// <returns></returns>
        public static List<SelectListItem> PropToToSelectListItems(Type t) 
        {
            List<SelectListItem> selectListItems = new List<SelectListItem> { new SelectListItem() };
            foreach (var item in t.GetProperties())
            {
                selectListItems.Add(new SelectListItem
                {
                    Text = item.GetValue(null,null).ToString(),
                    Value = item.GetValue(null, null).ToString()
                }); 
            }           
            return selectListItems;
        }

    }
}
