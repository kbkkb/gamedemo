using UnityEngine;
using System;
using System.Collections.Generic;
using GameData;

public class GameDataManager : Singleton<GameDataManager>
{
    // 玩家数据
    public PlayerStats playerStats { get; private set; }
    public Vector3 playerPosition { get; private set; }
    public string currentScene { get; private set; }

    // 游戏数据
    public float playTime { get; private set; }
    public DateTime saveTime { get; private set; }
    public Dictionary<string, bool> unlockedLevels { get; private set; }
    public List<InventoryItem> inventoryItems { get; private set; }
    public List<Achievement> achievements { get; private set; }

    private const string SAVE_KEY = "GameSaveData";
    private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("未找到PlayerController！");
        }

        // 初始化数据
        playerStats = PlayerStats.Instance;
        unlockedLevels = new Dictionary<string, bool>();
        inventoryItems = new List<InventoryItem>();
        achievements = new List<Achievement>();
        playTime = 0f;
    }

    private void Update()
    {
        // 更新游戏时间
        playTime += Time.deltaTime;
    }

    // 从PlayerController更新玩家数据
    public void UpdateFromPlayerController(PlayerController player)
    {
        playerPosition = player.transform.position;
    }

    // 将玩家数据应用到PlayerController
    public void ApplyToPlayerController(PlayerController player)
    {
        player.transform.position = playerPosition;
    }

    // 更新当前场景
    public void UpdateCurrentScene(string sceneName)
    {
        currentScene = sceneName;
    }

    // 更新游戏时间
    public void UpdatePlayTime(float time)
    {
        playTime = time;
    }

    // 保存游戏数据
    public void SaveGame()
    {
        if (PlayerStats.Instance == null)
        {
            Debug.LogError("PlayerStats单例未初始化！");
            return;
        }

        try
        {
            SaveData saveData = new SaveData
            {
                saveTime = DateTime.Now,
                playerPosition = GameObject.FindGameObjectWithTag("Player")?.transform.position ?? Vector3.zero,
                currentScene = currentScene,
                playTime = playTime,
                inventoryItems = inventoryItems,
                unlockedLevels = unlockedLevels,
                achievements = achievements
            };

            // 复制玩家数据
            saveData.playerStats = new PlayerStatsData();
            saveData.playerStats.CopyFrom(PlayerStats.Instance);

            string jsonData = JsonUtility.ToJson(saveData);
            PlayerPrefs.SetString(SAVE_KEY, jsonData);
            Debug.Log("游戏数据保存成功！");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"保存游戏数据时发生错误: {e.Message}");
        }
    }

    // 加载游戏数据
    public void LoadGame()
    {
        if (PlayerStats.Instance == null)
        {
            Debug.LogError("PlayerStats单例未初始化！");
            return;
        }

        if (!HasSaveData())
        {
            Debug.LogWarning("没有找到存档数据！");
            return;
        }

        try
        {
            string jsonData = PlayerPrefs.GetString("GameSaveData");
            SaveData saveData = JsonUtility.FromJson<SaveData>(jsonData);

            if (saveData == null)
            {
                Debug.LogError("存档数据解析失败！");
                return;
            }

            // 应用玩家数据
            if (saveData.playerStats != null)
            {
                saveData.playerStats.ApplyTo(PlayerStats.Instance);
            }

            // 应用玩家位置
            if (saveData.playerPosition != null)
            {
                var player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    player.transform.position = saveData.playerPosition;
                }
                else
                {
                    Debug.LogWarning("未找到Player对象！");
                }
            }

            // 应用场景数据
            if (!string.IsNullOrEmpty(saveData.currentScene))
            {
                currentScene = saveData.currentScene;
            }

            // 应用游戏时间
            playTime = saveData.playTime;

            // 应用背包数据
            if (saveData.inventoryItems != null)
            {
                inventoryItems = new List<InventoryItem>(saveData.inventoryItems);
            }

            // 应用关卡解锁状态
            if (saveData.unlockedLevels != null)
            {
                unlockedLevels = new Dictionary<string, bool>(saveData.unlockedLevels);
            }

            // 应用成就数据
            if (saveData.achievements != null)
            {
                achievements = new List<Achievement>(saveData.achievements);
            }

            Debug.Log("游戏数据加载成功！");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"加载游戏数据时发生错误: {e.Message}");
        }
    }

    // 检查是否有存档
    public bool HasSaveData()
    {
        return PlayerPrefs.HasKey(SAVE_KEY);
    }

    public DateTime? GetLastSaveTime()
    {
        if (!HasSaveData()) return null;

        string jsonData = PlayerPrefs.GetString(SAVE_KEY);
        SaveData saveData = JsonUtility.FromJson<SaveData>(jsonData);
        return saveData.saveTime;
    }
} 