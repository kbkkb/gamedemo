using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    class UIElement
    {
        public string Resources;
        public bool Cache;
        public GameObject Instance;
    }

    private Dictionary<Type, UIElement> UIResources = new Dictionary<Type, UIElement>();
    private Canvas mainCanvas;

    private void Awake()
    {
        UIResources.Add(typeof(UIBag), new UIElement() { Resources = "UI/UIBag", Cache = false });
    }

    private void Start()
    {
        mainCanvas = FindObjectOfType<Canvas>();
        if (mainCanvas == null)
        {
            Debug.LogError("没有找到 Canvas！");
        }
    }

    public T Show<T>() where T : Component
    {
        Type type = typeof(T);
        if (UIResources.ContainsKey(type))
        {
            UIElement info = UIResources[type];
            if (info.Instance != null)
            {
                info.Instance.SetActive(true);
            }
            else
            {
                UnityEngine.Object prefab = Resources.Load(info.Resources);
                if (prefab == null)
                {
                    Debug.LogError($"UI 资源未找到: {info.Resources}");
                    return null;
                }

                info.Instance = GameObject.Instantiate(prefab) as GameObject;
                UIResources[type] = info; // 更新字典

                Canvas canvas = mainCanvas ?? FindObjectOfType<Canvas>();
                if (canvas != null)
                {
                    info.Instance.transform.SetParent(canvas.transform, false);
                }
                else
                {
                    Debug.LogError("没有找到 Canvas，无法正确设置父物体！");
                }
            }
            return info.Instance.GetComponent<T>();
        }
        Debug.LogError($"UI 类型 {type} 未注册");
        return null;
    }

    public void Close(Type type)
    {
        if (UIResources.ContainsKey(type))
        {
            UIElement info = UIResources[type];
            if (info.Instance != null)
            {
                if (info.Cache)
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
    }

    public void DestroyUIManager()
    {
        Destroy(gameObject);
    }
}
