using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Code.Data;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    private Dictionary<int, Item> items = new Dictionary<int, Item>();
    public float[] Rank = new float[10];
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        
        
    }

    private void Start()
    {
        Rank = LoadArray();
        if (Rank == null)
        {
            Rank = new float[10];
            for (int i = 0; i < Rank.Length; i++)
            {
                Rank[i] = -1;
            }

            SaveArray(Rank);
        }
    }
    // 存储数组到PlayerPrefs
    
    public void SaveArray(float[] rank)
    {
        if (rank.Length > 10)
        {
            Debug.LogError("数组长度不能超过10！");
            return;
        }

        string keyPrefix = "ArrayElement_";
        PlayerPrefs.SetInt(keyPrefix + "Length", rank.Length); // 存储数组长度
        PlayerPrefs.Save(); // 保存设置

        for (int i = 0; i < rank.Length; i++)
        {
            PlayerPrefs.SetFloat(keyPrefix + i, rank[i]); // 存储数组元素
        }
        PlayerPrefs.Save(); // 保存设置
        Rank = LoadArray();
    }
    
    public float[] LoadArray()
    {
        string keyPrefix = "ArrayElement_";
        int length = PlayerPrefs.GetInt(keyPrefix + "Length", -1); // 读取数组长度

        if (length == -1)
        {
            Debug.LogError("没有找到数组数据！");
            return null;
        }
        else if (length > 10)
        {
            Debug.LogError("数组长度超过了10！");
            return null;
        }

        float[] array = new float[length];
        for (int i = 0; i < length; i++)
        {
            array[i] = PlayerPrefs.GetFloat(keyPrefix + i); // 读取数组元素
        }

        return array;
    }
    
    public static int[] LoadArray(string key)
    {
        int length = PlayerPrefs.GetInt(key + "Length", -1);
        if (length == -1) return null;
        int[] array = new int[length];
        for (int i = 0; i < length; i++)
        {
            array[i] = PlayerPrefs.GetInt(key + i);
        }
        return array;
    }
    
    
    
    
    
    
    

    [System.Serializable]
    public class ItemListWrapper
    {
        public List<Item> list;
    }

    private void LoadData()
    {
        // 加载资源中的 JSON 文件，路径不要加扩展名
        TextAsset itemsJson = Resources.Load<TextAsset>("Resources/Data/ItemDefine");

        if (itemsJson != null)
        {
            Debug.Log("JSON Content: " + itemsJson.text);

            // 使用 ItemListWrapper 作为中间包装
            ItemListWrapper itemWrapper = JsonUtility.FromJson<ItemListWrapper>(itemsJson.text);
        
            // 检查解析后的结果
            if (itemWrapper != null && itemWrapper.list != null)
            {
                //Debug.Log("ItemWrapper loaded, list size: " + itemWrapper.list.Count);

                // 将物品加载到字典中
                foreach (var item in itemWrapper.list)
                {
                    //Debug.Log($"Loaded item: {item.ItemName}, ID: {item.ItemId}");
                    items[item.ItemId] = item;
                }
            }

        }


    }
    
    public Item GetItemById(int id)
    {
        return items.TryGetValue(id, out var item) ? item : null;
    }
    [System.Serializable]
    public class Wrapper<T>
    {
        public List<T> list;
    }
}