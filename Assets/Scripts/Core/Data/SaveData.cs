using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using GameData;

[Serializable]
public class SaveData
{
    // 基本游戏数据
    public PlayerStatsData playerStats;  // 玩家数据
    public Vector3 playerPosition;   // 玩家位置
    public string currentScene;      // 当前场景名称
    public float playTime;           // 游戏时间
    public DateTime saveTime;         // 存档时间

    // 其他游戏数据
    public Dictionary<string, bool> unlockedLevels;
    public List<InventoryItem> inventoryItems;
    public List<Achievement> achievements;

    public SaveData()
    {
        // 初始化基本数据
        playerStats = new PlayerStatsData();
        playerPosition = Vector3.zero;
        currentScene = "SampleScene";
        playTime = 0f;
        saveTime = DateTime.Now;

        // 初始化其他数据
        unlockedLevels = new Dictionary<string, bool>();
        inventoryItems = new List<InventoryItem>();
        achievements = new List<Achievement>();
    }

    // 从当前游戏状态创建存档数据
    public static SaveData CreateFromCurrentState()
    {
        GameDataManager dataManager = GameDataManager.Instance;
    
        SaveData saveData = new SaveData();
        saveData.playerStats.CopyFrom(dataManager.playerStats); //  这里进行转换
        saveData.playerPosition = dataManager.playerPosition;
        saveData.currentScene = dataManager.currentScene;
        saveData.playTime = dataManager.playTime;
        saveData.saveTime = DateTime.Now;
        saveData.unlockedLevels = new Dictionary<string, bool>(dataManager.unlockedLevels);
        saveData.inventoryItems = new List<InventoryItem>(dataManager.inventoryItems);
        saveData.achievements = new List<Achievement>(dataManager.achievements);

        return saveData;
    }

} 