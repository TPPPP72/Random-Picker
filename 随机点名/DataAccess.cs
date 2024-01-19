using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using Windows.Storage;

namespace 随机点名
{
    public partial class Userdata(int id, string name, string headshot, int time)
    {
        private int _id = id;
        private string _name = name;
        private string _headshot = headshot;
        private int _time = time;
        public int ID { get { return _id; } set { _id = value; } }
        public string NAME { get { return _name; } set { _name = value; } }
        public string HEADSHOT { get { return _headshot; } set { _headshot = value; } }
        public int TIME { get { return _time; } set { _time = value; } }
    }
    public static class DataAccess
    {
        public async static void InitializeDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("MemberList.db", CreationCollisionOption.OpenIfExists);
            using SqliteConnection db = new($"Filename={Path.Combine(ApplicationData.Current.LocalFolder.Path, "MemberList.db")}");
            db.Open();
            string tableCommand = "CREATE TABLE IF NOT EXISTS MemberTable ( ID INTEGER PRIMARY KEY, NAME NVARCHAR(2048) NOT NULL, HEADSHOT NVARCHAR(2048) NOT NULL, TIME INTEGER DEFAULT 0 NOT NULL)";
            SqliteCommand createTable = new(tableCommand, db);
            createTable.ExecuteReader();
        }
        public static void AddData(Userdata user)
        {
            using SqliteConnection db = new($"Filename={Path.Combine(ApplicationData.Current.LocalFolder.Path, "MemberList.db")}");
            db.Open();
            SqliteCommand insertCommand = new()
            {
                Connection = db,
                CommandText = "INSERT INTO MemberTable VALUES (NULL, @Entry1, @Entry2, 0);"
            };
            insertCommand.Parameters.AddWithValue("@Entry1", user.NAME);
            insertCommand.Parameters.AddWithValue("@Entry2", user.HEADSHOT.Length > 0 ? user.HEADSHOT : "ms-appx:///Assets/Headshot.png");
            insertCommand.ExecuteReader();
        }
        public static void AddData(string input)
        {
            using SqliteConnection db = new($"Filename={Path.Combine(ApplicationData.Current.LocalFolder.Path, "MemberList.db")}");
            db.Open();
            SqliteCommand insertCommand = new()
            {
                Connection = db,
                CommandText = "INSERT INTO MemberTable VALUES (NULL, @Entry1, @Entry2, 0);"
            };
            insertCommand.Parameters.AddWithValue("@Entry1", input);
            insertCommand.Parameters.AddWithValue("@Entry2", "ms-appx:///Assets/Headshot.png");
            insertCommand.ExecuteReader();
        }
        public static List<Userdata> GetData()
        {
            List<Userdata> read = new();
            using SqliteConnection db = new($"Filename={Path.Combine(ApplicationData.Current.LocalFolder.Path, "MemberList.db")}");
            db.Open();
            SqliteCommand selectCommand = new("SELECT * from MemberTable", db);
            SqliteDataReader query = selectCommand.ExecuteReader();
            while (query.Read())
            {
                Userdata thi = new(query.GetInt32(0), query.GetString(1), query.GetString(2), query.GetInt32(3));
                read.Add(thi);
            }
            return read;
        }
        public static void UpdateHeadshot(int id, string url)
        {
            using SqliteConnection db = new($"Filename={Path.Combine(ApplicationData.Current.LocalFolder.Path, "MemberList.db")}");
            db.Open();
            SqliteCommand insertCommand = new()
            {
                Connection = db,
                CommandText = "UPDATE MemberTable SET HEADSHOT = @Entry1 WHERE ID = @Entry2;"
            };
            insertCommand.Parameters.AddWithValue("@Entry1", url);
            insertCommand.Parameters.AddWithValue("@Entry2", id);
            insertCommand.ExecuteReader();
        }
        public static void AddTime(int id)
        {
            using SqliteConnection db = new($"Filename={Path.Combine(ApplicationData.Current.LocalFolder.Path, "MemberList.db")}");
            db.Open();
            SqliteCommand insertCommand = new()
            {
                Connection = db,
                CommandText = "UPDATE MemberTable SET TIME = TIME+1 WHERE ID = @Entry1;"
            };
            insertCommand.Parameters.AddWithValue("@Entry1", id);
            insertCommand.ExecuteReader();
        }
        public static void ResetTime(int id)
        {
            using SqliteConnection db = new($"Filename={Path.Combine(ApplicationData.Current.LocalFolder.Path, "MemberList.db")}");
            db.Open();
            SqliteCommand insertCommand = new()
            {
                Connection = db,
                CommandText = "UPDATE MemberTable SET TIME = 0 WHERE ID = @Entry1;"
            };
            insertCommand.Parameters.AddWithValue("@Entry1", id);
            insertCommand.ExecuteReader();
        }
        public static void DeleteUser(int id)
        {
            using SqliteConnection db = new($"Filename={Path.Combine(ApplicationData.Current.LocalFolder.Path, "MemberList.db")}");
            db.Open();
            SqliteCommand Command1 = new()
            {
                Connection = db,
                CommandText = "DELETE FROM MemberTable WHERE ID = @Entry;"
            };
            Command1.Parameters.AddWithValue("@Entry", id);
            Command1.ExecuteReader();
            SqliteCommand Command2 = new()
            {
                Connection = db,
                CommandText = "UPDATE MemberTable SET 'ID'=(ID-1) WHERE ID>@Entry;"
            };
            Command2.Parameters.AddWithValue("@Entry", id);
            Command2.ExecuteReader();
        }
        public static void ClearTable()
        {
            using SqliteConnection db = new($"Filename={Path.Combine(ApplicationData.Current.LocalFolder.Path, "MemberList.db")}");
            db.Open();
            SqliteCommand insertCommand = new()
            {
                Connection = db,
                CommandText = "DELETE FROM MemberTable;"
            };
            insertCommand.ExecuteReader();
        }
    }
}
