using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SQLite;
using System.Reflection;
//using Foundation;

namespace SymptomTracker; 
public class DB {
    private static string DBName = "log.db";
    public static SQLiteConnection conn;
    public static void OpenConnection() {
        string libFolder = FileSystem.AppDataDirectory;
        string fname = System.IO.Path.Combine(libFolder, DBName);
        conn = new SQLiteConnection(fname);

        conn.CreateTable<Symptom>();
    }

    public static void AddSymptiom(Symptom symptom)
    {
        if (conn == null)
        {
            OpenConnection();
        }
        conn.Insert(symptom);
    }

    public static void updateSymptom(Symptom symptom)
    {
        conn.Update(symptom);
    }

    public static List<Symptom> GetAllSymptoms()
    {
        if (conn == null)
        {
            OpenConnection();
        }
        return conn.Table<Symptom>().ToList();
    }


}

public class Symptom
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public int Intensity { get; set; }
    public string Description { get; set; }

    public override string ToString()
    {
        DateTime standardTime = new DateTime(1, 1, 1) + Time;
        return string.Format("{0} {1}", Date.ToString("MM/dd/yyyy"), standardTime.ToString("hh:mm tt"));
    }
}
