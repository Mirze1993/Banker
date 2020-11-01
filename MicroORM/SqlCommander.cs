
using MicroORM.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace MicroORM
{
    public class SqlCommander : CommanderBase
    {
       
        public override List<DbParameter> SetParametrs<T>(T t)
        {
            List<DbParameter> parametrs = new List<DbParameter>();
            if (t != null)
                foreach (var item in typeof(T).GetProperties())
                {
                    if (item.Name == "Id") continue;
                    object value = item.GetValue(t);
                    if (value == null) value = DBNull.Value;
                    parametrs.Add(new SqlParameter($"@{item.Name}", value));
                }
            return parametrs;
        }

        public override DbParameter SetParametr(string paramName, object value)
        {
            return new SqlParameter($"@{paramName}", value);
        }



        public SqlCommander()
        {
            connectionString = ORMConfig.ConnectionString;
            connection = new SqlConnection(connectionString);
        }


    }
}
