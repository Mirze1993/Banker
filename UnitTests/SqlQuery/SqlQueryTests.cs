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
        public SqlQueryTests()
        {
            MicroORM.ORMConfig.ConnectionString = "Server=.\\SQLExpress;Database=Banker;Integrated Security=true;";
            MicroORM.ORMConfig.DbType = MicroORM.DbType.MSSQL;
             
        }

       

        [TestMethod()]
        public void InsertTest()
        {
            var t = new Ins_BiznesOverdraftReposotory();
            var (m, b) = t.GetAll();
            Console.WriteLine(m.Count);
        }

        [TestMethod()]
        public void InsertTest2()
        {
            var t = new Ins_BiznesOverdraftReposotory();
            var (m, b) = t.GetAll();
            Console.WriteLine(m.Count);
        }

        [TestMethod()]
        public void InsertTest3()
        {
            var t = new Ins_BiznesOverdraftReposotory();
            var (m, b) = t.GetAll();
            Console.WriteLine(m.Count);
        }

        [TestMethod()]
        public void InsertTest4()
        {
            var t = new Ins_BiznesOverdraftReposotory();
            var (m, b) = t.GetAll();
            Console.WriteLine(m.Count);
        }

        [TestMethod()]
        public void InsertTest5()
        {
            var t = new Ins_BiznesOverdraftReposotory();
            var (m, b) = t.GetAll();
            Console.WriteLine(m.Count);
        }


    }
}