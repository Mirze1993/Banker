using MicroORM.Interface;
using MicroORM.SqlQuery;

namespace MicroORM
{
    public class DBContext
    {

        public CommanderBase CreateCommander()
        {
            CommanderBase commander = null; ;
            switch (ORMConfig.DbType)
            {
                case DbType.MSSQL:
                    commander = new SqlCommander();
                    break;
                case DbType.Oracle:
                    commander = new OracleCommander();
                    break;
                default:
                    break;
            }
            return commander;
        }

        public  IQuery<T> CreateQuary<T>()
        {
            IQuery<T> query = null;
            switch (ORMConfig.DbType)
            {
                case DbType.MSSQL:
                    query = new SqlQuery<T>();
                    break;
                case DbType.Oracle:
                    query = new SqlQuery<T>();
                    break;
                default:
                    break;
            }
            return query;
        }

    }
}
