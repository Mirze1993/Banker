using MicroORM.Interface;
using System;
using System.Collections.Generic;
using System.Data.Common;


namespace MicroORM
{
    public abstract class CRUD<T> : ICRUD<T> where T : class, new()
    {
        IQuery<T> query;

        public CRUD()
        {
            query = DBContext.CreateQuary<T>();
        }


        public virtual int Insert(T t)
        {
            string cmtext = query.Insert();

            using (CommanderBase commander = DBContext.CreateCommander())
            {
                var p = commander.SetParametrs(t);
                var id = commander.Scaller(cmtext, p);
                if (id != null) return Convert.ToInt32(id);
                else return 0;
            }
        }

        public virtual bool Delet(int id)
        {
            string cmtext = query.Delete(id.ToString());
            using CommanderBase commander = DBContext.CreateCommander();
            return commander.NonQuery(cmtext);
        }

        public virtual List<T> GetByColumName(string columName, object value)
        {
            string cmtext = query.GetByColumName(columName);
            using (CommanderBase commander = DBContext.CreateCommander())
                return commander.Reader<T>(cmtext, new List<DbParameter>() { commander.SetParametr(columName, value) });

        }

        public virtual T GetByColumNameFist(string columName, object value)
        {
            string cmtext = query.GetByColumName(columName);
            using (CommanderBase commander = DBContext.CreateCommander())
                return commander.ReaderFist<T>(cmtext, new List<DbParameter>() { commander.SetParametr(columName, value) });

        }

        public virtual List<T> GetAll(params string[] column)
        {
            string cmtext = query.GetAll(column);
            using (CommanderBase commander = DBContext.CreateCommander())
                return commander.Reader<T>(cmtext);
        }

        public virtual bool Update(T t, int id)
        {
            string cmtext = query.Update(id.ToString());
            using (CommanderBase commander = DBContext.CreateCommander())
                return commander.NonQuery(cmtext, commander.SetParametrs(t));
        }

        public virtual int RowCount()
        {
            string cmtext = query.RowCount();
            using (CommanderBase commander = DBContext.CreateCommander())
            {
                var o = commander.Scaller(cmtext);
                if (o != null) return Convert.ToInt32(o);
                else return 0;
            }
        }

        public virtual int RowCountWithSrc(string srcClm, string srcValue)
        {
            string cmtext = query.RowCountWithSrc(srcClm);
            using (CommanderBase commander = DBContext.CreateCommander())
            {
                var o = commander.Scaller(cmtext, new List<DbParameter>() { commander.SetParametr(srcClm, srcValue) });

                if (o != null) return Convert.ToInt32(o);
                else return 0;
            }
        }

        public virtual List<T> getFromTo(int from, int to)
        {
            string cmtext = query.getFromTo(from, to);
            using (CommanderBase commander = DBContext.CreateCommander())
                return commander.Reader<T>(cmtext);
        }

        public virtual List<T> getFromToWithSrc(int from, int to, string srcClm, string srcValue)
        {
            string cmtext = query.getFromToWithSrc(from, to, srcClm);
            using (CommanderBase commander = DBContext.CreateCommander())
                return commander.Reader<T>(cmtext, new List<DbParameter> { commander.SetParametr(srcClm, srcValue) });
        }

    }
}
