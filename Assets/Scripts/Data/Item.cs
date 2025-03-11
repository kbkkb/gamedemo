namespace Code.Data
{
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
    
    [System.Serializable]
    public class Item
    {
        public int ItemId;
        public string ItemName;
        public string Description;
        public string Type;
        public string Category;
        public int Level;
        public int SellPrice;
        public int StackLimit;
        public string Icon;
        public int Param;
        public string Function;
    }
}