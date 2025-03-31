using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string itemId;           // 物品ID
    public string itemName;         // 物品名称
    public string description;      // 物品描述
    public int quantity;           // 物品数量
    public ItemType itemType;      // 物品类型
    public Sprite itemIcon;        // 物品图标

    public InventoryItem(string id, string name, string desc, int qty, ItemType type)
    {
        itemId = id;
        itemName = name;
        description = desc;
        quantity = qty;
        itemType = type;
    }
}

// 物品类型枚举
public enum ItemType
{
    Consumable,    // 消耗品
    Equipment,     // 装备
    Material,      // 材料
    Quest          // 任务物品
} 