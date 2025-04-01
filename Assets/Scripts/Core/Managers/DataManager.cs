using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class DataManager : Singleton<DataManager>
{

    private Dictionary<int, Item> itemData = new Dictionary<int, Item>();  // 存储配置项


    // 读取JSON文件并解析
    public void LoadItemData(string jsonFilePath)
    {
        try
        {
            string json = File.ReadAllText(jsonFilePath);
            List<ItemJson> items = JsonConvert.DeserializeObject<List<ItemJson>>(json);

            // 将 ItemJson 数据转换为 Item 并存储到字典中
            foreach (var item in items)
            {
                itemData[item.Id] = new Item(
                    item.Id,
                    item.Name,
                    item.Description,
                    item.Icon,
                    item.Type,  // 使用 ItemType 枚举
                    item.StackLimit
                );
            }

            Debug.Log("物品数据加载成功！");
        }
        catch (Exception ex)
        {
            Debug.LogError($"加载物品数据时发生错误: {ex.Message}");
        }
    }
    public Dictionary<int, Item> GetAllItems()
    {
        return itemData;
    }
    
    // 根据物品ID获取物品
    public Item GetItemById(int id)
    {
        if (itemData.ContainsKey(id))
        {
            return itemData[id];
        }
        else
        {
            Debug.LogWarning($"未找到物品ID: {id}");
            return null;  // 或者抛出异常，具体看需求
        }
    }

    // 用于反序列化JSON文件的Item类
    [Serializable]
    public class ItemJson
    {
        public int Id;          // 物品的唯一ID
        public string Name;     // 物品的名称
        public string Description; // 物品的描述
        public string Icon;     // 物品的图标（路径）
        public ItemType Type;   // 物品的类型（如武器、消耗品等）
        public int StackLimit;    // 物品的最大堆叠数
    }
}