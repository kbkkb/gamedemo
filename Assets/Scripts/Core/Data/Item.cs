using System;

[Serializable]
public class Item
{
    public int Id;          // 物品的唯一ID
    public string Name;     // 物品的名称
    public string Description; // 物品的描述
    public string Icon;     // 物品的图标（路径）
    public ItemType Type;   // 物品的类型（如武器、消耗品等）
    public int StackLimit;    // 物品的最大堆叠数

    // 可以根据需求添加更多属性

    public Item(int id, string name, string description, string icon, ItemType type, int stackLimit)
    {
        Id = id;
        Name = name;
        Description = description;
        Icon = icon;
        Type = type;
        StackLimit = stackLimit;
    }

    
}

public enum ItemType
{
    Weapon,    // 武器
    Armor,     // 防具
    Consumable,// 消耗品
    Material   // 材料
}
public enum ItemFunction
{
    None,           // 无功能
    AddHP,          // 加血
    AddMP,          // 加蓝
    AddHPAndMP,     // 同时加血和蓝
    Heal,           // 恢复生命
    Damage,         // 造成伤害
    Buff,           // 增益效果
    Debuff          // 减益效果
}