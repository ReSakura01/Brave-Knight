#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_ANDROID || UNITY_IOS
using System;
using System.Data;
using Mono.Data.Sqlite;     // ★ 记得在 Player Settings → Other Settings → “Api Compatibility Level” 选 .NET Standard 2.1
using UnityEngine;
using System.IO;

public class SQLiteDataHandler : IDataHandler
{
    private readonly string dbPath;     // 绝对路径（含文件名）
    private const string TABLE_NAME = "GameData";

    public SQLiteDataHandler(string directory, string dbFileName)
    {
        Directory.CreateDirectory(directory);
        dbPath = Path.Combine(directory, dbFileName.EndsWith(".db") ? dbFileName : dbFileName + ".db");
        InitTableIfNeeded();
    }

    #region IDataHandler 实现
    public void Save(GameData data)
    {
        try
        {
            using var conn = new SqliteConnection($"URI=file:{dbPath}");
            conn.Open();

            using var cmd = conn.CreateCommand();
            // 简单做法：只存一行，先清空再插入
            cmd.CommandText = $"DELETE FROM {TABLE_NAME};";
            cmd.ExecuteNonQuery();

            cmd.CommandText = $"INSERT INTO {TABLE_NAME}(Currency) VALUES (@Currency);";
            cmd.Parameters.Add(new SqliteParameter("@Currency", data.currency));
            cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Debug.LogError($"[SQLite] Save 失败：{e}");
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
            Debug.LogError($"[SQLite] Load 失败：{e}");
        }
        return null;            // 返回 null 让 SaveManager 创建新档
    }
    #endregion

    #region 私有辅助
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
