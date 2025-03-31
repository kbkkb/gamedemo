using UnityEngine;
using System;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static readonly object _lock = new object();
    private static bool _isInitialized = false;
    protected static T _instance;
    
    public static T Instance
    {
        get
        {
            if (!_isInitialized)
            {
                lock (_lock)
                {
                    if (!_isInitialized)
                    {
                        try
                        {
                            // 场景中查找现有实例
                            _instance = FindObjectOfType<T>();
                            
                            if (_instance == null)
                            {
                                // 自动创建单例对象
                                GameObject go = new GameObject(typeof(T).Name + "_Singleton");
                                _instance = go.AddComponent<T>();
                                DontDestroyOnLoad(go);
                            }
                            
                            _isInitialized = true;
                        }
                        catch (Exception e)
                        {
                            Debug.LogError($"Error creating singleton of type {typeof(T)}: {e.Message}");
                            return null;
                        }
                    }
                }
            }
            return _instance;
        }
    }

    public static bool IsInitialized => _isInitialized;

    protected virtual void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogWarning($"Multiple instances of singleton {typeof(T)} detected. Destroying duplicate.");
            Destroy(gameObject);
        }
        else
        {
            _instance = (T)this;
            _isInitialized = true;
            DontDestroyOnLoad(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
            _isInitialized = false;
        }
    }

    public static void DestroyInstance()
    {
        if (_instance != null)
        {
            Destroy(_instance.gameObject);
            _instance = null;
            _isInitialized = false;
        }
    }
}