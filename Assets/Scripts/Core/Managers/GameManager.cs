using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    

    void Start()
    {
        // 设置 JSON 文件路径
        string filePath = "Assets/Resources/Data/itemDefine.json";
        
        // 加载物品数据
        DataManager.Instance.LoadItemData(filePath);

        // 获取所有物品数据
        Dictionary<int, Item> items = DataManager.Instance.GetAllItems();
        
        // 打印物品信息
        foreach (var item in items.Values)
        {
            Debug.Log($"物品ID: {item.Id}, 名称: {item.Name}, 描述: {item.Description}, 类型: {item.Type}");
        }

        // 获取某个特定物品
        // Item sword = DataManager.Instance.GetItemById(1);
        // Debug.Log($"特定物品: {sword.Name}, 描述: {sword.Description}");
    }
}