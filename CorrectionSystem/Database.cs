using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CorrectionSystem
{
    public class Database
    {


        /*
        static string databaseName = "CorrectionSystem.db";
        static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string databasePath = System.IO.Path.Combine(folderPath, databaseName);
        */

        //public static SqliteConnection conn = new SqliteConnection(@"Data Source=.\CorrectionSystem.db;Version=3;");
        //public static SqliteConnection conn = new SqliteConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString.s3db"].ConnectionString);

        private static string LoadConnectionString(string id = "Default")
        { return ConfigurationManager.ConnectionStrings[id].ConnectionString; }

        public static SQLiteConnection conn = new SQLiteConnection(LoadConnectionString());
        public static SQLiteCommand command = new SQLiteCommand("", conn);

        public static void openDB()
        {
            if (conn.State == ConnectionState.Closed)
            {
                try { conn.Open(); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        public static void closeDB()
        {
            if (conn.State == ConnectionState.Open)
            { 
                try { conn.Close(); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }


        public static void Run(string SQL)
        {
            command.CommandText = SQL;
            command.ExecuteNonQuery();
        }

        
        public static DataTable returnTable(string SQL)
        {
            DataTable dt = new DataTable();
            command.CommandText = SQL;
            command.ExecuteNonQuery();

            SQLiteDataAdapter da1 = new SQLiteDataAdapter(command);
            da1.Fill(dt);

            return dt;
        }

    }
}
