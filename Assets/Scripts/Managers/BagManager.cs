using System.Collections;
using System.Collections.Generic;
using Code.Data;
using UnityEngine;

[System.Serializable]
public class BagItem
{
    public int ItemId;  // 物品的ID
    public int Quantity;  // 物品的数量
}

[System.Serializable]
public class BagData
{
    public BagItem[] items; // 使用数组来存储背包中的物品
}

public class BagManager : MonoBehaviour
{
    public int MaxCapacity = 160; // 背包总容量（总格子数）
    public int CurrentCapacity;//当前空余格子数量
    private static BagManager _instance;
    public static BagManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BagManager>();
            }
            return _instance;
        }
    }

    private const string BagDataKey = "PlayerBagData"; // 存储数据的key
    public BagItem[] bagItems = new BagItem[160]; // 使用数组保存背包中的物品

    // 定义事件委托
    public delegate void BagChangedHandler();
    // 声明事件
    public event BagChangedHandler OnBagChanged;

    void Start()
    {
        LoadData(); // 游戏开始时加载存储的背包数据
        CurrentCapacity = MaxCapacity - FindBagLegth();
    }

    // 获取物品数量
    public int GetItemQuantity(int itemId)
    {
        var item = System.Array.Find(bagItems, b => b.ItemId == itemId);
        return item != null ? item.Quantity : 0;
    }

    // 添加物品
    public void AddItem(int itemId, int quantity)
    {
        // 获取物品信息
        Item item = DataManager.Instance.GetItemById(itemId);
        if (item == null)
        {
            Debug.LogError("Invalid item ID.");
            return;
        }

        // 检查现有堆叠
        for (int i = 0; i < FindBagLegth(); i++)
        {
            if (bagItems[i].ItemId == itemId)
            {
                int maxAddable = item.StackLimit - bagItems[i].Quantity;
                int toAdd = Mathf.Min(maxAddable, quantity);
                bagItems[i].Quantity += toAdd;
                quantity -= toAdd;
                break;
            }
        }

        // 检查是否还能新增堆叠
        int requiredSlots = Mathf.CeilToInt((float)quantity / item.StackLimit);

        if (requiredSlots > CurrentCapacity)
        {
            Debug.LogError("Not enough space in the bag.");
            return;
        }

        // 添加新堆叠
        while (quantity > 0)
        {
            int toAdd = Mathf.Min(item.StackLimit, quantity);
            int index = FindBagLegth();
            if (index != -1)
            {
                bagItems[index] = new BagItem();
                bagItems[index].ItemId = itemId;
                bagItems[index].Quantity += toAdd;
                quantity -= toAdd;
            }
            else
            {
                Debug.LogError("No empty slot found.");
                break;
            }
        }

        ResetBag();
        SaveData();
        // 触发事件通知 UI 更新
        OnBagChanged?.Invoke();
    }

    // 删除物品
    public void RemoveItem(int itemId, int quantity)
    {
        Item item = DataManager.Instance.GetItemById(itemId);
        if (item == null)
        {
            Debug.LogError("Invalid item ID.");
            return;
        }

        for (int i = 0; i < bagItems.Length; i++)
        {
            if (bagItems[i].ItemId == itemId)
            {
                if (bagItems[i].Quantity < quantity)
                {
                    Debug.LogError("Not enough space in the bag.");
                    return;
                }
                bagItems[i].Quantity -= quantity;
                if (bagItems[i].Quantity <= 0)
                {
                    bagItems[i].ItemId=0;
                    bagItems[i].Quantity = 0;
                }
                return;
            }
            else if(bagItems[i].ItemId ==0)
            {
                Debug.Log("Item not found.");
                return;
            }
        }

        ResetBag();
        SaveData();  // 删除物品后保存数据
        
        // 触发事件通知 UI 更新
        OnBagChanged?.Invoke();
    }

    // 保存数据到 PlayerPrefs
    public void SaveData()
    {
        BagData data = new BagData { items = bagItems }; // 只保存物品ID和数量
        string json = JsonUtility.ToJson(data); // 序列化为JSON
        PlayerPrefs.SetString(BagDataKey, json); // 存储到PlayerPrefs
        PlayerPrefs.Save();  // 保存数据
    }

    // 从 PlayerPrefs 加载数据
    public void LoadData()
    {
        if (PlayerPrefs.HasKey(BagDataKey)) // 判断是否有存储的背包数据
        {
            string json = PlayerPrefs.GetString(BagDataKey); // 获取存储的JSON字符串
            BagData data = JsonUtility.FromJson<BagData>(json); // 反序列化为BagData

            if (data != null && data.items != null)
            {
                bagItems=new BagItem[160];
                // 填充数据
                for (int i = 0; i < data.items.Length; i++)
                {   
                    bagItems[i] = data.items[i];

                }
            }
            else
            {
                Debug.LogWarning("Loaded data is invalid. Initializing with default values.");
                // 如果加载的数据无效，则重新初始化
                bagItems = new BagItem[160]; // 或者根据需要选择默认容量
            }
        }
        else
        {
            // 如果没有保存的背包数据，则初始化为默认数据
            Debug.Log("No saved data found. Initializing with default values.");
            bagItems = new BagItem[160]; // 默认容量
        }
    }

    public void outbagItems()
    {
        foreach (var bagitem in bagItems)
        {
            if (bagitem.ItemId != 0)
            {
                Debug.LogFormat("itemid:{0},itemCount:{1}", bagitem.ItemId, bagitem.Quantity);
            }
        }
    }

    public int FindBagLegth()
    {
        for (int i = 0; i < bagItems.Length; i++)
        {
            if (bagItems[i].ItemId== 0)
            {
                return i;
            }
        }
        return -1;
    }

    public void ResetBag()
    {
        // 排序时，空格排到最后，其他物品按 ItemId 升序排序
        System.Array.Sort(bagItems, (a, b) => 
        {
            if (a == null || a.ItemId == 0) return 1;  // 如果是空格或无效物品，排到后面
            if (b == null || b.ItemId == 0) return -1;
            return a.ItemId.CompareTo(b.ItemId);
        });
    }
}
