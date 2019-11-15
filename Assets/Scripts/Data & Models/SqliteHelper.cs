using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Data.Sqlite;
using UnityEngine;
using System.Data;

namespace Database
{
    public class SqliteHelper
    {
        private const string dbName = "fitmi_plus";

        public string dbPath;
        public IDbConnection dbConnection;

        public SqliteHelper()
        {
            dbPath = "URI=file:" + Application.persistentDataPath + "/" + dbName;
            Debug.Log("dbPath" + dbPath);
            dbConnection = new SqliteConnection(dbPath);
            dbConnection.Open();
        }

        ~SqliteHelper()
        {
            dbConnection.Close();
        }

        // Virtual methods (for overriding)
        public virtual IDataReader getDataById(int id)
        {
            Debug.Log("This function is not implemnted");
            throw null;
        }

        public virtual IDataReader getDataByString(string str)
        {
            Debug.Log("This function is not implemnted");
            throw null;
        }

        public virtual void deleteDataById(int id)
        {
            Debug.Log("This function is not implemented");
            throw null;
        }

        public virtual void deleteDataByString(string id)
        {
            Debug.Log("This function is not implemented");
            throw null;
        }

        public virtual IDataReader getAllData()
        {
            Debug.Log("This function is not implemented");
            throw null;
        }

        public virtual void deleteAllData()
        {
            Debug.Log("This function is not implemnted");
            throw null;
        }

        public virtual IDataReader getNumOfRows()
        {
            Debug.Log("This function is not implemnted");
            throw null;
        }

        // Helper methods
        public IDbCommand getDbCommand()
        {
            return dbConnection.CreateCommand();
        }

        public IDataReader getAllData(string table_name)
        {
            IDbCommand cmd = dbConnection.CreateCommand();
            cmd.CommandText = "SELECT * FROM " + table_name;
            return cmd.ExecuteReader();
        }

        public void deleteAllData(string table_name)
        {
            IDbCommand cmd = dbConnection.CreateCommand();
            cmd.CommandText = "DROP TABLE IF EXISTS " + table_name;
            cmd.ExecuteNonQuery();
        }

        public IDataReader getNumOfRows(string table_name)
        {
            IDbCommand cmd = dbConnection.CreateCommand();
            cmd.CommandText = "SELECT COALESCE(MAX(id)+1, 0) FROM " + table_name;
            return cmd.ExecuteReader();
        }

        public void close()
        {
            dbConnection.Close();
        }
    }
}