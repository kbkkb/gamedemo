using UnityEngine;
using System.IO;
using System;

public class SaveManager : Singleton<SaveManager>
{
    private string savePath;
    private const string SAVE_FILE_NAME = "save.json";
    private const string BACKUP_FILE_NAME = "save_backup.json";

    protected override void Awake()
    {
        base.Awake();
        savePath = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
    }

    // 保存存档
    public bool SaveGame(SaveData data)
    {
        try
        {
            // 创建备份
            if (File.Exists(savePath))
            {
                string backupPath = Path.Combine(Application.persistentDataPath, BACKUP_FILE_NAME);
                File.Copy(savePath, backupPath, true);
            }

            // 保存新存档
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(savePath, json);
            Debug.Log($"游戏已保存：{savePath}");
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"保存游戏失败：{e.Message}");
            RestoreFromBackup();
            return false;
        }
    }

    // 加载存档
    public SaveData LoadGame()
    {
        try
        {
            if (File.Exists(savePath))
            {
                string json = File.ReadAllText(savePath);
                return JsonUtility.FromJson<SaveData>(json);
            }
            else
            {
                Debug.LogWarning("未找到存档文件，返回默认数据");
                return new SaveData();
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"加载游戏失败：{e.Message}");
            return RestoreFromBackup();
        }
    }

    // 从备份恢复
    private SaveData RestoreFromBackup()
    {
        string backupPath = Path.Combine(Application.persistentDataPath, BACKUP_FILE_NAME);
        if (File.Exists(backupPath))
        {
            try
            {
                string json = File.ReadAllText(backupPath);
                Debug.Log("已从备份恢复存档");
                return JsonUtility.FromJson<SaveData>(json);
            }
            catch (Exception e)
            {
                Debug.LogError($"从备份恢复失败：{e.Message}");
            }
        }
        return new SaveData();
    }

    // 删除存档
    public bool DeleteSave()
    {
        try
        {
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
                Debug.Log("存档已删除");
                return true;
            }
            return false;
        }
        catch (Exception e)
        {
            Debug.LogError($"删除存档失败：{e.Message}");
            return false;
        }
    }

    // 检查存档是否存在
    public bool HasSave()
    {
        return File.Exists(savePath);
    }

    // 获取存档路径
    public string GetSavePath()
    {
        return savePath;
    }
}
