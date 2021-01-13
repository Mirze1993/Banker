using CommonTool;
using MicroORM.Interface;
using System;


namespace MicroORM.SqlQuery
{
    public class SqlQuery<T> : IQuery<T>
    {
        Type GetTypeT => typeof(T);

        public string Delete(string id)
        {
            return $"DELETE FROM {GetTypeT.Name} WHERE Id ={id} ";
        }

        public string GetAll(params string[] column)
        {
            string query = "";
            if (column.Length > 0)
            {
                string clm = "";
                foreach (var item in column)
                {
                    clm += item + ",";
                }
                clm = clm.Remove(clm.Length - 1);
                query = $"SELECT {clm}  FROM {GetTypeT.Name}";
            }
            else query = $"SELECT *  FROM {GetTypeT.Name}";
            return query;
        }
        /// <summary>
        /// sqlParametr @Id
        /// </summary>
        /// <returns></returns>
        public string GetByColumName(string columName)
        {
            return $"SELECT * FROM {GetTypeT.Name} WHERE {columName} =@{columName}";
        }

        public string getFromTo(int from, int to)
        {
            return "select * from " +
                   "  (select Row_Number() over" +
                   $"  (order by Id) as RowIndex, * from {GetTypeT.Name}) as Sub" +
                   $"  Where Sub.RowIndex >= {from} and Sub.RowIndex <= {to}";

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="srcClm"> sqlParametr @srcClm</param>
        /// <returns></returns>
        public string getFromToWithSrc(int from, int to, string srcClm)
        {
            return "select * from " +
                   "  (select Row_Number() over" +
                   $" (order by Id) as RowIndex, * from {GetTypeT.Name} " +
                   $" Where {srcClm} like '@{srcClm}%') as Sub" +
                   $" Where Sub.RowIndex >= {from} and Sub.RowIndex <= {to}";

        }

        /// <summary>
        /// columnNames are SqlParametr
        /// </summary>
        /// <returns></returns>
        public string Insert()
        {
            string columns = "";
            string values = "";
            foreach (var item in GetTypeT.GetProperties())
            {
                var t = (DbMapingAttribute)Attribute.GetCustomAttribute(item, typeof(DbMapingAttribute));
                if (t != null) if (t.Map == DbMap.noMaping) continue;

                if (item.Name == "Id") continue;
                columns += $"{item.Name} ,";
                values += $"@{item.Name} ,";
            }
            //last "," remove
            if (!String.IsNullOrEmpty(columns) && !String.IsNullOrEmpty(values))
            {
                columns = columns.Remove(columns.Length - 1);
                values = values.Remove(values.Length - 1);
                if (columns.Split(",").Length != values.Split(",").Length) return "";
                return $"INSERT INTO {GetTypeT.Name} ({columns}) VALUES({values}) " +
                    $" ;SELECT CAST(scope_identity() AS int)";
            }
            return "";
        }

        public string RowCount()
        {
            return $"Select count (*) from {GetTypeT.Name}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcClm">@srcClm is SlqParametr </param>
        /// <returns></returns>
        public string RowCountWithSrc(string srcClm)
        {
            return $"Select count (*) from {GetTypeT.Name} u " +
                $"Where u.{srcClm} like '@{srcClm}'+'%'";

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="colms">@colms are sqlParametr</param>
        /// <returns></returns>
        public string Update(string id, params string[] colms)
        {
            string columns = "";
            if (colms.Length > 0)
                foreach (var item in colms)
                {
                    if (item == "Id") continue;
                    columns += $"{item}=@{item} ,";
                }
            else
                foreach (var item in GetTypeT.GetProperties())
                {
                    var t = (DbMapingAttribute)Attribute.GetCustomAttribute(item, typeof(DbMapingAttribute));
                    if (t != null) if (t.Map == DbMap.noMaping) continue;
                    if (item.Name == "Id") continue;
                    columns += $"{item.Name}=@{item.Name} ,";
                }
            if (!String.IsNullOrEmpty(columns))
            {
                columns = columns.Remove(columns.Length - 1);
                return $"UPDATE  {GetTypeT.Name} set {columns} Where Id={id}";
            }
            return "";
        }
    }
}
