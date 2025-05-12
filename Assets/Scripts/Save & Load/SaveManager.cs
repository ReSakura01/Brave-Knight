using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum StorageType { File, SQLite }     // ★ 新增

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    [Header("通用配置")]
    [SerializeField] private string fileName = "";

    [Header("存储方式")]
    [SerializeField] private StorageType storageType = StorageType.File;

    private GameData gameData;
    private List<ISaveManager> saveManagers;
    private IDataHandler dataHandler;             // ← 用接口持有

    #region 生命周期
    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        // 根据枚举实例化不同 Handler
        switch (storageType)
        {
            case StorageType.SQLite:
                dataHandler = new SQLiteDataHandler(Application.persistentDataPath, fileName);
                break;
            default:
                dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
                break;
        }

        saveManagers = FindAllSaveManagers();
        LoadGame();
    }

    private void OnApplicationQuit() => SaveGame();
    #endregion

    #region 对外 API
    public void NewGame() => gameData = new GameData();

    public void LoadGame()
    {
        gameData = dataHandler.Load();
        if (gameData == null) NewGame();

        foreach (var mgr in saveManagers)
            mgr.LoadData(gameData);
    }

    public void SaveGame()
    {
        foreach (var mgr in saveManagers)
            mgr.SaveData(ref gameData);

        dataHandler.Save(gameData);
    }
    #endregion

    #region 内部工具
    private List<ISaveManager> FindAllSaveManagers() =>
        FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>().ToList();
    #endregion
}
