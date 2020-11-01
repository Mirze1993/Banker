using System;
using System.Collections.Generic;
using System.Text;

namespace MicroORM.Interface
{
    public interface IQuery<T>
    {
        string Delete(string id);
        string GetAll(params string[] column);
        /// <summary>
        /// sqlParametr @Id
        /// </summary>
        /// <returns></returns>
        string GetByColumName(string columName);
        string getFromTo(int from, int to);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="srcClm"> sqlParametr @srcClm</param>
        /// <returns></returns>
        string getFromToWithSrc(int from, int to, string srcClm);
        /// <summary>
        /// columnNames are SqlParametr
        /// </summary>
        /// <returns></returns>
        string Insert();
        string RowCount();
        /// <summary>
        /// @srcClm is SlqParametr
        /// </summary>
        /// <param name="srcClm">@srcClm is SlqParametr </param>
        /// <returns></returns>
        string RowCountWithSrc(string srcClm);
        /// <summary>
        /// @colms are sqlParametr
        /// </summary>
        /// <param name="id"></param>
        /// <param name="colms">@colms are sqlParametr</param>
        /// <returns></returns>
        string Update(string id, params string[] colms);

        
    }
}
