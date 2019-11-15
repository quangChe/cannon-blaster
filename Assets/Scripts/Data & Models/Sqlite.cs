using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class Sqlite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Create database path
        string path = "URI=file:" + Application.persistentDataPath + "/GameDatabase";

        // Connect to database
		IDbConnection db = new SqliteConnection(path);
		db.Open();

        // Initiate, build command and execute a create table query
		IDbCommand createTableCmd = db.CreateCommand();
		string createTableQuery = "CREATE TABLE IF NOT EXISTS fitmi_test (id INTEGER PRIMARY KEY, val INTEGER)";
		createTableCmd.CommandText = createTableQuery;
		createTableCmd.ExecuteReader();

        // Initiate, build command and execute an insertion query
		IDbCommand insertCmd = db.CreateCommand();
		string insertQuery = "INSERT INTO fitmi_test (id, val) VALUES(12, 7)";
		insertCmd.CommandText = insertQuery;
		insertCmd.ExecuteNonQuery();

        // Initiate, build command and execute a read query. Then store results in data reader.
		IDbCommand readCmd = db.CreateCommand();
		IDataReader reader;
		string readQuery = "SELECT * FROM fitmi_test";
		readCmd.CommandText = readQuery;
		reader = readCmd.ExecuteReader();


        // Log from the data reader
        while (reader.Read())
		{
			Debug.Log("id:" + reader[0].ToString());
			Debug.Log("val:" + reader[1].ToString());
		}

        // Close connection
		db.Close();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
