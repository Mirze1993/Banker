using MicroORM.Interface;
using System;
using System.Collections.Generic;
using System.Data.Common;


namespace MicroORM
{
    public abstract class CRUD<T> : ICRUD<T> where T : class, new()
    {
        IQuery<T> query;
        public DBContext DBContext { get; set; }

        public CRUD()
        {
            this.DBContext = new DBContext();
            query = DBContext.CreateQuary<T>();

        }


        public virtual (int,bool) Insert(T t,DbTransaction transaction=null)
        {
            string cmtext = query.Insert();

            using (CommanderBase commander = DBContext.CreateCommander())
            {
               
                var p = commander.SetParametrs(t);
                var (id,b) = commander.Scaller(cmtext, parameters:p,transaction:transaction);
                
                if (b&&id != null) return (Convert.ToInt32(id),b);
                else return (0,b);
            }
        }

        public virtual bool Delet(int id)
        {
            string cmtext = query.Delete(id.ToString());
            using CommanderBase commander = DBContext.CreateCommander();
            return commander.NonQuery(cmtext);
        }

        public virtual (List<T>,bool) GetByColumName(string columName, object value)
        {
            string cmtext = query.GetByColumName(columName);
            using (CommanderBase commander = DBContext.CreateCommander())
                return commander.Reader<T>(cmtext, new List<DbParameter>() { commander.SetParametr(columName, value) });

        }

        public virtual (T,bool) GetByColumNameFist(string columName, object value)
        {
            string cmtext = query.GetByColumName(columName);
            using (CommanderBase commander = DBContext.CreateCommander())
                return commander.ReaderFist<T>(cmtext, new List<DbParameter>() { commander.SetParametr(columName, value) });

        }

        public virtual (List<T>, bool) GetAll(params string[] column)
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

        public virtual (int,bool) RowCount()
        {
            string cmtext = query.RowCount();
            using (CommanderBase commander = DBContext.CreateCommander())
            {
                var (o,b) = commander.Scaller(cmtext);
                if (b && o != null) return (Convert.ToInt32(o), b);
                else return (0, b);
            }
        }

        public virtual (int, bool) RowCountWithSrc(string srcClm, string srcValue)
        {
            string cmtext = query.RowCountWithSrc(srcClm);
            using (CommanderBase commander = DBContext.CreateCommander())
            {
                var (o,b) = commander.Scaller(cmtext, new List<DbParameter>() { commander.SetParametr(srcClm, srcValue) });

                if (b && o != null) return (Convert.ToInt32(o), b);
                else return (0, b);
            }
        }

        public virtual (List<T>,bool) GetFromTo(int from, int to)
        {
            string cmtext = query.getFromTo(from, to);
            using (CommanderBase commander = DBContext.CreateCommander())
                return commander.Reader<T>(cmtext);
        }

        public virtual (List<T>, bool) GetFromToWithSrc(int from, int to, string srcClm, string srcValue)
        {
            string cmtext = query.getFromToWithSrc(from, to, srcClm);
            using (CommanderBase commander = DBContext.CreateCommander())
                return commander.Reader<T>(cmtext, new List<DbParameter> { commander.SetParametr(srcClm, srcValue) });
        }

    }
}
