using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum StorageType { File, SQLite }     // �� ����

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    [Header("ͨ������")]
    [SerializeField] private string fileName = "";

    [Header("�洢��ʽ")]
    [SerializeField] private StorageType storageType = StorageType.File;

    private GameData gameData;
    private List<ISaveManager> saveManagers;
    private IDataHandler dataHandler;             // �� �ýӿڳ���

    #region ��������
    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        // ����ö��ʵ������ͬ Handler
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

    #region ���� API
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

    #region �ڲ�����
    private List<ISaveManager> FindAllSaveManagers() =>
        FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>().ToList();
    #endregion
}
