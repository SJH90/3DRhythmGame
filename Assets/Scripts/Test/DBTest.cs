// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Mono.Data.Sqlite;
// using System.Data;
// using System;

// public class Test : MonoBehaviour
// {

//     // Use this for initialization
//     void Start()
//     {
//         string conn = "URI=file:" + Application.dataPath + "/testdb.db"; //Path to database.
//         IDbConnection dbconn;
//         dbconn = (IDbConnection)new SqliteConnection(conn);
//         dbconn.Open(); //Open connection to the database.
//         IDbCommand dbcmd = dbconn.CreateCommand();
//         string sqlQuery = "SELECT id,name FROM test";
//         dbcmd.CommandText = sqlQuery;
//         IDataReader reader = dbcmd.ExecuteReader();
//         while (reader.Read())
//         {
//             int id = reader.GetInt32(0);
//             string name = reader.GetString(1);

//             Debug.Log("id= " + id + "  name =" + name);
//         }
//         reader.Close();
//         reader = null;
//         dbcmd.Dispose();
//         dbcmd = null;
//         dbconn.Close();
//         dbconn = null;

//     }

// }
