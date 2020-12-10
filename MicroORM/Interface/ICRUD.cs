using System.Collections.Generic;
using System.Data.Common;

namespace MicroORM.Interface
{
    public interface ICRUD<T>  where T : class, new()
    {
        (int, bool) Insert(T t, DbTransaction transaction=null);


        bool Delet(int id);


        (List<T>, bool) GetByColumName(string columName, object value);


        (T, bool) GetByColumNameFist(string columName, object value);


        (List<T>, bool) GetAll(params string[] column);


        bool Update(T t, int id);


        (int, bool) RowCount();


        (int, bool) RowCountWithSrc(string srcClm, string srcValue);


        (List<T>, bool) GetFromTo(int from, int to);


        (List<T>, bool) GetFromToWithSrc(int from, int to, string srcClm, string srcValue);
        
    }
}
