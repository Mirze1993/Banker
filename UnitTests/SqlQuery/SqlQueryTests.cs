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
             t = new Ins_BiznesOverdraftReposotory();
        }

        public Ins_BiznesOverdraftReposotory t { get; }

        [TestMethod()]
        public void InsertTest()
        {
            var (m, b) = t.GetAll();
        }

        [TestMethod()]
        public void InsertTest2()
        {
            var (m, b) = t.GetAll();
        }

        [TestMethod()]
        public void InsertTest3()
        {
            var (m, b) = t.GetAll();
        }

        [TestMethod()]
        public void InsertTest4()
        {
            var (m, b) = t.GetAll();
        }

        [TestMethod()]
        public void InsertTest5()
        {
            var (m, b) = t.GetAll();
        }


    }
}