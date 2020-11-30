﻿
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;


namespace MicroORM
{
    public abstract class CommanderBase : IDisposable
    {
        protected DbConnection connection;
        public DbDataReader reader;
        protected DbCommand command;
        protected DbTransaction transaction;

        protected string connectionString;



        public abstract List<DbParameter> SetParametrs<T>(T t);


        public abstract DbParameter SetParametr(string paramName, object value);


        protected void ConnectionOpen()
        {
            if (connection.State != ConnectionState.Open) connection.Open();
        }



        public void TransactionStart()
        {
            transaction = connection.BeginTransaction(IsolationLevel.Serializable);
        }

        public void TransactionCommit()
        {
            if (transaction == null) return;
            transaction?.Commit();
            transaction?.Dispose();
        }


        public void TransactionComand(string commandText, List<DbParameter> parameters)
        {
            if (transaction == null) return;
            command.Transaction = transaction;
            var b = NonQuery(commandText, parameters);
            if (b) return;
            transaction.Rollback();
            transaction.Dispose();
            LogManager.GetLogger(GetType().Name).Error("Transaction   roleback");
        }
        public void TransactionComand(CommandType type, string commandText, List<DbParameter> parameters)
        {
            if (transaction == null) return;
            command.Transaction = transaction;
            var b = NonQuery(type, commandText, parameters);
            if (b) return;
            transaction.Rollback();
            transaction.Dispose();
            LogManager.GetLogger(GetType().Name).Error("Transaction   roleback");
        }


        protected void CommandStart(string commandText, List<DbParameter> parameters = null)
        {

            command = connection.CreateCommand();
            command.CommandText = commandText;
            if (parameters != null) command.Parameters.AddRange(parameters.ToArray());
        }

        protected void CommandStart(CommandType commandType, string commandText, List<DbParameter> parameters = null)
        {
            CommandStart(commandText, parameters);
            command.CommandType = commandType;
        }



        //nonquery
        public bool NonQuery(string commandText, List<DbParameter> Parameters = null)
        {

            CommandStart(commandText, Parameters);
            ConnectionOpen();
            bool b = false;
            try
            {
                b = command.ExecuteNonQuery() > 0;
            }
            catch (Exception e)
            {
                LogManager.GetLogger(GetType().Name).Error(e.Message);
            }
            return b;
        }

        public bool NonQuery(CommandType commandType, string commandText, List<DbParameter> Parameters = null)
        {

            CommandStart(commandType, commandText, Parameters);
            ConnectionOpen();
            bool b = false;
            try
            {
                b = command.ExecuteNonQuery() > 0;
            }
            catch (Exception e)
            {
                LogManager.GetLogger(GetType().Name).Error(e.Message);
            }
            return b;
        }



        //Scaller
        public object Scaller(string commandText, List<DbParameter> parameters = null)
        {

            CommandStart(commandText, parameters);
            ConnectionOpen();
            object b = null;
            try
            {
                b = command.ExecuteScalar();
            }
            catch (Exception e)
            {
                LogManager.GetLogger(GetType().Name).Error(e.Message);
            }
            return b;
        }

        public object Scaller(string commandText, CommandType type, List<DbParameter> parameters = null)
        {

            CommandStart(type, commandText, parameters);
            ConnectionOpen();
            object b = null;
            try
            {
                b = command.ExecuteScalar();
            }
            catch (Exception e)
            {
                LogManager.GetLogger(GetType().Name).Error(e.Message);
            }
            return b;
        }



        //reader
        public T Reader<T>(Func<DbDataReader, T> readMetod, string commandText, List<DbParameter> parameters = null)
        {

            CommandStart(commandText, parameters);
            ConnectionOpen();
            try
            {
                reader = command.ExecuteReader();
            }
            catch (Exception e)
            {
                LogManager.GetLogger(GetType().Name).Error(e.Message);
                return default(T);
            }
            var r = readMetod(reader);
            return r;
        }

        public T Reader<T>(Func<DbDataReader, T> readMetod, string commandText, CommandType type, List<DbParameter> parameters = null)
        {
            CommandStart(type, commandText, parameters);
            ConnectionOpen();
            try
            {
                reader = command.ExecuteReader();
            }
            catch (Exception e)
            {
                LogManager.GetLogger(GetType().Name).Error(e.Message); return default(T);
            }
            var r = readMetod(reader);
            return r;
        }


        public List<T> Reader<T>(string commandText, List<DbParameter> parameters = null) where T : class, new()
        {
            return Reader(GetList<T>, commandText, parameters);
        }

        public T ReaderFist<T>(string commandText, List<DbParameter> parameters = null) where T : class, new()
        {
            return Reader(GetFist<T>, commandText, parameters);
        }

        public List<T> Reader<T>(string commandText, CommandType type, List<DbParameter> parameters = null) where T : class, new()
        {
            return Reader(GetList<T>, commandText, type, parameters);
        }

        public T ReaderFist<T>(string commandText, CommandType type, List<DbParameter> parameters = null) where T : class, new()
        {
            return Reader(GetFist<T>, commandText, type, parameters);
        }

        protected List<T> GetList<T>(DbDataReader r) where T : class, new()
        {
            List<T> list = new List<T>();
            if (r == null) return list;

            while (r.Read())
            {
                T t = new T();
                foreach (var item in typeof(T).GetProperties())
                {
                    try
                    {
                        var value = r[item.Name];
                        item.SetValue(t, value);
                    }
                    catch { }
                }
                list.Add(t);
            }
            return list;
        }

        protected T GetFist<T>(DbDataReader r) where T : class, new()
        {
            if (r == null) return null;
            if (!r.HasRows) return null;
            T t = new T();
            while (r.Read())
            {
                foreach (var item in typeof(T).GetProperties())
                {
                    try
                    {
                        var value = r[item.Name];
                        item.SetValue(t, value);
                    }
                    catch { }
                }
                break;
            }
            return t;
        }


        public void Dispose()
        {
            if (reader != null)
                if (!reader.IsClosed) reader.Close();
            command?.Dispose();
            transaction?.Dispose();
            if (connection == null) return;
            if (connection.State != ConnectionState.Closed) connection.Close();
            connection.Dispose();
        }

    }
}
