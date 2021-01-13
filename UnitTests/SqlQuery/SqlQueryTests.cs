using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicroORM.SqlQuery;
using System;
using System.Collections.Generic;
using System.Text;
using Banker.Repository;
using Models.Inistances;

namespace MicroORM.SqlQuery.Tests
{
    

    [TestClass()]
    public class SqlQueryTests
    {
        [TestMethod()]
        public void InsertTest()
        {
            MicroORM.ORMConfig.ConnectionString = "Server=.\\SQLExpress;Database=Banker;Integrated Security=true;";
            MicroORM.ORMConfig.DbType = MicroORM.DbType.MSSQL;
            var t = new Ins_BiznesOverdraftReposotory();
            var (m, b) = t.GetByColumNameFist("Id", 1);
            Console.WriteLine(m.DovruyyeNovu.ToString());
            
            //var a =Enum.Parse( typeof(Ins_BiznesOverdraft_Enum_Aylar), "2");
            //Console.WriteLine(a);
        }
    }
}