﻿using System.Collections.Generic;
using System.Data.Common;

namespace MicroORM.Interface
{
    public interface ICRUD<T>  where T : class, new()
    {
        (int, bool) Insert(T t, DbTransaction transaction = null);
        (int, bool) Insert<M>(M t, DbTransaction transaction = null) where M : class, new();

        bool Delet(int id);
        bool Delet<M>(int id) where M : class, new();

        (List<T>, bool) GetByColumName(string columName, object value);
        (List<M>, bool) GetByColumName<M>(string columName, object value) where M : class, new();

        (T, bool) GetByColumNameFist(string columName, object value);
        (M, bool) GetByColumNameFist<M>(string columName, object value) where M : class, new();

        (List<T>, bool) GetAll(string[] column = null);
        (List<M>, bool) GetAll<M>(string[] column = null) where M : class, new();

        bool Update(T t, int id);
        bool Update<M>(M t, int id) where M : class, new();

        bool Update(string[] columns, object[] values, int id);
        bool Update<M>(string[] columns, object[] values, int id) where M : class, new();


        //(int, bool) RowCount();
        //(int, bool) RowCountWithSrc(string srcClm, string srcValue);
        //(List<T>, bool) GetFromTo(int from, int to);
        //(List<T>, bool) GetFromToWithSrc(int from, int to, string srcClm, string srcValue);
        
    }
}
