using System.Collections.Generic;


namespace MicroORM.Interface
{
    public interface ICRUD<T>  where T : class, new()
    {
        List<T> GetByColumName(string columName, object value);
        T GetByColumNameFist(string columName, object value);
        int Insert(T t);
        bool Update(T t, int id);
        bool Delet(int id);
        int RowCount();
        int RowCountWithSrc(string srcClm, string srcValue);
        List<T> getFromTo(int from, int to);
        List<T> getFromToWithSrc(int from, int to,string srcClm, string srcValue);
    }
}
