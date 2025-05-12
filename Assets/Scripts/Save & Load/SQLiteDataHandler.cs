#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_ANDROID || UNITY_IOS
using System;
using System.Data;
using Mono.Data.Sqlite;     // �� �ǵ��� Player Settings �� Other Settings �� ��Api Compatibility Level�� ѡ .NET Standard 2.1
using UnityEngine;
using System.IO;

public class SQLiteDataHandler : IDataHandler
{
    private readonly string dbPath;     // ����·�������ļ�����
    private const string TABLE_NAME = "GameData";

    public SQLiteDataHandler(string directory, string dbFileName)
    {
        Directory.CreateDirectory(directory);
        dbPath = Path.Combine(directory, dbFileName.EndsWith(".db") ? dbFileName : dbFileName + ".db");
        InitTableIfNeeded();
    }

    #region IDataHandler ʵ��
    public void Save(GameData data)
    {
        try
        {
            using var conn = new SqliteConnection($"URI=file:{dbPath}");
            conn.Open();

            using var cmd = conn.CreateCommand();
            // ��������ֻ��һ�У�������ٲ���
            cmd.CommandText = $"DELETE FROM {TABLE_NAME};";
            cmd.ExecuteNonQuery();

            cmd.CommandText = $"INSERT INTO {TABLE_NAME}(Currency) VALUES (@Currency);";
            cmd.Parameters.Add(new SqliteParameter("@Currency", data.currency));
            cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Debug.LogError($"[SQLite] Save ʧ�ܣ�{e}");
        }
    }

    public GameData Load()
    {
        try
        {
            using var conn = new SqliteConnection($"URI=file:{dbPath}");
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT Currency FROM {TABLE_NAME} LIMIT 1;";
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new GameData { currency = reader.GetInt32(0) };
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[SQLite] Load ʧ�ܣ�{e}");
        }
        return null;            // ���� null �� SaveManager �����µ�
    }
    #endregion

    #region ˽�и���
    private void InitTableIfNeeded()
    {
        using var conn = new SqliteConnection($"URI=file:{dbPath}");
        conn.Open();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = $@"
            CREATE TABLE IF NOT EXISTS {TABLE_NAME} (
                Id        INTEGER PRIMARY KEY AUTOINCREMENT,
                Currency  INTEGER NOT NULL
            );";
        cmd.ExecuteNonQuery();
    }
    #endregion
}
#endif
