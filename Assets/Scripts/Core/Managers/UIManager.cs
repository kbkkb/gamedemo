using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    class UIElement
    {
        public string Resources;
        public bool Cache;
        public GameObject Instance;
    }
    
    private Dictionary<Type, UIElement> UIResources = new Dictionary<Type, UIElement>();
    private void Awake()
    {
        // 确保只有一个实例存在
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 如果已经有一个实例存在，销毁当前对象
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // 如果需要在场景切换时保留单例
    }
    
    public UIManager()
    {
        this.UIResources.Add(typeof(UIBag), new UIElement() { Resources = "UI/UIBag", Cache = false });
    }
    
    public T Show<T>()
    {
        //Sound
        Type type = typeof(T);
        if(this.UIResources.ContainsKey(type))
        {
            UIElement info = this.UIResources[type];
            if(info.Instance!=null)
            {
                info.Instance.SetActive(true);
            }
            else
            {
                UnityEngine.Object prefab = Resources.Load(info.Resources);
                if(prefab==null)
                {
                    return default(T);
                }
                info.Instance = (GameObject)GameObject.Instantiate(prefab);
                Canvas canvas = UnityEngine.Object.FindObjectOfType<Canvas>();
                if (canvas != null)
                {
                    info.Instance.transform.SetParent(canvas.transform, false); // 设置父物体为 Canvas
                }
                else
                {
                    Debug.LogError("没有找到 Canvas，无法正确设置父物体！");
                }
            }
            return info.Instance.GetComponent<T>();
        }
        return default(T);
    }
    public void Close(Type type)
    {
        //sound
        if(this.UIResources.ContainsKey(type))
        {
            UIElement info = this.UIResources[type];
            if(info.Cache)
            {
                info.Instance.SetActive(false);
            }
            else
            {
                GameObject.Destroy(info.Instance);
                info.Instance = null;
            }
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
